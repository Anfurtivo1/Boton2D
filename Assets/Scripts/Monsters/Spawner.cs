using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    GameObject[] Spawner_Place_Posibility;
    List<MonsterData> Spawner_Monster_Posibility;
    MonsterData[] All_Spawner_Monster_Posibility;

    float Spawn_Rate = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
                    Spawner_Monster_Posibility.Add(All_Spawner_Monster_Posibility [i]);
                }
            }

            Spawn_Rate = 5f;
        }

    }
}
