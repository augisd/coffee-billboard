using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coffee>>> GetCoffees()
        {
            return await _context.Coffees.ToListAsync();
        }

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
            
            var created = _context.Coffees.Add(toAdd);
            
            await _context.SaveChangesAsync();

            return created.Entity;
        }

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

        private static bool IsJpegOrPng(IFormFile file) =>
            file.ContentType == "image/jpeg" | file.ContentType == "image/png";
    }
}
