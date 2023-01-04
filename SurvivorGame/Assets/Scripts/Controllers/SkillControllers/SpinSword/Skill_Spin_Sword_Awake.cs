using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Spin_Sword_Awake : SkillBase
{
    public Data.SkillStat _stat;

    Vector2 min;
    Vector2 max;
    float startRandY;
    float endRandY;
    float speed;

    void Start()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        startRandY = Random.Range(min.y + 1, max.y - 1);
        endRandY = Random.Range(min.y + 1, max.y - 1);

        speed = 0.3f;

        transform.position = new Vector2(min.x - 1, startRandY);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector2(max.x + 1, endRandY),
                speed
                );

        if (transform.position == new Vector3(max.x + 1, endRandY))
            Destroy(gameObject);
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
