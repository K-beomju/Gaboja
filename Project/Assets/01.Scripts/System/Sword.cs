using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    //칼의 정보
    public int SWORD_ID;
    public float SWORD_ATK;
    public string SWORD_NAME;

    GameObject nextSword;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //nextSword = SwordManager.instance.SwordObject[]
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Sword" && this.SWORD_ID == collision.GetComponent<Sword>().SWORD_ID) //충돌한 물체가 칼이고 아이디가 똑같을 시 합성
        {
            Instantiate(nextSword, transform.position, Quaternion.identity);
        }

    }
}
