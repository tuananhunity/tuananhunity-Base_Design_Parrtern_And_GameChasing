using BaseSDK;
using Base.Observer;
using System.Collections.Generic;
using UnityEngine;

public class BEIV01DinosaurNavigator : Navigator
{
    private int numberTriggerObstacle;
    public override (string, object) GetData(Adapter adapter, string eventName, object eventData)
    {
        switch (eventName)
        {
            case BEIV01DinosaurEvent.INIT_STATE_START:
                BEIV01DinosaurInitStateData bEIV01DinosaurInitStateData = adapter.GetData<BEIV01DinosaurInitStateData>(turn);
                return (BEIV01DinosaurEvent.INIT_STATE_START, bEIV01DinosaurInitStateData);
            case BEIV01DinosaurEvent.INIT_STATE_FINISH:
                return (BEIV01DinosaurEvent.INTRO_STATE_START, null);
            case BEIV01DinosaurEvent.INTRO_STATE_FINISH:
                BEIV01DinosaurGuidingStateData bEIV01DinosaurGuidingStateData = new();
                bEIV01DinosaurGuidingStateData.itemGuiding = (GameObject)eventData;
                return (BEIV01DinosaurEvent.GUIDING_STATE, bEIV01DinosaurGuidingStateData);
            case BEIV01DinosaurEvent.TRIGGER_STATE:
                BEIV01DinosaurTriggerStateData bEIV01DinosaurTriggerStateData = new();
                bEIV01DinosaurTriggerStateData.objTrigger = (GameObject)eventData;
                return (BEIV01DinosaurEvent.TRIGGER_STATE, bEIV01DinosaurTriggerStateData);
            case BEIV01DinosaurEvent.TRIGGER_ITEM_FINISH_STATE:
                numberTriggerObstacle = 0;
                BEIV01DinosaurTriggerDoneStateData bEIV01DinosaurTriggerDoneStateDataTriggerItem = new();
                bEIV01DinosaurTriggerDoneStateDataTriggerItem.numberTriggerObstacle = this.numberTriggerObstacle;
                return (BEIV01DinosaurEvent.TRIGGER_DONE_STATE_START, bEIV01DinosaurTriggerDoneStateDataTriggerItem);
            case BEIV01DinosaurEvent.TRIGGER_OBSTACLE_FINISH_STATE:
                numberTriggerObstacle++;
                BEIV01DinosaurTriggerDoneStateData bEIV01DinosaurTriggerDoneStateDataTriggerObstacle = new();
                bEIV01DinosaurTriggerDoneStateDataTriggerObstacle.numberTriggerObstacle = this.numberTriggerObstacle;
                if (numberTriggerObstacle == 3)
                {
                    numberTriggerObstacle = 0;
                }
                return (BEIV01DinosaurEvent.TRIGGER_DONE_STATE_START, bEIV01DinosaurTriggerDoneStateDataTriggerObstacle);
            case BEIV01DinosaurEvent.TRIGGER_DONE_STATE_FINISH:
                if(eventData != null)
                {
                    BEIV01DinosaurGuidingStateData bEIV01DinosaurGuidingStateDataTriggerDone = new();
                    bEIV01DinosaurGuidingStateDataTriggerDone.itemGuiding = (GameObject)eventData;
                    return (BEIV01DinosaurEvent.GUIDING_STATE, bEIV01DinosaurGuidingStateDataTriggerDone);
                }
                else
                {
                    return (BEIV01DinosaurEvent.ENDGAME_STATE, null);
                }
            default:
                return ("", null);
        }
        return ("", null);
    }
    public void OnReceiveEvent(EventMessage message)
    {
        if (message.eventChanelId == EventChanelID.GameState)
        {
            if (message.eventName == EventName.GameState.EXIT_GAME_PLAYING)
            {
                EventManagerPuto.Instance.Push(EventChanelID.GameState, EventName.GameState.GAME_STOP, null);
                Destroy(gameObject, .1f);
            }
            else if (message.eventName == EventName.GameState.GAME_STOP)
            {
                SoundChannel soundStop = new(SoundChannel.STOP_SOUND, null);
                ObserverManager.TriggerEvent<SoundChannel>(soundStop);
                SoundChannel soundPause = new(SoundChannel.PAUSE_SOUND, null);
                ObserverManager.TriggerEvent<SoundChannel>(soundPause);

                EventManagerPuto.Instance.Push(EventChanelID.GameState, EventName.GameState.GAME_END, null);
                Destroy(gameObject, .1f);

            }
        }
    }
}
