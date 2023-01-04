using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : SkillBase
{
    CircleCollider2D cc2d;
    private void Awake()
    {
        cc2d = GetComponent<CircleCollider2D>();
    }

    public override void SetData(SkillStat skill)
    {
        base.SetData(skill);

        cc2d.radius = stat.size;
    }

    protected override IEnumerator Attack()
    {
        yield break;
    }
}
