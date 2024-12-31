using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

using UnityEngine.InputSystem;
public class GatherInput : MonoBehaviour
{
    private Control myControls;

    public float valueX;
    public bool JumpInput;
    public bool FallInput;


    void Awake()
    {
        myControls = new Control();
    }

    private void OnEnable()
    {   
        //  Moving
        myControls.Player.Moving.performed += StartMoving;
        myControls.Player.Moving.canceled += StopMoving;
        //Jumping
        myControls.Player.Jumping.performed += StartJumping;
        myControls.Player.Jumping.canceled += StopJumping;
        //Falling
        myControls.Player.Falling.performed += StartFalling;
        myControls.Player.Falling.canceled += StopFalling;

        myControls.Player.Enable();
    }
    private void OnDisable()
    {
        myControls.Player.Moving.performed -= StartMoving;
        myControls.Player.Moving.canceled -= StopMoving;

        myControls.Player.Jumping.performed -= StartJumping;
        myControls.Player.Jumping.canceled -= StopJumping;

        myControls.Player.Falling.performed -= StartFalling;
        myControls.Player.Falling.canceled -= StopFalling;
        myControls.Player.Disable();
    }

    void StartMoving(InputAction.CallbackContext ctx)
    {
        valueX = ctx.ReadValue<float>();
    }
    void StopMoving(InputAction.CallbackContext ctx)
    {
        valueX = 0;
    } 
    private void StartJumping(InputAction.CallbackContext ctx)
    {
        JumpInput = true;
    }
    private void  StopJumping(InputAction.CallbackContext ctx)
    {
        JumpInput = false;
    }
    private void  StartFalling(InputAction.CallbackContext ctx)
    {
        FallInput = true;
    }
    private void StopFalling(InputAction.CallbackContext ctx) 
    {
        FallInput = false;
    }
    public void DisableControls()
    {
        valueX = 0;
        myControls.Player.Moving.performed -= StartMoving;
        myControls.Player.Moving.canceled -= StopMoving;

        myControls.Player.Jumping.performed -= StartJumping;
        myControls.Player.Jumping.canceled -= StopJumping;

        myControls.Player.Falling.performed -= StartFalling;
        myControls.Player.Falling.canceled -= StopFalling;
        myControls.Player.Disable();

    }
}

