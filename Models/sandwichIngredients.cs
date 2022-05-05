using System;
namespace sandwichAPI.Models
{
    
    public class sandwichIngredients
    {
        public int sandwichIngredientsID { get; set; }
        public string? breadType { get; set; }
        public string? cheeseType { get; set; }
        public string? condimentType { get; set; }
        public string? meatType { get; set; }
        public string? veggieType { get; set; }
        public string? otherType { get; set; }
        public int sandwichID { get; set; }
    }
}
