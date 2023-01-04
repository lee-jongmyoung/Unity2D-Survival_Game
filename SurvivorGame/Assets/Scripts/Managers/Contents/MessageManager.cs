using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageManager
{

    public void PostMessage(float damage, Vector3 worldPosition)
    {

        GameObject parent = GameObject.Find("@MESSAGE");

        GameObject go = Managers.Resource.Instantiate("UI/DamageText", parent.transform);
        go.transform.position = worldPosition + new Vector3(0, 0.5f, 0);
        TextMeshProUGUI tmpu = go.GetComponent<TextMeshProUGUI>();

        if (damage >= 100 && damage < 200)
            tmpu.color = new Color(1, 1, 0, 1);
        else if (damage >= 200)
            tmpu.color = new Color(1, 0, 0, 1);
        else
            tmpu.color = new Color(1, 1, 1, 1);

        tmpu.text = damage.ToString();

    }


}
