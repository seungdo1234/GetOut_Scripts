public enum EEnemyType
{
    Default,
    Scout, //잡몹1
}

// Flight가 살아있는지 죽어있는지의 상태를 나타낼 enum
// >> 차후 여러 형태를 추가할 수 있을 것 같아 enum으로 사용
public enum EFlightStatus
{
    Alive,
    Dead,
}

public enum EPoolObjectType
{
    Default,
    Bullet,
    Bomb,
    Enemy,
    Effect,
    SpecialBullet,
    ConsumableItem
    
}

public enum EWeaponType
{
    Defalut,
    AutoCannon,
    Rockets,
    Zapper,
    BigSpaceGun
}

public enum EHeartType
{
    Full, Half, Empty
}

public enum Sfx { EnemyHit, FlightExplosion, PlayerHit, PlayerMove,  ItemCon, ItemEqu , Zapper } //적군 타격, 플레이어 공격, 플레이어 타격, 소비 아이템 장착, 무기 아이템 장착
public enum Bgm { StartBgm, MainBgm, BossBgm } //시작 브금, 메인 브금, 보스 등장 BGM

public enum EConsumableItemType
{
    Default,
    HealthUp,
    AttackUp,
    BulletCountUp
}
