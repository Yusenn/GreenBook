using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenBook.Client.Shared.Domain;
using GreenBook.Server.Data;
using GreenBook.Server.IRepository;
using GreenBook.Shared.Domain;

namespace GreenBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _unitOfWork.Tags.GetAll();
            return Ok(tags);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTag(int id)
        {
            var tag = await _unitOfWork.Tags.Get(q => q.Id == id);

            if (tag == null)
            {
                return NotFound();
            }

            //return comment;
            return Ok(tag);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(int id, Tag tag)
        {
            if (id != tag.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Tags.Update(tag);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TagExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Tag>> PostTag(Tag tag)
        {
            await _unitOfWork.Tags.Insert(tag);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetTag", new { id = tag.Id }, tag);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await _unitOfWork.Tags.Get(q => q.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            await _unitOfWork.Tags.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> TagExists(int id)
        {
            var tag = await _unitOfWork.Tags.Get(q => q.Id == id);
            return tag != null;
        }
    }
}
