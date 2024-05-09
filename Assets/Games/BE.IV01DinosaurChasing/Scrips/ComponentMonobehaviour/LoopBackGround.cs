using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoopBackGround : MonoBehaviour
{
    [SerializeField] protected RectTransform bgContainer;
    [SerializeField] protected RectTransform bg1;
    [SerializeField] protected RectTransform bg2;
    [SerializeField] protected Vector2 startposition;
    [SerializeField] protected RectTransform canvas;

    float m_xsize;
    protected bool isStart = true;
    private float speed;
    private float distanceItemGuiding;
    public float SetSpeed { get => speed; set => speed = value; }

    protected virtual void Awake()
    {
        m_xsize = 3109;

        // Đặt vị trí của bg1 và bg2 trong bgContainer
        //bg1.anchoredPosition = new Vector2(0, -50);
        //bg2.anchoredPosition = new Vector2(m_xsize, -50);
        startposition = bgContainer.anchoredPosition;
        LayoutRebuilder.ForceRebuildLayoutImmediate(canvas);
        float ratio = canvas.sizeDelta.x / canvas.sizeDelta.y;
        if (ratio <= 1.6f)
        {
            distanceItemGuiding = 1500;
        }
        else
        {
            distanceItemGuiding = 1850;
        }
    }

    protected void Update()
    {
        if (!isStart) return;
        bgContainer.anchoredPosition += speed * Time.deltaTime * Vector2.left;

        if (bgContainer.anchoredPosition.x <= startposition.x - m_xsize)
        {
            startposition.x -= m_xsize;
            bg1.anchoredPosition = new Vector2(
            bg2.anchoredPosition.x + m_xsize,
            bg2.anchoredPosition.y);
            RectTransform temp = bg1;
            bg1 = bg2;
            bg2 = temp;

        }
    }
    public void ItemAppear(Action callback)
    {
        StartCoroutine(Appear(callback));
    }
    private IEnumerator Appear(Action callback)
    {
        Vector2 startposition = bgContainer.anchoredPosition;
        float timeDuration = 0;
        while (timeDuration < 4)
        {
            timeDuration += Time.deltaTime;
            if (bgContainer.anchoredPosition.x <= startposition.x - distanceItemGuiding)
            {
                callback.Invoke();
                yield break;
            }
            yield return null;
        }
        yield return null;
    }
}
