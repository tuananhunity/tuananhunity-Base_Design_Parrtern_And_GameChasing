using Cysharp.Threading.Tasks;
using DG.Tweening;
using Base.Observer;
using Spine.Unity;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BEIV01DinosaurIntroState : FSMState
{
    private const string MAX_INTRO = "Intro";
    private const string ELLIE_INTRO = "Intro";
    private const string ROBOT_INTRO = "Intro";
    private const string ROBOT_RUN = "Run";
    private const string ELLIE_RUN = "Run";
    private const string MAX_RUN_INTRO = "Max Run intro";
    private const string MAX_TAKE_EGG = "Max take egg";
    private const string MAX_RUN_LOOP = "Max run - look back";
    private const string MAX_RUN = "Max run";
    private const string DINOSAUR_EATEGG = "Khung long an trung";
    private const string DINOSAUR_START_RUN = "Khung long start to run away";
    private const string DINOSAUR_RUN_AWAY = "Khung long run away";
    private const string DINOSAUR_STOP = "Khung long  stop";
    private GameObject itemGuiding;
    private BEIV01DinosaurIntroStateDataObjectDependecy bEIV01DinosaurIntroStateDataObjectDependecy;
    private CancellationTokenSource cancellationTokenSource;

    public override void OnEnter(object data)
    {
        base.OnEnter(data);
        cancellationTokenSource = new();
        CancellationToken token = cancellationTokenSource.Token;

        itemGuiding = bEIV01DinosaurIntroStateDataObjectDependecy.objListItem.transform.GetChild(0).gameObject;
        Dowork(token);
    }

    public override void SetUp(object data)
    {
        bEIV01DinosaurIntroStateDataObjectDependecy = (BEIV01DinosaurIntroStateDataObjectDependecy)data;
    }
    private async void Dowork(CancellationToken token)
    {
        try
        {
            SoundChannel soundDataDelicious = new(SoundChannel.PLAY_SOUND, bEIV01DinosaurIntroStateDataObjectDependecy.introDataConfig.audioDelicious);
            SoundChannel soundDataOhno = new(SoundChannel.PLAY_SOUND_NEW_OBJECT, bEIV01DinosaurIntroStateDataObjectDependecy.introDataConfig.audioOhno);
            SoundChannel soundDataOpenMount = new(SoundChannel.PLAY_SOUND_NEW_OBJECT, bEIV01DinosaurIntroStateDataObjectDependecy.introDataConfig.audioOpenMount);
            SoundChannel soundDataMaxRun = new(SoundChannel.PLAY_SOUND, bEIV01DinosaurIntroStateDataObjectDependecy.introDataConfig.audioMaxMove);
            SoundChannel soundDataGetEgg = new(SoundChannel.PLAY_SOUND_NEW_OBJECT, bEIV01DinosaurIntroStateDataObjectDependecy.introDataConfig.audioGetEgg);
            SoundChannel soundDataDinosaurMove = new(SoundChannel.PLAY_SOUND_NEW_OBJECT, bEIV01DinosaurIntroStateDataObjectDependecy.introDataConfig.audioDinosaurMove);

            await UniTask.Delay(3000, cancellationToken: token);
            bEIV01DinosaurIntroStateDataObjectDependecy.RectDinosaur.DOAnchorPos(bEIV01DinosaurIntroStateDataObjectDependecy.pointDinosaurAppear.anchoredPosition, 2);
            bEIV01DinosaurIntroStateDataObjectDependecy.RectDinosaur.DOScale(new Vector3(1,1,1), 2);
            bEIV01DinosaurIntroStateDataObjectDependecy.BG1.DOAnchorPosY(bEIV01DinosaurIntroStateDataObjectDependecy.PointBG1.anchoredPosition.y, 2);
            bEIV01DinosaurIntroStateDataObjectDependecy.BG1.DOScale(Vector3.one, 2);
            bEIV01DinosaurIntroStateDataObjectDependecy.itemIntro.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 2);
            bEIV01DinosaurIntroStateDataObjectDependecy.itemIntro.DOAnchorPosY(bEIV01DinosaurIntroStateDataObjectDependecy.pointItemIntroAppear.anchoredPosition.y, 2);

            bEIV01DinosaurIntroStateDataObjectDependecy.canvasIntro.SetActive(false);
            SoundManager.Instance.PlayMusic(bEIV01DinosaurIntroStateDataObjectDependecy.introDataConfig.musicGame, null, bEIV01DinosaurIntroStateDataObjectDependecy.introDataConfig.volumeMusic, true);
            ObserverManager.TriggerEvent<SoundChannel>(soundDataDelicious);
            ObserverManager.TriggerEvent<SoundChannel>(soundDataOhno);
            bEIV01DinosaurIntroStateDataObjectDependecy.robotIntro.AnimationState.SetAnimation(0, ROBOT_INTRO, false);
            bEIV01DinosaurIntroStateDataObjectDependecy.ellieIntro.AnimationState.SetAnimation(0, ELLIE_INTRO, false);
            bEIV01DinosaurIntroStateDataObjectDependecy.maxIntro.AnimationState.SetAnimation(0, MAX_INTRO, false);
            bEIV01DinosaurIntroStateDataObjectDependecy.dinosaur.AnimationState.SetAnimation(0, DINOSAUR_EATEGG, false);

            await UniTask.Delay(1500, cancellationToken: token);
            ObserverManager.TriggerEvent<SoundChannel>(soundDataOpenMount);

            // [BEIV01] threecharacter appear
            bool isMoving = false;
            bEIV01DinosaurIntroStateDataObjectDependecy.threeCharacter.DOAnchorPos(bEIV01DinosaurIntroStateDataObjectDependecy.pointReturnThreeCharacter.anchoredPosition, 0.5f).SetEase(Ease.Linear).OnComplete(() => { isMoving = true; });

            await UniTask.WaitUntil(() => isMoving, cancellationToken: token);
            bEIV01DinosaurIntroStateDataObjectDependecy.threeCharacterPlay.DOAnchorPosX(bEIV01DinosaurIntroStateDataObjectDependecy.pointCharacterPlayTarget.GetComponent<RectTransform>().anchoredPosition.x, 3).SetEase(Ease.Linear);

            await UniTask.Delay(100, cancellationToken: token);
            bEIV01DinosaurIntroStateDataObjectDependecy.maxPlay.AnimationState.SetAnimation(0, MAX_RUN_INTRO, true);
            ObserverManager.TriggerEvent<SoundChannel>(soundDataMaxRun);
            bEIV01DinosaurIntroStateDataObjectDependecy.elliePlay.AnimationState.SetAnimation(0, ELLIE_RUN, true);
            bEIV01DinosaurIntroStateDataObjectDependecy.robotPlay.AnimationState.SetAnimation(0, ROBOT_RUN, true);

            await UniTask.Delay(1200, cancellationToken: token);
            bEIV01DinosaurIntroStateDataObjectDependecy.maxPlay.AnimationState.SetAnimation(0, MAX_TAKE_EGG, false).Complete += (trackEntry) =>
            {
                bEIV01DinosaurIntroStateDataObjectDependecy.maxPlay.AnimationState.SetAnimation(0, MAX_RUN_LOOP, true);
            };

            await UniTask.Delay(200, cancellationToken: token);
            bEIV01DinosaurIntroStateDataObjectDependecy.egg.SetActive(false);
            ObserverManager.TriggerEvent<SoundChannel>(soundDataGetEgg);

            await UniTask.Delay(400, cancellationToken: token);
            bEIV01DinosaurIntroStateDataObjectDependecy.maxPlay.transform.SetParent(bEIV01DinosaurIntroStateDataObjectDependecy.maxPlay.transform.parent.parent);
            ObserverManager.TriggerEvent<SoundChannel>(soundDataDinosaurMove);
            bEIV01DinosaurIntroStateDataObjectDependecy.dinosaur.AnimationState.SetAnimation(0, DINOSAUR_START_RUN, false).Complete += (trackEntry) =>
            {
                ObserverManager.TriggerEvent<SoundChannel>(soundDataDinosaurMove);
                bEIV01DinosaurIntroStateDataObjectDependecy.dinosaur.AnimationState.SetAnimation(0, DINOSAUR_RUN_AWAY, true).Complete += (trackEntry) =>
                {
                    ObserverManager.TriggerEvent<SoundChannel>(soundDataDinosaurMove);
                };
            };
            bEIV01DinosaurIntroStateDataObjectDependecy.loopBackGround.SetSpeed = 800;
            bEIV01DinosaurIntroStateDataObjectDependecy.itemIntro.transform.SetParent(bEIV01DinosaurIntroStateDataObjectDependecy.itemIntro.transform.parent.transform.GetChild(0).transform);

            await UniTask.Delay(1000, cancellationToken: token);
            bEIV01DinosaurIntroStateDataObjectDependecy.RectDinosaur.DOAnchorPosX(bEIV01DinosaurIntroStateDataObjectDependecy.pointReturnLeft.anchoredPosition.x, 2.1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                bEIV01DinosaurIntroStateDataObjectDependecy.maxPlay.AnimationState.SetAnimation(0, MAX_RUN, true).Complete += (trackEntry) =>
                {
                    ObserverManager.TriggerEvent<SoundChannel>(soundDataMaxRun);
                };
                bEIV01DinosaurIntroStateDataObjectDependecy.dinosaur.AnimationState.SetAnimation(0, DINOSAUR_STOP, false);
            });

            // [BEIV01] intro guiding
            bEIV01DinosaurIntroStateDataObjectDependecy.RectMaxPlay.DOAnchorPosX(bEIV01DinosaurIntroStateDataObjectDependecy.pointMaxPlay.anchoredPosition.x, 2.1f).SetEase(Ease.Linear).OnComplete(() =>
            {
                itemGuiding.transform.SetParent(bEIV01DinosaurIntroStateDataObjectDependecy.loopBackGround.gameObject.transform);
                bEIV01DinosaurIntroStateDataObjectDependecy.loopBackGround.ItemAppear(() =>
                {
                    bEIV01DinosaurIntroStateDataObjectDependecy.loopBackGround.SetSpeed = 0;
                    bEIV01DinosaurIntroStateDataObjectDependecy.bEIV01UserInput.IsInput = true;
                    TriggerFinishIntro();
                });
        
            });
        }
        catch { }
    
    }

    private void TriggerFinishIntro()
    {
        BEIV01DinosaurEvent bEIV01DinosaurEvent = new BEIV01DinosaurEvent(BEIV01DinosaurEvent.INTRO_STATE_FINISH, itemGuiding.transform.GetChild(3).gameObject);
        ObserverManager.TriggerEvent<BEIV01DinosaurEvent>(bEIV01DinosaurEvent);
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

public class BEIV01DinosaurIntroStateDataObjectDependecy
{
    public GameObject canvasBG { get; set; }
    public GameObject canvasIntro { get; set; }
    public SkeletonGraphic dinosaur { get; set; }
    public RectTransform RectDinosaur { get; set; }
    public SkeletonGraphic robotIntro { get; set; }
    public SkeletonGraphic ellieIntro { get; set; }
    public SkeletonGraphic maxIntro { get; set; }
    public SkeletonGraphic robotPlay { get; set; }
    public SkeletonGraphic maxPlay { get; set; }
    public RectTransform RectMaxPlay { get; set; }
    public SkeletonGraphic elliePlay { get; set; }
    public RectTransform threeCharacter { get; set; }
    public RectTransform threeCharacterPlay { get; set; }
    public GameObject egg { get; set; }
    public RectTransform itemIntro { get; set; }
    public RectTransform BG1 { get; set; }
    public RectTransform PointBG1 { get; set; }
    public RectTransform pointItemIntroAppear { get; set; }
    public GameObject objListItem { get; set; }
    public LoopBackGround loopBackGround { get; set; }
    public RectTransform pointReturnThreeCharacter { get; set; }
    public RectTransform pointCharacterPlayTarget { get; set; }
    public RectTransform pointDinosaurAppear { get; set; }
    public RectTransform pointReturnLeft { get; set; }
    public RectTransform pointMaxPlay { get; set; }
    public RectTransform pointItemGuiding { get; set; }
    public BEIV01DinosaurIntroConfig introDataConfig { get; set; }
    public BEIV01UserInput bEIV01UserInput { get; set; }
}