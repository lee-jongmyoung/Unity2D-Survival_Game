using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{

    PlayerStat _stat;

    public Image HPBarFilled;

    void Start()
    {
        _stat = transform.parent.GetComponent<PlayerStat>();
        HPBarFilled = Util.FindChild<Image>(gameObject, "HPBarFilled", true);
        HPBarFilled.fillAmount = 1f;
    }

    void Update()
    {
        float ratio = _stat.Hp / (float)_stat.MaxHp;
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        HPBarFilled.fillAmount = ratio;
    }
}
