using UnityEngine;
using UnityEngine.UI;

public class Throwing : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform throwPoint;
    public GameObject objectToThrow;

    [Header("Settings")]
    private float throwCooldown = 0.1f;

    [Header("Throwing")]
    public Button throwButton;
    private float throwForce = 15;
    public float throwUpwardForce;

    bool readyToThrow;

    private void Start()
    {
        readyToThrow = true;
    }

    private void Throw()
    {
        if (readyToThrow)
        {
            readyToThrow = false;
            //intantiate object to throw
            GameObject projectile = Instantiate(objectToThrow, throwPoint.position, cam.rotation);

            //Get rigidbody component
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

            //calculate direction
            Vector3 forceDirection = cam.transform.forward;

            RaycastHit hit;

            if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
            {
                forceDirection = (hit.point - throwPoint.position).normalized;
            }

            //Add force
            Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

            projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

            //Implement throwCooldown
            Invoke(nameof(ResetThrow), throwCooldown);

            Destroy(projectile, 1.5f);
        }

    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }

}