using Base.Observer;
using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading;
using Cysharp.Threading.Tasks;

public class BEIV01DinosaurEndGameState : FSMState
{
    private const string DINOSAUR_STOP = "Khung long  stop";
    private const string DINOSAUR_CHASING = "Khung long chasing";
    private const string DINOSAUR_RUN_AWAY = "Khung long run away";
    private const string DINOSAURMOM_APPEAR = "ALL";
    private const string MAX_INTRO = "Intro";

    private CancellationTokenSource cancellationTokenSource;
    private BEIV01DinosaurEndGameStateDataObjectDependecy bEIV01DinosaurEndGameStateDataObjectDependecy;


    public override void OnEnter(object data)
    {
        base.OnEnter(data);
        cancellationTokenSource = new();
        CancellationToken token = cancellationTokenSource.Token;

        Dowork(token);
    }

    public override void SetUp(object data)
    {
        bEIV01DinosaurEndGameStateDataObjectDependecy = (BEIV01DinosaurEndGameStateDataObjectDependecy)data;
    }
    private async void Dowork(CancellationToken token)
    {
        try
        {
            bEIV01DinosaurEndGameStateDataObjectDependecy.bEIV01UserInput.IsInput = false; // [BEIV01] lock jump

            bEIV01DinosaurEndGameStateDataObjectDependecy.dinosaur.AnimationState.SetAnimation(0, DINOSAUR_CHASING, true);
            bEIV01DinosaurEndGameStateDataObjectDependecy.rectDinosaur.DOAnchorPosX(bEIV01DinosaurEndGameStateDataObjectDependecy.pointDinosaurAttack.anchoredPosition.x, 1).SetEase(Ease.Linear);

            SoundChannel soundDataDinosaurMove = new(SoundChannel.PLAY_SOUND_NEW_OBJECT, bEIV01DinosaurEndGameStateDataObjectDependecy.endGameDataConfig.audioLegDinosaurMom);
            ObserverManager.TriggerEvent<SoundChannel>(soundDataDinosaurMove);

            SoundChannel soundDataDinosaurAttack = new(SoundChannel.PLAY_SOUND_NEW_OBJECT, bEIV01DinosaurEndGameStateDataObjectDependecy.endGameDataConfig.audioAttack);
            ObserverManager.TriggerEvent<SoundChannel>(soundDataDinosaurAttack);

            await UniTask.Delay(3000, cancellationToken: token);
            SoundManager.Instance.StopMusic();
            SoundChannel sounDataLegDinosaurMom = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, bEIV01DinosaurEndGameStateDataObjectDependecy.endGameDataConfig.audioLegDinosaurMom);
            ObserverManager.TriggerEvent<SoundChannel>(sounDataLegDinosaurMom);
            bEIV01DinosaurEndGameStateDataObjectDependecy.dinosaurMom.AnimationState.SetAnimation(0, DINOSAURMOM_APPEAR, false);
            bool isMoving = false;
            bool isPlayAnimation = false;
            bEIV01DinosaurEndGameStateDataObjectDependecy.rectDinosaurMom.DOAnchorPosX(bEIV01DinosaurEndGameStateDataObjectDependecy.pointDinosaurMomAppear.anchoredPosition.x, 2.75f).SetEase(Ease.Linear).OnComplete(() => { isMoving = true; });

            await UniTask.WaitUntil(() => isMoving);
            bEIV01DinosaurEndGameStateDataObjectDependecy.loopBackGround.SetSpeed = 0;
            bEIV01DinosaurEndGameStateDataObjectDependecy.rectMaxPlay.DOAnchorPosX(bEIV01DinosaurEndGameStateDataObjectDependecy.pointCharacterPlayTarget.anchoredPosition.x, 2).SetEase(Ease.Linear).OnComplete(() =>
            {
                SoundChannel stopSounData = new SoundChannel(SoundChannel.STOP_SOUND, null);
                ObserverManager.TriggerEvent<SoundChannel>(stopSounData);
                bEIV01DinosaurEndGameStateDataObjectDependecy.maxPlay.AnimationState.SetAnimation(0, MAX_INTRO, false);
            });
            SoundChannel pauseSoundData = new(SoundChannel.PAUSE_SOUND, null);
            ObserverManager.TriggerEvent<SoundChannel>(pauseSoundData);

            // [Note] Sfx hốt hoảng
            bEIV01DinosaurEndGameStateDataObjectDependecy.dinosaur.AnimationState.SetAnimation(0, DINOSAUR_STOP, false).Complete += (trackEntry) => { isPlayAnimation = true; };
            await UniTask.WaitUntil(() => isPlayAnimation);

            SoundChannel sounDataPanic = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, bEIV01DinosaurEndGameStateDataObjectDependecy.endGameDataConfig.audioPanic);
            ObserverManager.TriggerEvent<SoundChannel>(sounDataPanic); // [BEIV01] Audio Panic

