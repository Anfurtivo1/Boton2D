using UnityEngine;

public class Bala : MonoBehaviour
{
    private Vector3 moveDir;

    public void SetDirection(Vector3 dir)
    {
        moveDir = dir.normalized;
    }

    void Update()
    {
        // Mueve el proyectil cada frame
        transform.position += moveDir * Player.playerInstance.bullet_speed * Time.deltaTime;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Destroy(gameObject);
    //}


    // Si usas triggers en lugar de colisiones normales: Para eliminarlo si se sale del area de juego

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("He sido destruido");
        Destroy(gameObject);
        
    }

}
