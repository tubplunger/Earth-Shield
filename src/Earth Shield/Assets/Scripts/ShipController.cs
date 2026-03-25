using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float thrust = 8f;
    public float rotationSpeed = 180f;

    public float invincibilityTime = 2f;

    private Rigidbody2D rb;
    private bool isInvincible = false;
    private SpriteRenderer sr;

    public float maxSpeed = 10f;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireForce = 15f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
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

        AudioManager.instance.PlayShoot();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid") && !isInvincible)
        {
            GameManager.instance.LoseLife();
            StartCoroutine(Invincibility());
        }
    }

    IEnumerator Invincibility()
    {
        isInvincible = true;

        float timer = 0f;

        while (timer < invincibilityTime)
        {
            // flicker
            sr.enabled = !sr.enabled;

            yield return new WaitForSeconds(0.15f);
            timer += 0.15f;
        }

        sr.enabled = true;
        isInvincible = false;
    }
}
