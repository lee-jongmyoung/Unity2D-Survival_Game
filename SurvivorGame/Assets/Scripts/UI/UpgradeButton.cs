using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Image icon;

    private void Awake()
    {
        icon = Util.FindChild<Image>(gameObject, "Image", true);
    }

    public void Set(UpgradeData upgradeData)
    {
        icon.sprite = upgradeData.icon;
    }
}
