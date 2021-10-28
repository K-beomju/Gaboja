using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Slider slider;

    public static Player instance;
    public int playerHp;
    public int maxHp = 200;
    public GameObject player;
    [SerializeField]
    Animator playerAni;
    public SpriteRenderer playerRender;
    public Text hpTxt;
    public float remainHp;

    public int attackRange;
    private EffectObject effectObject;

    private void Awake() {
        instance = this;
    }
    void Start()
    {
        playerHp = maxHp;
        remainHp = (playerHp / maxHp * 100);
        hpTxt.text = string.Format("{0} ({1}%)",playerHp,remainHp);
        playerRender = GetComponent<SpriteRenderer>();
        player = this.gameObject;
        playerAni = GetComponent<Animator>();
    }

    public void SetHp(float current, float max)
    {
        slider.value = current / max;
        remainHp = (current / max * 100);
        hpTxt.text = string.Format("{0} ({1}%)",current, remainHp );
    }

    // Update is called once per frame
    void Update()
    {
        int enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
        Debug.DrawRay(transform.position, Vector2.right * 1.2f, Color.red);
        RaycastHit2D enemyHit = Physics2D.Raycast(transform.position, Vector2.right, 1.2f, enemyLayer);
        if(enemyHit)
        {

            playerAni.SetBool("isAtk", true);
            playerAni.SetBool("isRun", false);
        }
        if(!enemyHit)
        {


            playerAni.SetBool("isRun",true);
            playerAni.SetBool("isAtk", false);
        }
    }


    public void Attack()
    {
        Collider2D objects = Physics2D.OverlapCircle(transform.position, attackRange, 1 << LayerMask.NameToLayer("Enemy"));
        var target = objects.GetComponent<Enemy>();
            if (target != null)
            {
        target.GetComponent<Rigidbody2D>().AddForce(transform.right * 0.5f,ForceMode2D.Impulse);

                Debug.Log(target.name);
                target.OnDamage(10);
                effectObject = GameManager.GetCreateScreenEffect(0);
                effectObject.SetPositionData(target.transform.position + new Vector3(0,0.3f,0), Quaternion.identity);
            }


    }

       void OnDrawGizmos()
    {


        Gizmos.DrawWireSphere(transform.position,attackRange);
    }


}
