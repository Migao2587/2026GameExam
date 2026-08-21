using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerInput2 : MonoBehaviour
{
    [HideInInspector]public InputActions inputAction;
    private Vector2 inputDirection;
    public UnityEvent<Vector2> move;
    public UnityEvent Pause;
    [HideInInspector]public bool isPause = false;
    public UnityEvent Continue;
    private InputAction playermove;
    private InputAction pause;

    private void Awake()
    {
        inputAction = new InputActions();
        inputAction.Play.ESC.performed += OnEscPressed;
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }
    // Update is called once per frame
    void Update()
    {
        //移动
        inputDirection = inputAction.Play.WASD.ReadValue<Vector2>();
        move?.Invoke(inputDirection);

    }

    //ESC回调
    void OnEscPressed(InputAction.CallbackContext ctx)
    {
        if (!isPause)
        {
            Debug.Log("按下ESC");
            isPause = true;
            Pause?.Invoke();
            WASD(false);
        }
        else
        { 
            Continue?.Invoke();
            isPause = false;
            WASD(true);
        }
    }

    public void WASD(bool k)
    {
        if (k)
        {
            inputAction?.Play.WASD.Enable();
        }
        else
        {
            inputAction.Play.WASD.Disable();
        }
    }
    
    public void ESC(bool k)
    {
        if (k)
        {
            inputAction.Play.ESC.Enable();
        }
        else
        {
            inputAction.Play.ESC.Disable();
        }

    }

    private void OnDisable()
    {
        inputAction.Disable();
    }
}
