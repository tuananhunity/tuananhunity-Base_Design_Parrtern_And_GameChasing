using Base.Observer;
using System.Collections.Generic;
using UnityEngine;

public class BEIV01DinosaurPrepareNextTurnState : FSMState
{
    private BEIV01DinosaurPrepareNextTurnStateDataObjectDependecy bEIV01DinosaurPrepareNextTurnStateDataObjectDependecy;


    public override void OnEnter(object data)
    {
        base.OnEnter(data);
        BEIV01DinosaurPrepareNextTurnStateData prepareNextTurnStateData = (BEIV01DinosaurPrepareNextTurnStateData)data;
        Dowork(prepareNextTurnStateData);
    }

    public override void SetUp(object data)
    {

        bEIV01DinosaurPrepareNextTurnStateDataObjectDependecy = (BEIV01DinosaurPrepareNextTurnStateDataObjectDependecy)data;

    }
    private void Dowork(BEIV01DinosaurPrepareNextTurnStateData prepareNextTurnStateData)
    {

    }
    private void TriggerFinishSetData()
    {
        
    }
}

public class BEIV01DinosaurPrepareNextTurnStateData
{
    public BEIV01PlayData bEIV01PlayData { get; set; }
}

public class BEIV01DinosaurPrepareNextTurnStateDataObjectDependecy
{ 
    public GameObject objListItem { get; set; }
    public BEIV01UserInput bEIV01UserInput { get; set; }
    public BEIV01Trigger bEIV01Trigger { get; set; }
    public LoopBackGround loopBackGround { get; set; }
}