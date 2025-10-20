using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Puntos donde pueden aparecer enemigos")]
    public GameObject[] Spawner_Place_Posibility = new GameObject[4];

    [Header("Datos de monstruos")]
    public MonsterData[] All_Spawner_Monster_Posibility;         
    private List<MonsterData> Spawner_Monster_Posibility = new List<MonsterData>(); 

    [Header("Tiempos de spawn")]
    [Tooltip("Tiempo actual entre spawns")]
    public float Spawn_Rate = 5f;

    [Tooltip("Cada cuanto tiempo se reduce el Spawn_Rate")]
    public float Time_To_Change = 15f;

    [Tooltip("Reducción de spawn rate cada intervalo")]
    public float SpawnRateReduction = 0.2f;

    [Tooltip("Spawn rate mínimo")]
    public float Min_Spawn_Rate = 1f;

    private float timeToChangeTimer;

    private void Start()
    {
        timeToChangeTimer = Time_To_Change;

        if (All_Spawner_Monster_Posibility.Length > 0)
        {
            if (All_Spawner_Monster_Posibility[0].Can_Spawn)
                Spawner_Monster_Posibility.Add(All_Spawner_Monster_Posibility[0]);
        }

        
    }

    public void StartGame() 
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Spawn_Rate);

            //Actualizo mi loop con los posibles monstruos
            UpdateSpawnableMonsters();

            //Y luego spawneo uno de los posibles
            if (Spawner_Monster_Posibility.Count > 0)
            {
                int randomMonsterIndex = Random.Range(0, Spawner_Monster_Posibility.Count);
                MonsterData selectedMonster = Spawner_Monster_Posibility[randomMonsterIndex];

                int randomPlaceIndex = Random.Range(0, Spawner_Place_Posibility.Length);
                GameObject spawnPoint = Spawner_Place_Posibility[randomPlaceIndex];

                //Y generamos al monstruo con el prefab evil
                GameObject newEnemy = Instantiate(
                    selectedMonster.Monster_Prefab_Evil,
                    spawnPoint.transform.position,
                    spawnPoint.transform.rotation
                );


                //Añade automáticamente el fade in
                MonsterFadeIn fade = newEnemy.AddComponent<MonsterFadeIn>();
                fade.fadeDuration = 1.2f; // o el tiempo que quieras

                //Le pasamos datos al enemy controller
                EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    enemyController.Initialize(selectedMonster);
                }
            }

            UpdateSpawnRate();
        }
    }

    private void UpdateSpawnableMonsters()
    {
        foreach (MonsterData monster in All_Spawner_Monster_Posibility)
        {
            //Aqui desbloqueo en caso de que uno ya puedo spawnear
            if (!Spawner_Monster_Posibility.Contains(monster))
            {
                bool canUnlock = false;

                if (monster.Other_Monster_Kill == null)
                {
                    //Si no necesito matar a uno antes para que spawnee, hago que se genere de base
                    canUnlock = monster.Can_Spawn;
                }
                else
                {
                    //Si requiere matar a otro, compruebo su progreso
                    int otherID = monster.Other_Monster_Kill.Monster_ID;
                    int kills = GameManager.Instance.MonsterKills.ContainsKey(otherID)
                        ? GameManager.Instance.MonsterKills[otherID]
                        : 0;

                    if (kills >= monster.Other_Monster_Kill_Amount)
                        canUnlock = true;
                }

                if (canUnlock)
                {
                    Spawner_Monster_Posibility.Add(monster);
                    monster.Can_Spawn = true;
                    Debug.Log($"[SPAWNER] Se desbloqueó el monstruo: {monster.name}");
                }
            }

            //Desactivo si ya se mataron los suficientes de ese tipo
            int selfKills = GameManager.Instance.MonsterKills.ContainsKey(monster.Monster_ID)
                ? GameManager.Instance.MonsterKills[monster.Monster_ID]
                : 0;

            if (selfKills >= monster.Amount_Monster_NeedKill && Spawner_Monster_Posibility.Contains(monster))
            {
                Spawner_Monster_Posibility.Remove(monster);
                monster.Can_Spawn = false;
                Debug.Log($"[SPAWNER] Se desactivó el spawn de {monster.name} (se alcanzó {selfKills}/{monster.Amount_Monster_NeedKill})");
            }
        }
    }

    private void UpdateSpawnRate()
    {
        timeToChangeTimer -= Spawn_Rate;
        if (timeToChangeTimer <= 0f)
        {
            timeToChangeTimer = Time_To_Change;

            Spawn_Rate = Mathf.Max(Spawn_Rate - SpawnRateReduction, Min_Spawn_Rate);
            Debug.Log($"[SPAWNER] Nuevo spawn rate: {Spawn_Rate}s");
        }
    }
}