using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skill_Thunder : SkillBase
{
    public AudioClip _audioClip;
    protected override IEnumerator Attack()
    {

        while (true)
        {
            yield return new WaitForSeconds(stat.timetoattack);

            if (!isAwake)
            {
                // 범위내의 몬스터들을 랜덤으로 정렬
                var monsters = GameObject.FindGameObjectsWithTag("Monster")
                    .Where(x => Vector2.Distance(transform.position, x.transform.position) <= new Vector2(7f, 6f).magnitude)
                    .OrderBy(x => Guid.NewGuid())
                    .ToList();

                for (int i = 0; i < stat.projectilecount; i++)
                {
                    if (Managers.Object.objects.Count < 1)
                    {
                        StopCoroutine("_coAttack");
                        yield break;
                    }
                    if (monsters.Count > 0)
                    {
                        if (monsters.Count < i + 1)
                            continue;

                        if (i == 0)
                            Managers.Sound.Play(_audioClip);

                        GameObject thunder = Managers.Resource.Instantiate("Skills/Thunder/Thunder_Projectile");
                        thunder.transform.position = transform.position;
                        thunder.transform.SetParent(containProjectile.transform);

                        Skill_Thunder_Projectile stp = thunder.GetComponent<Skill_Thunder_Projectile>();

                        stp.gameObject.SetActive(true);
                        stp.Init(stat);
                        stp.LaunchProjectile(monsters[i].transform);
                    }
                }
            }
            else
            {
                GameObject thunder_Awake = Managers.Resource.Instantiate("Skills/Thunder/Thunder_Projectile_Awake");
                thunder_Awake.transform.SetParent(containProjectile.transform);
                Managers.Sound.Play(_audioClip);

                Skill_Thunder_Projectile_Awake stpa = thunder_Awake.GetComponent<Skill_Thunder_Projectile_Awake>();
                stpa.Init(stat);

            }

        }
    }

}
