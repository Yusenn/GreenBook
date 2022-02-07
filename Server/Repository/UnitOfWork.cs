using GreenBook.Client.Shared.Domain;
using GreenBook.Server.Data;
using GreenBook.Server.IRepository;
using GreenBook.Server.Models;
using GreenBook.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace GreenBook.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Comment> _comments;
        private IGenericRepository<Post> _posts;
        private IGenericRepository<Like> _likes;
        private IGenericRepository<Location> _locations;
        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IGenericRepository<Comment> Comments
            => _comments ??= new GenericRepository<Comment>(_context);
        public IGenericRepository<Post> Posts
            => _posts ??= new GenericRepository<Post>(_context);
        public IGenericRepository<Like> Likes
            => _likes ??= new GenericRepository<Like>(_context);
        public IGenericRepository<Location> Locations
           => _locations ??= new GenericRepository<Location>(_context);
        //public IGenericRepository<ApplicationUser> ApplicationUser
        //=> _applicationuser ??= new GenericRepository<ApplicationUser>(_context);


        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            //string user = "System";
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            foreach (var entry in entries)
            {
                ((BaseDomainModel)entry.Entity).DateUpdate = DateTime.Now;
                ((BaseDomainModel)entry.Entity).UpdateBy = user.UserName;
                if (entry.State == EntityState.Added)
                {
                    ((BaseDomainModel)entry.Entity).DateCreate = DateTime.Now;
                    ((BaseDomainModel)entry.Entity).CreateBy = user.UserName;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}