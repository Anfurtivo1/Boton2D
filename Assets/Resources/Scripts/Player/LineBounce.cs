using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; // Nuevo Input System

[RequireComponent(typeof(LineRenderer))]
public class LineBounce : MonoBehaviour
{
    public static LineBounce lineBounceInstance;

    [Header("Movimiento")]
    public float amplitude = 45f;   // Ángulo máximo a cada lado
    public float speed = 2f;        // Velocidad de oscilación

    [Header("Línea")]
    public float length = 5f;       // Longitud de la línea

    [Header("Disparo")]
    public Bala projectilePrefab;  // Prefab del proyectil
    public float projectileSpeed = 40f;  // Velocidad del proyectil

    Vector3 start;
    Vector3 end;
    Vector3 direction;

    private LineRenderer line;
    private float angle;
    public bool isOnMenus = false;

    public Image progressBar;

    void Awake()
    {
        if (lineBounceInstance == null)
        {
            lineBounceInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;

        // 🔹 Cambiamos el material por uno que soporte transparencia
        line.material = new Material(Shader.Find("Sprites/Default"));

        // 🔹 Color semitransparente
        Color semiTransparent = new Color(1f, 1f, 1f, 0.1f);
        line.startColor = semiTransparent;
        line.endColor = semiTransparent;

        // 🔹 (Opcional) ajustar grosor de la línea
        line.startWidth = 0.25f;
        line.endWidth = 0.25f;
    }


    void Update()
    {
        if (!Player.playerInstance.canShoot)
        {
            progressBar.fillAmount += 1f / Player.playerInstance.player_AttackRate * Time.deltaTime;
        }

        // Movimiento oscilante del apuntado
        angle = Mathf.Sin(Time.time * speed) * amplitude;
        direction = Quaternion.Euler(0, 0, angle) * Vector3.up;

        start = transform.position;
        end = transform.position + direction * length;

        // 🔹 Hacer raycast para detectar colisión
        RaycastHit2D hit = Physics2D.Raycast(start, direction, length);

        if (hit.collider != null)
        {
            // Si choca, la línea se corta justo en el punto de impacto
            end = hit.point;
        }

        line.SetPosition(0, start);
        line.SetPosition(1, end);

        // 🔹 (Opcional) Dibujar el raycast en modo debug
        //Debug.DrawRay(start, direction * length, Color.red);
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

        if (context.performed && Player.playerInstance.canShoot) // Solo cuando se presiona
        {
            progressBar.fillAmount = 0;
            Debug.Log("Entre en OnAttack");
            Player.playerInstance.canShoot = false;

            Vector3 fixedPoint = new Vector3(-0.05f, -4.35f, 0f); // Tu punto exacto

            Shoot(direction, fixedPoint);
            if (!isOnMenus)
            {
                StartCoroutine(ShootCooldown()); // Inicia cooldown
            }
            
        }
        // Si quieres, puedes detectar cuando se suelta con context.canceled
    }

    void Shoot(Vector3 direction, Vector3 spawnPos)
    {
        if (projectilePrefab == null) return;
        
        Debug.Log("Estoy en el Shoot");
        if (!isOnMenus)
        {
            Bala projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = direction.normalized * projectileSpeed;
            }
        }
        
    }

    // Corutina para el cooldown
    private IEnumerator ShootCooldown()
    {
        Debug.Log("Estoy en el Shoot Cooldown");
        if (!isOnMenus)
        {
            yield return new WaitForSeconds(Player.playerInstance.player_AttackRate);
            Player.playerInstance.canShoot = true;
        }
    }



}
