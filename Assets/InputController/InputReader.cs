using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, InputPlayer.IPlayerActions
{
    private Vector2 mousePosition = new Vector2();
    private Vector2 mousePositionInWorld = new Vector2();
    private bool click = false;
    private bool escape = false;
    
    private InputPlayer controls;
    public bool Click { get => click; set => click = value; }
    public bool Escape { get => escape; set => escape = value; }
    public Vector2 MousePosition { get => mousePosition; set => mousePosition = value; }
    public Vector2 MousePositionInWorld => Camera.main.ScreenToWorldPoint(mousePosition);
    private void Start()
    {
        controls = new InputPlayer();
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        
    }

    public void OnLook(InputAction.CallbackContext context)
    {

    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.ReadValue<float>() == 1f) return;
        
        click = true;
    }

    public void OnEscape(InputAction.CallbackContext context)
    {        
        if(context.ReadValue<float>() == 1f) return;
        
        escape = true;
    }

    public void OnMouse(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }
}
