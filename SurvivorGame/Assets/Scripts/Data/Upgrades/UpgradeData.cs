using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class UpgradeData : ScriptableObject
{
    public Define.UpgradeType upgradeType;
    public int No;
    public Sprite icon;
}
