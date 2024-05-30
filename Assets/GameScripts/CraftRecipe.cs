using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct Ingredient
{
    public string NameIngredient;
    public int CountIngredient;
}

[Serializable]
public struct DrinkRecipe
{
    public string NameRecipe;
    public List<Ingredient> Ingredients;
}

[Serializable]
public struct DrinkReady
{
    public string NameRecipe;
    public Sprite StickerImage;
}

[CreateAssetMenu]
public class CraftRecipe : ScriptableObject
{
    public List<DrinkRecipe> recipes;
    public List<DrinkReady> resultsDrink;
}
