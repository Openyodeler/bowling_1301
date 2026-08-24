using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public int point = 1;
    public bool hasFallen = false;

    void Update()
    {
        if (!hasFallen && Vector3.Angle(transform.up, Vector3.up) > 45f)
        {
            hasFallen = true;

            // Add point
            Debug.Log("Pin fell! +1 point");
        }
    }
    public void StopPin()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PinZone"))
        {
            hasFallen = true;
        }
    }
}