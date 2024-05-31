using UnityEngine;

[System.Serializable]
public class CurrentSticker
{
    public string CurrentStickerName { get;set; }
    public Sprite CurrentStickerSprite { get;set; }

    public CurrentSticker(string name, Sprite sprite)
    {
        CurrentStickerName = name;
        CurrentStickerSprite = sprite;
    }
}
