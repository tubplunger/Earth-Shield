using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public AsteroidSize size;

    public GameObject mediumPrefab;
    public GameObject smallPrefab;

    public float minSpeed = 1f;
    public float maxSpeed = 3f;

    private Rigidbody2D rb;
    public Sprite[] possibleSprites;

    public enum AsteroidSize
    {
        Large,
        Medium,
        Small
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(Random.insideUnitCircle * 2f, ForceMode2D.Impulse);

        Vector2 direction = ((Vector2)Camera.main.transform.position - (Vector2)transform.position).normalized;
        direction += Random.insideUnitCircle * 0.3f;
        direction = direction.normalized;
        float speed = Random.Range(minSpeed, maxSpeed);

        rb.velocity = direction * speed;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (possibleSprites.Length > 0)
        {
            sr.sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);

            Split();
            Destroy(gameObject);

            GameManager.instance.AddScore(1);
        }
    }

    void Split()
    {
        if (size == AsteroidSize.Large)
        {
            SpawnChildren(mediumPrefab, 2);
        }
        else if (size == AsteroidSize.Medium)
        {
            SpawnChildren(smallPrefab, 2);
        }
        // small does nothing
    }

    void SpawnChildren(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject child = Instantiate(prefab, transform.position, Quaternion.identity);

            Rigidbody2D childRb = child.GetComponent<Rigidbody2D>();

            Vector2 randomDir = Random.insideUnitCircle.normalized;
            float speed = Random.Range(minSpeed * 1.2f, maxSpeed * 1.5f);

            childRb.velocity = randomDir * speed;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
