using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Transform holdPos;
    public float throwForce = 500f;
    public float pickUpRange = 5f;
    private GameObject heldObj;
    private Rigidbody heldObjRb;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(touch.position);

                if (Physics.Raycast(ray, out hit, pickUpRange))
                {
                        if (heldObj == null)
                        {
                            PickUpObject(hit.collider.gameObject);
                        }
                        else if (heldObj == hit.collider.gameObject)
                        {
                            ThrowObject();
                        }
                }
            }
        }
        // Ensure held object stays at hold position
        if (heldObj != null)
        {
            heldObj.transform.position = holdPos.position;
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            heldObjRb.isKinematic = true;
            heldObj.transform.SetParent(holdPos); // Set hold position as parent
        }
    }

    void ThrowObject()
    {
        heldObjRb.isKinematic = false;
        heldObjRb.transform.SetParent(null); // Unparent from hold position
        heldObjRb.AddForce(Camera.main.transform.forward * throwForce);
        heldObj = null;
        heldObjRb = null;
    }
}