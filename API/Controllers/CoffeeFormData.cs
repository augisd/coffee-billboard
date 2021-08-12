using Microsoft.AspNetCore.Http;

namespace API.Controllers
{
    public class CoffeeFormData
    {
        public string Title { get; set; }
        public string Price { get; set; }
        public IFormFile Picture { get; set; }
    }
}
