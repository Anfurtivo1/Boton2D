using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Datos para el enemigo")]
    public MonsterData Enemy_Data; //Dato del monstruo
    public GameObject Enemy_Target; //Mi jugador
    public float Enemy_Base_Speed = 1f; //La velocidad base que llevar�

    [Header("Para calcular")]
    private float Enemy_Current_Speed; //Para calcular su velocidad
    private int Enemy_Current_HP; //La vida que tendr� actual el enemigo

    [Header("Referencia")]
    public GameManager gameManager; //Referencias

    void Start()
    {
        Enemy_Target = GameObject.FindGameObjectWithTag("Player");
        

        //Calculo velocidad y vida inicial
        Enemy_Current_Speed = Enemy_Base_Speed * Enemy_Data.Monster_Speed;
        Enemy_Current_HP = Enemy_Data.Monster_HP;

        if (GameManager.Instance != null)
        {
            gameManager = GameManager.Instance;
            //Debug.Log("GameManager encontrado y asignado.");
        }

        

    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {

        if (Enemy_Target != null)
        {
            //La direcci�n
            Vector3 direction = (Enemy_Target.transform.position - transform.position).normalized;

            //La velocidad
            transform.position += direction * Enemy_Current_Speed * Time.deltaTime;

        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            //Recibe da�o
            TakeDamage(Player.playerInstance.bullet_Damage);

            //Y destruyo la bala
            Destroy(collision.gameObject);
        }
    }

    private void TakeDamage(int damage)
    {
        Enemy_Current_HP -= damage;

        if (Enemy_Current_HP <= 0)
            Die();
    }

    private void Die()
    {
        //Le doy dinero al jugador
        gameManager.Money_Amount += Enemy_Data.Money_Amount_Monster;

        //Y al contador de enemigos muertos de ese tipo, le sumo
        if (gameManager.MonsterKills.ContainsKey(Enemy_Data.Monster_ID))
            gameManager.MonsterKills[Enemy_Data.Monster_ID]++;
        else
            gameManager.MonsterKills.Add(Enemy_Data.Monster_ID, 1);

        // Destruir enemigo
        Destroy(gameObject);
    }


}
