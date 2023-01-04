using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern3_3 : SkillBase
{
    private float damageTime; // 데미지가 들어갈 딜레이 (매 프레임마다가 아닌 일정 시간마다 데미지를 주기 위하여)
    private float currentDamageTime;

    private void Awake()
    {
        damageTime = 0.1f;
        Destroy(gameObject, 20f);
    }
    private void Update()
    {
        if (currentDamageTime > 0)
            currentDamageTime -= Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerStat ps = collision.GetComponent<PlayerStat>();
        if (ps != null)
        {
            if(currentDamageTime <= 0)
            {
                ps.OnAttacked(2);
                currentDamageTime = damageTime;
            }
        }
    }

    protected override IEnumerator Attack()
    {
        yield break;
    }
}
