using BaseSDK;
using Base.Observer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerRequestListenner : MonoBehaviour, IEventListener
{
    private AudioClip audioClipSend = null;
    private void Awake()
    {
        EventManagerPuto.Instance.AddListener(this, EventChanelID.GamePlay);
    }

    private void OnDestroy()
    {
        EventManagerPuto.Instance?.RemoveListener(this);
    }

    public void OnReceiveEvent(EventMessage message)
    {
        if (message.eventChanelId == EventChanelID.GamePlay)
        {
            switch (message.eventName)
            {
                case EventName.GamePlay.RECORD_COMPLETE:
                    BEPD01DialogueEvent bEPD01DialogueEvent = new BEPD01DialogueEvent(BEPD01DialogueEvent.WAITTING_SERVER_STATE_START, null);
                    ObserverManager.TriggerEvent<BEPD01DialogueEvent>(bEPD01DialogueEvent);

                    break;
                case EventName.GamePlay.RECORD_COMPLETE_SAVE:
                    audioClipSend = (AudioClip)message.data.sender;
                    break;
                case EventName.GamePlay.RECORD_RESULT_SUCCESS:
                    BEPD01DialogueResultServerReturn dataResultReturn = new BEPD01DialogueResultServerReturn();
                    if (message.data.p0 is SpeechResponseData data)
                    {
                        dataResultReturn.DataRespone = data;
                    }
                    dataResultReturn.audioClip = audioClipSend;
                    BEPD01DialogueEvent bEPD01DialogueEventResult = new BEPD01DialogueEvent(BEPD01DialogueEvent.RESULT_SERVER_STATE_START, dataResultReturn);
                    ObserverManager.TriggerEvent<BEPD01DialogueEvent>(bEPD01DialogueEventResult);
                    break;
            }
        }
    }
}
