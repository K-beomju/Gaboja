using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : LivingEntity
{
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
}
