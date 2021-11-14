using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DropGold : MonoBehaviour
{
    private Rigidbody2D rigid;
    private float power = 1;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        int rand  = Random.Range(-1,2);
        rigid.AddForce(new Vector2(1,2) * rand, ForceMode2D.Impulse);
        StartCoroutine(MoveGold());
    }

    void OnDisable()
    {
        DOTween.Kill(this);
    }


      public void SetPositionData(Vector3 position, Quaternion rot)
    {
        transform.position = position;
        transform.rotation = rot;
    }

    private IEnumerator MoveGold()
    {
        yield return Yields.WaitSeconds(0.4f);
        transform.DOMove(new Vector3(-1,4.7f,0), 1).OnComplete(() =>
         {
            gameObject.SetActive(false);
            JsonSave.instance.GetDataClass().gold += 6;
            UiManager.Instance.SetGold();

            });
        // TODo 골드 얻는 함수 호출

    }

}
