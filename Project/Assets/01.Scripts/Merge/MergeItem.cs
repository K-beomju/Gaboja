
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MergeItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    private Image image;
    public Item item;
    bool isSelect = false;
    public bool readyAuto = false;
    private GameObject contactItem;


    void Awake()
    {
        image = GetComponent<Image>();

    }




    public void InitItem(Item i)
    {


        Debug.Log(i.itemType);
        item.itemType = i.itemType;
        item.itemImg = i.itemImg;

        item.swordID = Merge.Instance.ID++; //고유 아이디 부여

        image.transform.SetParent(Merge.Instance.parentObj.transform);
        image.sprite = item.itemImg;
    }


    private void OnMouseDown()
    {
        isSelect = true;
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isSelect && item.itemType == collision.GetComponent<MergeItem>().item.itemType)
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
        if (isSelect && item.itemType == collision.GetComponent<MergeItem>().item.itemType && contactItem != null)
        {
            contactItem = null;
        }
    }

    public void OnPointerDown(PointerEventData eventData) { }
    public void OnPointerUp(PointerEventData eventData)
    {
        isSelect = false;
        if (contactItem != null)
        {

            Debug.Log("merge");
            Destroy(contactItem);
            Destroy(gameObject);
            MergeItem contact = contactItem.GetComponent<MergeItem>();
            MergeItem gObj = gameObject.GetComponent<MergeItem>();
            Merge.Instance.swordList.Remove(contact);
            Merge.Instance.swordList.Remove(gObj);
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