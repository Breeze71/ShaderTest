using UnityEngine;

public class MousePos : MonoBehaviour
{
    private static InputProvider input = new();
    public static Vector2 GetMousePosition()
    {
        return (Vector2)Camera.main.ScreenToWorldPoint(input.LaserDirection());
    }
}
