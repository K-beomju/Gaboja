using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LivingEntity : MonoBehaviour, IDamageble
{
    public float initHealth;
    public float health { get; protected set; }
    public bool dead { get; protected set; }
    public event Action OnDeath;

    protected virtual void OnEnable()
    {
        dead = false;
        health = initHealth;
    }

    public virtual void OnDamage(float damage)
    {
        health -= damage;
        if(health <= 0 && !dead)
        {
            Die();
        }
    }

    public virtual void RestoreHealth(float value)
    {
        if (dead) return;
        health += value;
    }

    public virtual void Die()
    {
        //내 죽음을 구독하는 녀석이 있다면 발행해서 알려주자.
        if (OnDeath != null) OnDeath();
        dead = true;
    }




}
