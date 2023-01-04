using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonsterController
{
    string[] pattern;
    float speed;

    public GameObject containProjectile;

    private void Start()
    {
        containProjectile = GameObject.Find("@Projectile");
        speed = ms.MoveSpeed;
        pattern = ms.stat.pattern.Split(',');
        StartCoroutine("Pattern");
    }

    protected override void FixedUpdate()
    {
        Vector3 dir = (targetDest.position - transform.position + new Vector3(0.0f, 0.5f, 0.0f)).normalized;
        if (dir.x > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
        else
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y);

        rgdbd2d.velocity = dir * speed;
    }

    IEnumerator Pattern()
    {
        while (true)
        {
            int[] pattern1 = System.Array.ConvertAll<string, int>(pattern, int.Parse);
            int random = Random.Range(0, pattern1.Length);
            int randomPattern = pattern1[random];

            switch (randomPattern)
            {
                case 0:
                    StartCoroutine("Pattern0");
                    break;
                case 1:
                    StartCoroutine("Pattern1");
                    break;
                case 2:
                    StartCoroutine("Pattern2");
                    break;
                case 3:
                    StartCoroutine("Pattern3");
                    break;
                case 4:
                    StartCoroutine("Pattern4");
                    break;
            }
            yield return new WaitForSeconds(10f);
        }

    }

    // 속도 업
    IEnumerator Pattern0()
    {
        yield return new WaitForSeconds(3f);
        speed = 10f;
        yield return new WaitForSeconds(0.3f);
        speed = ms.MoveSpeed;

    }

    // 투사체 20발 발사
    IEnumerator Pattern1()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject pattern1 = Managers.Resource.Instantiate("Skills/Boss/Pattern1", containProjectile.transform);
            pattern1.transform.position = transform.position;

            float angle = Mathf.Atan2(targetDest.transform.position.y - transform.position.y, targetDest.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            pattern1.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            yield return new WaitForSeconds(0.1f);
        }
    }

    // 보라색 구체 발사 후 폭발
    IEnumerator Pattern2()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject pattern2 = Managers.Resource.Instantiate("Skills/Boss/Pattern2_1", containProjectile.transform);
            pattern2.transform.position = transform.position;

            float angle = Mathf.Atan2(targetDest.transform.position.y - transform.position.y, targetDest.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            pattern2.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);

            yield return new WaitForSeconds(3f);
        }
    }

    // 화염 구체 발사 후 폭발 -> 화염지속
    IEnumerator Pattern3()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject pattern3 = Managers.Resource.Instantiate("Skills/Boss/Pattern3_1", containProjectile.transform);
            pattern3.transform.position = transform.position;

            float angle = Mathf.Atan2(targetDest.transform.position.y - transform.position.y, targetDest.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            pattern3.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);

            yield return new WaitForSeconds(1f);
        }
    }

    // 화염 구체 발사 후 폭발 -> 화염지속
    IEnumerator Pattern4()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject pattern4 = Managers.Resource.Instantiate("Skills/Boss/Pattern4_1", containProjectile.transform);
            pattern4.transform.position = targetDest.position;

            yield return new WaitForSeconds(3f);
        }
    }


    protected override IEnumerator CoHitEffect(float knockback = 0f)
    {
        yield break;
    }
}
