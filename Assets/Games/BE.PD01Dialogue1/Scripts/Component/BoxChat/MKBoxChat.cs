
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MKBoxChat : BaseBoxChat
{
    private LayoutGroup layoutGroup;
    private ContentSizeFitter contentSizeFitter;
    private Vector3 orginalScale;
    protected override void Awake()
    {
        base.Awake();
        orginalScale = transform.localScale;
        layoutGroup = GetComponent<LayoutGroup>();
        contentSizeFitter = GetComponent<ContentSizeFitter>();
    }
    public override void SetData(string content, bool isActive)
    {
        ActiveLayout(true);
        text.SetText(content);
        transform.localScale = Vector3.zero;
        gameObject.SetActive(isActive);
    }

    public override void DoAppear(float duration)
    {
        gameObject.SetActive(true);
        transform.DOScale(orginalScale, duration).SetEase(Ease.OutBack);
    }

    private void ActiveLayout(bool isActive)
    {
        if (layoutGroup != null)
            layoutGroup.enabled = isActive;
        if (contentSizeFitter != null)
            contentSizeFitter.enabled = isActive;
    }
    void OnEnable()
    {
        StartCoroutine(IEOnEnable());
    }
    IEnumerator IEOnEnable()
    {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(this.transform as RectTransform);
        yield return new WaitForEndOfFrame();
       // ActiveLayout(false);
    }

    private void OnDestroy()
    {
        DOTween.Kill(gameObject);
    }

    public override void SetData(string content, float maxWidth, bool isActive)
    {
        if (text == null)
            text = GetComponentInChildren<BaseText>();
        ActiveLayout(true);
        text.SetText(content, maxWidth);
        transform.localScale = Vector3.zero;
        if (!isActive)
            StartCoroutine(IESetDisable());
    }

    IEnumerator IESetDisable()
    {
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
    }
}
