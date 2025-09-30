using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    GameObject[] Spawner_Place_Posibility = new GameObject[4];
    List<MonsterData> Spawner_Monster_Posibility = new List<MonsterData>();
    MonsterData[] All_Spawner_Monster_Posibility;

    float Spawn_Rate = 5f;

    public GameObject prefabEnemigo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


        // Crear 4 vacíos en coordenadas fijas
        Spawner_Place_Posibility[0] = new GameObject("SpawnPoint0");
        Spawner_Place_Posibility[0].transform.position = new Vector3(0, 0, 0);

        Spawner_Place_Posibility[1] = new GameObject("SpawnPoint1");
        Spawner_Place_Posibility[1].transform.position = new Vector3(2, 0, 0);

        Spawner_Place_Posibility[2] = new GameObject("SpawnPoint2");
        Spawner_Place_Posibility[2].transform.position = new Vector3(-3, 0, 0);

        Spawner_Place_Posibility[3] = new GameObject("SpawnPoint3");
        Spawner_Place_Posibility[3].transform.position = new Vector3(5, 0, 0);


        All_Spawner_Monster_Posibility = new MonsterData[3];

        MonsterData monster0 = new MonsterData();
        MonsterData monster1 = new MonsterData();
        MonsterData monster2 = new MonsterData();

        All_Spawner_Monster_Posibility[0] = monster0;
        All_Spawner_Monster_Posibility[1] = monster1;
        All_Spawner_Monster_Posibility[2] = monster2;

        All_Spawner_Monster_Posibility[0].Can_Spawn = true;
        All_Spawner_Monster_Posibility[1].Can_Spawn = false;
        All_Spawner_Monster_Posibility[2].Can_Spawn = true;
    }

    // Update is called once per frame
    void Update()
    {
        Spawn_Rate -= Time.deltaTime;

        Debug.Log(" El tiempo es "+(int)Spawn_Rate);

        if (Spawn_Rate <= 0)
        {

            for (int i = 0; i < All_Spawner_Monster_Posibility.Length; i++)
            {
                if (All_Spawner_Monster_Posibility[i].Can_Spawn)
                {
                    int contador = 0;

                    Spawner_Monster_Posibility.Add(All_Spawner_Monster_Posibility [i]);
                    Instantiate(prefabEnemigo, Spawner_Place_Posibility[contador].transform.position, Spawner_Place_Posibility[contador].transform.rotation);

                    

                    Debug.Log("Se ha creado un enemigo en: "+ Spawner_Place_Posibility[contador]);

                    if (contador < Spawner_Place_Posibility.Length)
                    {
                        contador++;
                        Debug.Log("Se suma al contador");

                    }
                    //Debug.Log("Spawner lista tiene: "+ Spawner_Monster_Posibility.Count);
                }
            }

            Spawn_Rate = 5f;
        }

    }
}
