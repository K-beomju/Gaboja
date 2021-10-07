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
        Debug.Log("������");
        isSelect = true;
        transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("����");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("�ռ�");

        isSelect = false;
        if (contactItem != null)
        {
            Destroy(contactItem);
            Destroy(gameObject);
            //Merge�� �ν��Ͻ�ȭ ���Ѽ� ������ũ������Ʈ ȣ�� (���߿� ����)
            GameObject.Find("ItemData").GetComponent<Merge>().ItemCreate(item.SwordID + 1);
        }
    }

   


    //�� �����δ� �����ս��� �ʹ� ���� ���� ���� 
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
