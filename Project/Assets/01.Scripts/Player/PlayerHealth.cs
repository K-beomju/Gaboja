using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    private EffectObject effectObject;
    private DamageText damageText;

    public Slider hpSlider;
    public Text hpText;



    protected override void OnEnable()
    {
        base.OnEnable();
    }

    void Start()
    {
        initHealth = JsonSave.instance.GetPlayerClass().initHealth;
        health = initHealth;
        SetFill();
    }

    public  void SetFill()
    {
        hpSlider.value = health / (initHealth + JsonSave.instance.GetUpgradeClass().upAbility[1] - JsonSave.instance.GetUpgradeClass().upCost[1]);
        hpText.text = string.Format("{0} [{1}]%",health , Mathf.Round((health / (initHealth + JsonSave.instance.GetUpgradeClass().upAbility[1] - JsonSave.instance.GetUpgradeClass().upCost[1]) * 100)).ToString());
    }

    public override void OnDamage(float damage)
    {
        if (dead) return;
        base.OnDamage(damage);
        SetFill();
    }

    public override void Die()
    {
        base.Die();
    }

     public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            float damge = JsonSave.instance.GetEnemyClass().enemyDamage;
            effectObject = GameManager.GetCreateScreenEffect(1);
            effectObject.SetPositionData(transform.position, Quaternion.identity);

            damageText = GameManager.GetDamageText();
            damageText.SetText(damge);
            damageText.SetPositionData(transform.position, Quaternion.identity);

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 2.5f, ForceMode2D.Impulse);
            OnDamage(damge);

        }

    }
}
