using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public PlayerController player;

    public float time;
    int[] bossSpawnTime = {1, 2, 4, 5};
    TimerUI timerUI;
    protected override void Init()
    {
        base.Init();
        timerUI = FindObjectOfType<TimerUI>();

        SceneType = Define.Scene.Game;

        // 기본스킬
        Managers.Skill.AddSkill(10, "Knife", 1);

        StartCoroutine("CoStartSpawn");
        StartCoroutine("CoStartBossSpawn");
    }
    void Update()
    {
        time += Time.deltaTime;
        timerUI.UpdateTime(time);

        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            Time.timeScale += 1f;
        }
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            Time.timeScale -= 1f;
        }
    }

    IEnumerator CoStartSpawn()
    {
        while (true)
        {
            int minutes = (int)(time / 60f);

            Managers.Spawn.SpawnMonster(player.transform, minutes, false);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator CoStartBossSpawn()
    {
        int cnt = 0;
        while (true)
        {
            yield return new WaitForSeconds(bossSpawnTime[cnt] * 60);

            Managers.Spawn.SpawnMonster(player.transform, 0, true);
            if(cnt < 3)
                cnt++;
        }
    }

    public override void Clear()
    {
    }
}
