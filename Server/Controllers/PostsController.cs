﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenBook.Client.Shared.Domain;
using GreenBook.Server.Data;
using GreenBook.Server.IRepository;

namespace GreenBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public PostsController(ApplicationDbContext context)
        public PostsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Posts
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<post>>> GetPosts()
        public async Task<IActionResult> GetPosts()
        {
            //return await _context.Posts.ToListAsync();
            var Posts = await _unitOfWork.Posts.GetAll();
            return Ok(Posts);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<post>> Getpost(int id)
        public async Task<IActionResult> Getpost(int id)
        {
            //var post = await _context.Posts.FindAsync(id);
            var post = await _unitOfWork.Posts.Get(q => q.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            //return post;
            return Ok(post);
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putpost(int id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            //_context.Entry(post).State = EntityState.Modified;
            _unitOfWork.Posts.Update(post);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await postExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Post>> Postpost(Post post)
        {
            await _unitOfWork.Posts.Insert(post);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("Getpost", new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletepost(int id)
        {
            var post = await _unitOfWork.Posts.Get(q => q.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            await _unitOfWork.Posts.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> postExists(int id)
        {
            var post = await _unitOfWork.Posts.Get(q => q.Id == id);
            return post != null;
        }
    }
}