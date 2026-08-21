using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent
{
    //击杀事件
    public static event System.Action OnMousKill;

    public static void killMous()
    { 
        OnMousKill?.Invoke();
    }
}
