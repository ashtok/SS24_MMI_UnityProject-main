using UnityEngine;
using System.Collections.Generic;

public class MapTouchHandler : MonoBehaviour
{
    public POIManager poiManager;
    public LineRenderer lineRenderer;
    public float touchRadius = 0.05f; // Adjust as needed

    private bool isDrawingCircle = false;
    private Vector3 circleCenter;
    private float circleRadius;
    private List<Vector3> circlePoints = new List<Vector3>();
    private Vector3 lastTouchPoint;

    private Collider controllerCollider;

    void Start()
    {
        controllerCollider = GetComponent<Collider>(); // Assuming this script is on the controller

        // Initialize LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.loop = true;

        // Hide the LineRenderer initially
        lineRenderer.enabled = false;
    }

    void Update()
    {
        // Update circle drawing if active
        if (isDrawingCircle)
        {
            Vector3 point = lastTouchPoint;
            if (!circlePoints.Contains(point))
            {
                circlePoints.Add(point);
                circleRadius = Vector3.Distance(circleCenter, point);
                DrawCircle(circleCenter, circleRadius);
            }
            else
            {
                isDrawingCircle = false;
                Debug.Log($"Circle drawn with center at {circleCenter} and radius {circleRadius}");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        POIManager.POI touchedPOI = GetPOIFromCollider(other);
        Debug.Log(touchedPOI + " is the touched poi");
        if (touchedPOI != null)
        {
            lastTouchPoint = touchedPOI.position; // Update lastTouchPoint
            HandleMapTouch(touchedPOI.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Detect continuous touch on the map
        if (isDrawingCircle)
        {
            Vector3 touchPoint = other.ClosestPointOnBounds(transform.position);
            lastTouchPoint = touchPoint;
        }
    }
    
    private POIManager.POI GetPOIFromCollider(Collider other)
    {
        foreach (var poi in poiManager.placesToEat)
        {
            if (other.gameObject.name == poi.name)
            {
                return poi;
            }
        }

        foreach (var poi in poiManager.parks)
        {
            if (other.gameObject.name == poi.name)
            {
                return poi;
            }
        }

        foreach (var poi in poiManager.shops)
        {
            if (other.gameObject.name == poi.name)
            {
                return poi;
            }
        }

        return null;
    }
    private POIManager.POI DetectTouchOnPOI(Collider other)
    {
        foreach (var poi in poiManager.placesToEat)
        {
            if (other.bounds.Contains(poi.position))
            {
                return poi;
            }
            Debug.Log("1");
        }

        foreach (var poi in poiManager.parks)
        {
            if (other.bounds.Contains(poi.position))
            {
                return poi;
            }
            Debug.Log("2");
        }

        foreach (var poi in poiManager.shops)
        {
            if (other.bounds.Contains(poi.position))
            {
                return poi;
            }
            Debug.Log("3");
        }

        return null;
    }

    public POIManager.POI HandleMapTouch(Vector3 touchPoint)
    {
        Debug.Log($"Touch Point: {touchPoint}");

        POIManager.POI closestPOI = null;
        float closestDistance = float.MaxValue;

        foreach (var poi in poiManager.placesToEat)
        {
            float distance = Vector3.Distance(touchPoint, poi.position);
            //Debug.Log($"Distance to {poi.name}: {distance}");
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPOI = poi;
            }
        }

        foreach (var poi in poiManager.parks)
        {
            float distance = Vector3.Distance(touchPoint, poi.position);
            //Debug.Log($"Distance to {poi.name}: {distance}");
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPOI = poi;
            }
        }

        foreach (var poi in poiManager.shops)
        {
            float distance = Vector3.Distance(touchPoint, poi.position);
            //Debug.Log($"Distance to {poi.name}: {distance}");
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPOI = poi;
            }
        }

        if (closestPOI != null)
        {
            Debug.Log($"Touched place: {closestPOI.name}");
            return closestPOI;
        }
        else
        {
            Debug.Log("No place found at the touched point.");
            return null;
        }
    }

    public void StartDrawingCircle()
    {
        isDrawingCircle = true;
        circleCenter = lastTouchPoint;
        circlePoints.Clear();

        // Make LineRenderer visible when starting to draw
        lineRenderer.enabled = true;
    }

    private void DrawCircle(Vector3 center, float radius)
    {
        int segments = 50;
        lineRenderer.positionCount = segments + 1;
        for (int i = 0; i < segments + 1; i++)
        {
            float angle = i * 2 * Mathf.PI / segments;
            float x = center.x + Mathf.Cos(angle) * radius;
            float z = center.z + Mathf.Sin(angle) * radius;
            lineRenderer.SetPosition(i, new Vector3(x, center.y, z));
        }
    }

    public Vector3 GetLastTouchPoint()
    {
        return lastTouchPoint;
    }

    public Vector3 GetCircleCenter()
    {
        return circleCenter;
    }

    public float GetCircleRadius()
    {
        return circleRadius;
    }
}
