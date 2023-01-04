using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region Stat
    [Serializable]
    public class Stat
    {
        public int level;
        public int maxHp;
        public int totalExp;
    }

    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
            foreach (Stat stat in stats)
                dict.Add(stat.level, stat);

            return dict;
        }
    }
    #endregion
    #region MonsterStat
    [Serializable]
    public class MonsterStat
    {
        public string name;
        public int hp;
        public int speed;
        public int attack;
        public string pattern;
    }

    [Serializable]
    public class MonsterStatData : ILoader<string, MonsterStat>
    {
        public List<MonsterStat> stats = new List<MonsterStat>();

        public Dictionary<string, MonsterStat> MakeDict()
        {
            Dictionary<string, MonsterStat> dict = new Dictionary<string, MonsterStat>();
            foreach (MonsterStat stat in stats)
                dict.Add(stat.name, stat);

            return dict;
        }
    }
    #endregion

    #region Skill
    [Serializable]
    public class SkillStat
    {
        public int skillno;
        public int damage;
        public float timetoattack;
        public float speed;
        public float knockback;
        public float projectilecount;
        public float size;
        public string ex;
    }

    [Serializable]
    public class SkillStatData : ILoader<int, SkillStat>
    {
        public List<SkillStat> stats = new List<SkillStat>();

        public Dictionary<int, SkillStat> MakeDict()
        {
            Dictionary<int, SkillStat> dict = new Dictionary<int, SkillStat>();
            foreach (SkillStat stat in stats)
                dict.Add(stat.skillno, stat);

            return dict;
        }
    }
    #endregion
}
