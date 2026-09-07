using UnityEngine;

public class Alien : MonoBehaviour
{
    public int points = 10;

    [Header("Tiro")]
    public GameObject enemyBulletPrefab;
    public float shootChance = 0.01f;

    private AlienManager alienManager;
    private GameManager gameManager;

    private bool destroyed = false;

    void Start()
    {
        alienManager = GetComponentInParent<AlienManager>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        Shoot();
        
    }

    void Shoot()
    {
        if (enemyBulletPrefab == null)
        {
            return;
        }

        if (Random.value < shootChance * Time.deltaTime)
        {
            Instantiate(
                enemyBulletPrefab,
                transform.position,
                Quaternion.identity
            );
        }
    }

    public void DestroyAlien()
    {
        if (destroyed)
        {
            return;
        }

        destroyed = true;

        if (gameManager != null)
        {
            gameManager.AddScore(points);
        }

        if (alienManager != null)
        {
            alienManager.AlienDestroyed();
        }

        Destroy(gameObject);
    }
}