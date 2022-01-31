using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Animator animator;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player2")
        {
            animator = gameObject.GetComponent<Animator>();
            animator.SetTrigger("destroy");
        }
    }
}
