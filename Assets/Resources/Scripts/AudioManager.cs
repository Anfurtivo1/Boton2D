using UnityEngine;

public class AudioManager:MonoBehaviour
{
    public static AudioManager AudioManagerinstance;
    void Awake()
    {
        if (AudioManagerinstance == null)
        {
            AudioManagerinstance = this;
            DontDestroyOnLoad(gameObject); // el objeto no se destruye al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // evita duplicados
        }
    }
}
