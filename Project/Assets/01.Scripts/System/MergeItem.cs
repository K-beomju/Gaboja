using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MergeItem : MonoBehaviour, IDragHandler , IPointerUpHandler ,IPointerDownHandler
{
    Merge merge;
    Image sr;
    Item item;
    bool isSelect = false;
    GameObject contactItem;

    public void InitItem(Item i)
    {
        item = i;
        sr = GetComponent<Image>();
        sr.sprite = item.SwordIMG;
    }

    

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("딸려옴");
        isSelect = true;
        transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("누름");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("합성");

        isSelect = false;
        if (contactItem != null)
        {
            Destroy(contactItem);
            Destroy(gameObject);
            //Merge를 인스턴스화 시켜서 아이템크리에이트 호출 (나중에 수정)
            GameObject.Find("ItemData").GetComponent<Merge>().ItemCreate(item.SwordID + 1);
        }
    }

   


    //이 밑으로는 퍼포먼스가 너무 낮음 추후 수정 
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
