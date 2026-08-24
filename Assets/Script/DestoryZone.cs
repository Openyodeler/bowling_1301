using UnityEngine;

public class DestoryZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SetActive(false);
    }
}
