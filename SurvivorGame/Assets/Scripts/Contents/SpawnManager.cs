using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    Vector2 spawnArea;
    int spawnMonster = 0;
    int spawnBoss = 1;

    GameObject root;

    public void SpawnMonster(Transform player, int time, bool isBoss)
    {
        root = GameObject.Find("@MONSTER");

        if (isBoss)
        {
            string monsterName = "Boss0" + spawnBoss;

            Vector3 position = RandomPosition();

            GameObject newMonster = Managers.Resource.Instantiate($"Monster/Boss/{monsterName}", root.transform);
            newMonster.GetComponent<MonsterStat>().SetStat(monsterName);
            newMonster.transform.position = position + player.transform.position;
            Managers.Object.Add(newMonster);
            spawnMonster++;
            spawnBoss++;
        }
        else
        {
            int SpawnCount = (time * 2) + 1;
            for (int i = 0; i < SpawnCount; i++)
            {
                Vector3 position = RandomPosition();

                int random = Random.Range(1, 5);
                string monsterName = "Monster" + spawnMonster + random;

                GameObject newMonster = Managers.Resource.Instantiate($"Monster/{monsterName}", root.transform);
                newMonster.GetComponent<MonsterStat>().SetStat(monsterName);
                newMonster.transform.position = position + player.transform.position;
                Managers.Object.Add(newMonster);
            }
        }
    }
    
    private Vector3 RandomPosition()
    {
        spawnArea.x = 30.0f;
        spawnArea.y = 20.0f;

        Vector3 position = new Vector3();

        float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;
        if(UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * f;
        }
        else
        {
            position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
            position.x = spawnArea.x * f;
        }
        position.z = 0;

        return position;
    }
    public void Clear()
    {
        spawnMonster = 0;
        spawnBoss = 0;
    }
}
