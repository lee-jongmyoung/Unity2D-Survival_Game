using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (panel.activeInHierarchy == false)
            {
                OpenMenu();
            }
        }
    }

    public void MainMenu()
    {
        Managers.Scene.LoadScene(Define.Scene.Menu);
    }

    public void OpenMenu()
    {
        Managers.Pause.PauseGame();
        panel.SetActive(true);
    }
}
