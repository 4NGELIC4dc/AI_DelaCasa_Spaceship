using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
            Destroy(gameObject, animationLength);
        }
        else
        {
            Destroy(gameObject, 1f); // fallback
        }
    }
}
