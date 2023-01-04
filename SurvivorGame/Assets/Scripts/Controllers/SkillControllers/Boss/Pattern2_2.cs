using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern2_2 : SkillBase
{
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStat ps = collision.GetComponent<PlayerStat>();
        if (ps != null)
        {
            ps.OnAttacked(30);
        }
    }

    protected override IEnumerator Attack()
    {
        yield break;
    }
}
