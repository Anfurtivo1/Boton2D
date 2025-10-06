using UnityEngine;

public class Bala : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 moveDir;

    public void SetDirection(Vector3 dir)
    {
        moveDir = dir.normalized;
    }

    void Update()
    {
        // Mueve el proyectil cada frame
        transform.position += moveDir * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }


    // Si usas triggers en lugar de colisiones normales:

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("He sido destruido");
        Destroy(gameObject);
    }

}
