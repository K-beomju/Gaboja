using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private SpriteRenderer playerRender;
    private Animator playerAni;

   // public int playerHp;
  //  public int maxHp = 200;
   // public float remainHp;

  //  public Slider slider;
  //  public Text hpTxt;

    public int attackRange;
    private EffectObject effectObject;


    private void Awake()
    {

        playerAni = GetComponent<Animator>();
        playerRender = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
  //      playerHp = maxHp;
 //       remainHp = (playerHp / maxHp * 100);
//        hpTxt.text = string.Format("{0} ({1}%)",playerHp,remainHp);
    }

    public void SetHp(float current, float max)
    {
   //     slider.value = current / max;
 //       remainHp = (current / max * 100);
  //      hpTxt.text = string.Format("{0} ({1}%)",current, remainHp );
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





}
