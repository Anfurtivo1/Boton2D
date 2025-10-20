using UnityEngine;

public class BouncingObject : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;
    private Camera cam;
    private Vector2 minBounds, maxBounds;
    private float halfWidth, halfHeight;

    void Start()
    {
        cam = Camera.main;

        // Dirección inicial aleatoria
        direction = Random.insideUnitCircle.normalized;

        // Calculamos los límites del mundo visibles por la cámara
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 10));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 10));
        minBounds = new Vector2(bottomLeft.x, bottomLeft.y);
        maxBounds = new Vector2(topRight.x, topRight.y);

        // Tamaño del sprite para no salir del borde
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Vector2 size = sr.bounds.size;
            halfWidth = size.x / 2f;
            halfHeight = size.y / 2f;
        }
        else
        {
            halfWidth = halfHeight = 0.5f; // valor por defecto
        }
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos += (Vector3)(direction * speed * Time.deltaTime);

        // Rebote horizontal
        if (pos.x - halfWidth < minBounds.x || pos.x + halfWidth > maxBounds.x)
        {
            direction.x = -direction.x;
            pos.x = Mathf.Clamp(pos.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        }

        // Rebote vertical
        if (pos.y - halfHeight < minBounds.y || pos.y + halfHeight > maxBounds.y)
        {
            direction.y = -direction.y;
            pos.y = Mathf.Clamp(pos.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        }

        transform.position = pos;
    }
}
