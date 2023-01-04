using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_FireBall : SkillBase
{
    public AudioClip _audioClip;
    [SerializeField] float spread = 5f;

    protected override IEnumerator Attack()
    {

        while (true)
        {
            yield return new WaitForSeconds(stat.timetoattack);


            List<GameObject> monsterList = MonsterSort();

            for (int i = 0; i < stat.projectilecount; i++)
            {
                if (Managers.Object.objects.Count < 1)
                {
                    StopCoroutine("_coAttack");
                    yield break;
                }
                if(i==0)
                    Managers.Sound.Play(_audioClip);

                GameObject FireBall;

                if (isAwake)
                    FireBall = Managers.Resource.Instantiate("Skills/FireBall/FireBall_Projectile_Awake");
                else
                    FireBall = Managers.Resource.Instantiate("Skills/FireBall/FireBall_Projectile");

                FireBall.transform.position = transform.position;
                FireBall.transform.SetParent(containProjectile.transform);

                if (isAwake)
                {
                    Skill_FireBall_Projectile_Awake sbp = FireBall.GetComponent<Skill_FireBall_Projectile_Awake>();
                    sbp.transform.rotation = ClosestMonsterDir(monsterList[0].transform);
                    sbp.transform.Rotate(new Vector3(0, 0, -(spread * stat.projectilecount) / 2));
                    sbp.transform.Rotate(new Vector3(0, 0, (i * spread)));
                    sbp.Init(stat);
                }
                else
                {
                    Skill_FireBall_Projectile sbp = FireBall.GetComponent<Skill_FireBall_Projectile>();
                    sbp.transform.rotation = ClosestMonsterDir(monsterList[0].transform);
                    sbp.transform.Rotate(new Vector3(0, 0, -(spread * stat.projectilecount) / 2));
                    sbp.transform.Rotate(new Vector3(0, 0, (i * spread)));
                    sbp.Init(stat);
                }
            }

        }
    }

}
