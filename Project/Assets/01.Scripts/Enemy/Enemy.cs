using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    int enemyHp = 20;
    SpriteRenderer renderer;
    Animator enemyAni;

    [SerializeField]
    float enemySpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        enemyAni = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //레이캐스트 거리 그리기
        Debug.DrawRay(transform.position, Vector2.left * 1f, Color.green);

        //기본 에네미 이동
        transform.Translate(Vector3.left * enemySpeed * Time.deltaTime);
        int hit = 1 << LayerMask.NameToLayer("Player");
        RaycastHit2D playerHit = Physics2D.Raycast(transform.position, Vector2.left, 1f, hit);
        if(playerHit)
        {
            enemyAni.SetBool("isATK",true);
            enemySpeed = 0;
        }

        if(enemyHp <= 0)
        {
            //풀링으로 대체
            Destroy(this.gameObject);

        }


    }

    public IEnumerator DamagedPlayer()
    {
        Player.instance.playerRender.color = new Color(255 / 255f, 138 / 255f, 138 / 255f);
        Player.instance.playerHp -= 5;
        Player.instance.SetHp(Player.instance.playerHp , Player.instance.maxHp);
        Debug.Log(Player.instance.playerHp);
        yield return new WaitForSeconds(0.1f);
        Player.instance.playerRender.color = new Color(255,255,255);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "HitBox")
        {
            //플레이어 공격력 만큼 깍는다.
            enemyHp -= 10;
            renderer.color = new Color(255 / 255f, 138 / 255f, 138 / 255f);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "HitBox")
        renderer.color = new Color(255,255,255);
    }

    public void OnDamage(int damage)
    {
        enemyHp -= damage;
            StartCoroutine(Co());
    }

    IEnumerator Co()
    {
         renderer.color = new Color(255 / 255f, 138 / 255f, 138 / 255f);
         yield return new WaitForSeconds(0.3f);
                 renderer.color = new Color(255,255,255);


    }
}
