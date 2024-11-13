using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseReader 
{
    private PlayerInput _playerInput;

    public event Action<Vector2> Clicked;

    public MouseReader()
    {
        _playerInput = new PlayerInput();
    }

    public void Enable()
    {
        _playerInput.Enable();
        _playerInput.Player.Click.performed += click => Notify();
    }

    public void Disable()
    {
        _playerInput.Disable();
        _playerInput.Player.Click.performed -= click => Notify();
    }

    private void Notify()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        
        Clicked?.Invoke(mousePosition);
    }
}
