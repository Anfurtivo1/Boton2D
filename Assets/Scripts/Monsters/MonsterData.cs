using UnityEngine;

public enum AdvantageType
{
    FasterFire,
    BulletBounce,
    Shield,
    ExtraLife,
    MoneyBoost
}

public enum MonsterRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}

[CreateAssetMenu(fileName = "NewMonster", menuName = "Monsters/Monster Data")]
public class MonsterData : ScriptableObject
{
    [Header("Identificación")]
    public int Monster_ID;

    [Header("Progresión")]
    //public int Amount_Monster_Killed;
    public int Amount_Monster_NeedKill;
    public bool Is_In_Monster_House;
    public bool Can_Monster_House;

    [Header("Ventajas")]
    public AdvantageType Advantage_Type;
    public float Advantage_Value;

    [Header("Spawn y combate")]
    public bool Can_Spawn;
    public int Monster_HP;
    public float Monster_Speed;

    [Header("Economía")]
    public int Money_Amount_Monster;
    public int Cost_Monster;

    [Header("Visuales")]
    public Sprite Monster_Sprite;

    [Header("Tienda")]
    public MonsterRarity Monster_Rarity;
    [TextArea] public string Monster_Description;

    [Header("Condiciones de Aparición")]
    public MonsterData Other_Monster_Kill;
    public int Other_Monster_Kill_Amount;
}

