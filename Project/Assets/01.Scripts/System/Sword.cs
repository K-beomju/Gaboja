using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class Sword : MonoBehaviour,IBeginDragHandler ,IDragHandler , IEndDragHandler , IDropHandler
{
    public List<SwordInfo> swordInfo = new List<SwordInfo>();
    public GameObject currentSword;

    GameObject nextSword;

    void Start()
    {
        
    }

    void Update()
    {
        
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("h");
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {

    }

    public void MergeSword()
    {
            
    }


   
    
}
