using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Thunder_Projectile_Awake : SkillBase
{
    public Data.SkillStat _stat;

    Animator anim;
    Vector2 min;
    Vector2 max;
    float randX;

    void Start()
    {
        anim = GetComponent<Animator>();
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        randX = Random.Range(min.x, max.x);

        transform.position = new Vector2(randX, (min.y + max.y) / 2);

    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime * 1.5 >= 0.8f)
        {
            Destroy(gameObject);
        }
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

    protected override IEnumerator Attack()
    {
        yield break;
    }
}
