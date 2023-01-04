using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();
    public Dictionary<string, Data.MonsterStat> MonsterStatDict { get; private set; } = new Dictionary<string, Data.MonsterStat>();
    public Dictionary<int, Data.SkillStat> SkillStatDict { get; private set; } = new Dictionary<int, Data.SkillStat>();

    public void Init()
    {
        StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
        MonsterStatDict = LoadJson<Data.MonsterStatData, string, Data.MonsterStat>("MonsterStatData").MakeDict();
        SkillStatDict = LoadJson<Data.SkillStatData, int, Data.SkillStat>("SkillData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
