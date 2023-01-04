using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern4_1 : SkillBase
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
            GameObject pattern4_2 = Managers.Resource.Instantiate("Skills/Boss/Pattern4_2");
            pattern4_2.transform.position = transform.position + new Vector3(0.5f, 2f);
            Destroy(gameObject);
        }
    }

    protected override IEnumerator Attack()
    {
        yield break;
    }
}
