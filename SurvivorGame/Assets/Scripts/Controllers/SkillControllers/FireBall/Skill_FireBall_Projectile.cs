using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_FireBall_Projectile : SkillBase
{
    public Data.SkillStat _stat;

    public void Awake()
    {
        Destroy(gameObject, 5);
    }

    public void Update()
    {
        transform.Translate(Vector2.up * _stat.speed * Time.deltaTime);
    }

    public void Init(Data.SkillStat stat)
    {
        _stat = stat;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterStat ms = collision.GetComponent<MonsterStat>();
        if (ms != null)
        {
            float damage = DamageRange(_stat.damage);

            PostDamage(damage, collision.transform.position);
            ms.OnAttacked(damage, _stat.knockback);
            Destroy(gameObject);
        }
    }

    protected override IEnumerator Attack()
    {
        yield break;
    }
}
