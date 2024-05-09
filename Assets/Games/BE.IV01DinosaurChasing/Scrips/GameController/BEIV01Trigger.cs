using Base.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEIV01Trigger : MonoBehaviour
{
    private bool isTrigger = true;
    public bool IsTrigger { get => isTrigger; set => isTrigger = value; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTrigger) return;
        BEIV01DinosaurEvent bEIV01DinosaurEvent = new BEIV01DinosaurEvent(BEIV01DinosaurEvent.TRIGGER_STATE, collision.gameObject);
        ObserverManager.TriggerEvent<BEIV01DinosaurEvent>(bEIV01DinosaurEvent);
    }
}
