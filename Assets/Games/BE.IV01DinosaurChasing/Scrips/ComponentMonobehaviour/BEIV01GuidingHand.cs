using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEIV01GuidingHand : MonoBehaviour
{
    private BEIV01Image bEIV01Image;
    private Animator animator;
    void Awake()
    {
        bEIV01Image = GetComponent<BEIV01Image>();
        animator = GetComponent<Animator>();
    }

    public void EnableAnimator(bool isEnable)
    {
        animator.enabled = isEnable;
    }
    public void FadeIn(float timeFade)
    {
        bEIV01Image.FadeImage(1, timeFade);
    }
    public void FadeOut(float timeFade)
    {
        bEIV01Image.FadeImage(0, timeFade);
    }
}
