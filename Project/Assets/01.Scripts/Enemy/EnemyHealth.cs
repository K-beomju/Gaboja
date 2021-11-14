using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : LivingEntity
{
    public string name;
    public Sprite sprite;

    private DropGold dropGold;
    private EffectObject effectObject;
    private DamageText damageText;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
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

        StartCoroutine(hitColor());
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
        effectObject = GameManager.GetCreateScreenEffect(0);
        effectObject.SetPositionData(transform.position, Quaternion.identity);




        gameObject.SetActive(false);
        sr.color = new Color(255f/ 255f , 255f/255f,255f/255f , 255f/255f);

    }

    private IEnumerator hitColor()
    {
        sr.color = new Color(255f/ 255f , 115f/255f,115f/255f , 255f/255f);
        yield return Yields.WaitSeconds(0.2f);
        sr.color = new Color(255f/ 255f , 255f/255f,255f/255f , 255f/255f);

    }

}
