using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace DishBook.Models
{
    public class Recipe
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CookTimeMinutes { get; set; }
        public int Servings { get; set; }
        public string Ingredients { get; set; } = string.Empty; // "Name|Amount\nName|Amount"
        public string Directions { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public bool IsFavorite { get; set; }
        public string? ImagePath { get; set; }
    }
}
