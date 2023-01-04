using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnetItem : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] GameObject[] _coin;
    bool isMagnet = false;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");

    }
    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStat ps = collision.GetComponent<PlayerStat>();
        if (ps != null)
        {
            _coin = GameObject.FindGameObjectsWithTag("Coin");

            foreach (GameObject coin in _coin)
            {
                coin.GetComponent<PickUp>().magnetItem = true;
            }
            Managers.Resource.Destroy(gameObject);
        }

    }
}
