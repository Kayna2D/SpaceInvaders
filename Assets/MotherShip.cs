using UnityEngine;

public class MotherShip : MonoBehaviour
{
    public float speed = 5f;
    public int points = 50;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (transform.position.x >= 9f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBullet bullet = collision.GetComponent<PlayerBullet>();

        if (bullet != null)
        {
            if (gameManager != null)
            {
                gameManager.AddScore(points);
            }

            Destroy(bullet.gameObject);
            Destroy(gameObject);
        }
    }
}