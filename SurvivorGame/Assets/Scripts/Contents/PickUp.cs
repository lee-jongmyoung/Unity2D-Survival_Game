using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private float magnetForce = 4f; // 자석 세기
    private float distanceForce = 8f; // 거리에 따른 자석효과
    private bool magnetZone = false;
    public bool magnetItem;

    [SerializeField] AudioClip _audioClip;
    [SerializeField] GameObject _player;
    [SerializeField] int increment;
    private Rigidbody2D rgbd2d;

    private void Awake()
    {
        magnetItem = false;
        _player = GameObject.FindWithTag("Player");
        rgbd2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        magnetItem = false;
    }

    private void FixedUpdate()
    {
        //if (magnetZone)
        //{
        //    Vector2 dirMagnet = _player.transform.position - transform.position + new Vector3(0f, 0.5f);
        //    float distance = Vector2.Distance(_player.transform.position, transform.position);
        //    float magnetDistanceForce = (distanceForce / distance) * magnetForce;
        //    rgbd2d.AddForce(magnetDistanceForce * dirMagnet, ForceMode2D.Force);
        //}
        if (magnetItem || magnetZone)
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        PlayerStat ps = collision.GetComponent<PlayerStat>();
        if (ps != null)
        {
            ps.Exp += increment;
            Managers.Sound.Play(_audioClip, Define.Sound.Effect, 1.0f, 0.3f);
            Managers.Resource.Destroy(gameObject);
        }

        Magnet magnet = collision.GetComponent<Magnet>();
        if (magnet != null)
        {
            magnetZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Magnet magnet = collision.GetComponent<Magnet>();
        if (magnet != null)
        {
            magnetZone = false;
        }
    }
}
