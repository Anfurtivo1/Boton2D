using System.Collections.Generic;
using UnityEngine;

public class MonsterHouseManager : MonoBehaviour
{

    public List<MonsterData> House_Current_Monsters;
    public int House_Max_Slots;

    public List<GameObject> House_Monster_Prefabs;

    public Player House_Player;

    public Dictionary<AdvantageType, float> House_Active_Advantages;

    public GameManager House_GameManager;

    public GameObject House_UI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Player.playerInstance != null)
        {
            House_Player = Player.playerInstance;
        }

        if (GameManager.Instance != null)
        {
            House_GameManager = GameManager.Instance;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
