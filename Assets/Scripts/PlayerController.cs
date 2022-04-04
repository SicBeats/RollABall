using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.0f;
    public float jumpVel = 10f;
    public float jumpCount = 0;
    public bool isGrounded;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;

    private int count;
    private float movementX;
    private float movementZ;
    private float spaceHit;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementZ = movementVector.y;
    }

    void OnJump(InputValue action)
    {
        spaceHit = action.Get<float>();
        Debug.Log("Jump!");
        if (jumpCount <= 0)
        {
            rb.AddForce(Vector3.up * jumpVel, ForceMode.Impulse);
            jumpCount += 1;
        }
    }

    void SetCountText()
    {
        countText.text = "Score: " + count.ToString();

        if (count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {

        Vector3 movement = new Vector3(movementX, 0.0f, movementZ);

        if (isGrounded)
        {
            jumpCount = 0;
        }

        rb.AddForce(movement * speed);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;

            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("canJumpOn"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("canJumpOn"))
        {
            isGrounded = false;
        }
    }
}