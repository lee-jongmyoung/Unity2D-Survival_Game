using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern3_1 : SkillBase
{
    private void Start()
    {
        StartCoroutine("CoStartPattern3_2");
    }

    private void Update()
    {
        transform.Translate(Vector2.left * 10f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStat ps = collision.GetComponent<PlayerStat>();
        if (ps != null)
        {
            Pattern2_2();
        }
    }

    IEnumerator CoStartPattern3_2()
    {
        float rand = Random.Range(2, 4);
        yield return new WaitForSeconds(rand);
        Pattern2_2();
    }

    void Pattern2_2()
    {
        GameObject pattern2_2 = Managers.Resource.Instantiate("Skills/Boss/Pattern3_2");
        pattern2_2.transform.position = transform.position;
        Destroy(gameObject);
    }

    protected override IEnumerator Attack()
    {
        yield break;
    }
}
