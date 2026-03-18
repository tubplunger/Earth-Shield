using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float minSpeed = 1f;
    public float maxSpeed = 3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(Random.insideUnitCircle * 2f, ForceMode2D.Impulse);

        Vector2 direction = ((Vector2)Camera.main.transform.position - (Vector2)transform.position).normalized;
        direction += Random.insideUnitCircle * 0.3f;
        direction = direction.normalized;
        float speed = Random.Range(minSpeed, maxSpeed);

        rb.velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);

            GameManager.instance.AddScore(1);
        }
    }

    void OnBecomeInvisible()
    {
        Destroy(gameObject);
    }
}
