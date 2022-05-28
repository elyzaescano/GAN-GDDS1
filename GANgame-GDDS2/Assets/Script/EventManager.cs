using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action<int> DoorOpenEvent;

    public int doorID;
    public void DoorOpen()
    {
        DoorOpenEvent?.Invoke(doorID);
    }

}
