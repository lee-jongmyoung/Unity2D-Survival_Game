using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_SpinSword : SkillBase
{
    [SerializeField] public float deg;

    public float count;


    public void Start()
    {
        base.Init();
        count = stat.projectilecount;
    }


    private void Update()
    {
        if (!isAwake)
        {
            if (count != stat.projectilecount)
            {
                StopCoroutine("Attack");
                StartCoroutine("Attack");
                count = stat.projectilecount;
            }

            Rotating();
        }
    }

    private void Rotating()
    {

        // Sword가 플레이어 주변을 공전
        deg += Time.deltaTime * stat.speed;
        if (deg < 360)
        {
            for (int i = 0; i < stat.projectilecount; i++)
            {
                var rad = Mathf.Deg2Rad * (deg + (i * (360 / stat.projectilecount)));
                var x = stat.size * Mathf.Sin(rad);
                var y = stat.size * Mathf.Cos(rad);
                transform.GetChild(i).transform.position = transform.position + new Vector3(x, y);
                transform.GetChild(i).transform.rotation = Quaternion.Euler(0, 0, (deg + (i * (360 / stat.projectilecount))) * -1);
            }

        }
        else
        {
            deg = 0;
        }

    }

    protected override IEnumerator Attack()
    {
        while (true)
        {
            if (isAwake)
            {

                for (int j = 0; j < stat.projectilecount; j++)
                {
                    transform.GetChild(j).gameObject.SetActive(false);
                }

                GameObject spinSwordAwake = Managers.Resource.Instantiate("Skills/SpinSword/SpinSword_Awake");
                spinSwordAwake.transform.SetParent(containProjectile.transform);
                Skill_Spin_Sword_Awake sssa = spinSwordAwake.GetComponent<Skill_Spin_Sword_Awake>();
                sssa.Init(stat);

                yield return new WaitForSeconds(stat.timetoattack);
            }
            else
            {
                StartCoroutine("BiggerAnim");
                yield return new WaitForSeconds(5.0f);
                StartCoroutine("SmallerAnim");
                yield return new WaitForSeconds(stat.timetoattack);
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterStat ms = collision.GetComponent<MonsterStat>();
        if (ms != null)
        {
            float damage = DamageRange(stat.damage);

            PostDamage(damage, collision.transform.position);
            ms.OnAttacked(damage, stat.knockback);
        }
    }

    IEnumerator BiggerAnim()
    {
        for (int j = 0; j < stat.projectilecount; j++)
            transform.GetChild(j).gameObject.SetActive(true);

        for (int i = 10; i >= 0; i--)
        {
            float f = i / 10.0f;

            for (int j = 0; j < stat.projectilecount; j++)
                transform.GetChild(j).localScale = Vector3.one * (1 - f);

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator SmallerAnim()
    {
        for (int i = 0; i < 10; i++)
        {
            float f = i / 10.0f;

            for (int j = 0; j < stat.projectilecount; j++)
                transform.GetChild(j).localScale = Vector3.one * (1 - f);

            if (i == 9)
            {
                for (int j = 0; j < stat.projectilecount; j++)
                {
                    transform.GetChild(j).gameObject.SetActive(false);
                }

            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
