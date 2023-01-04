using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Bird : SkillBase
{
    [SerializeField] float deg;
    GameObject _circle;
    CircleCollider2D cc2d;
    public SpriteRenderer _sprite;
    public AudioClip _audioClip;


    private void Awake()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        _sprite = GetComponent<SpriteRenderer>();

        GameObject skills = GameObject.Find("GetSkills");
        _circle = Managers.Resource.Instantiate("Skills/Bird/Bird_RangeCircle");
        _circle.transform.SetParent(skills.transform);
        cc2d = _circle.GetComponent<CircleCollider2D>();
    }


    private void Update()
    {
        Move();

        Rotating();
    }

    void Move()
    {
        if (pc.lastHorizontalVector > 0)
        {
            _sprite.flipX = false;
            transform.position = Vector3.MoveTowards(
                gameObject.transform.position,
                pc.transform.position + new Vector3(-1f, 2f),
                0.03f
                );
        }
        else
        {
            _sprite.flipX = true;
            transform.position = Vector3.MoveTowards(
                gameObject.transform.position,
                pc.transform.position + new Vector3(1f, 2f),
                0.03f
                );
        }
    }

    // Bird의 사정거리를 보여주는 원이 플레이어 주변으로 공전
    void Rotating()
    {
        if (_circle == null)
            return;

        deg += Time.deltaTime * 30.0f;
        if (deg < 360)
        {
            var rad = Mathf.Deg2Rad * deg;
            var x = 7 * Mathf.Sin(rad);
            var y = 7 * Mathf.Cos(rad);
            _circle.transform.position = pc.transform.position + new Vector3(x, y);
        }
        else
            deg = 0;
    }

    // Bird의 사정거리를 보여주는 원안에 랜덤 point를 리턴
    public Vector3 RandomSphereInPoint(float radius)
    {
        Vector3 getPoint = Random.onUnitSphere;

        float r = Random.Range(0.0f, radius);

        return (getPoint * r) + _circle.transform.position;
    }

    protected override IEnumerator Attack()
    {
        while (true)
        {
            if(isAwake)
                yield return new WaitForSeconds(stat.timetoattack);
            else
                yield return new WaitForSeconds(stat.timetoattack);

            Managers.Sound.Play(_audioClip);

            for (int i = 0; i < stat.projectilecount; i++)
            {
                Vector3 attackPoint = RandomSphereInPoint(cc2d.radius*6);
                GameObject birdAttack = Managers.Resource.Instantiate("Skills/Bird/Bird_Projectile");
                birdAttack.transform.position = transform.position;
                birdAttack.transform.rotation = GetDir(attackPoint);
                birdAttack.transform.SetParent(containProjectile.transform);

                Skill_Bird_Projectile sbp = birdAttack.GetComponent<Skill_Bird_Projectile>();
                sbp.ShootProjectile(attackPoint);
                sbp.Init(stat);

                float time = 4.0f / stat.projectilecount;
                yield return new WaitForSeconds(time);
            }
        }

    }

    protected Quaternion GetDir(Vector3 attackPoint)
    {
        float angle = Mathf.Atan2(attackPoint.y - transform.position.y, attackPoint.x - transform.position.x) * Mathf.Rad2Deg;

        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
