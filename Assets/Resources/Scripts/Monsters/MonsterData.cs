using UnityEngine;

public enum AdvantageType
{
    None,
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
    [Tooltip("ID único de este monstruo (debe ser distinto para cada uno)")]
    public int Monster_ID;
    [Tooltip("Nombre del monstruo para mostrar en UI / debug")]
    public string Monster_Name;

    [Header("Progresión y Spawn")]
    [Tooltip("Cantidad de este monstruo que el jugador debe matar para que deje de spawnear")]
    public int Amount_Monster_NeedKill;
    [Tooltip("Indica si el monstruo puede aparecer actualmente en el spawn")]
    public bool Can_Spawn = true;
    [Tooltip("Otro monstruo que debe matarse antes para desbloquear este")]
    public MonsterData Other_Monster_Kill;
    [Tooltip("Cantidad de kills necesarias del otro monstruo para desbloquear este")]
    public int Other_Monster_Kill_Amount;

    [Header("Combate")]
    [Tooltip("Vida base del monstruo")]
    public int Monster_HP = 10;
    [Tooltip("Velocidad base del monstruo")]
    public float Monster_Speed = 1f;
    [Tooltip("Tiempo de vida del monstruo antes de autodestruirse (segundos)")]
    public float Monster_LifeTime = 20f;

    [Header("Economía")]
    [Tooltip("Dinero que da al morir")]
    public int Money_Amount_Monster = 5;
    [Tooltip("Coste para comprarlo en la 'pecera'")]
    public int Cost_Monster = 10;

    [Header("Visuales")]
    [Tooltip("Sprite que representa al monstruo en juego")]
    public Sprite Monster_Sprite;

    [Header("Ventajas en pecera")]
    [Tooltip("Si está en la pecera, qué bonus da")]
    public AdvantageType Advantage_Type = AdvantageType.None;
    [Tooltip("Valor numérico del bonus")]
    public float Advantage_Value = 0f;
    [Tooltip("Si se puede meter este monstruo en la pecera")]
    public bool Can_Monster_House = false;
    [Tooltip("Si actualmente está en la pecera del jugador")]
    public bool Is_In_Monster_House = false;

    [Header("Tienda y rareza")]
    [Tooltip("Rareza para control de progresión y UI")]
    public MonsterRarity Monster_Rarity = MonsterRarity.Common;
    [TextArea]
    [Tooltip("Descripción para la tienda o enciclopedia")]
    public string Monster_Description;

    [Header("Prefabs")]
    [Tooltip("Prefab del monstruo 'malo' que ataca al jugador")]
    public GameObject Monster_Prefab_Evil;
    [Tooltip("Prefab del monstruo 'bueno' que estará en la pecera")]
    public GameObject Monster_Prefab_Good;
}