using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : TopDownController
{
    
    
    public void OnMove(InputAction.CallbackContext context)
    {
        if (FlightDataManager.Instance.PlayerFlightStat.CurrentStat.EFlightStatus == EFlightStatus.Dead  ||   GameManager.Instance.GameClearUI.activeSelf)
        {
            return;
        }
        
        Vector2 dir = context.ReadValue<Vector2>().normalized;
        CallMoveEvent(dir);
    }

    public void OnSpecialFire(InputAction.CallbackContext context)
    {
        if (FlightDataManager.Instance.PlayerFlightStat.CurrentStat.EFlightStatus == EFlightStatus.Dead)
        {
            return;
        }
        
        if(context.phase != InputActionPhase.Performed)
        {
            CallSpecialFireEvent(context.phase == InputActionPhase.Started);   
        }
    }
}