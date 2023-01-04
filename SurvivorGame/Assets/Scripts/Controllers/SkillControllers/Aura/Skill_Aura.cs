using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Aura : SkillBase
{
    Vector2 attackSize;
    Collider2D c2d;

    private void Awake()
    {
        c2d = GetComponent<Collider2D>();
        attackSize = c2d.bounds.size;
    }


    public override void SetData(Data.SkillStat skill)
    {
        base.SetData(skill);
        transform.localScale = new Vector3(stat.size, stat.size, 0f);
        attackSize = c2d.bounds.size;
    }

    protected override IEnumerator Attack()
    {

        while (true)
        {
            yield return new WaitForSeconds(stat.timetoattack);

            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, attackSize, 0f);
            Damage(colliders);

        }
    }

    private void Damage(Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders)
        {
            MonsterStat ms = collider.GetComponent<MonsterStat>();

            if (ms != null)
            {
                float damage = DamageRange(stat.damage);
                PostDamage(damage, collider.transform.position);
                ms.OnAttacked(damage, stat.knockback);
            }
        }
    }
}
