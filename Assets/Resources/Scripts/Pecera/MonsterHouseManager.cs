using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Enemy
{
    Unborn,
    Elemental,
    Vapor,
    Jellypus,
    Mimic,
    Siren,
    Teratoma
}

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

    //// Límites de spawn (los mismos que el BouncingObject)
    //public float minX = -2f;
    //public float maxX = 2f;
    //public float minY = -14f;
    //public float maxY = -6f;

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


    public void MostrarMonster(GameObject monsterPrefab)
    {

        monsterPrefab.SetActive(true);

        //float randomX = Random.Range(minX, maxX);
        //float randomY = Random.Range(minY, maxY);
        //Vector3 spawnPos = new Vector3(randomX, randomY, 0f);

        //GameObject newMonster = Instantiate(monsterPrefab, spawnPos, Quaternion.identity);

        //// Puedes configurar los límites del BouncingObject del nuevo monstruo
        //BouncingObject bounce = newMonster.GetComponent<BouncingObject>();
        //if (bounce != null)
        //{
        //    bounce.minX = minX;
        //    bounce.maxX = maxX;
        //    bounce.minY = minY;
        //    bounce.maxY = maxY;
        //}

        //monstersSpawned.Add(newMonster);

        //Debug.Log($"Spawned {newMonster.name} en {spawnPos}");
    }



    //public void DespawnMonster(List<GameObject> monsters)
    //{
    //    foreach (var item in monsters)
    //    {
    //        Object.Destroy(item);
    //    }
    //}
    public void OcultarMonster(List<GameObject> monsters)
    {
        foreach (var item in monsters)
        {
            item.SetActive(false);
        }
    }

    public void MejorasManager(string nombreMonstruo,bool quitarPonerMejoras)
    {
        if (quitarPonerMejoras)//Si quieres quitarlas es true, si quieres ponerlas es false
        {
            switch (nombreMonstruo)//Se quitan
            {
                case "Unborn":
                    Debug.Log($"El " + nombreMonstruo + " tiene ID 1 → quita acción A");
                    Player.playerInstance.bullet_speed = Player.playerInstance.bullet_speed / 2;
                    break;

                case "Elemental":
                    Debug.Log($"El " + nombreMonstruo + "  tiene ID 2 → quita acción B");
                    Player.playerInstance.player_AttackRate = Player.playerInstance.player_AttackRate * 0.75f;
                    break;
                case "Vapor":
                    Debug.Log($"El " + nombreMonstruo + " tiene ID 3 → quita acción C");
                    LifeManager.maxLives = LifeManager.maxLives + 1;
                    LifeManager.lifeImages[3].gameObject.SetActive(true);
                    break;

                case "Jellypus":
                    Debug.Log($"El " + nombreMonstruo + " tiene ID 4 → quita acción D");
                    Player.playerInstance.player_AttackRate = Player.playerInstance.player_AttackRate * 0.75f;
                    break;
                case "Mimic":
                    Debug.Log($"El " + nombreMonstruo + " tiene ID 5 → quita acción E");
                    LifeManager.maxLives = LifeManager.maxLives - 1;
                    LifeManager.lifeImages[4].gameObject.SetActive(true);
                    break;

                case "Siren":
                    Debug.Log($"El " + nombreMonstruo + " tiene ID 6 → quita acción F");
                    Player.playerInstance.mejoraDinero1 = false;
                    break;
                case "Teratoma":
                    Debug.Log($"El " + nombreMonstruo + " tiene ID 7 → quita acción G");
                    Player.playerInstance.mejoraDinero2 = false;
                    break;

                default:
                    break;
            }
        }

        if (!quitarPonerMejoras)//Si quieres quitarlas es true, si quieres ponerlas es false
        {
            switch (nombreMonstruo)//Se ponen
            {
                case "Unborn":
                    Debug.Log($"El " + nombreMonstruo + " tiene ID 1 → hacer acción A");
                    Player.playerInstance.bullet_speed = Player.playerInstance.bullet_speed * 2;
                    break;

                case "Elemental":
                    Debug.Log($"El " + nombreMonstruo + "  tiene ID 2 → hacer acción B");
                    Player.playerInstance.player_AttackRate = Player.playerInstance.player_AttackRate / 0.75f;
                    break;
                case "Vapor":
                    Debug.Log($"El " + nombreMonstruo + " tiene ID 3 → hacer acción C");
                    LifeManager.maxLives = LifeManager.maxLives + 1;
                    LifeManager.lifeImages[3].gameObject.SetActive(true);
                    break;

                case "Jellypus":
                    Debug.Log($"El " + nombreMonstruo + " tiene ID 4 → hacer acción D");
                    Player.playerInstance.player_AttackRate = Player.playerInstance.player_AttackRate / 0.75f;
                    break;
                case "Mimic":
                    Debug.Log($"El " + nombreMonstruo + " tiene ID 5 → hacer acción E");
                    LifeManager.maxLives = LifeManager.maxLives + 1;
                    LifeManager.lifeImages[4].gameObject.SetActive(true);
                    break;

                case "Siren":
                    Debug.Log(  $"El " + nombreMonstruo + " tiene ID 6 → hacer acción F");
                    Player.playerInstance.mejoraDinero1 = true;
                    break;
                case "Teratoma":
                    Debug.Log($"El " + nombreMonstruo + " tiene ID 7 → hacer acción G");
                    Player.playerInstance.mejoraDinero2 = true;
                    break;

                default:
                    break;
            }
        }
        
    }

    public void cargarMonstruosPecera()
    {
        foreach (var item in shopManager.Shop_Bought_Monsters)
        {
            switch (item.Monster_ID)
            {
                case 1:
                    House_Monster_Prefabs[item.Monster_ID-1].gameObject.SetActive(true);
                    MostrarMonster(House_Monster_Prefabs[item.Monster_ID-1]);

                    MejorasManager(Enemy.Unborn.ToString(),false);

                    //Debug.Log($"El {item.Monster_Name} tiene ID 1 → hacer acción A");
                    //Player.playerInstance.bullet_speed = Player.playerInstance.bullet_speed * 2;
                    break;

                case 2:
                    House_Monster_Prefabs[item.Monster_ID-1].gameObject.SetActive(true);
                    MostrarMonster(House_Monster_Prefabs[item.Monster_ID - 1]);

                    MejorasManager(Enemy.Elemental.ToString(), false);

                    //Debug.Log($"El {item.Monster_Name} tiene ID 2 → hacer acción B");
                    //Player.playerInstance.player_AttackRate = Player.playerInstance.player_AttackRate / 0.75f;
                    break;

                case 3:
                    House_Monster_Prefabs[item.Monster_ID-1].gameObject.SetActive(true);
                    MostrarMonster(House_Monster_Prefabs[item.Monster_ID - 1]);

                    MejorasManager(Enemy.Vapor.ToString(), false);

                    //Debug.Log($"El {item.Monster_Name} tiene ID 3 → hacer acción C");
                    ////+1 corazon
                    //LifeManager.maxLives = LifeManager.maxLives + 1;
                    //LifeManager.lifeImages[3].gameObject.SetActive( true );
                    break;
                case 4:
                    House_Monster_Prefabs[item.Monster_ID - 1].gameObject.SetActive(true);
                    MostrarMonster(House_Monster_Prefabs[item.Monster_ID - 1]);

                    MejorasManager(Enemy.Jellypus.ToString(), false);

                    //Debug.Log($"El {item.Monster_Name} tiene ID 4 → hacer acción D");
                    //Player.playerInstance.player_AttackRate = Player.playerInstance.player_AttackRate / 0.75f;
                    break;

                case 5:
                    House_Monster_Prefabs[item.Monster_ID - 1].gameObject.SetActive(true);
                    MostrarMonster(House_Monster_Prefabs[item.Monster_ID - 1]);

                    MejorasManager(Enemy.Mimic.ToString(), false);

                    //Debug.Log($"El {item.Monster_Name} tiene ID 5 → hacer acción E");
                    ////LifeManager.maxLives = LifeManager.maxLives + 1;
                    //LifeManager.maxLives = LifeManager.maxLives + 1;
                    //LifeManager.lifeImages[4].gameObject.SetActive(true);
                    break;

                case 6:
                    House_Monster_Prefabs[item.Monster_ID - 1].gameObject.SetActive(true);
                    MostrarMonster(House_Monster_Prefabs[item.Monster_ID - 1]);

                    MejorasManager(Enemy.Siren.ToString(), false);

                    //Debug.Log($"El {item.Monster_Name} tiene ID 6 → hacer acción F");
                    //Player.playerInstance.mejoraDinero1 = true;
                    break;
                case 7:
                    House_Monster_Prefabs[item.Monster_ID - 1].gameObject.SetActive(true);
                    MostrarMonster(House_Monster_Prefabs[item.Monster_ID - 1]);

                    MejorasManager(Enemy.Teratoma.ToString(), false);

                    //Debug.Log($"El {item.Monster_Name} tiene ID 7 → hacer acción G");
                    //Player.playerInstance.mejoraDinero1 = false;
                    //Player.playerInstance.mejoraDinero2 = true;
                    break;

                default:
                    Debug.Log($"El {item.Monster_Name} tiene un ID desconocido");
                    break;
            }
        }
    }

}
