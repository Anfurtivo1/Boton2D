using UnityEngine;
using UnityEngine.InputSystem; // Nuevo Input System

[RequireComponent(typeof(LineRenderer))]
public class LineBounce : MonoBehaviour
{
    [Header("Movimiento")]
    public float amplitude = 45f;   // Ángulo máximo a cada lado
    public float speed = 2f;        // Velocidad de oscilación

    [Header("Línea")]
    public float length = 5f;       // Longitud de la línea

    [Header("Disparo")]
    public GameObject projectilePrefab;  // Prefab del proyectil
    public float projectileSpeed = 10f;  // Velocidad del proyectil

    private LineRenderer line;
    private float angle;
    private bool attackPressed = false;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
    }

    void Update()
    {
        // Movimiento oscilante del apuntado
        angle = Mathf.Sin(Time.time * speed) * amplitude;
        Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.up;

        Vector3 start = transform.position;
        Vector3 end = transform.position + direction * length;

        line.SetPosition(0, start);
        line.SetPosition(1, end);

        // Si se ha pulsado la acción Attack, dispara
        if (attackPressed)
        {
            Shoot(direction, end);
            attackPressed = false;
        }
    }

    private void PlayerInputOnActionTriggered(InputAction.CallbackContext context)
    {
        if(context.action.name == "Attack")
        {
            Debug.Log("Estoy en el primero");
            OnAttack(context);
        }
    }

    // Este método será llamado automáticamente por PlayerInput si usas "Send Messages"
    // Debe llamarse exactamente igual que tu acción Input: "Attack"
    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Estoy en el segundo");
        attackPressed = true;
        // Si quieres, puedes detectar cuando se suelta con context.canceled
    }

    void Shoot(Vector3 direction, Vector3 spawnPos)
    {
        if (projectilePrefab == null) return;

        GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * projectileSpeed;
        }

        // Opcional: destruir el proyectil tras unos segundos
        Destroy(projectile, 5f);
    }



}
