using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // Nuevo Input System

[RequireComponent(typeof(LineRenderer))]
public class LineBounce : MonoBehaviour
{
    [Header("Movimiento")]
    public float amplitude = 45f;   // Ángulo máximo a cada lado
    public float speed = 2f;        // Velocidad de oscilación

    [Header("Línea")]
    public float length = 5f;       // Longitud de la línea

    [Header("Disparo")]
    public Bala projectilePrefab;  // Prefab del proyectil
    public float projectileSpeed = 40f;  // Velocidad del proyectil
    public float shootCooldown = 2f;     // Cooldown entre disparos en segundos

    Vector3 start;
    Vector3 end;
    Vector3 direction;

    private LineRenderer line;
    private float angle;
    private bool canShoot = true;

    public Image progressBar;


    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;

        if (projectilePrefab != null)
        {
            //projectileSpeed = projectilePrefab.speed;
        }


    }

    void Update()
    {
        if (!canShoot)
        {
        progressBar.fillAmount += 1f / shootCooldown * Time.deltaTime;
//        Debug.Log("Estoy en el if negativo");
        }

        //if (canShoot)
        //{
        //    Debug.Log("Estoy dentro del if positivo");
        //    if (progressBar.fillAmount >= 1)
        //    {
        //        canShoot = true;
        //        progressBar.fillAmount = 0;
        //        //proyectilGenerator.generateProyectil();
        //        //src.clip = disparar;
        //        //src.Play();
        //    }
        //}


        // Movimiento oscilante del apuntado
        angle = Mathf.Sin(Time.time * speed) * amplitude;
        direction = Quaternion.Euler(0, 0, angle) * Vector3.up;

        start = transform.position;
        end = transform.position + direction * length;

        line.SetPosition(0, start);
        line.SetPosition(1, end);
    }

    //private void PlayerInputOnActionTriggered(InputAction.CallbackContext context)
    //{
    //    if(context.action.name == "Attack")
    //    {
    //        Debug.Log("Estoy en el primero");
    //        OnAttack(context);
    //    }
    //}

    // Este método será llamado automáticamente por PlayerInput si usas "Send Messages"
    // Debe llamarse exactamente igual que tu acción Input: "Attack"
    public void OnAttack(InputAction.CallbackContext context)
    {

        if (context.performed && canShoot) // Solo cuando se presiona
        {
            progressBar.fillAmount = 0;
            Debug.Log("Entre en OnAttack");
            canShoot = false;
            Shoot(direction, end);
            StartCoroutine(ShootCooldown()); // Inicia cooldown
        }
        // Si quieres, puedes detectar cuando se suelta con context.canceled
    }

    void Shoot(Vector3 direction, Vector3 spawnPos)
    {
        if (projectilePrefab == null) return;

        Bala projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * projectileSpeed;
        }
    }

    // Corutina para el cooldown
    private IEnumerator ShootCooldown()
    {
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }



}
