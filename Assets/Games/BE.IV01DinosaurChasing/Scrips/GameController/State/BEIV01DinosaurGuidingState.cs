using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using System;
using Base.Observer;
using System.Collections.Generic;
using System.Linq;
using Spine.Unity;

public class BEIV01DinosaurGuidingState : FSMState
{
    private BEIV01GuidingHand itemGuiding;
    private BEIV01DinosaurGuidingStateDataObjectDependecy bEIV01DinosaurGuidingStateDataObjectDependecy;
    private CancellationTokenSource cancellationTokenSource;
    private List<AudioClip> lstAudioGuiding;

    private int numberCountToPlayAudio;
    private float fadeTime = 0.5f;

    public override void OnEnter(object data)
    {
        base.OnEnter(data);
        BEIV01DinosaurGuidingStateData guidingStateData = (BEIV01DinosaurGuidingStateData)data;
        cancellationTokenSource = new();
        CancellationToken token = cancellationTokenSource.Token;
        itemGuiding = guidingStateData.itemGuiding.GetComponent<BEIV01GuidingHand>();

        Dowork(token);
    }

    public override void SetUp(object data)
    {
        bEIV01DinosaurGuidingStateDataObjectDependecy = (BEIV01DinosaurGuidingStateDataObjectDependecy)data;

        lstAudioGuiding = bEIV01DinosaurGuidingStateDataObjectDependecy.guidingDataConfig.lstAudioGuiding.ToList();
    }
    private async void Dowork(CancellationToken token)
    {
        EndGuidingEmediately(token);
        try
        {
            bool isPlaySoundDone = false;
            Action ActionDone = () =>
            {
                isPlaySoundDone = true;
            };
            int index = UnityEngine.Random.Range(0, lstAudioGuiding.Count);
            AudioClip audioGuiding = lstAudioGuiding[index];
            SoundChannel sounDataGuiding = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, audioGuiding, ActionDone);
            ObserverManager.TriggerEvent<SoundChannel>(sounDataGuiding);

            PlayGuidingHand(token, null);
            await UniTask.WaitUntil(() => isPlaySoundDone, cancellationToken: token);

            LoopGuiding(token);
        }
        catch { }
    }
    private async void LoopGuiding(CancellationToken token)
    {
        try
        {
            numberCountToPlayAudio++;
            await UniTask.Delay(3000, cancellationToken: token);
            if (numberCountToPlayAudio == 4)
            {
                // play Audio Guiding
                numberCountToPlayAudio = 0;

                bool isPlaySoundDone = false;
                Action ActionDone = () =>
                {
                    isPlaySoundDone = true;
                };
                int index = UnityEngine.Random.Range(0, lstAudioGuiding.Count);
                AudioClip audioGuiding = lstAudioGuiding[index];
                SoundChannel sounDataGuiding = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, audioGuiding, ActionDone);
                ObserverManager.TriggerEvent<SoundChannel>(sounDataGuiding);

                
                PlayGuidingHand(token, null); ;
                await UniTask.WaitUntil(() => isPlaySoundDone, cancellationToken: token);
                LoopGuiding(token);
            }
            else
            {
                bool isPlayGuidingHand = false;
                PlayGuidingHand(token, () => isPlayGuidingHand = true);
                await UniTask.WaitUntil(() => isPlayGuidingHand, cancellationToken: token);

                LoopGuiding(token);
            }
        }
        catch { }

    }
    private async void PlayGuidingHand(CancellationToken token, Action callback)
    {
        try
        {
            if (!UserManager.instance.GetLocalProfileSettings().ai_guideChooseAnswer)
            {
                callback?.Invoke();
            }
            else
            {
                itemGuiding.FadeIn(fadeTime);
                await UniTask.Delay(500, cancellationToken: token);

                this.itemGuiding.EnableAnimator(true);
                await UniTask.Delay(2000, cancellationToken: token);

                itemGuiding.FadeOut(fadeTime);
                this.itemGuiding.EnableAnimator(false);
                await UniTask.Delay(500, cancellationToken: token);
                callback?.Invoke();
            }  
        }
        catch { }
    }
    private async void EndGuidingEmediately(CancellationToken token)
    {
        try
        {
            await UniTask.WaitUntil(() => !bEIV01DinosaurGuidingStateDataObjectDependecy.bEIV01UserInput.IsEndGuiding, cancellationToken: token);
            OnExit();
        }
        catch { }
    }
    public override void OnExit()
    {
        itemGuiding.gameObject.SetActive(false);
        cancellationTokenSource?.Cancel();
    }
    public override void OnDestroy()
    {
        cancellationTokenSource?.Cancel();
        cancellationTokenSource?.Dispose();
    }

}

public class BEIV01DinosaurGuidingStateData
{
    public GameObject itemGuiding { get; set; }
}


public class BEIV01DinosaurGuidingStateDataObjectDependecy
{
    public BEIV01DinosaurGuidingConfig guidingDataConfig { get; set; }
    public BEIV01UserInput bEIV01UserInput { get; set; }
}