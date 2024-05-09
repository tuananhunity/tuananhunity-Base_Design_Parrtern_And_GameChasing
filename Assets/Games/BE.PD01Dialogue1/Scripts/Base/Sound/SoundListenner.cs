using Base.Observer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundListenner : MonoBehaviour, EventListener<SoundChannel>
{
    public void OnMMEvent(SoundChannel eventType)
    {
        switch (eventType.EventName)
        {
            case SoundChannel.PLAY_SOUND:
                SoundManager.Instance.PlayFx(eventType.AudioClip, eventType.ActionDone, eventType.Loop, eventType.Volume);
                break;
            case SoundChannel.PLAY_SOUND_NEW_OBJECT:
                SoundManager.Instance.PlayFxOnNewGameObject(eventType.AudioClip, eventType.ActionDone, eventType.Volume);
                break;
            case SoundChannel.PAUSE_SOUND:
                break;
            case SoundChannel.STOP_SOUND:
                break;
        }
    }

    private void OnEnable()
    {
        this.ObserverStartListening<SoundChannel>();
    }

    private void OnDisable()
    {
        this.ObserverStopListening<SoundChannel>();
    }
}
