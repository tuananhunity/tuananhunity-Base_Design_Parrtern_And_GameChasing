using Base.Observer;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BEIV01UserInput : MonoBehaviour
{
    [SerializeField] private SkeletonGraphic skeletonMax;
    [SerializeField] private AudioClip audioMaxMove;
    [SerializeField] private AudioClip audioJump;
    [SerializeField] private LoopBackGround loopBackGround; 
    [SerializeField] private GameObject lstItem;
    [SerializeField] private RectTransform pointMaxPlay;
    RectTransform rectMax;

    private const string MAX_JUMP = "Max jump opt2";
    private const string MAX_RUN = "Max run";

    public bool IsEndGuiding = true;
    private bool isInput = true;
    public bool IsInput { get ; set; }
    private void Awake()
    {
        rectMax = skeletonMax.GetComponent<RectTransform>();
    }
    void Update()
    {
        UserInput();
    }
    private void UserInput()
    {
        if (!IsInput) return;
        if (!isInput) return;
        StartCoroutine(MaxJump());
    }
    private IEnumerator MaxJump()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isInput = false;
            if (IsEndGuiding)
            {
                IsEndGuiding = false;
                loopBackGround.SetSpeed = 800;
                //lstItem.transform.GetChild(0).transform.SetParent(loopBackGround.gameObject.transform);
                SoundChannel pauseSoundData = new(SoundChannel.PAUSE_SOUND, null);
                ObserverManager.TriggerEvent<SoundChannel>(pauseSoundData);
            }
            SoundChannel soundJumpData = new(SoundChannel.PLAY_SOUND_NEW_OBJECT, audioJump);
            ObserverManager.TriggerEvent<SoundChannel>(soundJumpData);

            rectMax.DOAnchorPosY(rectMax.anchoredPosition.y + 120, 0.5f).OnComplete(() =>
            {
                rectMax.DOAnchorPosY(rectMax.anchoredPosition.y - 120, 0.5f);
            });
            skeletonMax.AnimationState.SetAnimation(0, MAX_JUMP, false).Complete += (trackEntry) =>
            {
                if (skeletonMax.AnimationState.GetCurrent(0).Animation.Name.Equals(MAX_JUMP))
                {
                    SoundChannel soundDataMaxRun = new(SoundChannel.PLAY_SOUND, audioMaxMove, null, 1, true);
                    ObserverManager.TriggerEvent<SoundChannel>(soundDataMaxRun);
                    skeletonMax.AnimationState.SetAnimation(0, MAX_RUN, true);
                }
            };
            yield return new WaitForSeconds(1);
            isInput = true;
        }
    }
}
