using System.Collections.Generic;

public class DrinkRecipe
{
    public string Name { get; private set; }
    public List<string> Ingredients { get; private set; }
    public List<int> Ratios { get; private set; }

    public DrinkRecipe(string name, List<string> ingredients, List<int> proportions)
    {
        Name = name;
        Ingredients = ingredients;
        Ratios = proportions;
    }
}
