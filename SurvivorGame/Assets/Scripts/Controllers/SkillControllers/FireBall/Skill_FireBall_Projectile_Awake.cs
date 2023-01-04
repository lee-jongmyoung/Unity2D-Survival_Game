using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_FireBall_Projectile_Awake : SkillBase
{
    public Data.SkillStat _stat;
    float attackSize;
    CircleCollider2D bc2d;

    public void Awake()
    {
        Destroy(gameObject, 5);
        bc2d = GetComponent<CircleCollider2D>();
        attackSize = bc2d.radius;
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
        }
    }

    //private void Damage(Collider2D[] colliders)
    //{
    //    foreach (Collider2D collider in colliders)
    //    {
    //        MonsterStat ms = collider.GetComponent<MonsterStat>();

    //        if (ms != null)
    //        {
    //            float damage = DamageRange(_stat.damage);

    //            PostDamage(damage, collider.transform.position);
    //            ms.OnAttacked(damage, _stat.knockback);
    //        }
    //    }
    //}

    protected override IEnumerator Attack()
    {
        yield break;
    }
}
