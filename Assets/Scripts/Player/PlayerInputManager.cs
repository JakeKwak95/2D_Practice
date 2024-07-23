using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    PlayerControl inputActions;
    PlayerManager playerManager;

    private void Start()
    {
        playerManager = PlayerManager.Instance;
    }

    private void OnEnable()
    {
        inputActions = new PlayerControl();
        inputActions.Enable();

        EventsBroadcaster.OnFail += HandleFailEvent;
    }

    private void OnDisable()
    {
        inputActions.Disable();

        EventsBroadcaster.OnFail += HandleFailEvent;
    }

    private void Update()
    {
        HaldeTriggerInput();

        HandleMovementInput();

        HandleStateChangeInput();
    }

    private void HaldeTriggerInput()
    {
        if (inputActions.Player.Jump.WasPressedThisFrame())
        {
            playerManager.Locomotion.Jump();
        }
    }

    private void HandleStateChangeInput()
    {
        playerManager.Locomotion.Crouch(inputActions.Player.Crouch.IsPressed());

        if (inputActions.Player.ChangeSkateState.WasPressedThisFrame())
        {
            playerManager.Locomotion.ChangeSprintState();
        }
    }

    private void HandleMovementInput()
    {
        if (inputActions.Player.Horizontal.IsPressed())
        {
            playerManager.Locomotion.MoveHorizontally(inputActions.Player.Horizontal.ReadValue<float>() * transform.right);
        }
        else
        {
            SetSpeedPram(0);
        }
    }


    private void HandleFailEvent()
    {
        inputActions.Disable();
    }

    void SetSpeedPram(float speed)
    {
        playerManager.Locomotion.SetSpeedParam(speed);
    }

    public void ToggleInput()
    {
        if (!inputActions.Player.enabled)
            inputActions.Enable();
        else 
            inputActions.Disable();
    }
    public void EnableInput()
    {
        inputActions.Enable();
    }
    public void DisableInput()
    {
        inputActions.Disable();
    }


    #region Signals

    public void SendSignal()
    {
        ToggleInput();
        SetSpeedPram(0);
    }


    #endregion
}
