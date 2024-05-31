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

[Serializable]
public struct Sticker
{
    public string NameOrder;
    public Sprite OrderSticker;
}

[CreateAssetMenu]
public class CraftRecipe : ScriptableObject
{
    public List<DrinkRecipe> recipes;
    public List<DrinkReady> resultsDrink;
    public List<Sticker> stickers;
}
