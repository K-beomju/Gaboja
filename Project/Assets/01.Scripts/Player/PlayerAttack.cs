using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerAnimation playerAnim;
    public int attackRange;
    public Merge merge;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimation>();
    }


    void Update()
    {
        int enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
        Debug.DrawRay(transform.position, Vector2.right * attackRange, Color.red);
        RaycastHit2D enemyHit = Physics2D.Raycast(transform.position, Vector2.right, attackRange, enemyLayer);
        playerAnim.Ray(enemyHit);
    }


    public void SwordAttack()
    {
        merge.SwordInit();
    }


    public void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector2.right * attackRange);
    }





}
