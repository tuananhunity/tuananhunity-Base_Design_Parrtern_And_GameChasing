using Cysharp.Threading.Tasks;
using DG.Tweening;
using Base.Observer;
using Spine.Unity;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BEIV01DinosaurTriggerState : FSMState
{
    private const string MAX_TRIGGER_OBSTACLE = "Max fall";
    private const string MAX_SCARY = "Max scary";
    private const string MAX_RUN = "Max run";
    private const string MAX_SCARY_TO_NORMAL = "Max scary to normal";
    private const string DINOSAUR_CHASING = "Khung long chasing";
    private const string DINOSAUR_STOP = "Khung long  stop";
    private BEIV01DinosaurTriggerStateDataObjectDependecy bEIV01DinosaurTriggerStateDataObjectDependecy;
    private CancellationTokenSource cancellationTokenSource;

    public override void OnEnter(object data)
    {
        base.OnEnter(data);
        BEIV01DinosaurTriggerStateData triggerStateData = (BEIV01DinosaurTriggerStateData)data;
        bEIV01DinosaurTriggerStateDataObjectDependecy.bEIV01Trigger.IsTrigger = false;
        Dowork(triggerStateData);
    }

    public override void SetUp(object data)
    {
        bEIV01DinosaurTriggerStateDataObjectDependecy = (BEIV01DinosaurTriggerStateDataObjectDependecy)data;
    }

    private void Dowork(BEIV01DinosaurTriggerStateData triggerStateData)
    {
        cancellationTokenSource = new();
        CancellationToken token = cancellationTokenSource.Token;

        if (triggerStateData.objTrigger.CompareTag("DayXaHoi"))
        {
            TriggerObstacle(token, triggerStateData);
        }
        else if (triggerStateData.objTrigger.CompareTag("Item"))
        {
            TriggerItem(token, triggerStateData);
        }
    }
    private async void TriggerItem(CancellationToken token, BEIV01DinosaurTriggerStateData triggerStateData)
    {
        try
        {
            SoundChannel soundDataVocab = new(SoundChannel.PLAY_SOUND_NEW_OBJECT, triggerStateData.objTrigger.GetComponent<AudioSource>().clip);
            ObserverManager.TriggerEvent<SoundChannel>(soundDataVocab);
            triggerStateData.objTrigger.SetActive(false);
            triggerStateData.objTrigger.transform.parent.GetChild(2).gameObject.SetActive(true);
            await UniTask.Delay(2500, cancellationToken: token);
            triggerStateData.objTrigger.transform.parent.transform.SetParent(bEIV01DinosaurTriggerStateDataObjectDependecy.listItemStudied.transform);
            triggerStateData.objTrigger.transform.parent.gameObject.SetActive(false);

            // TRIGGER_ITEM_FINISH_STATE
            BEIV01DinosaurEvent bEIV01DinosaurEvent = new BEIV01DinosaurEvent(BEIV01DinosaurEvent.TRIGGER_ITEM_FINISH_STATE, null);
            ObserverManager.TriggerEvent<BEIV01DinosaurEvent>(bEIV01DinosaurEvent);
        }
        catch { }
    }
    private async void TriggerObstacle(CancellationToken token, BEIV01DinosaurTriggerStateData triggerStateData)
    {
        try
        {
            bEIV01DinosaurTriggerStateDataObjectDependecy.bEIV01UserInput.IsInput = false; // [BEIV01] lock jump khi va chạm obstacle
            bool isPlayAnimation = false;
            bEIV01DinosaurTriggerStateDataObjectDependecy.maxPlay.AnimationState.SetAnimation(0, MAX_TRIGGER_OBSTACLE, false).Complete += (trackEntry) =>
            {
                isPlayAnimation = true;
                bEIV01DinosaurTriggerStateDataObjectDependecy.maxPlay.AnimationState.SetAnimation(0, MAX_SCARY, true).Complete += (trackEntry) =>
                {
                    SoundChannel soundDataMaxRun = new(SoundChannel.PLAY_SOUND, bEIV01DinosaurTriggerStateDataObjectDependecy.triggerDataConfig.audioMaxMove);
                    ObserverManager.TriggerEvent<SoundChannel>(soundDataMaxRun);
                };
            };
            await UniTask.WaitUntil(() => isPlayAnimation, cancellationToken: token);

            triggerStateData.objTrigger.transform.SetParent(bEIV01DinosaurTriggerStateDataObjectDependecy.objListItem.transform);
            triggerStateData.objTrigger.transform.position = bEIV01DinosaurTriggerStateDataObjectDependecy.objListItem.transform.position;
            bEIV01DinosaurTriggerStateDataObjectDependecy.RectDinosaur.DOAnchorPosX(bEIV01DinosaurTriggerStateDataObjectDependecy.pointDinosaurAttack.anchoredPosition.x, 1f).SetEase(Ease.Linear);
            bEIV01DinosaurTriggerStateDataObjectDependecy.dinosaur.AnimationState.SetAnimation(0, DINOSAUR_CHASING, true).Complete += (trackEntry) =>
            {
                SoundChannel soundDataDinosaurMove = new(SoundChannel.PLAY_SOUND_NEW_OBJECT, bEIV01DinosaurTriggerStateDataObjectDependecy.triggerDataConfig.audioDinosaurMove);
                ObserverManager.TriggerEvent<SoundChannel>(soundDataDinosaurMove);
            };
            SoundChannel soundDataDinosaurAttack = new(SoundChannel.PLAY_SOUND_NEW_OBJECT, bEIV01DinosaurTriggerStateDataObjectDependecy.triggerDataConfig.audioAttack);
            ObserverManager.TriggerEvent<SoundChannel>(soundDataDinosaurAttack);

            await UniTask.Delay(2000, cancellationToken: token);
            bEIV01DinosaurTriggerStateDataObjectDependecy.RectDinosaur.DOAnchorPosX(bEIV01DinosaurTriggerStateDataObjectDependecy.pointReturnLeft.anchoredPosition.x, 1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                bEIV01DinosaurTriggerStateDataObjectDependecy.dinosaur.AnimationState.SetAnimation(0, DINOSAUR_STOP, false);
            });

            await UniTask.Delay(1000, cancellationToken: token);
            bEIV01DinosaurTriggerStateDataObjectDependecy.maxPlay.AnimationState.SetAnimation(0, MAX_SCARY_TO_NORMAL, false).Complete += (trackEntry) =>
            {
                bEIV01DinosaurTriggerStateDataObjectDependecy.maxPlay.AnimationState.SetAnimation(0, MAX_RUN, true).Complete += (trackEntry) =>
                {
                    SoundChannel soundDataMaxRun = new(SoundChannel.PLAY_SOUND, bEIV01DinosaurTriggerStateDataObjectDependecy.triggerDataConfig.audioMaxMove);
                    ObserverManager.TriggerEvent<SoundChannel>(soundDataMaxRun);
                };
                // TRIGGER_OBSTACLE_FINISH_STATE
                BEIV01DinosaurEvent bEIV01DinosaurEvent = new BEIV01DinosaurEvent(BEIV01DinosaurEvent.TRIGGER_OBSTACLE_FINISH_STATE, null);
                ObserverManager.TriggerEvent<BEIV01DinosaurEvent>(bEIV01DinosaurEvent);
            };
        }
        catch { }
    }
    public override void OnExit()
    {
        cancellationTokenSource?.Cancel();
    }
    public override void OnDestroy()
    {
        cancellationTokenSource?.Cancel();
        cancellationTokenSource?.Dispose();
    }
}

public class BEIV01DinosaurTriggerStateData
{
    public GameObject objTrigger { get; set; }
}

public class BEIV01DinosaurTriggerStateDataObjectDependecy
{
    public GameObject objListItem { get; set; }
    public SkeletonGraphic maxPlay { get; set; }
    public SkeletonGraphic dinosaur { get; set; }
    public RectTransform RectDinosaur { get; set; }
    public GameObject listItemStudied { get; set; }
    public RectTransform pointDinosaurAttack { get; set; }
    public RectTransform pointReturnLeft { get; set; }
    public BEIV01Trigger bEIV01Trigger { get; set; }
    public BEIV01UserInput bEIV01UserInput { get; set; }
    public BEIV01DinosaurTriggerConfig triggerDataConfig { get; set; }
}