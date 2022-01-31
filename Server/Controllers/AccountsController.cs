using GreenBook.Server.Data;
using GreenBook.Server.Models;
using GreenBook.Server.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        UserManager<ApplicationUser> _userManager;
        public AccountsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
       
        /// <summary>
        /// Retrieve all the users
        /// </summary>
        /// <returns>ClientApplicationUser Objects in a List</returns>
        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var appUserList = await _context.ApplicationUsers.ToListAsync();
            List<ClientApplicationUser> clientAppUserList = new List<ClientApplicationUser>();
            foreach (var appUser in appUserList)
            {
                ClientApplicationUser clientAppUser = new ClientApplicationUser(appUser);
                clientAppUserList.Add(clientAppUser);
            }
            return Ok(clientAppUserList);
        }
        /// <summary>
        /// Retrieve the role information based on user id
        /// </summary>
        /// <returns>Role in a string</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(string id)
        {
            var appUserList = await _context.ApplicationUsers.ToListAsync();
            foreach (var appUser in appUserList)
            {
                if (appUser.Id == id)
                {
                    var UserResult = await _userManager.IsInRoleAsync(appUser,"Admin");
                    if (UserResult)
                    {
                        return Ok("Admin");
                    }
                    UserResult = await _userManager.IsInRoleAsync(appUser, "User");
                    if (UserResult)
                    {
                        return Ok("User");
                    }
                }
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(string id)
        {
            var appUserList = await _context.ApplicationUsers.ToListAsync();
            foreach (var appUser in appUserList)
            {
                if (appUser.Id == id)
                {
                    await _userManager.DeleteAsync(appUser);
                    await _userManager.UpdateAsync(appUser);
                }
            }
            return NotFound();
        }
    }
}
