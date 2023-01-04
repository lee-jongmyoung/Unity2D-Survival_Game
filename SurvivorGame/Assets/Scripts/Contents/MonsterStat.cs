using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    [SerializeField]
    public int _level;
    [SerializeField]
    public float _hp;
    [SerializeField]
    public int _maxHp;
    [SerializeField]
    public int _attack;
    [SerializeField]
    public float _moveSpeed;

    public string _name;

    public AudioClip _audioClip;

    public Data.MonsterStat stat;

    private float[] randomCoin = { 85f, 12f, 2.8f, 0.2f};


    public float Hp { get { return _hp; } set { _hp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    private void Start()
    {
        _hp = 10;
        _attack = 1;
        _moveSpeed = 1.0f;

        _name = gameObject.name;

        SetStat(_name);
    }

    public void SetStat(string name)
    {
        Dictionary<string, Data.MonsterStat> dict = Managers.Data.MonsterStatDict;
        stat = dict[name];

        _hp = stat.hp;
        _moveSpeed = stat.speed;
        _attack = stat.attack;
    }

    public void OnAttacked(float damage, float knockback)
    {
        if (Hp <= 0)
            return;

        Hp -= damage;
        Managers.Sound.Play(_audioClip, Define.Sound.Effect, 1.0f, 0.3f);

        if (Hp <= 0)
        {
            OnDead();
            return;
        }

        GetComponent<MonsterController>().StartCoroutine("CoHitEffect", knockback);
    }

    public void OnDead()
    {
        //GetComponent<MonsterController>().StartCoroutine("CoDeadEffect");

        Managers.Resource.Destroy(gameObject);
        Managers.Object.Remove(gameObject);

        // 코인 랜덤생성
        GameObject equip = null;
        GameObject parent = GameObject.Find("@EQUIP");
        float randomEquip = Choose(randomCoin);
        if (randomEquip < 3)
        {
            equip = Managers.Resource.Instantiate($"Equip/Coin{randomEquip}", parent.transform);
        }
        else
        {
            equip = Managers.Resource.Instantiate("Equip/magnetItem", parent.transform);
        }

        equip.transform.position = transform.position;
    }
    // 코인 확률생성 함수
    float Choose(float[] probs)
    {

        float total = 0;

        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }
}
