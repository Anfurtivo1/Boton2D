using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpotLight2DRadiusPulse : MonoBehaviour
{
    public Light2D spotLight2D;

    public float minRadius = 1f;
    public float maxRadius = 5f;
    public float speed = 1f;

    void Start()
    {
        if (spotLight2D == null)
            spotLight2D = GetComponent<Light2D>();

        if (spotLight2D == null)
            Debug.LogError("No Light2D component found on this GameObject.");
    }

    void Update()
    {
        if (spotLight2D != null)
        {
            float radiusRange = maxRadius - minRadius;
            float newRadius = minRadius + (Mathf.Sin(Time.time * speed) * 0.5f + 0.5f) * radiusRange;
            spotLight2D.pointLightOuterRadius = newRadius;
        }
    }
}
