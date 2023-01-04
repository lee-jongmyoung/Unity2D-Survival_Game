using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeEx : MonoBehaviour
{
    TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void Set(string text)
    {
        _text.text = text;
    }
}
