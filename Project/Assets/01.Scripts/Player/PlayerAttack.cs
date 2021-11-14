using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerAnimation playerAnim;
    private int enemyLayer;

    public int attackRange;
    public Merge merge;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimation>();
    }

    void Start()
    {
        enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
    }


    void Update()
    {
        Debug.DrawRay(transform.position, Vector2.right * attackRange, Color.red);
        RaycastHit2D enemyHit = Physics2D.Raycast(transform.position, Vector2.right, attackRange, enemyLayer);
        playerAnim.Ray(enemyHit);
    }

    public void Attack()
    {
        Collider2D hitEnemis = Physics2D.OverlapCircle(transform.position,attackRange, enemyLayer);
        IDamageble target = hitEnemis.GetComponent<IDamageble>();

        int swordDmg = merge.SwordInit();

        merge.SwordEffect(hitEnemis.transform);
        target.OnDamage(swordDmg +  JsonSave.instance.GetUpgradeClass().
        upAbility[0] - JsonSave.instance.GetUpgradeClass().upCost[0]);


        SoundManager.Instance.PlayFXSound("SwordAttack");


    }


    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,attackRange);
        Gizmos.DrawRay(transform.position, Vector2.right * attackRange);
    }





}
