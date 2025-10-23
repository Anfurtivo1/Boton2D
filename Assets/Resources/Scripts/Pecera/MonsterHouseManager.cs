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

    public ShopItemDisplayFull shopManager;

    public List<GameObject> monstersSpawned;

    // Límites de spawn (los mismos que el BouncingObject)
    public float minX = -2f;
    public float maxX = 2f;
    public float minY = -14f;
    public float maxY = -6f;

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


    public void SpawnMonster(GameObject monsterPrefab)
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(randomX, randomY, 0f);

        GameObject newMonster = Instantiate(monsterPrefab, spawnPos, Quaternion.identity);

        // Puedes configurar los límites del BouncingObject del nuevo monstruo
        BouncingObject bounce = newMonster.GetComponent<BouncingObject>();
        if (bounce != null)
        {
            bounce.minX = minX;
            bounce.maxX = maxX;
            bounce.minY = minY;
            bounce.maxY = maxY;
        }

        monstersSpawned.Add(newMonster);

        Debug.Log($"Spawned {newMonster.name} en {spawnPos}");
    }



    public void DespawnMonster(List<GameObject> monsters)
    {
        foreach (var item in monsters)
        {
            Object.Destroy(item);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void cargarMonstruosPecera()
    {
        foreach (var item in shopManager.Shop_Bought_Monsters)
        {
            switch (item.Monster_ID)
            {
                case 1:
                    House_Monster_Prefabs[item.Monster_ID-1].gameObject.SetActive(true);
                    SpawnMonster(House_Monster_Prefabs[item.Monster_ID-1]);

                    Debug.Log($"El {item.Monster_Name} tiene ID 1 → hacer acción A");
                    Player.playerInstance.bullet_speed = Player.playerInstance.bullet_speed * 2;
                    break;

                case 2:
                    House_Monster_Prefabs[item.Monster_ID-1].gameObject.SetActive(true);
                    SpawnMonster(House_Monster_Prefabs[item.Monster_ID - 1]);

                    Debug.Log($"El {item.Monster_Name} tiene ID 2 → hacer acción B");
                    Player.playerInstance.player_AttackRate = Player.playerInstance.player_AttackRate / 0.75f;
                    break;

                case 3:
                    House_Monster_Prefabs[item.Monster_ID-1].gameObject.SetActive(true);
                    SpawnMonster(House_Monster_Prefabs[item.Monster_ID - 1]);

                    Debug.Log($"El {item.Monster_Name} tiene ID 3 → hacer acción C");
                    //+1 corazon
                    LifeManager.maxLives = LifeManager.maxLives + 1;
                    LifeManager.lifeImages[3].gameObject.SetActive( true );
                    break;
                case 4:
                    House_Monster_Prefabs[item.Monster_ID - 1].gameObject.SetActive(true);
                    SpawnMonster(House_Monster_Prefabs[item.Monster_ID - 1]);

                    Debug.Log($"El {item.Monster_Name} tiene ID 4 → hacer acción D");
                    Player.playerInstance.player_AttackRate = Player.playerInstance.player_AttackRate / 0.75f;
                    break;

                case 5:
                    House_Monster_Prefabs[item.Monster_ID - 1].gameObject.SetActive(true);
                    SpawnMonster(House_Monster_Prefabs[item.Monster_ID - 1]);

                    Debug.Log($"El {item.Monster_Name} tiene ID 5 → hacer acción E");
                    //LifeManager.maxLives = LifeManager.maxLives + 1;
                    LifeManager.maxLives = LifeManager.maxLives + 1;
                    LifeManager.lifeImages[4].gameObject.SetActive(true);
                    break;

                case 6:
                    House_Monster_Prefabs[item.Monster_ID - 1].gameObject.SetActive(true);
                    SpawnMonster(House_Monster_Prefabs[item.Monster_ID - 1]);

                    Debug.Log($"El {item.Monster_Name} tiene ID 6 → hacer acción F");
                    Player.playerInstance.mejoraDinero1 = true;
                    break;
                case 7:
                    House_Monster_Prefabs[item.Monster_ID - 1].gameObject.SetActive(true);
                    SpawnMonster(House_Monster_Prefabs[item.Monster_ID - 1]);

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
