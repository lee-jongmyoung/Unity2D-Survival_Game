using System.Collections;
using UnityEngine;

public class Skill_Thunder_Projectile : SkillBase
{
    Data.SkillStat _stat;
    Animator anim;
    Vector2 attackSize;
    Vector3 offset;
    BoxCollider2D bc2d;

    public void Init(Data.SkillStat stat)
    {
        _stat = stat;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        bc2d = GetComponent<BoxCollider2D>();
        attackSize = bc2d.size;
        offset = bc2d.offset;
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime * 1.5 >= 0.8f)
        {
            Destroy(gameObject);
        }
    }

    public void LaunchProjectile(Transform monster)
    {
        transform.position = monster.position + new Vector3(0.1f, 1.5f);

        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + offset, attackSize, 0f);
        Damage(colliders);
    }

    private void Damage(Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders)
        {
            MonsterStat ms = collider.GetComponent<MonsterStat>();

            if (ms != null)
            {
                float damage = DamageRange(_stat.damage);

                PostDamage(damage, collider.transform.position);
                ms.OnAttacked(damage, _stat.knockback);
            }
        }
    }

    protected override IEnumerator Attack()
    {
        yield break;
    }

}
