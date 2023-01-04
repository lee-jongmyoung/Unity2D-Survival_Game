using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField]
    protected Transform targetDest;
    protected SpriteRenderer _sprite;
    protected Rigidbody2D rgdbd2d;
    protected MonsterStat ms;
    protected Animator animator;

    Coroutine _coAttack = null;


    private void Awake()
    {
        rgdbd2d = GetComponent<Rigidbody2D>();
        targetDest = GameObject.FindWithTag("Player").transform;
        _sprite = Util.FindChild<SpriteRenderer>(gameObject, "Body", true);
        ms = gameObject.GetComponent<MonsterStat>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _sprite.color = new Color(1, 1, 1, 1);
    }

    protected virtual void FixedUpdate()
    {
        Vector3 dir = (targetDest.position - transform.position + new Vector3(0.0f,0.5f,0.0f)).normalized;
        if (dir.x > 0)
            transform.localScale = new Vector3(-1f, transform.localScale.y);
        else
            transform.localScale = new Vector3(1f, transform.localScale.y);

        rgdbd2d.velocity = dir * ms.MoveSpeed;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            if (_coAttack == null && gameObject.activeSelf)
                _coAttack = StartCoroutine("CoAttack");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == targetDest.gameObject)
        {
            if (_coAttack != null)
            {
                StopCoroutine(_coAttack);
                _coAttack = null;
            }
        }
    }

    IEnumerator CoAttack()
    {
        while (true)
        {
            targetDest.gameObject.GetComponent<PlayerStat>().OnAttacked(ms.Attack);
            yield return new WaitForSeconds(0.3f);
        }
    }

    protected virtual IEnumerator CoHitEffect(float knockback = 0f)
    {
        Vector3 backVec = (targetDest.transform.position - transform.position).normalized;
        transform.Translate(-backVec * knockback);

        _sprite.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.3f);
        _sprite.color = new Color(1, 1, 1, 1);
    }
    IEnumerator CoDeadEffect()
    {
        for (int i = 10; i >= 0; i--)
        {
            float f = i / 10.0f;
            _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, f);
            yield return new WaitForSeconds(0.01f);
        }

        Managers.Resource.Destroy(gameObject);
        Managers.Object.Remove(gameObject);
    }
}
