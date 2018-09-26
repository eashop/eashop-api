using EaShop.Api.ViewModels;
using EaShop.Data;
using EaShop.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EaShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public IEnumerable<OrderRead> GetOrders()
        {
            return _context.Orders
                .AsNoTracking()
                .Include(o => o.GoodsInOrder)
                .ThenInclude(g => g.Goods)
                .Select(order => new OrderRead
                {
                    Id = order.Id,
                    Date = order.Date,
                    UserId = order.UserId,
                    GoodsInOrder = order.GoodsInOrder.Select(g => new GoodsInOrderRead
                    {
                        GoodsId = g.GoodsId,
                        Price = g.Price,
                        Quantity = g.Quantity,
                        Name = g.Goods.Name,
                        Image = g.Goods.Image
                    })
                });
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        [ProducesResponseType(201, Type = typeof(OrderRead))]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.Orders
                .AsNoTracking()
                .Include(o => o.GoodsInOrder)
                .ThenInclude(g => g.Goods)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var result = new OrderRead
            {
                Id = order.Id,
                Date = order.Date,
                UserId = order.UserId,
                GoodsInOrder = order.GoodsInOrder.Select(g => new GoodsInOrderRead
                {
                    GoodsId = g.GoodsId,
                    Price = g.Price,
                    Quantity = g.Quantity,
                    Name = g.Goods.Name,
                    Image = g.Goods.Image
                })
            };

            return Ok(result);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder([FromRoute] int id, [FromBody] OrderUpdate order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Orders.Update(new Order
            {
                Id = order.Id,
                Date = order.Date,
                UserId = order.UserId,
                GoodsInOrder = order.GoodsInOrder.Select(g => new GoodsInOrder
                {
                    GoodsId = g.GoodsId,
                    OrderId = order.Id,
                    Price = g.Price,
                    Quantity = g.Quantity
                })
                .ToList()
            });

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] OrderCreate order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newOrder = new Order
            {
                Date = order.Date,
                UserId = order.UserId,
                GoodsInOrder = new Collection<GoodsInOrder>(
                    order.GoodsInOrder.Select(g => new GoodsInOrder
                    {
                        GoodsId = g.GoodsId,
                        Price = g.Price,
                        Quantity = g.Quantity
                    })
                    .ToList()
                )
            };

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = newOrder.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}