using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] Spawner_Place_Posibility = new GameObject[4];
    List<MonsterData> Spawner_Monster_Posibility = new List<MonsterData>();
    public MonsterData[] All_Spawner_Monster_Posibility;

    float Spawn_Rate = 5f;
    float Spawn_Rate_Changed = 5f;
    float Time_To_Change = 15f;
    float StopTime=5f;

    public GameObject[] prefabEnemigos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Spawner_Monster_Posibility.Add(All_Spawner_Monster_Posibility[0]);
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

                    int monstruoRandom = Random.Range(0,Spawner_Monster_Posibility.Count);

                    Debug.Log("La posicion random es: " + posicionRandom);

                    Debug.Log("El monstruo random tiene el id: " + monstruoRandom);

                    Instantiate(prefabEnemigos[monstruoRandom], Spawner_Place_Posibility[posicionRandom].transform.position, Spawner_Place_Posibility[posicionRandom].transform.rotation);

                    ComprobarValores();

                    ///Debug.Log("Se ha creado un enemigo en: "+ Spawner_Place_Posibility[posicionRandom]);
                    //Debug.Log("Spawner lista tiene: "+ Spawner_Monster_Posibility.Count);

                }
            }

            Spawn_Rate = Spawn_Rate_Changed;
            //Debug.Log("La spawn rate es: " + Spawn_Rate);
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
    
    public void ComprobarValores()
    {
        foreach (var index in GameManager.Instance.MonsterKills)
        {
            int id = index.Key;
            int valor = index.Value;
            
            if (valor == 25 && index.Key == 1)
            {
                MonsterData monster = All_Spawner_Monster_Posibility[id];

                Spawner_Monster_Posibility.Add(All_Spawner_Monster_Posibility[id]);
                Debug.Log($"El monstruo {monster.name} alcanzó {valor} y se añadió a la lista");

            }

            if (valor == 30 && index.Key == 2)
            {
                MonsterData monster = All_Spawner_Monster_Posibility[id];

                Spawner_Monster_Posibility.Add(All_Spawner_Monster_Posibility[id]);
                Debug.Log($"El monstruo {monster.name} alcanzó {valor} y se añadió a la lista");

            }

            if (valor == 50 && index.Key == 3)
            {
                MonsterData monster = All_Spawner_Monster_Posibility[id];

                Spawner_Monster_Posibility.Add(All_Spawner_Monster_Posibility[id]);
                Debug.Log($"El monstruo {monster.name} alcanzó {valor} y se añadió a la lista");

            }

            if (valor == 75 && index.Key == 4)
            {
                MonsterData monster = All_Spawner_Monster_Posibility[id];

                Spawner_Monster_Posibility.Add(All_Spawner_Monster_Posibility[id]);
                Debug.Log($"El monstruo {monster.name} alcanzó {valor} y se añadió a la lista");

            }

            if (valor == 150 && index.Key == 5)
            {
                MonsterData monster = All_Spawner_Monster_Posibility[id];

                Spawner_Monster_Posibility.Add(All_Spawner_Monster_Posibility[id]);
                Debug.Log($"El monstruo {monster.name} alcanzó {valor} y se añadió a la lista");

            }

            if (valor == 250 && index.Key == 6)
            {
                MonsterData monster = All_Spawner_Monster_Posibility[id];

                Spawner_Monster_Posibility.Add(All_Spawner_Monster_Posibility[id]);
                Debug.Log($"El monstruo {monster.name} alcanzó {valor} y se añadió a la lista");

            }
            
        }
    }


}
