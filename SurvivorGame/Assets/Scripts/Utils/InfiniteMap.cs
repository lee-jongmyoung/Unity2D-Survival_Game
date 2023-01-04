using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMap : MonoBehaviour
{
    GameObject player;
    PlayerController pc;
    [SerializeField] public Vector3 playerDir;

    private void Awake()
    {
        player = GameObject.Find("Player");
        pc = player.GetComponent<PlayerController>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = player.transform.position;
        Vector3 myPos = transform.position;

        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);


        playerDir = pc.moveDir;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                    transform.Translate(Vector3.right * dirX * 120);
                else if (diffX < diffY)
                    transform.Translate(Vector3.up * dirY * 120);
                break;
            case "Enemy":
                break;
        }
    }

}