            bEIV01DinosaurEndGameStateDataObjectDependecy.dinosaur.AnimationState.SetAnimation(0, DINOSAUR_RUN_AWAY, true).Complete += (trackEntry) =>
            {
                SoundChannel soundDataDinosaurMove = new(SoundChannel.PLAY_SOUND_NEW_OBJECT, bEIV01DinosaurEndGameStateDataObjectDependecy.endGameDataConfig.audioLegDinosaurMom);
                ObserverManager.TriggerEvent<SoundChannel>(soundDataDinosaurMove);
            };
            bool isRoate = false;
            bEIV01DinosaurEndGameStateDataObjectDependecy.rectDinosaur.DOLocalRotate(new Vector3(0, 180, 0), 0.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).OnComplete(() => { isRoate = true; });
            await UniTask.WaitUntil(() => isRoate);

            isMoving = false;
            bEIV01DinosaurEndGameStateDataObjectDependecy.rectDinosaur.DOAnchorPosX(bEIV01DinosaurEndGameStateDataObjectDependecy.pointReturnLeft.anchoredPosition.x, 2f).SetEase(Ease.Linear).OnComplete(() => { isMoving = true; });
            await UniTask.WaitUntil(() => isMoving);

            bEIV01DinosaurEndGameStateDataObjectDependecy.dinosaur.AnimationState.SetAnimation(0, DINOSAUR_STOP, false);
            ObserverManager.TriggerEvent<SoundChannel>(pauseSoundData);

            // active scene Egg
            bEIV01DinosaurEndGameStateDataObjectDependecy.eggEndGame.SetActive(true);
            SoundChannel sounDataEggBreak = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, bEIV01DinosaurEndGameStateDataObjectDependecy.endGameDataConfig.audioBreakEgg);
            ObserverManager.TriggerEvent<SoundChannel>(sounDataEggBreak);

            await UniTask.Delay(1600, cancellationToken: token);
            SoundChannel sounDataBlink = new SoundChannel(SoundChannel.PLAY_SOUND_NEW_OBJECT, bEIV01DinosaurEndGameStateDataObjectDependecy.endGameDataConfig.audioDinosaurChildBlink);
            ObserverManager.TriggerEvent<SoundChannel>(sounDataBlink);
        }
        catch { }
    }

    private void TriggerFinishEndGame()
    {
        //BEIV01DinosaurEvent bEIV01DinosaurEvent = new BEIV01DinosaurEvent(BEIV01DinosaurEvent.INIT_STATE_FINISH, null);
        //ObserverManager.TriggerEvent<BEIV01DinosaurEvent>(bEIV01DinosaurEvent);
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

public class BEIV01DinosaurEndGameStateData
{
    public BEIV01PlayData bEIV01PlayData { get; set; }
}

public class BEIV01DinosaurEndGameStateDataObjectDependecy
{ 
    public SkeletonGraphic dinosaur { get; set; }
    public RectTransform rectDinosaur { get; set; }
    public SkeletonGraphic dinosaurMom { get; set; }
    public RectTransform rectDinosaurMom { get; set; }
    public SkeletonGraphic maxPlay { get; set; }
    public RectTransform rectMaxPlay { get; set; }
    public GameObject eggEndGame { get; set; }
    public RectTransform pointDinosaurMomAppear { get; set; }
    public RectTransform pointCharacterPlayTarget { get; set; }
    public RectTransform pointDinosaurAttack { get; set; }
    public RectTransform pointReturnLeft { get; set; }
    public BEIV01UserInput bEIV01UserInput { get; set; }
    public LoopBackGround loopBackGround { get; set; }
    public BEIV01Trigger bEIV01Trigger { get; set; }
    public BEIV01DinosaurEndGameConfig endGameDataConfig { get; set; }
}