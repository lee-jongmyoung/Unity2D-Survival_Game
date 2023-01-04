using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern3_3 : SkillBase
{
    private float damageTime; // �������� �� ������ (�� �����Ӹ��ٰ� �ƴ� ���� �ð����� �������� �ֱ� ���Ͽ�)
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
