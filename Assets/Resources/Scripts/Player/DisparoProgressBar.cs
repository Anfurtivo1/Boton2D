using UnityEngine;
using UnityEngine.UI;

public class DisparoProgressBar : MonoBehaviour
{
    public Image progressBar;
    float progreso2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        progreso2 = Time.deltaTime;
        progreso2 = progreso2 / 2;
        progressBar.fillAmount = progreso2 + progressBar.fillAmount;
    }
}
