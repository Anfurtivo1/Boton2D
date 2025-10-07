using UnityEngine;

public class LiquidSway : MonoBehaviour
{
    public float swayAmount = 0.1f; // How far to move side to side
    public float swaySpeed = 2f;    // Speed of sway

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        float offsetX = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        transform.localPosition = initialPosition + new Vector3(offsetX, 0, 0);
    }
}
