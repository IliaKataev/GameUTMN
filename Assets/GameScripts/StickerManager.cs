using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickerManager : MonoBehaviour
{
    public Image stickerImage;
    public CraftRecipe craftRecipe;

    public CurrentSticker currentSticker { get; set; }

    void Awake()
    {
        //Debug.Log("StickerManager инициализирован в Awake");
        SetRandomSticker();
        DisplayRecipe();
        
    }

    public void SetRandomSticker()
    {
        int randomIndex = Random.Range(0, craftRecipe.stickers.Count);
        string stickerName = craftRecipe.stickers[randomIndex].NameOrder;
        Sprite stickerSprite = craftRecipe.stickers[randomIndex].OrderSticker;

        // Инициализация currentSticker новым значением
        currentSticker = new CurrentSticker(stickerName, stickerSprite);

        //Debug.Log(currentSticker.CurrentStickerName + "В СТИКЕР МАНАГЕРЕ");
        stickerImage.sprite = currentSticker.CurrentStickerSprite;
    }

    public void DisplayRecipe()
    {
        foreach (var recipe in craftRecipe.recipes)
            if (currentSticker.CurrentStickerName == recipe.NameRecipe)
                foreach (var ingredient in recipe.Ingredients)
                    Debug.Log(ingredient.NameIngredient + ": " + ingredient.CountIngredient);
    }

    public CurrentSticker CurrentStickerObject
    {
        get { return currentSticker; }
    }

    // Метод для получения имени текущего стикера
    public string GetCurrentStickerName()
    {
        return currentSticker.CurrentStickerName;
        
    }


    //public string GetCurrentStickerName()
    //{
    //    return currentStickerName;
    //}
}
