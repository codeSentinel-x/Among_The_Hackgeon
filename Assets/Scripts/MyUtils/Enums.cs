namespace MyUtils.Enums {
    public enum ObstacleType{
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
        AlwaysOpen,
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

}