using System;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<bool> OnSpecialFireEvent;

    public void CallMoveEvent(Vector2 dir)
    {
        OnMoveEvent?.Invoke(dir);
    }
    
    public void CallSpecialFireEvent(bool isPress )
    {
        OnSpecialFireEvent?.Invoke(isPress);
    } 
}
