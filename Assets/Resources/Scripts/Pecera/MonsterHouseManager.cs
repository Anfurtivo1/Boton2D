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

    public LifeManager LifeManager;

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

    public void SpawnMonster()
    {
        //for (int i = 0; i < length; i++)
        //{
        //    Camera cam = Camera.main;
        //    Vector3 randomViewport = new Vector3(Random.value, Random.value, 0);
        //    Vector3 spawnPos = cam.ViewportToWorldPoint(randomViewport);
        //    spawnPos.z = 0f;
        //    Instantiate(prefab, spawnPos, Quaternion.identity);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cargarMonstruosPecera()
    {
        foreach (var item in ShopItemDisplayFull.Instance.Shop_Bought_Monsters)
        {
            switch (item.Monster_ID)
            {
                case 1:
                    Debug.Log($"El {item.Monster_Name} tiene ID 1 → hacer acción A");
                    Player.playerInstance.bullet_speed = Player.playerInstance.bullet_speed * 2;
                    break;

                case 2:
                    Debug.Log($"El {item.Monster_Name} tiene ID 2 → hacer acción B");
                    Player.playerInstance.player_AttackRate = Player.playerInstance.player_AttackRate / 0.75f;
                    break;

                case 3:
                    Debug.Log($"El {item.Monster_Name} tiene ID 3 → hacer acción C");
                    //+1 corazon
                    LifeManager.maxLives = LifeManager.maxLives + 1;
                    break;
                case 4:
                    Debug.Log($"El {item.Monster_Name} tiene ID 4 → hacer acción D");
                    Player.playerInstance.player_AttackRate = Player.playerInstance.player_AttackRate / 0.75f;
                    break;

                case 5:
                    Debug.Log($"El {item.Monster_Name} tiene ID 5 → hacer acción E");
                    //LifeManager.maxLives = LifeManager.maxLives + 1;
                    Player.playerInstance.bullet_Damage = Player.playerInstance.bullet_Damage + 1;
                    break;

                case 6:
                    Debug.Log($"El {item.Monster_Name} tiene ID 6 → hacer acción F");
                    Player.playerInstance.mejoraDinero1 = true;
                    break;
                case 7:
                    Debug.Log($"El {item.Monster_Name} tiene ID 7 → hacer acción G");
                    Player.playerInstance.mejoraDinero1 = false;
                    Player.playerInstance.mejoraDinero2 = true;
                    break;

                default:
                    Debug.Log($"El {item.Monster_Name} tiene un ID desconocido");
                    break;
            }
        }
    }

}
