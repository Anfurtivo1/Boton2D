using UnityEngine;

public class DisableOnStart : MonoBehaviour
{
    [SerializeField] private float delay = 0.1f;

    private void Start()
    {
        // Llama a la funci�n despu�s del delay indicado
        Invoke(nameof(DisableObject), delay);
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }
}