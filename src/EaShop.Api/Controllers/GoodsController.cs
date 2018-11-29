using EaShop.Api.ViewModels;
using EaShop.Data;
using EaShop.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EaShop.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GoodsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Goods
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Goods>))]
        [ProducesResponseType(400)]
        public IActionResult GetGoods([FromQuery] Pagination pagination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pagination.PageSize == null || pagination.PageNumber == null)
            {
                return Ok(_context.Goods);
            }
            else
            {
                try
                {
                    var result = _context.Goods
                        .Skip((int)pagination.PageSize * ((int)pagination.PageNumber - 1))
                        .Take((int)pagination.PageSize);
                    return Ok(result);
                }
                catch
                {
                    return BadRequest(pagination);
                }
            }
        }

        // GET: api/Goods/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Goods))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetGoods([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var goods = await _context.Goods.FindAsync(id);

            if (goods == null)
            {
                return NotFound();
            }

            return Ok(goods);
        }

        // GET: api/Goods/search
        [HttpPost("search")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Goods>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult SearchGoods([FromBody] SearchWithPagination search)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(search.Name))
            {
                return BadRequest("Name is invalid");
            }

            var goods = _context.Goods
                .Where(g => g.Name.ToLowerInvariant().Contains(search.Name.ToLowerInvariant()));

            if (goods == null)
            {
                return NotFound();
            }

            if (search.PageSize == null || search.PageNumber == null)
            {
                return Ok(goods);
            }
            else
            {
                try
                {
                    var result = goods
                        .Skip((int)search.PageSize * ((int)search.PageNumber - 1))
                        .Take((int)search.PageSize);
                    return Ok(result);
                }
                catch
                {
                    return BadRequest(search);
                }
            }

        }

        // PUT: api/Goods/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutGoods([FromRoute] int id, [FromBody] Goods goods)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != goods.Id)
            {
                return BadRequest();
            }

            _context.Entry(goods).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoodsExists(id))
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

        // POST: api/Goods
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(201, Type = typeof(Goods))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostGoods([FromBody] Goods goods)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Goods.Add(goods);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGoods", new { id = goods.Id }, goods);
        }

        // DELETE: api/Goods/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200, Type = typeof(Goods))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteGoods([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var goods = await _context.Goods.FindAsync(id);
            if (goods == null)
            {
                return NotFound();
            }

            _context.Goods.Remove(goods);
            await _context.SaveChangesAsync();

            return Ok(goods);
        }

        private bool GoodsExists(int id) => _context.Goods.Any(e => e.Id == id);
    }
}