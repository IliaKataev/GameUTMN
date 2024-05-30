using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickerManager : MonoBehaviour
{
    public Image stickerImage;
    public CraftRecipe craftRecipe;
    private Sprite currentStickerSprite;
    [SerializeField]
    [HideInInspector]
    public static string currentStickerName;

    void Start()
    {
        SetRandomSticker();
        DisplayRecipe();
        Debug.Log("Стикер");
    }

    public void SetRandomSticker()
    {
        int randomIndex = Random.Range(0, craftRecipe.stickers.Count);
        currentStickerSprite = craftRecipe.stickers[randomIndex].OrderSticker;
        currentStickerName = craftRecipe.stickers[randomIndex].NameOrder;
        Debug.Log(currentStickerName);
        stickerImage.sprite = currentStickerSprite;
    }

    public void DisplayRecipe()
    {
        foreach (var recipe in craftRecipe.recipes)
            if (currentStickerName == recipe.NameRecipe)
                foreach (var ingredient in recipe.Ingredients)
                    Debug.Log(ingredient.NameIngredient + ": " + ingredient.CountIngredient);
    }
}
