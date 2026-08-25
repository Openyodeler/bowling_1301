using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bowling : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private int forcePower;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && GameManager.instance.ended == false)
        ShootBall();
        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
            MoveRight();
        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
            MoveLeft();


    }
    public void ShootBall()
    {
        rb.AddForce(Vector3.forward * forcePower, ForceMode.Impulse);
        Invoke(nameof(InvokeEndRound), 4f);
    }

    private void MoveRight()
    {
        transform.position += new Vector3(1f,0f,0f) * Time.deltaTime;
    }

    private void MoveLeft()
    {
        transform.position -= new Vector3(1f, 0f, 0f) * Time.deltaTime;
    }

    public void StopBall()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void InvokeEndRound()
    {
        GameManager.instance.EndRound();
    }
}
