using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pinLocation;
    [SerializeField] private Pin[] Pins;

    [SerializeField] private Bowling ball;


    private void Start()
    {
        SettingPin();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            returnball();
            SettingPin();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            returnball();
            StandingPin();
        }
    }

    private void SettingPin()
    {
        int i = 0;

        foreach (var pin in Pins)
        {
            pin.StopPin();

            pin.transform.localPosition = pinLocation[i].transform.localPosition;
            pin.transform.localRotation = Quaternion.identity;

            pin.gameObject.SetActive(true);

            Rigidbody rb = pin.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.Sleep();

            i++;
        }
    }

    private void StandingPin()
    {
        int i = 0;

        foreach (var pin in Pins)
        {
            pin.StopPin();
            if (pin.hasFallen != true)
            {
                pin.transform.localPosition = pinLocation[i].transform.localPosition;
                pin.transform.localRotation = Quaternion.identity;

                pin.gameObject.SetActive(true);

                Rigidbody rb = pin.GetComponent<Rigidbody>();
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;

                rb.Sleep();
            }
            else
            {
                pin.gameObject.SetActive (false);
            }
            i++;
        }
    }

    private void returnball()
    {   
        ball.StopBall();
        ball.transform.position = new Vector3(0, 1.5f, -10);
        ball.gameObject.SetActive(true);
    }
}
