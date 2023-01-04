using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUPMenu : MonoBehaviour
{
    [SerializeField] GameObject panel;

    [SerializeField] PlayerStat player;

    [SerializeField] List<UpgradeButton> upgradeButtons;
    [SerializeField] List<UpgradeName> upgradeNames;
    [SerializeField] List<UpgradeEx> upgradeExs;

    public void OpenMenu(List<UpgradeData> upgradeDatas)
    {
        //Clear();
        Managers.Pause.PauseGame();
        panel.SetActive(true);

        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            Managers.Skill.skillDict.TryGetValue(upgradeDatas[i].No, out int level);

            Dictionary<int, Data.SkillStat> dict = Managers.Data.SkillStatDict;

            upgradeNames[i].Set(upgradeDatas[i].name);
            upgradeExs[i].Set(dict[upgradeDatas[i].No + 1 + level].ex);
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Set(upgradeDatas[i]);
        }

    }

    public void Upgrade(int pressedButtonID)
    {
        player.GetComponent<PlayerStat>().Upgrade(pressedButtonID);
        CloseMenu();
    }

    public void CloseMenu()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(false);
        }

        Managers.Pause.UnPauseGame();
        panel.SetActive(false);
    }

}
