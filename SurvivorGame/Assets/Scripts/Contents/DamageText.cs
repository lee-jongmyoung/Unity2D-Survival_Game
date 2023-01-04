using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    Vector3 dir;
    public SpriteRenderer _sprite;
    public TextMeshProUGUI _tmpu;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _tmpu = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        StartCoroutine("DestroyDamageText");
    }

    private void Update()
    {
        transform.Translate(dir * Time.deltaTime);
    }


    IEnumerator DestroyDamageText()
    {
        // 특정 방향으로 이동하는 이펙트
        transform.position = new Vector3(0, 4.85f, 0);
        dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), -1).normalized;

        // Fade Out
        for (int i = 10; i >= 0; i--)
        {
            float f = i / 10.0f;
            _tmpu.faceColor = new Color(1.0f, 1.0f, 1.0f, f);
            _tmpu.outlineColor = new Color(0f, 0f, 0f, f);
            yield return new WaitForSeconds(0.1f);
        }

        Managers.Resource.Destroy(gameObject);

    }

}
