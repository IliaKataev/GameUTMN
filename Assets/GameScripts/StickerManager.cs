using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickerManager : MonoBehaviour
{
    public Image stickerImage; // Image component to display the sticker
    public Sprite[] stickerSprites1; // Array to hold the first set of stickers
    public Sprite[] stickerSprites2; // Array to hold the second set of stickers

    private List<Sprite> allStickers;
    private Sprite currentSticker;
    private bool canChangeSticker = false; // ���� ��� �������� ����� �������

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
        currentSticker = allStickers[randomIndex];
        stickerImage.sprite = currentSticker;
    }

    public void ChangeSticker()
    {
        if (canChangeSticker)
        {
            SetRandomSticker();
            Debug.Log("������ �������");
        }
        else
        {
            Debug.Log("������� ��������� ������� �����!");
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
            Debug.Log("������� ��������� ������� �����!");
        }
    }
    
    public string GetCurrentStickerName()
    {
        Debug.Log($"Using item: {currentSticker.name}");
        return currentSticker.name; // ���������� �������� �������� �������
    }

    public void SetCanChangeSticker(bool value)
    {
        canChangeSticker = value;
    }
}
