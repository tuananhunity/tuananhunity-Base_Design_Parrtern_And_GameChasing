using Base.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEIV01DinosaurGameManager : GameManager, EventListener<BEIV01DinosaurEvent>
{
    protected override void OnEnable()
    {
        base.OnEnable();
        this.ObserverStartListening<BEIV01DinosaurEvent>();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        this.ObserverStopListening<BEIV01DinosaurEvent>();
    }

    protected override void Start()
    {
        base.Start();

        fSMSystem.SetupStateData(dependency);
        BEIV01DinosaurInitStateData bEIV01DinosaurInitStateData = new();
        BEIV01DinosaurEvent bEIV01DinosaurEvent = new(BEIV01DinosaurEvent.INIT_STATE_START, bEIV01DinosaurInitStateData);
        ObserverManager.TriggerEvent<BEIV01DinosaurEvent>(bEIV01DinosaurEvent);
    }

    public void OnMMEvent(BEIV01DinosaurEvent eventType)
    {
        (string eventName, object data) navigatorData = navigator.GetData(adapter, eventType.EventName, eventType.Data);
        fSMSystem.GotoState(navigatorData.eventName, navigatorData.data);
    }

    
}
