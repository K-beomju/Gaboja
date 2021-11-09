using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    private EffectObject effectObject;
    private DamageText damageText;


    protected override void OnEnable()
    {
        base.OnEnable();
    }

    private void Start()
    {

    }

    public override void OnDamage(float damage)
    {
        if (dead) return;
        base.OnDamage(damage);
       // StartCoroutine(ShowBloodEffect(point, normal));
    }

    public override void Die()
    {
        base.Die();
    }

     public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            float damge = JsonSave.instance.GetDataClass().enemyData.enemyDamage;
            effectObject = GameManager.GetCreateScreenEffect(0);
            effectObject.SetPositionData(transform.position, Quaternion.identity);

            damageText = GameManager.GetDamageText();
            damageText.SetText(damge);
            damageText.SetPositionData(transform.position, Quaternion.identity);

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 2.5f, ForceMode2D.Impulse);
            OnDamage(damge);
            Debug.Log(health);
        }

    }
}
