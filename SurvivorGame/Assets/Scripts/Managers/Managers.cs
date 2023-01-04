using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    #region Contents
    MapManager _map = new MapManager();
    ObjectManager _object = new ObjectManager();
    SkillManager _skill = new SkillManager();
    MessageManager _message = new MessageManager();
    SpawnManager _spawn = new SpawnManager();

    public static MapManager Map { get { return Instance._map; } }
    public static ObjectManager Object { get { return Instance._object; } }
    public static SkillManager Skill { get { return Instance._skill; } }
    public static MessageManager Message { get { return Instance._message; } }
    public static SpawnManager Spawn { get { return Instance._spawn; } }
    #endregion

    #region Core
    DataManager _data = new DataManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    PauseManager _pause = new PauseManager();

    public static DataManager Data { get { return Instance._data; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static PauseManager Pause { get { return Instance._pause; } }
    #endregion

    void Start()
    {
        Init();
    }

    void Update()
    {

    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._data.Init();
            s_instance._pool.Init();
            s_instance._sound.Init();
        }
    }

    public static void Clear()
    {
        Sound.Clear();
        Scene.Clear();
        Pool.Clear();
        Object.Clear();
        Skill.Clear();
        Pause.UnPauseGame();
    }
}
