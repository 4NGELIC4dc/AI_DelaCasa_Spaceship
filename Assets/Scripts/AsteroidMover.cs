using UnityEngine;

public class AsteroidMover : MonoBehaviour
{
    public float speed = 4f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Destroy off screen
        if (transform.position.x < Camera.main.transform.position.x - 15f)
        {
            Destroy(gameObject);
        }
    }
}
