using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RayVisual : MonoBehaviour
{
    [Header("Ray Settings")]
    public float rayLength = 10f;
    public LayerMask collisionMask;

    [Header("Visual Settings")]
    public Material rayMaterial;
    public float lineWidth = 0.05f;

    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SetupLine();
    }

    void SetupLine()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.material = rayMaterial;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.useWorldSpace = true;
    }

    void Update()
    {
        UpdateRay();
    }

    void UpdateRay()
    {
        Vector3 start = transform.position;
        Vector3 direction = transform.up; // hacia arriba
        Vector3 end = start + direction * rayLength;

        RaycastHit2D hit = Physics2D.Raycast(start, direction, rayLength, collisionMask);
        if (hit.collider != null)
        {
            end = hit.point;
        }

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }

    // Permite cambiar el material dinámicamente si lo deseas
    public void SetRayMaterial(Material newMaterial)
    {
        rayMaterial = newMaterial;
        lineRenderer.material = newMaterial;
    }
}
