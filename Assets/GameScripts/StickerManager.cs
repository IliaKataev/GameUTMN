using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickerManager : MonoBehaviour
{
    public Image stickerImage; // ��������� Image ��� ����������� �������
    public Sprite[] stickerSprites1; // ������ ��� �������� ������� ������ ��������
    public Sprite[] stickerSprites2; // ������ ��� �������� ������� ������ ��������

    private List<Sprite> allStickers; // ������ ��� �������� ���� ��������
    private bool canChangeSticker = true; // ���� ��� �������� ����� �������

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
            Debug.Log("����������� ������ �������");
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
        if (stickerImage != null && stickerImage.sprite != null)
        {
            Debug.Log($"{stickerImage.sprite.name.Split('.')[0]}");// ���������� �������� ����� ������� (��� ����������) �������� �������
            return stickerImage.sprite.name.Split('.')[0];
        }
        else
        {
            return null; // ���� ��� �������, ���������� null ��� ������ �������� �� ���������
        }
    }

    public void SetCanChangeSticker(bool value)
    {
        canChangeSticker = value;
    }
}
