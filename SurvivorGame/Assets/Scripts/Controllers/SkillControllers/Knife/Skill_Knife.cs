using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Knife : SkillBase
{
    [SerializeField] float spread = 0.3f;
    public AudioClip _audioClip;

    private void Awake()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    protected override IEnumerator Attack()
    {

        while (true)
        {
            for (int i = 0; i < stat.projectilecount; i++)
            {
                Vector3 knifePosition = transform.position;
                GameObject knife = Managers.Resource.Instantiate("Skills/Knife/Knife_Projectile");
                knife.transform.SetParent(containProjectile.transform);

                float rand = Random.Range(1, stat.projectilecount);

                if(pc.lastVerticalVector != 0 && pc.lastHorizontalVector == 0)
                {
                    knifePosition.x -= (spread * stat.projectilecount) / 2;
                    knifePosition.x += (rand * spread);
                    knife.transform.position = knifePosition + new Vector3(0.1f, 0f);
                }
                else
                {
                    knifePosition.y -= (spread * stat.projectilecount) / 2;
                    knifePosition.y += (rand * spread);
                    knife.transform.position = knifePosition + new Vector3(0f, 0.3f);
                }

                knife.GetComponent<Skill_Knife_Projectile>().Init(stat);
                Managers.Sound.Play(_audioClip, Define.Sound.Effect, 1.0f, 0.6f);
                yield return new WaitForSeconds(1 / stat.projectilecount);
            }


        }
    }
}
