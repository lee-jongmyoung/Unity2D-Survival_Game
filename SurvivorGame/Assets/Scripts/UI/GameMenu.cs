using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField]
    GameObject panel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(panel.activeInHierarchy == false)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
            }
        }
    }

    public void CloseMenu()
    {
        Managers.Pause.UnPauseGame();
        panel.SetActive(false);
    }

    public void OpenMenu()
    {
        Managers.Pause.PauseGame();
        panel.SetActive(true);
    }
}
