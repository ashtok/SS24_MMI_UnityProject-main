using System.Collections.Generic;
using UnityEngine;

public class POIManager : MonoBehaviour
{
    public GameObject poiPrefab; // Prefab for the POIs

    // Parent objects for each category
    public Transform placesToEatParent;
    public Transform parksParent;
    public Transform shopsParent;

    public List<POI> placesToEat = new List<POI>();
    public List<POI> parks = new List<POI>();
    public List<POI> shops = new List<POI>();

    private List<GameObject> poiInstances = new List<GameObject>();

    [System.Serializable]
    public class POI
    {
        public string name;
        public Vector3 position;
    }

    private void Start()
    {
        ShowPOIs("PlacesToEat");
        ShowPOIs("Parks");
        ShowPOIs("Shops");
    }

    public void ShowPOIs(string category)
    {
        //HideAllPOIs();

        List<POI> poiList = GetPOIListByCategory(category);
        Color color = GetColorByCategory(category);
        Transform parentTransform = GetParentByCategory(category);

        foreach (POI poi in poiList)
        {
            if (category == "PlacesToEat" || category == "Shops")
            {
                poi.position = GetRandomPosition();
            }
            
            GameObject poiInstance = Instantiate(poiPrefab, parentTransform);
            poiInstance.transform.localPosition = poi.position;
            poiInstance.GetComponent<Renderer>().material.color = color;
            poiInstance.name = poi.name;
            poiInstances.Add(poiInstance);
            //Debug.Log("POI Added :"+poi.name);
        }
    }

    public void ShowPoiName(string name)
    {
        //Debug.Log("Poi name : "+name);
        foreach (GameObject poiInstance in poiInstances)
        {
            if (poiInstance.name == name)
            {
                //Debug.Log("POI Found: " + name);
                poiInstance.GetComponent<global::POI>().showName();
            }
        }
    }

    public GameObject GetPoiByName(string name)
    {
        foreach (GameObject poiInstance in poiInstances)
        {
            if (poiInstance.name == name)
            {
                return poiInstance;
            }
        }
        return null;
    }

    public void HidePOIs(string category)
    {
        Transform parentTransform = GetParentByCategory(category);
        foreach (Transform child in parentTransform)
        {
            Destroy(child.gameObject);
        }
        poiInstances.Clear(); // Clear the list to remove references to destroyed objects
        // Return the random position with the same z value as the points
        //return new Vector3(randomX, randomY, point1.z);
    }

    public void ShowPOIsInCircle(Vector3 center, float radius, string category)
    {
        List<POI> poiList = GetPOIListByCategory(category);
        Color color = GetColorByCategory(category);
        Transform parentTransform = GetParentByCategory(category);

        foreach (POI poi in poiList)
        {
            if (Vector3.Distance(center, poi.position) <= radius)
            {
                GameObject poiInstance = Instantiate(poiPrefab, parentTransform);
                poiInstance.transform.localPosition = poi.position;
                poiInstance.GetComponent<Renderer>().material.color = color;
                poiInstance.name = poi.name;
                poiInstances.Add(poiInstance);
                Debug.Log("POI Added within circle: " + poi.name);
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 point1 = new Vector3(-0.451f, 0.456f, -0.5f);
        Vector3 point2 = new Vector3(0.442f, 0.456f, -0.5f);
        Vector3 point3 = new Vector3(0.442f, -0.445f, -0.5f);
        Vector3 point4 = new Vector3(-0.451f, -0.445f, -0.5f);

        // Find the min and max values for x and y
        float minX = Mathf.Min(point1.x, point2.x, point3.x, point4.x);
        float maxX = Mathf.Max(point1.x, point2.x, point3.x, point4.x);
        float minY = Mathf.Min(point1.y, point2.y, point3.y, point4.y);
        float maxY = Mathf.Max(point1.y, point2.y, point3.y, point4.y);

        // Generate a random x and y within the bounds
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        // Return the random position with the same z value as the points
        return new Vector3(randomX, randomY, point1.z);
    }

    private List<POI> GetPOIListByCategory(string category)
    {
        switch (category)
        {
            case "PlacesToEat":
                return placesToEat;
            case "Parks":
                return parks;
            case "Shops":
                return shops;
            default:
                return new List<POI>();
        }
    }

    private Color GetColorByCategory(string category)
    {
        switch (category)
        {
            case "PlacesToEat":
                return Color.red;
            case "Parks":
                return Color.green;
            case "Shops":
                return Color.blue;
            default:
                return Color.white;
        }
    }

    private Transform GetParentByCategory(string category)
    {
        switch (category)
        {
            case "PlacesToEat":
                return placesToEatParent;
            case "Parks":
                return parksParent;
            case "Shops":
                return shopsParent;
            default:
                return null;
        }
    }
}
