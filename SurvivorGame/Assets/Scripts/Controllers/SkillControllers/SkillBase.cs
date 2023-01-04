using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    public Data.SkillStat stat;

    public List<GameObject> FoundObjects;
    public GameObject closestMonster;
    public float closestTmp;
    public Coroutine _coAttack;

    public GameObject containProjectile;

    [SerializeField] public PlayerController pc;

    public bool isAwake = false;

    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        containProjectile = GameObject.Find("@Projectile");
        _coAttack = StartCoroutine("Attack");
    }


    // 가장 가까운 몬스터 순서대로 정렬
    protected List<GameObject> MonsterSort()
    {
        var list = GameObject.FindGameObjectsWithTag("Monster").OrderBy(
               x => Vector2.Distance(transform.position, x.transform.position)
               ).ToList();

        return list;
    }

    // 적 방향으로 투사체 회전
    protected Quaternion ClosestMonsterDir(Transform monster)
    {
        float angle = Mathf.Atan2(monster.position.y - transform.position.y, monster.position.x - transform.position.x) * Mathf.Rad2Deg;

        return Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    // 플레이어 방향으로 투사체 회전 8방향
    protected Vector3 PlayerDir()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        Vector3 dir;

        float angle = Mathf.Atan2(pc.lastVerticalVector, pc.lastHorizontalVector) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        if (pc.lastHorizontalVector == 0 && pc.lastVerticalVector == 0)
            dir = new Vector3(1, 0);
        else
            dir = new Vector3(pc.lastHorizontalVector, pc.lastVerticalVector);

        return dir;

    }

    //데미지 범위 +-20%
    protected float DamageRange(float damage)
    {
        float range = damage / 10 * 2;

        damage = Mathf.Round(Random.Range(damage - range, damage + range));

        return damage;
    }

    // FireBall 1-10 Knife 11-20
    public virtual void SetData(Data.SkillStat skill)
    {
        stat = skill;
    }

    protected virtual void PostDamage(float damage, Vector3 targetPosition)
    {
        Managers.Message.PostMessage(damage, targetPosition);
    }

    protected abstract IEnumerator Attack();
}
