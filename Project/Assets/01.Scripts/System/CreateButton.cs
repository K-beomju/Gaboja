using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreateButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Merge merge;
   
    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(TestCo());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopAllCoroutines();
    }

    private IEnumerator TestCo()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            merge.ItemCreate(0);
        }
    }
}
