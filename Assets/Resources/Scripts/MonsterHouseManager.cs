using System.Collections.Generic;
using UnityEngine;

public class MonsterHouseManager : MonoBehaviour
{

    List<MonsterData> House_Current_Monsters;
    int House_Max_Slots;

    List<GameObject> House_Monster_Prefabs;

    Player House_Player;

    Dictionary<AdvantageType, float> House_Active_Advantages;

    GameManager House_GameManager;

    GameObject House_UI;

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
