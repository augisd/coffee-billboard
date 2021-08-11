using System;

namespace API.Models
{
    public class Coffee
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
    }
}