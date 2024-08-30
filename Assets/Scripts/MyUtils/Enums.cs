namespace MyUtils.Enums {
    public enum ObstacleType {
        Static,
        Pushable,
        PushableExploding,
        StaticExploding,
        PushableDestroyable,
        StaticDestroyable,

    }
    public enum RoomType {
        EnemyRoom,
        StartRoom,
        ResetRoom,
        BossRoom,
        ExitRoom,
        Tunnel,
    }
    public enum DoorOpenType {
        Normal,
        OpenOnShoot,
        OpenOnBlank,
        OpenOnDestroyAllItemOfType,
        OpenOnTime,
        OpenOnButtonClick,
        OpenOnPlayerStat,
        OpenOnDefeatEnemy,
        OpenOnCustomItemHold,
        BossRoom,
        BossRoomDoors,
    }
    public enum DoorState {
        Hidden,
        Visible,
        Opened,
    }
    public enum KeyBindType {
        MoveLeft,
        MoveRight,
        MoveUp,
        MoveDown,
        UseBlank,
        Dash,
        Shoot,
        Reload,
        ChangeWeapon,
        Loop,
        ShowHelp,
        ShowReadme,
        Pause,
        Interact,
        Restart,
        ExitToStartScreen,
        Exit,
    }
    public enum KeyPressMode {
        KeyDown,
        KeyUP,
        Both,
    }
}