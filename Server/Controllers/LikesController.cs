using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenBook.Server.Data;
using GreenBook.Shared.Domain;
using GreenBook.Server.IRepository;

namespace GreenBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        
        //public LikesController(ApplicationDbContext context)
        public LikesController (IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Likes
        [HttpGet]
       // public async Task<ActionResult<IEnumerable<Like>>> GetLike()
        public async Task <IActionResult> GetLikes()
        {
            //return await _context.Like.ToListAsync();
            var likes = await _unitOfWork.Likes.GetAll();
            return Ok(likes);
        }


        // GET: api/Likes/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Like>> GetLike(int id)
        public async Task<IActionResult> GetLike(int id)
        {
            //var like = await _context.Like.FindAsync(id);
            var like = await _unitOfWork.Likes.Get(q => q.Id == id);

            if (like == null)
            {
                return NotFound();
            }

            // return like;
            return Ok(like);
        }

        // PUT: api/Likes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        
        //public async Task<IActionResult> PutLike(int id, Like like)
        public async Task<IActionResult> PutLike(int id, Like like)
        {
            if (id != like.Id)
            {
                return BadRequest();
            }

            //_context.Entry(like).State = EntityState.Modified;
            _unitOfWork.Likes.Update(like);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await LikeExists(id))
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

        // POST: api/Likes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Like>> PostLike(Like like)
        {
            //context.Like.Add(like);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Likes.Insert(like);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetLike", new { id = like.Id }, like);
        }

        // DELETE: api/Likes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLike(int id)
        {
            //var like = await _context.Like.FindAsync(id);
            var like = await _unitOfWork.Likes.Get(q => q.Id == id); 
            if (like == null)
            {
                return NotFound();
            }

            await _unitOfWork.Likes.Delete(id);
            await _unitOfWork.Save(HttpContext);
            //_context.Like.Remove(like);
            //await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> LikeExists(int id)
        {
            var like = await _unitOfWork.Likes.Get(q => q.Id == id);
            return like != null;
            //return _context.Like.Any(e => e.Id == id);
        }
    }
}
