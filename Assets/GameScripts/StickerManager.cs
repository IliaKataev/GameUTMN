using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickerManager : MonoBehaviour
{
    public Image stickerImage; // Компонент Image для отображения стикера
    public Sprite[] stickerSprites1; // Массив для хранения первого набора стикеров
    public Sprite[] stickerSprites2; // Массив для хранения второго набора стикеров

    private List<Sprite> allStickers; // Список для хранения всех стикеров
    private bool canChangeSticker = true; // Флаг для контроля смены стикера

    void Start()
    {
        LoadStickers();
        SetRandomSticker();
        stickerImage.GetComponent<Button>().onClick.AddListener(TryChangeSticker);
    }

    void LoadStickers()
    {
        allStickers = new List<Sprite>();
        allStickers.AddRange(stickerSprites1);
        allStickers.AddRange(stickerSprites2);
    }

    void SetRandomSticker()
    {
        int randomIndex = Random.Range(0, allStickers.Count);
        Sprite currentSticker = allStickers[randomIndex];
        stickerImage.sprite = currentSticker;
    }

    public void ChangeSticker()
    {
        if (canChangeSticker)
        {
            SetRandomSticker();
            Debug.Log("Стандартный стикер изменен");
        }
        else
        {
            Debug.Log("Сначала завершите текущий заказ!");
        }
    }

    public void TryChangeSticker()
    {
        if (canChangeSticker)
        {
            ChangeSticker();
        }
        else
        {
            Debug.Log("Сначала завершите текущий заказ!");
        }
    }

    public string GetCurrentStickerName()
    {
        if (stickerImage != null && stickerImage.sprite != null)
        {
            Debug.Log($"{stickerImage.sprite.name.Split('.')[0]}");// Возвращаем название файла спрайта (без расширения) текущего стикера
            return stickerImage.sprite.name.Split('.')[0];
        }
        else
        {
            return null; // Если нет спрайта, возвращаем null или другое значение по умолчанию
        }
    }

    public void SetCanChangeSticker(bool value)
    {
        canChangeSticker = value;
    }
}
