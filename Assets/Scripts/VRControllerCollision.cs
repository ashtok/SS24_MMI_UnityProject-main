using UnityEngine;

public class VRControllerCollision : MonoBehaviour
{
    private MapTouchHandler mapTouchHandler;

    private void Start()
    {
        mapTouchHandler = FindObjectOfType<MapTouchHandler>(); // Find the MapTouchHandler script
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Pass the touch point to the MapTouchHandler
        if (mapTouchHandler != null)
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                mapTouchHandler.HandleMapTouch(contact.point);
            }
        }
    }
}