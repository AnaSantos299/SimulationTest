using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    public GameObject[] objectsToHide;
    public GameObject[] objectsToShow;

    // Method to hide objects
    public void HideObjects()
    {
        foreach (var obj in objectsToHide)
        {
            obj.SetActive(false);
        }
    }

    // Method to show objects
    public void ShowObjects()
    {
        foreach (var obj in objectsToShow)
        {
            obj.SetActive(true);
        }
    }
}
