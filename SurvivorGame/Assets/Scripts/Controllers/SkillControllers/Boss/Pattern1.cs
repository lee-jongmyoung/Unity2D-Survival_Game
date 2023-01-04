using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern1 : SkillBase
{
    private void Start()
    {
        Destroy(gameObject, 5);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * 10f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStat ps = collision.GetComponent<PlayerStat>();
        if (ps != null)
        {
            ps.OnAttacked(5);
        }
    }

    protected override IEnumerator Attack()
    {
        yield break;
    }
}
