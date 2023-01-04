using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    TextMeshProUGUI text;
    public float time;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        time += Time.deltaTime;

        int minutes = (int)(time / 60f);
        int seconds = (int)(time % 60f);
        text.text = minutes.ToString() + ":" + seconds.ToString("00");
    }
}
