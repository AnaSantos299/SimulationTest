using UnityEngine;
using Yarn.Unity;

public class InputHandle : MonoBehaviour
{
    // Reference to the Scene3Events script
    public Scene3Events scene3Events;

    // Name of the method to call on click
    public string methodName;

#if UNITY_WEBGL
    // This method is called when the user releases the mouse button over the Collider
    void OnMouseClick()
    {
        InvokeMethod();
    }

    // Handles mouse input in WebGL and standalone builds
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    OnMouseClick(); // Reuse the same logic for mouse clicks
                }
            }
        }
    }
#endif

#if UNITY_IOS || UNITY_ANDROID
    // Handles touch input on mobile devices
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        InvokeMethod(); // Call the method directly on touch
                    }
                }
            }
        }
    }
#endif

    // Method to invoke the assigned method on Scene3Events
    private void InvokeMethod()
    {
        var method = scene3Events.GetType().GetMethod(methodName);
        if (method != null)
        {
            method.Invoke(scene3Events, null);
        }
        else
        {
            Debug.LogWarning("Method " + methodName + " not found on Scene3Events");
        }
    }
}
