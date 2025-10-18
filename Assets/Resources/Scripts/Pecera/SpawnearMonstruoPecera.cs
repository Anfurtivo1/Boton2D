using UnityEngine;

public class SpawnearMonstruoPecera : MonoBehaviour
{
    public GameObject prefab; // asigna tu prefab aquí

    public void SpawnObject()
    {
        Camera cam = Camera.main;
        Vector3 randomViewport = new Vector3(Random.value, Random.value, 0);
        Vector3 spawnPos = cam.ViewportToWorldPoint(randomViewport);
        spawnPos.z = 0f;
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }

}
