
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MergeItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Image image;
    private MergeItem contactItem; // contactItem을 GameObject로 두고 MergeItem getcomponent 유발 -> 그냥 MergeItem으로 놓고 쓰면 됌
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
        Debug.Log(item.swordID);
        image.sprite = item.itemImg;

        image.transform.SetParent(Merge.Instance.parentObj.transform);
    }

    private void OnMouseDown()
    {
        isSelect = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) // 기존 코드랑 비교해보기, Merge Dead() 함수 참고
    {
        if (!isSelect) return;
        MergeItem contact = collision.GetComponent<MergeItem>();

        if (isSelect && this.item.itemType == contact.item.itemType)
        {
            readyAuto = true;
            if (contactItem != null)
            {
                contactItem = null;
            }

            contactItem = contact;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        MergeItem contact = collision.GetComponent<MergeItem>();

        if (isSelect && this.item.itemType == contact.item.itemType && contactItem != null)
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
            Debug.Log("merge");
            Merge.Instance.Dead(contactItem);
            Merge.Instance.Dead(this);
            Merge.Instance.mergingItem(item.itemType + 1);

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        isSelect = true;
        Vector3 mousePos = Input.mousePosition;
        transform.position = mousePos;
    }


}