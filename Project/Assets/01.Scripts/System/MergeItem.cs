using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeItem : MonoBehaviour
{
    SpriteRenderer sr;
    Item item;
    bool isSelect = false;
    GameObject contactItem;

    public void InitItem(Item i)
    {
        item = i;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = item.itemImg;
    }

    private void OnMouseDown()
    {
        isSelect = true;
    }

    private void OnMouseDrag()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    private void OnMouseUp()
    {
        isSelect = false;
        if (contactItem != null)
        {
            Destroy(contactItem);
            Destroy(gameObject);
            GameObject.Find("ItemData").GetComponent<Merge>().ItemCreate(item.SwordID + 1);
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
            contactItem.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
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
