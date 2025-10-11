using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Vida")]
    public MonsterData Enemy_Data;   
    private float currentHP;

    private float lifetimeTimer;
    private bool isDead = false;

    [Header("Movimiento")]
    GameObject Enemy_Target;   
    public float Enemy_Base_Speed = 1f;
    private float Enemy_Current_Speed;

    [Header("Componentes opcionales")]
    public Animator animator;            //Esto por si tenemos que manejar la animación uwu
    public SpriteRenderer spriteRenderer; //Este solo lo tengo mientras para hacer cambios visuales cuando recibe daño 
    private void Start()
    {
        if (Enemy_Data != null)
        {
            Initialize(Enemy_Data);
        }
        //Encontrar jugador
        if (Enemy_Target == null)
            Enemy_Target = GameObject.FindGameObjectWithTag("Player");

        //Calcular su velocidad
        Enemy_Current_Speed = Enemy_Base_Speed * Enemy_Data.Monster_Speed;
    }


    public void Initialize(MonsterData data)
    {
        Enemy_Data = data;
        currentHP = Enemy_Data.Monster_HP;
        lifetimeTimer = Enemy_Data.Monster_LifeTime;

        if (animator == null)
            animator = GetComponent<Animator>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (Enemy_Target == null) return;

        Vector3 direction = (Enemy_Target.transform.position - transform.position).normalized;
        transform.position += direction * Enemy_Current_Speed * Time.deltaTime;
    }
    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHP -= amount;

        if (currentHP <= 0f)
        {
            Die();
        }
        else
        {
            //Feedback de daño
            if (animator != null)
                animator.SetTrigger("Hit");
            
            if (spriteRenderer != null)
                StartCoroutine(DamageFlash());
        }
    }

    private IEnumerator DamageFlash()
    {
        Color original = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = original;
    }

    private void Die()
    {
        isDead = true;

        //Sumar la kill al GameManager
        if (Enemy_Data != null && GameManager.Instance != null)
        {
            GameManager.Instance.AddMonsterKill(Enemy_Data.Monster_ID);
        }
        Enemy_Current_Speed = 0f;
        //Animación de muerte si hacemos al final¿?
        if (animator != null)
            animator.SetTrigger("Die");

        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(Player.playerInstance.bullet_Damage);
            Destroy(collision.gameObject);
        }
    }
}
