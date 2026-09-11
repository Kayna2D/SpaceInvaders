using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    public float minX = -3.3f;
    public float maxX = 3.3f;

    public GameObject bulletPrefab;

    public Transform firePoint;

    void Update()
    {
        // Movimento
        float horizontal = Input.GetAxisRaw("Horizontal");

        Vector3 movement = Vector3.right * horizontal * speed * Time.deltaTime;

        transform.position += movement;

        Vector3 position = transform.position;

        position.x = Mathf.Clamp(position.x, minX, maxX);

        transform.position = position;


        // Tiro
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.identity
        );
    }
}