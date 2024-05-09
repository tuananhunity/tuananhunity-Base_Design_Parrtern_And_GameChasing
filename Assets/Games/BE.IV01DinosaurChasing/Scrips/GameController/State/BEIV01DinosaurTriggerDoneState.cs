using DG.Tweening;
using Base.Observer;
using System.Collections.Generic;
using UnityEngine;

public class BEIV01DinosaurTriggerDoneState : FSMState
{
    private BEIV01DinosaurTriggerDoneStateDataObjectDependecy bEIV01DinosaurTriggerDoneStateDataObjectDependecy;
    private GameObject itemSpawn;

    public override void OnEnter(object data)
    {
        base.OnEnter(data);
        BEIV01DinosaurTriggerDoneStateData triggerDoneStateData = (BEIV01DinosaurTriggerDoneStateData)data;
        Dowork(triggerDoneStateData);
    }

    public override void SetUp(object data)
    {
        bEIV01DinosaurTriggerDoneStateDataObjectDependecy = (BEIV01DinosaurTriggerDoneStateDataObjectDependecy)data;
    }
    private void Dowork(BEIV01DinosaurTriggerDoneStateData triggerDoneStateData)
    {
        if (bEIV01DinosaurTriggerDoneStateDataObjectDependecy.objListItem.transform.childCount > 0)
        {
            itemSpawn = bEIV01DinosaurTriggerDoneStateDataObjectDependecy.objListItem.transform.GetChild(0).gameObject;
            SpawnItem(triggerDoneStateData);
        }
        else
        {
            BEIV01DinosaurEvent bEIV01DinosaurEvent = new BEIV01DinosaurEvent(BEIV01DinosaurEvent.TRIGGER_DONE_STATE_FINISH, null);
            ObserverManager.TriggerEvent<BEIV01DinosaurEvent>(bEIV01DinosaurEvent);
        }
    }
    private void SpawnItem(BEIV01DinosaurTriggerDoneStateData triggerDoneStateData)
    {
        if (triggerDoneStateData.numberTriggerObstacle == 3)
        {
            itemSpawn.transform.SetParent(bEIV01DinosaurTriggerDoneStateDataObjectDependecy.loopBackGround.gameObject.transform);
            bEIV01DinosaurTriggerDoneStateDataObjectDependecy.loopBackGround.ItemAppear(() =>
            {
                bEIV01DinosaurTriggerDoneStateDataObjectDependecy.loopBackGround.SetSpeed = 0;
                bEIV01DinosaurTriggerDoneStateDataObjectDependecy.bEIV01UserInput.IsEndGuiding = true;

                BEIV01DinosaurEvent bEIV01DinosaurEvent = new BEIV01DinosaurEvent(BEIV01DinosaurEvent.TRIGGER_DONE_STATE_FINISH, itemSpawn.transform.GetChild(3).gameObject);
                ObserverManager.TriggerEvent<BEIV01DinosaurEvent>(bEIV01DinosaurEvent);
            });
        }
        else
        {
            itemSpawn.transform.SetParent(bEIV01DinosaurTriggerDoneStateDataObjectDependecy.loopBackGround.gameObject.transform);
        }
        bEIV01DinosaurTriggerDoneStateDataObjectDependecy.bEIV01Trigger.IsTrigger = true;
        bEIV01DinosaurTriggerDoneStateDataObjectDependecy.bEIV01UserInput.IsInput = true;
    }
}

public class BEIV01DinosaurTriggerDoneStateData
{
    public int numberTriggerObstacle { get; set; }
}

public class BEIV01DinosaurTriggerDoneStateDataObjectDependecy
{ 
    public GameObject objListItem { get; set; }
    public RectTransform pointItemGuiding { get; set; }
    public LoopBackGround loopBackGround { get; set; }
    public BEIV01UserInput bEIV01UserInput { get; set; }
    public BEIV01Trigger bEIV01Trigger { get; set; }
}