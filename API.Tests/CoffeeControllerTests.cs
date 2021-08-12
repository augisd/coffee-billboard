using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Xunit;

namespace API.Tests
{
    public class CoffeeControllerTests : IDisposable
    {
        private CoffeeContext _context;
        private CoffeeController _controller;

        public CoffeeControllerTests()
        {
            _context = new CoffeeContext(new DbContextOptionsBuilder<CoffeeContext>()
                .UseSqlite("DataSource=:memory:").Options);
            _controller = new CoffeeController(_context, new HostingEnvironment());            
        }

        [Fact]
        public async Task RetrievesAListOfCoffees()
        {
            // Arrange
            _context.Coffees.Add(new Coffee()
            {
                Id = Guid.Parse("ef72c61a-c8ee-4ca4-808d-ba9f6ec85b75"),
                Price = 1.99m,
                Title = "Latte"
            });
            _context.Coffees.Add(new Coffee()
            {
                Id = Guid.Parse("2fbf80e4-610f-4101-9399-5513a9843029"),
                Price = 1.45m,
                Title = "Espresso"
            });
            
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.GetCoffees();

            // Assert
            Assert.Equal(2, result.Value.Count());
            Assert.IsAssignableFrom<ActionResult<IEnumerable<Coffee>>>(result);
        }

        [Fact]
        public async Task RetrievesACoffee()
        {
            // Arrange
            var id = Guid.NewGuid();
            const decimal price = 1.99m;
            const string title = "Cappuccino";
            
            _context.Coffees.Add(new Coffee()
            {
                Id = id,
                Price = price,
                Title = title
            });
            
            await _context.SaveChangesAsync();
            
            // Act
            var result = _controller.GetCoffee(id);

            // Assert
            Assert.Equal(id, result.Result.Value.Id);
            Assert.Equal(price, result.Result.Value.Price);
            Assert.Equal(title, result.Result.Value.Title);
        }

        [Fact]
        public void CreatesACoffee()
        {
            // Arrange
            const decimal price = 1.99m;
            const string title = "Latte";
            
            // Act
            var result = _controller.PostCoffee(new CoffeeFormData()
            {
                Price = price.ToString(CultureInfo.InvariantCulture),
                Title = title
            });
            
            // Assert
            Assert.Equal(result.Result.Value.Price, price);
            Assert.Equal(result.Result.Value.Title, title);
        }

        [Fact]
        public async Task DeletesACoffee()
        {
            // Arrange
            var id = Guid.NewGuid();
            const decimal price = 1.99m;
            const string title = "Latte";
            
            _context.Coffees.Add(new Coffee()
            {
                Id = id,
                Price = price,
                Title = title
            });

            await _context.SaveChangesAsync();
            
            // Act
            var response = _controller.DeleteCoffee(id);
            var result = _controller.GetCoffee(id);

            // Assert
            Assert.IsAssignableFrom<NoContentResult>(response.Result);
            Assert.Null(result.Result.Value);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}