using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player playerInstance;
    public GameObject bullet;
    public int bullet_Damage = 1;        
    public int player_HP = 3;
    public float player_AttackRate = 2f;
    public bool canShoot = true;
    public LifeManager lifeManager;
    public float bullet_speed = 10f;//añadir al guardado
    public bool mejoraDinero1;//añadir al guardado
    public bool mejoraDinero2;//añadir al guardado


    void Awake()
    {
        if (playerInstance == null)
        {
            playerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Muelte");
            Time.timeScale = 0f;

            if (lifeManager != null)
            {
                lifeManager.LoseLife();
                Guardado.instance.GuardarDatos();
            }
            else
            {
                Debug.Log("No se encontró LifeManager en la escena.");
            }
        }
    }
}

