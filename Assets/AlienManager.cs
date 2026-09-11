using UnityEngine;

public class AlienManager : MonoBehaviour
{
    [Header("Movimento")]
    public float moveDistance = 0.5f;
    public float descentDistance = 0.5f;

    [Header("Velocidade")]
    public float initialWaitTime = 1f;
    public float minimumWaitTime = 0.1f;
    public float speedIncrease = 0.05f;

    [Header("Limites")]
    public float leftLimit = -7f;
    public float rightLimit = 7f;

    [Header("Invasão")]
    public float invasionLimit = -4f;
    public bool resetOnInvasion = true;

    [Header("Fase 2")]
    public bool createReinforcedRow = false;
    public GameObject reinforcedAlienPrefab;
    public int reinforcedAlienCount = 12;
    public float reinforcedRowY = 4f;
    public float reinforcedAlienSpacing = 0.8f;
    public float reinforcedShootChance = 0.03f;

    private float timer = 0f;
    private float waitTime;

    private int direction = 1;
    private int remainingAliens;
    private bool phaseCompleted = false;
    private bool invasionHandled = false;

    private Vector3 initialPosition;
    private GameManager gameManager;

    void Start()
    {
        initialPosition = transform.position;
        gameManager = FindFirstObjectByType<GameManager>();
        waitTime = initialWaitTime;

        if (createReinforcedRow)
        {
            CreateReinforcedRow();
        }

        remainingAliens = GetComponentsInChildren<Alien>(true).Length;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            MoveFormation();
            timer = 0f;
        }
    }

    void MoveFormation()
    {
        Transform leftmost = GetLeftmostAlien();
        Transform rightmost = GetRightmostAlien();

        if (leftmost == null || rightmost == null)
        {
            return;
        }

        // Verifica se chegou ao limite
        if (direction == 1 &&
            rightmost.position.x >= rightLimit)
        {
            ChangeDirection();
            return;
        }

        if (direction == -1 &&
            leftmost.position.x <= leftLimit)
        {
            ChangeDirection();
            return;
        }

        // Move a formação
        transform.position += Vector3.right * moveDistance * direction;
    }

    void ChangeDirection()
    {
        direction *= -1;

        // Desce a formação
        transform.position += Vector3.down * descentDistance;

        CheckInvasion();
    }

    Transform GetLeftmostAlien()
    {
        Transform leftmost = null;

        foreach (Transform alien in transform)
        {
            if (alien == null)
                continue;

            if (leftmost == null ||
                alien.position.x < leftmost.position.x)
            {
                leftmost = alien;
            }
        }

        return leftmost;
    }

    Transform GetRightmostAlien()
    {
        Transform rightmost = null;

        foreach (Transform alien in transform)
        {
            if (alien == null)
                continue;

            if (rightmost == null ||
                alien.position.x > rightmost.position.x)
            {
                rightmost = alien;
            }
        }

        return rightmost;
    }

    public void AlienDestroyed()
    {
        waitTime -= speedIncrease;

        if (waitTime < minimumWaitTime)
        {
            waitTime = minimumWaitTime;
        }

        remainingAliens = Mathf.Max(0, remainingAliens - 1);

        if (remainingAliens == 0 && !phaseCompleted)
        {
            phaseCompleted = true;

            if (gameManager != null)
            {
                gameManager.CompletePhase();
            }
        }
    }

    void CheckInvasion()
    {
        if (invasionHandled)
        {
            return;
        }

        Transform lowestAlien = GetLowestAlien();

        if (lowestAlien == null || lowestAlien.position.y > invasionLimit)
        {
            return;
        }

        invasionHandled = true;

        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<GameManager>();
        }

        if (gameManager == null)
        {
            return;
        }

        gameManager.LoseLife();

        if (gameManager.lives <= 0 || !resetOnInvasion)
        {
            return;
        }

        transform.position = initialPosition;
        direction = 1;
        timer = 0f;
        invasionHandled = false;
    }

    Transform GetLowestAlien()
    {
        Transform lowestAlien = null;

        foreach (Transform alien in transform)
        {
            if (alien == null)
                continue;

            if (lowestAlien == null ||
                alien.position.y < lowestAlien.position.y)
            {
                lowestAlien = alien;
            }
        }

        return lowestAlien;
    }

    void CreateReinforcedRow()
    {
        if (reinforcedAlienPrefab == null || reinforcedAlienCount <= 0)
        {
            return;
        }

        float rowWidth = (reinforcedAlienCount - 1) * reinforcedAlienSpacing;
        float startX = -rowWidth * 0.5f;

        for (int i = 0; i < reinforcedAlienCount; i++)
        {
            GameObject alienObject = Instantiate(
                reinforcedAlienPrefab,
                transform
            );

            alienObject.name = "ReinforcedAlien (" + (i + 1) + ")";
            alienObject.transform.localPosition = new Vector3(
                startX + i * reinforcedAlienSpacing,
                reinforcedRowY,
                0f
            );

            Alien alien = alienObject.GetComponent<Alien>();

            if (alien != null)
            {
                alien.shootChance = reinforcedShootChance;
            }
        }
    }
}
