using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float avoidForce = 5f;
    public float rayDistance = 5f;
    public float avoidanceStrength = 10f;
    public LayerMask obstacleMask;

    public GameObject explosionPrefab;
    public GameManager gameManager;

    private Vector2 moveDirection = Vector2.right;
    private Vector2 avoidanceDirection = Vector2.zero;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }

    void Update()
    {
        HandleAvoidance();
        MoveShip();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            AudioManager.Instance.PlayExplosion();
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.SendMessage("GameOver");
            Destroy(gameObject); // Destroy spaceship
        }
    }

    void HandleAvoidance()
    {
        Vector2 forward = transform.right;
        float circleRadius = 2f; // Increase to detect wider range
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, circleRadius, forward, rayDistance, obstacleMask);

        // Debug visualization
        Debug.DrawRay(transform.position, forward * rayDistance, Color.red);
        if (hit.collider != null)
        {
            Debug.Log("Asteroid detected at distance: " + hit.distance);
            Vector2 hitNormal = hit.normal;
            avoidanceDirection = new Vector2(hitNormal.y, -hitNormal.x); // perpendicular direction
        }
        else
        {
            avoidanceDirection = Vector2.zero;
        }
    }

    void MoveShip()
    {
        Vector2 finalDirection = moveDirection + avoidanceDirection;
        transform.Translate(finalDirection.normalized * moveSpeed * Time.deltaTime);
    }
}
