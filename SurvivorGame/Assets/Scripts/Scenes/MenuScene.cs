using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Menu;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }
    public override void Clear()
    {

    }
}
