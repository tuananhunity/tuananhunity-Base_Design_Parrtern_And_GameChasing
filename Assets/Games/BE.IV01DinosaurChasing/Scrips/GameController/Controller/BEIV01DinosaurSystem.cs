using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEIV01DinosaurSystem : FSMSystem
{
    private FSMState bEIV01DinosaurInitState;
    private FSMState bEIV01DinosaurIntroState;
    private FSMState bEIV01DinosaurGuidingState;
    private FSMState bEIV01DinosaurTriggerState;
    private FSMState bEIV01DinosaurTriggerDoneState;
    private FSMState bEIV01DinosaurEndGameState;
    //private FSMState bEIV01DinosaurPrepareNextTurnState;

    private void Awake()
    {
        bEIV01DinosaurInitState = new BEIV01DinosaurInitState();
        bEIV01DinosaurIntroState = new BEIV01DinosaurIntroState();
        bEIV01DinosaurGuidingState = new BEIV01DinosaurGuidingState();
        bEIV01DinosaurTriggerState = new BEIV01DinosaurTriggerState();
        bEIV01DinosaurTriggerDoneState = new BEIV01DinosaurTriggerDoneState();
        bEIV01DinosaurEndGameState = new BEIV01DinosaurEndGameState();
        //bEIV01DinosaurPrepareNextTurnState = new BEIV01DinosaurPrepareNextTurnState();
    }

    public override void GotoState(string eventName, object data)
    {
        switch (eventName)
        {
            case BEIV01DinosaurEvent.INIT_STATE_START:
                GotoState(bEIV01DinosaurInitState, data);
                break;
            case BEIV01DinosaurEvent.INTRO_STATE_START:
                GotoState(bEIV01DinosaurIntroState, null);
                break;
            case BEIV01DinosaurEvent.GUIDING_STATE:
                GotoState(bEIV01DinosaurGuidingState, data);
                break;
            case BEIV01DinosaurEvent.TRIGGER_STATE:
                GotoState(bEIV01DinosaurTriggerState, data);
                break;
            case BEIV01DinosaurEvent.TRIGGER_DONE_STATE_START:
                GotoState(bEIV01DinosaurTriggerDoneState, data);
                break;
            case BEIV01DinosaurEvent.ENDGAME_STATE:
                GotoState(bEIV01DinosaurEndGameState, null);
                break;
                //case BEIV01DinosaurEvent.PREPARE_NEXTTURN_STATE_START:
                //    GotoState(bEIV01DinosaurPrepareNextTurnState, null);
                //    break;
        }
    }

    public override void SetupStateData<T>(T data)
    {
        if (data is Dependency dependency)
        {
            BEIV01DinosaurInitStateDataObjectDependecy initStateData = dependency.GetStateData<BEIV01DinosaurInitStateDataObjectDependecy>();
            bEIV01DinosaurInitState.SetUp(initStateData);
            BEIV01DinosaurIntroStateDataObjectDependecy introStateData = dependency.GetStateData<BEIV01DinosaurIntroStateDataObjectDependecy>();
            bEIV01DinosaurIntroState.SetUp(introStateData);
            BEIV01DinosaurGuidingStateDataObjectDependecy guidingStateData = dependency.GetStateData<BEIV01DinosaurGuidingStateDataObjectDependecy>();
            bEIV01DinosaurGuidingState.SetUp(guidingStateData);
            BEIV01DinosaurTriggerStateDataObjectDependecy triggerStateData = dependency.GetStateData<BEIV01DinosaurTriggerStateDataObjectDependecy>();
            bEIV01DinosaurTriggerState.SetUp(triggerStateData);
            BEIV01DinosaurTriggerDoneStateDataObjectDependecy triggerDoneStateData = dependency.GetStateData<BEIV01DinosaurTriggerDoneStateDataObjectDependecy>();
            bEIV01DinosaurTriggerDoneState.SetUp(triggerDoneStateData);
            BEIV01DinosaurEndGameStateDataObjectDependecy endGameStateData = dependency.GetStateData<BEIV01DinosaurEndGameStateDataObjectDependecy>();
            bEIV01DinosaurEndGameState.SetUp(endGameStateData);
            //BEIV01DinosaurPrepareNextTurnStateDataObjectDependecy prepareNextTurnStateData = dependency.GetStateData<BEIV01DinosaurPrepareNextTurnStateDataObjectDependecy>();
            //bEIV01DinosaurPrepareNextTurnState.SetUp(prepareNextTurnStateData);
        }
    }
}
