using UnityEngine;
using System;
using UnityEngine.InputSystem;
public class InputProvider
{
    /* 實例化共享對象 */
    private static PlayerInput input = new();

    public void Enable()
    {
        input.Player.Movement.Enable();
        input.Player.Shoot.Enable();
        input.Player.LaserDir.Enable();
    }

    public void Disable()
    {
        input.Player.Movement.Disable();
        input.Player.Shoot.Disable();
        input.Player.LaserDir.Disable();
    }
     
    /* 讀取二維輸入 */
    public Vector2 Vector2Move()
    {
        return input.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 LaserDirection()
    {
        return input.Player.LaserDir.ReadValue<Vector2>();
    }

    /* Shoot Event */
    public event Action<InputAction.CallbackContext> ShootLaser
    {
        // 觸發 Event 的三個時間段
        add
        {
            input.Player.Shoot.started += value;
            input.Player.Shoot.performed += value;
            input.Player.Shoot.canceled += value;
        }
        remove
        {
            input.Player.Shoot.started -= value;
            input.Player.Shoot.performed -= value;
            input.Player.Shoot.canceled -= value;
        }
    }

}
