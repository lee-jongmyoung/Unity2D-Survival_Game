using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField] EXPBar expBar;

    [SerializeField] LevelUPMenu levelUPMenu;

    [SerializeField] GameOver gameOver;

    [SerializeField] List<UpgradeData> upgrades;
    List<UpgradeData> selectedUpgrades;
    PlayerController pc;
    GameObject playerHit;
    public AudioClip _audioClip;

    [SerializeField]
    public int _level;
    [SerializeField]
    public int _hp;
    [SerializeField]
    public int _maxHp;
    [SerializeField]
    public int _attack;
    [SerializeField]
    public float _moveSpeed;
    [SerializeField]
    public int _exp;

    public int Level { get { return _level; } set { _level = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp 
    {
        get 
        {
            return _maxHp; 
        }
        set 
        {
            _maxHp = value;

            Hp = value;
        }
    }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    public int Exp 
    {
        get 
        {
            return _exp; 
        }
        set 
        {
            _exp = value;
            expBar.UpdateEXPBar(value);
            // 레벨업 체크

            int level = Level;
            while (true)
            {
                Data.Stat stat;
                if (Managers.Data.StatDict.TryGetValue(level + 1, out stat) == false)
                    break;
                if (_exp < stat.totalExp)
                    break;
                if (Level != level)
                    break;
                level++;
            }

            if(level != Level)
            {
                List<UpgradeData> upgradeList = new List<UpgradeData>();
                upgradeList = GetUpgrades();

                if (selectedUpgrades == null) 
                    selectedUpgrades = new List<UpgradeData>();

                selectedUpgrades.Clear();
                selectedUpgrades.AddRange(upgradeList);

                Level = level;
                SetStat(Level);
                levelUPMenu.OpenMenu(upgradeList);
            }

        }
    }

    private void Start()
    {
        _level = 1;
        _hp = 100;
        _maxHp = 100;
        _exp = 0;
        _moveSpeed = 3.0f;

        pc = GetComponent<PlayerController>();

        SetStat(_level);
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Exp += 5;
        }
    }

    public void Upgrade(int selectedUpgradeId) 
    {
        UpgradeData upgradeData = selectedUpgrades[selectedUpgradeId];
        if(upgradeData.name == "HP")
        {
            MaxHp += 10;
            return;
        }

        // 현재 가지고 있는 스킬이 아니면 Add
        if (Managers.Skill.skillDict.TryGetValue(upgradeData.No, out int level) == false)
        {
            Managers.Skill.AddSkill(upgradeData.No, upgradeData.name, 1);
        }
        // 현재 가지고 있는 스킬이면 Upgrade
        else
        {
            Managers.Skill.UpgradeSkill(upgradeData.No, upgradeData.name, level+1);

            // 스킬레벨이 10이면 더이상 업그레이드 리스트에 나오지 않는다.
            if (level + 1 == 10)
                upgrades.Remove(upgradeData);
        }

    }

    public void SetStat(int level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        Data.Stat stat = dict[level];
        Data.Stat stat1 = dict[level+1];

        _hp = MaxHp;
        _maxHp = MaxHp;
        _exp = 0;
        expBar.UpdateEXPBar(0);
        expBar.UpdateTotalEXPBar(stat1.totalExp);
        expBar.UpdateLevel(level);
    }

    public void OnAttacked(int damage)
    {
        if (pc.isDashing) return;
        Hp -= damage;
        playerHit = Managers.Resource.Instantiate("Player/PlayerHit", transform);
        Managers.Sound.Play(_audioClip, Define.Sound.Effect, 1.0f, 0.3f);
        Destroy(playerHit, 0.4f);

        if(Hp <= 0)
        {
            OnDead();
        }
    }

    public void OnDead()
    {
        gameOver.OpenMenu();
    }

    public List<UpgradeData> GetUpgrades()
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();
        List<int> dupCheck = new List<int>();
        int count = 0;
        int maxCount = 3;


        if (upgrades.Count < 4)
        {
            int cnt = 4 - upgrades.Count;
            for (int i = 0; i < cnt; i++)
            {
                int idx = upgrades.FindIndex(a => a.upgradeType == Define.UpgradeType.ItemUpgrade);
                upgradeList.Add(upgrades[idx]);
                maxCount--;
            }

        }

        while (count < maxCount)
        {
            int upgradeNumber = Random.Range(0, upgrades.Count);
            if (upgrades[upgradeNumber].upgradeType == Define.UpgradeType.WeaponUpgrade)
            {
                if (!dupCheck.Contains(upgradeNumber))
                {
                    upgradeList.Add(upgrades[upgradeNumber]);
                    dupCheck.Add(upgradeNumber);
                    count++;
                }
            }
        }

        return upgradeList;
    }
}
