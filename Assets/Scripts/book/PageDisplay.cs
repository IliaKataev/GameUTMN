using UnityEngine;
using UnityEngine.UI;
public class PageDisplay : MonoBehaviour
{
    [SerializeField] private Image pageImage;

    public void DisplayPage(Page page)
    {
        pageImage.sprite=page.pageImage;
    }
}
