using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementApi.DataAccessLayer;
using OrderManagementApi.Models;

namespace OrderManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderManagementDbContext _context;

        public OrdersController(OrderManagementDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders/customer/5
        [HttpGet("customer/{id}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByCustomer(long id)
        {
            return await _context.Orders.Where(o => o.CustomerId == id).ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(long id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // GET: api/Orders/5
        [HttpGet("orderRows/{id}")]
        public async Task<ActionResult<IEnumerable<OrderRow>>> GetOrderRows(long id)
        {
            var orderRows = await _context.OrderRows.Where(o => o.OrderId == id).ToArrayAsync();

            if (orderRows == null)
            {
                return NotFound();
            }

            return orderRows;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(long id, Order order)
        {
            var originalOrder = _context.Orders
                .Where(o => o.Id == id)
                .Include(o => o.Rows)
                .SingleOrDefault();

            originalOrder.Rows.ToList().ForEach(r => _context.Entry(r).State = EntityState.Deleted);

            await _context.SaveChangesAsync();

            foreach (var orderRow in order.Rows)
            {
                orderRow.Id = 0;

                _context.OrderRows.Add(orderRow);                
            }

            await _context.SaveChangesAsync();

            _context.Entry(originalOrder).CurrentValues.SetValues(order);

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

        //[HttpPut("Update/{orderId}/{rowId}")]
        //public async Task<IActionResult> DeleteOrderRowandPutOrder(long orderId, Order order, long rowId)
        //{
        //    DeleteRowFromOrder(orderId, rowId);

        //    order.Id = orderId;

        //    _context.Entry(order).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!OrderExists(orderId))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Orders
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("customer/{id}")]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            //if (ModelState.IsValid == false)
            //{
            //    return BadRequest(ModelState);
            //}

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(long id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        //[HttpDelete("Delete/OrderRow/{orderId}/{rowId}")]
        //public void DeleteRowFromOrder(long orderId, long rowId)
        //{

        //    if (OrderExists(orderId) == false)
        //    {
        //        throw new ArgumentException("Order not found");
        //    }

        //    OrderRow row = GetRowInOrder(orderId, rowId);
        //    if (row != null)
        //    {
        //        _context.OrderRows.Remove(row);
        //        _context.SaveChanges();
        //    }
        //}
        public OrderRow GetRowInOrder(long orderId, long rowId)
        {
            return _context.OrderRows.FirstOrDefault(r => r.OrderId == orderId && r.Id == rowId);
        }

        private bool OrderExists(long id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
