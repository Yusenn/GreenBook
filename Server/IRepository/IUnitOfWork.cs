using GreenBook.Client.Shared.Domain;
using GreenBook.Server.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GreenBook.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Comment> Comments { get; }
        IGenericRepository<Post> Posts { get; }
        //IGenericRepository<ApplicationUser> ApplicationUser { get; }
    }
}