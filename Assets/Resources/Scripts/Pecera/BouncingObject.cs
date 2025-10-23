using UnityEngine;

public class BouncingObject : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;

    // Límites de rebote
    public float minX = -2f;
    public float maxX = 2f;
    public float minY = -14f;
    public float maxY = -6f;

    private float halfWidth = 0.5f;
    private float halfHeight = 0.5f;

    void Start()
    {
        // Dirección inicial aleatoria
        direction = Random.insideUnitCircle.normalized;

        // Obtener tamaño del sprite
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Vector2 size = sr.bounds.size;
            halfWidth = size.x / 2f;
            halfHeight = size.y / 2f;
        }
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos += (Vector3)(direction * speed * Time.deltaTime);

        // Rebote horizontal
        if (pos.x - halfWidth < minX || pos.x + halfWidth > maxX)
        {
            direction.x = -direction.x;
            pos.x = Mathf.Clamp(pos.x, minX + halfWidth, maxX - halfWidth);
        }

        // Rebote vertical
        if (pos.y - halfHeight < minY || pos.y + halfHeight > maxY)
        {
            direction.y = -direction.y;
            pos.y = Mathf.Clamp(pos.y, minY + halfHeight, maxY - halfHeight);
        }

        transform.position = pos;
    }
}
