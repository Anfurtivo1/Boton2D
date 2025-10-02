using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Ajustes del juego")]
    public int Money_Amount = 0;
    public Dictionary<int, int> MonsterKills = new Dictionary<int, int>(); //Diccionario de kills por ID de monstruo

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
    public void RegisterKill(int monsterID)
    {
        if (MonsterKills.ContainsKey(monsterID))
        {
            MonsterKills[monsterID]++;
        }
        else
        {
            MonsterKills.Add(monsterID, 1);
        }

        Debug.Log("Monstruo " + monsterID + " asesinados: " + MonsterKills[monsterID]);
    }
    public int GetMonsterKills(int monsterID)
    {
        if (MonsterKills.ContainsKey(monsterID))
            return MonsterKills[monsterID];
        return 0;
    }

    public void DamagePlayer(int damage)
    {
        Player.playerInstance.player_HP -= damage;
        if (Player.playerInstance.player_HP  <= 0)
        {
            Player.playerInstance.player_HP  = 0;
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        Debug.Log("Perdiste Panoli");
    }


}
