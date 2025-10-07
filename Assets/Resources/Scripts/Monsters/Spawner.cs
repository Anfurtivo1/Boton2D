using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] Spawner_Place_Posibility = new GameObject[4];
    List<MonsterData> Spawner_Monster_Posibility = new List<MonsterData>();
    MonsterData[] All_Spawner_Monster_Posibility;

    float Spawn_Rate = 5f;
    float Spawn_Rate_Changed = 5f;
    float Time_To_Change = 15f;
    float StopTime=5f;

    public GameObject prefabEnemigo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        All_Spawner_Monster_Posibility = new MonsterData[3];

        MonsterData monster0 = new MonsterData();
        MonsterData monster1 = new MonsterData();
        MonsterData monster2 = new MonsterData();

        All_Spawner_Monster_Posibility[0] = monster0;
        All_Spawner_Monster_Posibility[1] = monster1;
        All_Spawner_Monster_Posibility[2] = monster2;

        All_Spawner_Monster_Posibility[0].Can_Spawn = false;
        All_Spawner_Monster_Posibility[1].Can_Spawn = true;
        All_Spawner_Monster_Posibility[2].Can_Spawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSpawnRate();
        Spawn_Rate -= Time.deltaTime;

        //Debug.Log(" El tiempo es "+(int)Spawn_Rate);

        if (Spawn_Rate <= 0)
        {

            for (int i = 0; i < All_Spawner_Monster_Posibility.Length; i++)
            {
                if (All_Spawner_Monster_Posibility[i].Can_Spawn)
                {
                    int posicionRandom = Random.Range(0, Spawner_Place_Posibility.Length);

                    Debug.Log("La posicion random es: "+ posicionRandom);

                    Spawner_Monster_Posibility.Add(All_Spawner_Monster_Posibility [i]);
                    Instantiate(prefabEnemigo, Spawner_Place_Posibility[posicionRandom].transform.position, Spawner_Place_Posibility[posicionRandom].transform.rotation);



                    ///Debug.Log("Se ha creado un enemigo en: "+ Spawner_Place_Posibility[posicionRandom]);
                    //Debug.Log("Spawner lista tiene: "+ Spawner_Monster_Posibility.Count);
                    
                }
            }

            Spawn_Rate = Spawn_Rate_Changed;
            Debug.Log("La spawn rate es: " + Spawn_Rate);
        }

    }

    public void ChangeSpawnRate() 
    {
        Time_To_Change -= Time.deltaTime;
        if (Time_To_Change <= 0 && StopTime >= 1f) 
        {
            Spawn_Rate_Changed = Spawn_Rate_Changed - 0.2f;
            Time_To_Change = 15f;
            StopTime = Spawn_Rate_Changed;

        }
    }
}
