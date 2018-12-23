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
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Category))]
        public IEnumerable<Category> GetCategories() => _context.Categories.OrderByDescending(c => c.Id);

        // GET: api/Categories/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // GET: api/Categories/5/goods
        [HttpGet("{id}/goods")]
        [ProducesResponseType(200, Type = typeof(Goods))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetCategoryGoods([FromRoute] int id, [FromQuery] Pagination pagination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IQueryable<Goods> result;
            if (pagination.PageSize == null || pagination.PageNumber == null)
            {
                result = _context.Goods
                    .Where(g => g.CategoryId == id)
                    .OrderByDescending(g => g.Id);
            }
            else
            {
                result = _context.Goods
                    .Where(g => g.CategoryId == id)
                    .Skip((int)pagination.PageSize * ((int)pagination.PageNumber - 1))
                    .Take((int)pagination.PageSize)
                    .OrderByDescending(g => g.Id);
            }            
            return Ok(result);
        }
    }
}