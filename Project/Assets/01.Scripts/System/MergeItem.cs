using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MergeItem : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public Merge merge;
    public Image sr;
    public Item item;
    public bool isSelect = false;
    public GameObject contactItem;

  

    public void InitItem(Item i, GameObject swordList)
    {
        item = i;
        transform.SetParent(swordList.transform);
        sr = GetComponent<Image>();
        sr.sprite = item.SwordIMG;
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
        if (contactItem != null)
        {
            Destroy(gameObject);
            Destroy(contactItem);
            //Merge를 인스턴스화 시켜서 아이템크리에이트 호출 (나중에 수정)
            GameObject.Find("ItemData").GetComponent<Merge>().MergeItem(item.SwordID + 1);

       

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