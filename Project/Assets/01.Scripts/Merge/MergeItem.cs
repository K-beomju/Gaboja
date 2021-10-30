
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class MergeItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Image image;
    private GameObject contactItem; // contactItem을 GameObject로 두고 MergeItem getcomponent 유발 -> 그냥 MergeItem으로 놓고 쓰면 됌
    private bool isSelect;
    private bool readyAuto;

    public Item item;


    void Awake()
    {
        image = GetComponent<Image>();
        isSelect = false;
        readyAuto = false;

    }

    public void InitItem(Item i)
    {
        Debug.Log(i.itemType);

        item.itemType = i.itemType;
        item.itemImg = i.itemImg;
        item.swordID = Merge.Instance.ID++; //고유 아이디 부여


        image.sprite = item.itemImg;
        image.transform.SetParent(Merge.Instance.parentObj.transform);



    }



    private void OnMouseDown()
    {
        isSelect = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) // 기존 코드랑 비교해보기, Merge Dead() 함수 참고
    {


        if (isSelect && this.item.itemType == collision.GetComponent<MergeItem>().item.itemType)
        {
            readyAuto = true;
            if (contactItem != null)
            {
                contactItem = null;
            }

            contactItem = collision.gameObject;
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {

        if (isSelect && this.item.itemType == collision.GetComponent<MergeItem>().item.itemType && contactItem != null)
        {
         contactItem = null;
        }
    }

    public void OnPointerDown(PointerEventData eventData) { }
    public void OnPointerUp(PointerEventData eventData)  // 기존 코드랑 비교해보기 , Merge Dead() 함수 참고
    {
        isSelect = false;
        if (contactItem != null)
        {
            MergeItem d = contactItem.GetComponent<MergeItem>();
            Debug.Log("merge");
            Merge.Instance.Dead(d);
            Merge.Instance.Dead(this);
            Merge.Instance.mergingItem(item.itemType + 1);
            Merge.Instance.CheckNewSword(item.itemType);


        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        isSelect = true;
        Vector3 mousePos = Input.mousePosition;
        transform.position = mousePos;
    }





}