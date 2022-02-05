using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Animator animator;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player2"))
        {
            animator = gameObject.GetComponent<Animator>();
            animator.SetTrigger("destroy");
        }
    }
}
