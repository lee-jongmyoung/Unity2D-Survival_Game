using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Bird_Projectile : SkillBase
{
    Data.SkillStat _stat;
    Vector3 _attackPoint;
    private float m_timerCurrent = 0;
    BoxCollider2D bc2d;
    Vector2 attackSize;

    public SpriteRenderer _sprite;

    Vector3[] m_points = new Vector3[4];

    private float m_timerMax = 0;

    private void Awake()
    {
        bc2d = GetComponent<BoxCollider2D>();
        attackSize = bc2d.size;
        _sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        BezierCurve();
    }

    public void Init(Data.SkillStat stat)
    {
        _stat = stat;

        // ��ǥ���� ���� �� �ð�
        m_timerMax = 1.0f;

        // ���� ����.
        m_points[0] = transform.position;

        // ���� ������ �������� ���� ����Ʈ ����.
        m_points[1] = transform.position +
            (6.0f * Random.Range(-1.5f, 0.5f) * transform.right) + // X (��, �� ��ü)
            (6.0f * Random.Range(-1.5f, 1.5f) * transform.up) + // Y (�Ʒ��� ����, ���� ��ü)
            (6.0f * Random.Range(-1.0f, -0.8f) * transform.forward); // Z (�� �ʸ�)

        // ���� ������ �������� ���� ����Ʈ ����.
        m_points[2] = _attackPoint;

        // ���� ����.
        m_points[3] = _attackPoint;


    }

    #region BezierCurve
    void BezierCurve()
    {
        if (m_timerCurrent > m_timerMax)
        {
            return;
        }

        // ��� �ð� ���.
        m_timerCurrent += Time.deltaTime * 2.5f;

        // ������ ����� X,Y,Z ��ǥ ���.
        transform.position = new Vector3(
            CubicBezierCurve(m_points[0].x, m_points[1].x, m_points[2].x, m_points[3].x),
            CubicBezierCurve(m_points[0].y, m_points[1].y, m_points[2].y, m_points[3].y),
            CubicBezierCurve(m_points[0].z, m_points[1].z, m_points[2].z, m_points[3].z)
        );
        if (transform.position == _attackPoint)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, attackSize, 0f);
            Damage(colliders);
            StartCoroutine("CoDestroyEffect");
        }
    }

    private float CubicBezierCurve(float a, float b, float c, float d)
    {
        // (0~1)�� ���� ���� ������ � ���� ���ϱ� ������, ������ ���� �ð��� ���ߴ�.
        float t = m_timerCurrent / m_timerMax; // (���� ��� �ð� / �ִ� �ð�)

        // ������.
        float ab = Mathf.Lerp(a, b, t);
        float bc = Mathf.Lerp(b, c, t);
        float cd = Mathf.Lerp(c, d, t);

        float abbc = Mathf.Lerp(ab, bc, t);
        float bccd = Mathf.Lerp(bc, cd, t);

        return Mathf.Lerp(abbc, bccd, t);
    }
    #endregion

    public void ShootProjectile(Vector3 attackPoint)
    {
        _attackPoint = attackPoint;
    }

    private void Damage(Collider2D[] colliders)
    {
        foreach (Collider2D collider in colliders)
        {
            MonsterStat ms = collider.GetComponent<MonsterStat>();

            if (ms != null)
            {
                float damage = DamageRange(_stat.damage);

                PostDamage(damage, collider.transform.position);
                ms.OnAttacked(damage, _stat.knockback);
            }
        }
    }
    IEnumerator CoDestroyEffect()
    {
        for (int i = 10; i >= 0; i--)
        {
            float f = i / 10.0f;
            _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, f);
            yield return new WaitForSeconds(0.01f);
        }

        Destroy(gameObject);
    }

    protected override IEnumerator Attack()
    {
        yield break;
    }
}
