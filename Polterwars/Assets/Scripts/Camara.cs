using UnityEngine;

public class Camara : MonoBehaviour
{
    private Vector3 dragOrigin;
    private Camera cam;

    [Header("Configuración de Zoom")]
    public float zoomStep = 2.0f;
    public float minZoom = 3.0f;
    public float maxZoom = 15.0f;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        HandlePan();
        HandleZoom();
    }

    void HandlePan()
    {
        // Al hacer clic con el botón izquierdo (0) o derecho (1) del ratón
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position += difference;
        }
    }

    void HandleZoom()
    {
        float scrollData = Input.GetAxis("Mouse ScrollWheel");

        if (scrollData != 0.0f)
        {
            if (cam.orthographic)
            {
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scrollData * zoomStep, minZoom, maxZoom);
            }
        }
    }
}
