using UnityEngine;

public static class RectTranExtension
{
    public static void SetWidth(this RectTransform t , float width)
    {
        t.sizeDelta = new Vector2(width , t.rect.height);
    }
}