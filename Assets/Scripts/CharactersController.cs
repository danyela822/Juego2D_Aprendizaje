using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : MonoBehaviour
{
    //Velocidad a la que se mueve el personaje
    float speed = 3f;
    //Tipo de personaje (Principal, ayudante 1 o ayudante 2)
    public int type;
    //Variable para acceder a las animaciones de los personajes
    Animator animator;
    //Variable para controlar el movimiento del personaje en la escena
    Vector3 move;
    
    Rigidbody2D rigidbody2d;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    //Metodo que indica hacia que direccion debe moverse el personaje
    public void Move(string direction)
    {
        if(direction == "up")
        {
	        move = Vector2.up;
        }
        else if (direction == "down")
        {
	        move = Vector2.down;
        }
        else if(direction == "right")
        {
            move = Vector2.right;
        }
        else if(direction == "left")
        {
	        move = Vector2.left;
        }
    }

    //Metodo que detiene el movimiento del personaje
    public void NotMove()
    {
        move = Vector3.zero;
    }

    //Metodo que cambia la posicion del personaje en la escena
    void FixedUpdate()
    {
        this.transform.position += move * speed * Time.deltaTime;
        //rigidbody2d.MovePosition(rigidbody2d.position + move * speed * Time.deltaTime);
    }
}
