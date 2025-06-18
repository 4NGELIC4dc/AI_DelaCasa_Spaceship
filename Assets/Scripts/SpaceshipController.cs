using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float avoidForce = 3f;
    public float rayDistance = 3f;
    public LayerMask obstacleMask;

    private Vector2 moveDirection = Vector2.right;
    private Vector2 avoidanceDirection = Vector2.zero;

    void Update()
    {
        HandleAvoidance();
        MoveShip();
    }

    void HandleAvoidance()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, obstacleMask);

        if (hit.collider != null)
        {
            // Avoid up or down depending on available space
            Vector2 upCheck = Vector2.up;
            Vector2 downCheck = Vector2.down;

            bool canGoUp = !Physics2D.Raycast(transform.position, upCheck, 1f, obstacleMask);
            bool canGoDown = !Physics2D.Raycast(transform.position, downCheck, 1f, obstacleMask);

            if (canGoUp)
                avoidanceDirection = Vector2.up;
            else if (canGoDown)
                avoidanceDirection = Vector2.down;
            else
                avoidanceDirection = Vector2.zero;
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
