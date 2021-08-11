using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Microsoft.Extensions.Hosting;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeController : ControllerBase
    {
        private readonly CoffeeContext _context;
        private static IHostEnvironment _env;
        
        public CoffeeController(CoffeeContext context, IHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/coffee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coffee>>> GetCoffees()
        {
            return await _context.Coffees.ToListAsync();
        }

        // GET: api/coffee/148bea3e-39d3-422e-9bfc-4283d501c105
        [HttpGet("{id}")]
        public async Task<ActionResult<Coffee>> GetCoffee(Guid id)
        {
            var coffee = await _context.Coffees.FindAsync(id);

            if (coffee == null)
            {
                return NotFound();
            }

            return coffee;
        }

        // PUT: api/coffee/148bea3e-39d3-422e-9bfc-4283d501c105
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoffee(Guid id, Coffee coffee)
        {
            if (id != coffee.Id)
            {
                return BadRequest();
            }

            _context.Entry(coffee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoffeeExists(id))
                {
                    return NotFound();
                }
                
                throw;
            }

            return NoContent();
        }

        // POST: api/coffee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Coffee>> PostCoffee([FromForm] CoffeeFormData data)
        {
            var toAdd = new Coffee()
            {
                Price = decimal.Parse(data.Price),
                Title = data.Title
            };

            if (data.Picture is not null && IsJpegOrPng(data.Picture))
            {
                var picture = data.Picture;
                var format = picture.ContentType.Split("/")[1];
                var filePath = $"Images/{Path.GetRandomFileName().Replace(".", "")[..8]}.{format}";
                var path = Path.Combine(_env.ContentRootPath, filePath);

                await using var stream = new FileStream(path, FileMode.Create);
                await picture.CopyToAsync(stream);

                toAdd.ImagePath = filePath;
            }
            
            _context.Coffees.Add(toAdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoffee", new { id = toAdd.Id }, toAdd);
        }

        // DELETE: api/coffee/148bea3e-39d3-422e-9bfc-4283d501c105
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoffee(Guid id)
        {
            var coffee = await _context.Coffees.FindAsync(id);
            if (coffee == null)
            {
                return NotFound();
            }

            _context.Coffees.Remove(coffee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoffeeExists(Guid id)
        {
            return _context.Coffees.Any(e => e.Id == id);
        }

        public class CoffeeFormData
        {
            public string Title { get; set; }
            public string Price { get; set; }
            public IFormFile Picture { get; set; }
        }

        private static bool IsJpegOrPng(IFormFile file) =>
            file.ContentType == "image/jpeg" | file.ContentType == "image/png";
    }
}
