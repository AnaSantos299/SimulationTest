using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoyAlvo : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Destroy(gameObject);
        }
    }
}
