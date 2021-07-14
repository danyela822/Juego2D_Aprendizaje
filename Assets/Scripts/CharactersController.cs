using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : Reference
{
    List<GameObject> screenCharacters = new List<GameObject>();

    //Lista de todos lo personajes que hay en el juego
    List<Character> allCharacters = new List<Character>();

    //Lista de los personajes que pertenecen al nivel que se esta jugando
    List<Character> levelCharacters;

    //Metodo para seleccionar los personajes que apareceran en el nivel y ubicarlos en la pantalla.
    //Debe recibir la cantidad de persojes que apareceran, el tema (Castillo, Bosque, Oceano) y la lista de objetos de los personajes
    public void SelectCharactersLevel(int numCharacters, string theme, List<GameObject> allCh)
    {
        //Inicializacion de la Lista de los personajes que seran usandos en un nivel
        levelCharacters = new List<Character>();

        if (numCharacters == 3)
        {
            for (int i = 0; i < allCharacters.Count; i++)
            {
                if (allCharacters[i].theme == theme)
                {
                    print("levelCharacters: " + i + " " + allCharacters[i].nameCharacter + "x: " + allCharacters[i].x + " y: " + allCharacters[i].y);
                    //Añadir a la lista de personajes del nivel
                    levelCharacters.Add(new Character(allCharacters[i].nameCharacter, allCharacters[i].theme, allCharacters[i].type, allCharacters[i].x, allCharacters[i].y));
                   

                    //Añadir a la lista de personajes en pantalla
                    screenCharacters.Add(Instantiate(allCh[i], new Vector3(allCharacters[i].x,allCharacters[i].y, 0), allCh[i].transform.rotation));
                   
                }
            }
        }
        else
        {
            int numType = App.generalController.gameController.RamdonNumber(2, 4);
            for (int i = 0; i < allCharacters.Count; i++)
            {
                if (allCharacters[i].theme == theme && (allCharacters[i].type == 1 || allCharacters[i].type == numType))
                {
                    levelCharacters.Add(new Character(allCharacters[i].nameCharacter, allCharacters[i].theme, allCharacters[i].type, allCharacters[i].x, allCharacters[i].y));

                    screenCharacters.Add(Instantiate(allCh[i], new Vector3(allCharacters[i].x, allCharacters[i].y, 0), allCh[i].transform.rotation));
                } 
            }
        }
    }

    //Metodo para crear todos los personajes del juego
    public void CreateCharacters(GameObject [,] matrix)
    {
        float x;
        float y;
        float x1 = 0;
        float y1 = 0;
        for(int i = 0; i < matrix.GetLength(0); i++)
        {
            for(int j = 0; j < matrix.GetLength(1); j++)
            {
                if(matrix[i,j].GetComponent<Block>().GetID() == 3)
                {
                    x = (matrix[i, j].GetComponent<BoxCollider2D>().size.x)/2;
                    y = (matrix[i, j].GetComponent<BoxCollider2D>().size.y)/2;
                    x1 = matrix[i, j].transform.position.x;
                    y1 = matrix[i, j].transform.position.y;
                }
            }
        }

        allCharacters.Add(new Character("King", "Castle", 1, x1, y1));
        allCharacters.Add(new Character("Knight", "Castle", 2, 0, 0));
        allCharacters.Add(new Character("Miner", "Castle", 3, 0, 0));
        allCharacters.Add(new Character("Caperucita Roja", "Forest", 1, x1, y1));
        allCharacters.Add(new Character("Satyr", "Forest", 2, 0, 0));
        allCharacters.Add(new Character("Robin Hood", "Forest", 3, 0, 0));
        allCharacters.Add(new Character("Pirate", "Sea", 1, x1, y1));
        allCharacters.Add(new Character("Fish", "Sea", 2, 0, 0));
        allCharacters.Add(new Character("Ghost", "Sea", 3, 0, 0));
    }


    //Variable para controlar la posicion del personaje en la escena
    Vector2 move;

    //Variable para acceder a las animaciones de los personajes
    Animator animator;

    //Variable para controlar el movimiento del personaje en la escena
    Rigidbody2D rigidbody2d;

    //Velocidad a la que se mueve el personaje
    float speed = 1f;

    //Variables para controlar el acceso a los movimientos del personaje
    bool walkAllDirection = false;
    bool walkUpDown = false;
    bool walkRightLeft = false;

    //Metodo para activar el movimiento determinado de un personaje especifico
    public void ActivateMovement(int type)
    {
        switch (type)
        {
            case 1:
                //Activar componentes para los personajes que caminan en todas direcciones
                ActivateComponents(type);       
                
                //Activar movimiento en todas la direcciones
                walkAllDirection = true;
                break;
            case 2:
                //Activar componentes para los personajes que caminan hacia arriba y hacia abajo
                ActivateComponents(type);
                
                //Activar movimiento hacia arriba y hacia abajo
                walkUpDown = true;
                walkRightLeft = false;
                walkAllDirection = false;
                break;
            case 3:
                //Activar componentes para los personajes que caminan hacia la derecha y hacia la izquierda
                ActivateComponents(type);

                //Activar movimiento hacia la derecha y hacia la izquierda
                walkRightLeft = true;
                walkUpDown = false;
                walkAllDirection = false;
                break;
            default:
                break;
        }
    }

    //Metodo para activar los componentes que permiten el movimiento de los personajes
    void ActivateComponents(int type)
    {
        for (int i = 0; i < levelCharacters.Count; i++)
        {
            //Buscar los personajes del nivel por su tema
            if (levelCharacters[i].type == type)
            {
                //Activar el rigibody y animator del personaje
                print("ACTIVAR COMPONENTES DE: "+levelCharacters[i].nameCharacter);
                rigidbody2d = screenCharacters[i].GetComponent<Rigidbody2D>();
                animator = screenCharacters[i].GetComponent<Animator>();
                
            }
        }
    }

    //Metodo que indica hacia que direccion debe moverse el personaje
    public void Move(string direction)
    {
        //Verficar que se esta mandando una direccion para mover al personaje
        if(direction!=null)
        {
            if (walkAllDirection == true)
            {
                MoveUpDown(direction);
                MoveRightLeft(direction);
            }
            else if (walkUpDown == true)
            {
                MoveUpDown(direction);
            }
            else if (walkRightLeft == true)
            {
                MoveRightLeft(direction);
            }
        }
    }

    //Metodo para mover al personaje de arriba a abajo
    void MoveUpDown(string direction)
    {
        if (direction == "up")
        {
            move = Vector2.up;
            
            //Activar animaciones del personaje
            animator.SetFloat("mov_y", move.y);
            animator.SetBool("walking", true);
        }
        else if (direction == "down")
        {
            move = Vector2.down;

            //Activar animaciones del personaje
            animator.SetFloat("mov_y", move.y);
            animator.SetBool("walking", true);
        }
    }

    //Metodo para mover al personaje de izquierda a deracha
    void MoveRightLeft(string direction)
    {
        if (direction == "right")
        {
            move = Vector2.right;

            //Activar animaciones del personaje
            animator.SetFloat("mov_x", move.x);
            animator.SetBool("walking", true);
        }
        else if (direction == "left")
        {
            move = Vector2.left;

            //Activar animaciones del personaje
            animator.SetFloat("mov_x", move.x);
            animator.SetBool("walking", true);
        }
    }

    //Metodo que detiene el movimiento del personaje
    public void NotMove()
    {
        //Verificar que el animator del personaje esta activo
        if(animator!=null)
        {
            move = Vector2.zero;
            animator.SetBool("walking", false);
        }
    }

    //Metodo que cambia la posicion del personaje en la escena
    void FixedUpdate()
    {
        //Verificar que el rigibody del personaje esta activo
        if (rigidbody2d != null)
        {
           print("Position: "+rigidbody2d.position + move * speed);
           rigidbody2d.MovePosition(rigidbody2d.position + move * speed * Time.deltaTime);
        }
    }
}
