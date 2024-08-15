namespace MyUtils.Enums {
    public enum RoomType {
        EnemyRoom,
        StartRoom,
        ResetRoom,
        BossRoom,
        ExitRoom,
        Tunnel,
    }
    public enum DoorOpenType {
        OpenOnShoot,
        OpenOnBlank,
        OpenOnDestroyAllItemOfType,
        OpenOnTime,
        OpenOnButtonClick,
        OpenOnPlayerStat,
        OpenOnDefeatEnemy,
        OpenOnCustomItemHold,
    }
    public enum DoorState {
        Hidden,
        Visible,
        Opened,
    }

}