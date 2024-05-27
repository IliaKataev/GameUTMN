using System.Collections.Generic;



public class DrinkRecipe
{
    public string Name { get; set; }
    public Dictionary<string, int> Ingredients { get; set; }

    public DrinkRecipe(string name, Dictionary<string,int> ingredients)
    {
        Name = name;
        Ingredients = ingredients;
    }
}

//public class Ingredient
//{
//    public string Name { get; set; }
//    public int Quantity { get; set; }

//    public Ingredient(string name, int quantity)
//    {
//        Name = name;
//        Quantity = quantity;
//    }
//}

