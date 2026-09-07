using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 8f;

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (transform.position.y > 5.5f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Alien alien = collision.GetComponent<Alien>();

        if (alien != null)
        {
            alien.DestroyAlien();

            Destroy(gameObject);
        }
    }
}