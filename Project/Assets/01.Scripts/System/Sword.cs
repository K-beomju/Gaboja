using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    //Į�� ����
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
        if(collision.gameObject.tag == "Sword" && this.SWORD_ID == collision.GetComponent<Sword>().SWORD_ID) //�浹�� ��ü�� Į�̰� ���̵� �Ȱ��� �� �ռ�
        {
            Instantiate(nextSword, transform.position, Quaternion.identity);
        }

    }
}
