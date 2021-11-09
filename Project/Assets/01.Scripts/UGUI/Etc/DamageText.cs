using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DamageText : MonoBehaviour
{
    private Text text;
    private RectTransform rect;

    void Awake()
    {
        text = GetComponent<Text>();
        rect = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        rect.DOMoveY(1f, 1f).SetEase(Ease.InOutSine).SetRelative().OnComplete(() => gameObject.SetActive(false));
        rect.DOScale(new Vector2(1.2f,1.2f), 0.5f).OnComplete(() => rect.DOScale(new Vector2(1,1), 0.1f)).SetDelay(0.2f);
    }

      public void SetPositionData(Vector3 position, Quaternion rot)
    {
        transform.position = position;
        transform.rotation = rot;
    }

    public void SetText(float damage)
    {
        text.text = damage.ToString();
    }





}
