using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TouchToCollect : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    //Layer 3
    LayerMask layerMask = 1 << 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Debug.DrawRay(ray.origin, ray.direction * 20f);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity,layerMask))
            {
                Debug.Log("One item was collected");
                Destroy(hit.transform.gameObject);
            }
        }
        
    }
}
