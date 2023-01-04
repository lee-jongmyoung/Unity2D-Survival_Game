using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Ellipse : SkillBase
{
    public AudioClip _audioClip;

    [SerializeField]
    GameObject skillObject;
    Animator anim;
    Vector2 attackSize;
    BoxCollider2D bc2d;
    [SerializeField] float deg;

    private void Awake()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        anim = skillObject.GetComponent<Animator>();
        bc2d = skillObject.GetComponent<BoxCollider2D>();
        attackSize = bc2d.size;
    }

    private void Update()
    {

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            skillObject.SetActive(false);
        }
    }

    protected override IEnumerator Attack()
    {
        while (true)
        {
            if (stat.size > 0)
            {
                attackSize = bc2d.size + new Vector2(stat.size / 2, stat.size / 4);
                skillObject.transform.localScale = new Vector3(1.0f + stat.size * 0.1f, 1.0f + stat.size * 0.1f);
            }

            skillObject.SetActive(true);
            Managers.Sound.Play(_audioClip);

            if (isAwake)
            {
                StartCoroutine("CoFlameEllipseAwake");
            }

            Collider2D[] colliders = Physics2D.OverlapBoxAll(skillObject.transform.position, attackSize, 0f);
            Damage(colliders);
            yield return new WaitForSeconds(stat.timetoattack);
        }
    }

    private void Damage(Collider2D[] colliders)
    {
        foreach(Collider2D collider in colliders)
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

    IEnumerator CoFlameEllipseAwake()
    {
        for (int i = 0; i < 12; i++)
        {
            var rad = Mathf.Deg2Rad * (30 * i + 1);
            var x = 7 * Mathf.Sin(rad);
            var y = 7 * Mathf.Cos(rad);
            GameObject flame_Awake = Managers.Resource.Instantiate("Skills/FlameEllipse/FlameEllipse_Awake");
            flame_Awake.transform.position = pc.transform.position + new Vector3(x, y);


            Skill_Ellipse_Awake sea = flame_Awake.GetComponent<Skill_Ellipse_Awake>();
            sea.Init(stat);

            yield return new WaitForSeconds(0.1f);
        }
    }

}
