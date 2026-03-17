using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float thrust = 8f;
    public float rotationSpeed = 180f;

    private Rigidbody2D rb;

    public float maxSpeed = 10f;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireForce = 15f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // rotation
        float rotateInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.forward * -rotateInput * rotationSpeed * Time.deltaTime);

        // thrust
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(transform.up * thrust);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Rigidbody2D projRb = projectile.GetComponent<Rigidbody2D>();
        projRb.velocity = (Vector2)firePoint.up * 15f + rb.velocity;
    }
}
