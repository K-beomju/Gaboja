using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MergeItem : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    //private Merge merge;

    private Image image;
    private Item item;
    private bool isSelect;
    private GameObject contactItem;

    public Merge merge;


    private void Awake()
    {

        image = GetComponent<Image>();
        merge = FindObjectOfType<Merge>();
    }
    void OnEnable()
    {
        isSelect = false;
    }

    public void InitItem(Item i, Transform swordList)
    {
        item = i;
        transform.SetParent(swordList);
        image.sprite = item.SwordIMG;

        // 함수를 실행시킬때마다 Getcompnent를 사용하면 성능에 저하가 발생되기에
        // 미리 변수로 받아놔서 사용하는 게 최적화에 좋음
        // item = i;
        // transform.SetParent(swordList.transform);
        // sr = GetComponent<Image>();
        // sr.sprite = item.SwordIMG;
    }

    public void OnDrag(PointerEventData eventData)
    {
        isSelect = true;
        transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData) {  }

    public void OnPointerUp(PointerEventData eventData)
    {
        isSelect = false;
        if (contactItem != null && contactItem.gameObject.activeSelf)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
            contactItem.SetActive(false);
            contactItem.transform.SetParent(GameManager.instance.transform);
            gameObject.transform.SetParent(GameManager.instance.transform);
            //Destroy(contactItem);
            merge.MergeItem(item.SwordID + 1);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isSelect && item.SwordID == collision.GetComponent<MergeItem>().item.SwordID)
        {
            if (contactItem != null)
            {
                contactItem = null;
            }
            contactItem = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isSelect && item.SwordID == collision.GetComponent<MergeItem>().item.SwordID && contactItem != null)
        {
            contactItem = null;
        }
    }

}