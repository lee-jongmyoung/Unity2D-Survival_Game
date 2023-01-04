using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum UpgradeType
    {
        WeaponUpgrade,
        ItemUpgrade,
    }
    public enum PlayerState
    {
        Idle,
        Moving,
        Skill,
        Dead,
    }

    public enum MoveDir
    {
        None,
        Up,
        Down,
        Left,
        Right,
    }
    public enum Scene
    {
        Unknown,
        Menu,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }

    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }

    public enum CameraMode
    {
        QuarterView,
    }
}
