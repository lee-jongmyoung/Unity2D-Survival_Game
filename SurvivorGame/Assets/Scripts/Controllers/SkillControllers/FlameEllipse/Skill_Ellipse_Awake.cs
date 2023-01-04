using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Ellipse_Awake : SkillBase
{
    public AudioClip _audioClip;
    Animator anim;
    Data.SkillStat _stat;
    float attackSize;
    CircleCollider2D cc2d;

    void Awake()
    {
        anim = GetComponent<Animator>();
        cc2d = GetComponent<CircleCollider2D>();
        attackSize = cc2d.radius;
        Managers.Sound.Play(_audioClip);
    }
    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Destroy(gameObject);
        }
    }
    public void Init(Data.SkillStat stat)
    {
        _stat = stat;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackSize);
        Damage(colliders);
    }

    private void Damage(Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders)
        {
            MonsterStat ms = collider.GetComponent<MonsterStat>();

            if (ms != null)
            {
                float damage = DamageRange(_stat.damage) / 2;
                PostDamage(damage, collider.transform.position);
                ms.OnAttacked(damage, stat.knockback);
            }
        }
    }

    protected override IEnumerator Attack()
    {
        yield break;
    }
}
