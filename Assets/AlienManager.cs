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

    private float timer = 0f;
    private float waitTime;

    private int direction = 1;

    void Start()
    {
        waitTime = initialWaitTime;
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
    }
}