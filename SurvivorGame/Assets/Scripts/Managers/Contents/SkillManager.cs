using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager
{
    public Dictionary<int, int> skillDict = new Dictionary<int, int>();
    public List<IEnumerator> coroutines;

    // �ű� ��ų add
    public void AddSkill(int skillNo, string skillName, int skillLevel)
    {
        GameObject skills = GameObject.Find("GetSkills");
        GameObject skillGo = Managers.Resource.Instantiate($"Skills/{skillName}/{skillName}", skills.transform);

        skillDict.Add(skillNo, skillLevel);
        Dictionary<int, Data.SkillStat> dict = Managers.Data.SkillStatDict;
        skillGo.GetComponent<SkillBase>().SetData(dict[skillNo + skillLevel]);
    }

    // ���� ��ų upgrade
    public void UpgradeSkill(int skillNo, string skillName, int skillLevel)
    {
        skillDict[skillNo] = skillLevel;
        GameObject upgradeTarget  = GameObject.Find(skillName);

        Dictionary<int, Data.SkillStat> dict = Managers.Data.SkillStatDict;
        upgradeTarget.GetComponent<SkillBase>().SetData(dict[skillNo + skillLevel]);

        //��ų ������ 10�϶� ����
        if (skillLevel == 10)
            upgradeTarget.GetComponent<SkillBase>().isAwake = true;
    }

    public void Clear()
    {
        skillDict.Clear();
    }


}
