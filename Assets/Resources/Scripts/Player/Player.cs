using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player playerInstance;
    public GameObject bullet;
    public int bullet_Damage = 1;        
    public int player_HP = 3;
    public float player_AttackRate = 0.5f;

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
}
