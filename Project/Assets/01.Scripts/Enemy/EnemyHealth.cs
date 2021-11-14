using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : LivingEntity
{
    public string name;
    public Sprite sprite;

    private DropGold dropGold;
    private DamageText damageText;

    void Awake()
    {

    }

     protected override void OnEnable()
    {
        base.OnEnable();
    }

    void Start()
    {
        initHealth = JsonSave.instance.GetEnemyClass().enemyHp;

    }



    public override void OnDamage(float damage)
    {
        if (dead) return;
        UiManager.Instance.SetFill(health, initHealth, name, sprite);
        base.OnDamage(damage);

        damageText = GameManager.GetDamageText();
        damageText.SetText(damage);
        damageText.SetPositionData(transform.position , Quaternion.identity);
        Debug.Log("맞는중");
    }

    public override void Die()
    {
        base.Die();
        UiManager.Instance.SetFill(0, 1, name,  sprite);
        for (int i = 0; i < 3; i++)
        {
        dropGold = GameManager.GetDropGold();
        dropGold.SetPositionData(transform.position , Quaternion.identity);
        }
        gameObject.SetActive(false);




    }


}
