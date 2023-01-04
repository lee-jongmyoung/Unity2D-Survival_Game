using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern3_2 : SkillBase
{
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            GameObject pattern2_3 = Managers.Resource.Instantiate("Skills/Boss/Pattern3_3");
            pattern2_3.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStat ps = collision.GetComponent<PlayerStat>();
        if (ps != null)
        {
            ps.OnAttacked(10);
        }
    }

    protected override IEnumerator Attack()
    {
        yield break;
    }
}
