using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Knife_Projectile : SkillBase
{
    Data.SkillStat _stat;
    Vector3 shootDir;

    void Awake()
    {
        Destroy(gameObject, 2);
    }

    void Update()
    {
        transform.position += shootDir.normalized * _stat.speed * Time.deltaTime;
    }

    public void Init(Data.SkillStat stat)
    {
        _stat = stat;
        ShootProjectile();
    }

    public void ShootProjectile()
    {
        Vector3 dir = PlayerDir();
        shootDir = dir;


    }
    public void OnTriggerEnter2D(Collider2D collision)
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
