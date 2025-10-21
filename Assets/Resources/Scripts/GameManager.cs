using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Ajustes del juego")]
    public int Money_Amount = 0;

    [Tooltip("Diccionario que almacena la cantidad de enemigos asesinados por ID de monstruo")]
    public Dictionary<int, int> MonsterKills = new Dictionary<int, int>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("userFirstTimeExperience"))//Ya tenía datos guardados, estos datos se guardan al acabar cada run
        {
            Guardado.instance.CargarDatos();
        }
    }
        

    #region --- Dinero ---
    public void AddMoney(int amount)
    {
        Money_Amount += amount;
        Debug.Log("Dinero actual: " + Money_Amount);
    }

    public bool SpendMoney(int amount)
    {
        if (Money_Amount >= amount)
        {
            Money_Amount -= amount;
            return true;
        }
        return false;
    }
    #endregion

    #region --- Kills ---
    public void AddMonsterKill(int monsterID) //Añado una kill al matar ese monstruo 
    {
        if (MonsterKills.ContainsKey(monsterID))
        {
            MonsterKills[monsterID]++;
        }
        else
        {
            MonsterKills.Add(monsterID, 1);
        }

        Debug.Log($"Monstruo {monsterID} asesinados: {MonsterKills[monsterID]}");
    }
    public int GetMonsterKills(int monsterID) //Me devuelve el número de kills que llevo
    {
        if (MonsterKills.ContainsKey(monsterID))
            return MonsterKills[monsterID];
        return 0;
    }
    #endregion

    #region --- Daño al jugador ---
    public void DamagePlayer(int damage)
    {
        Player.playerInstance.player_HP -= damage;

        if (Player.playerInstance.player_HP <= 0)
        {
            Player.playerInstance.player_HP = 0;
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        Debug.Log("Has muerto, Panoli");
    }
    #endregion
}