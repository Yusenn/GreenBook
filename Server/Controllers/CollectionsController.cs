using GreenBook.Server.IRepository;
using GreenBook.Shared.Domain;
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
    public class CollectionsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CollectionsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetCollections()
        {
            var collections = await _unitOfWork.Collections.GetAll();
            return Ok(collections);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCollection(int id)
        {
            var collection = await _unitOfWork.Collections.Get(q => q.Id == id);

            if (collection == null)
            {
                return NotFound();
            }

            //return comment;
            return Ok(collection);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollection(int id, Collection collection)
        {
            if (id != collection.Id)
            {
                return BadRequest();
            }

            _unitOfWork.Collections.Update(collection);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CollectionExists(id))
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
        public async Task<ActionResult<Collection>> PostLocation(Collection collection)
        {
            await _unitOfWork.Collections.Insert(collection);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetCollection", new { id = collection.Id }, collection);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollection(int id)
        {
            var collection = await _unitOfWork.Collections.Get(q => q.Id == id);
            if (collection == null)
            {
                return NotFound();
            }

            await _unitOfWork.Collections.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> CollectionExists(int id)
        {
            var collection = await _unitOfWork.Collections.Get(q => q.Id == id);
            return collection != null;
        }
    }
}
