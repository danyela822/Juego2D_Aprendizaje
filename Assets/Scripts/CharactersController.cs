using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

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
            print("NUMERO DEL PERSONAJE: " + numType);
            if(numType == 2)
            {
                App.generalView.gameView.character_3.interactable = false;
            }
            else
            {
                App.generalView.gameView.character_2.interactable = false;
            }
           
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

    bool isWalking;
    private Vector2 starPos,nextPos;
    float timeToMove = 0.1f;
    float elapsedTime = 0;
    private IEnumerator MovePlayer(Vector2 direction)
    {
        print("ENTRO A LA CORUTINA");
        isWalking = true;

        //float elapsedTime = 0;

        starPos = rigidbody2d.position;
        nextPos = starPos + direction;

        print("START POS: "+starPos);
        print("NEXT POS: "+nextPos);
        while (elapsedTime < timeToMove)
        {
            
            Vector2 dir = nextPos - starPos;
            RaycastHit2D hit = Physics2D.Raycast(starPos, dir, dir.sqrMagnitude,6);
            elapsedTime += Time.deltaTime;

            // Se verifica mediante un raycast que no se interponga nada al movimiento.
            if (hit.collider == null)
            {
                Vector3 position = Vector3.Lerp(starPos, nextPos, (elapsedTime / timeToMove));
                rigidbody2d.position = position;
                
                if (!isWalking)
                { isWalking = true; }
            }
            else
            {
                Debug.LogError("There is an obstacle, the player can't move");
                nextPos = starPos;
            }
        }
        yield return null;
        
        rigidbody2d.position = nextPos;
        isWalking = false;
    }
    Vector2 direction_1;
    //Metodo para mover al personaje de arriba a abajo
    void MoveUpDown(string direction)
    {

        if (direction == "up" && isWalking == false)
        {
            direction_1 = new Vector2(0, 0.67f);
            move = new Vector2(0, 0.67f);
            //StartCoroutine(MovePlayer(move));
            //StopCoroutine("MovePlayer");
            //move = new Vector2(0, 0.67f);
            //move = Vector3.MoveTowards(transform.position,new Vector3(0,4,0),0f);
            print("MOVE: " + move);
            //Activar animaciones del personaje
            animator.SetFloat("mov_y", move.y);
            animator.SetBool("walking", true);
        }
        else if (direction == "down" && isWalking == false)
        {
            direction_1 = new Vector2(0, -0.67f);
            move = new Vector2(0, -0.67f);
            //StartCoroutine(MovePlayer(move));
            //StopCoroutine("MovePlayer");
            print("MOVE: " + move);
            //Activar animaciones del personaje
            animator.SetFloat("mov_y", move.y);
            animator.SetBool("walking", true);

        }
    }
    //Metodo para mover al personaje de izquierda a deracha
    void MoveRightLeft(string direction)
    {
        if (direction == "right" && isWalking == false)
        {
            direction_1 = new Vector2(0.67f,0);
            move = new Vector2(0.67f,0);
            //StartCoroutine(MovePlayer(move));
            //StopCoroutine("MovePlayer");
            //Activar animaciones del personaje
            animator.SetFloat("mov_x", move.x);
            animator.SetBool("walking", true);
        }
        else if (direction == "left" && isWalking == false)
        {
            direction_1 = new Vector2(-0.67f, 0);
            move = new Vector2(-0.67f,0);
            //StartCoroutine(MovePlayer(move));
            //StopCoroutine("MovePlayer");
            //Activar animaciones del personaje
            animator.SetFloat("mov_x", move.x);
            animator.SetBool("walking", true);
        }
    }

    //Metodo que detiene el movimiento del personaje
    public void NotMove()
    {
        print("LEVANTO BOTON: "+animator.isInitialized);
        //Verificar que el animator del personaje esta activo
        if(animator!=null)
        {
            move = Vector2.zero;
            animator.SetBool("walking", false);
        }
    }

    public LayerMask obstacleMask;

    public bool isTochingTheObstacle()
    {
        Debug.DrawRay(rigidbody2d.position, Vector2.down * 0.4f, Color.red);
        Debug.DrawRay(rigidbody2d.position, Vector2.up * 0.4f, Color.red);
        Debug.DrawRay(rigidbody2d.position, Vector2.right * 0.4f, Color.red);
        Debug.DrawRay(rigidbody2d.position, Vector2.left * 0.4f, Color.red);

        if (Physics2D.Raycast(rigidbody2d.position, Vector2.down, 0.4f, obstacleMask) || Physics2D.Raycast(rigidbody2d.position, Vector2.up, 0.4f, obstacleMask) 
            || Physics2D.Raycast(rigidbody2d.position, Vector2.right, 0.4f, obstacleMask) || Physics2D.Raycast(rigidbody2d.position, Vector2.left, 0.4f, obstacleMask))
        {
            return true;
        }
        else
        {
            return false;
        }

        /*if (Physics2D.Raycast(rigidbody2d.position,Vector2.down, 0.4f, obstacleMask))
        {
            return 1;
        }
        else if(Physics2D.Raycast(rigidbody2d.position, Vector2.up, 0.4f, obstacleMask))
        {
            return 2;
        }
        else if(Physics2D.Raycast(rigidbody2d.position, Vector2.right, 0.4f, obstacleMask))
        {
            return 3;
        }
        else if(Physics2D.Raycast(rigidbody2d.position, Vector2.left, 0.4f, obstacleMask))
        {
            return 4;
        }
        else
        {
            return 0;
        }*/

    }
    IEnumerator MoveI(Vector2 newPosI)
    {
        isWalking = true;

        while((newPosI - rigidbody2d.position).sqrMagnitude > Mathf.Epsilon)
        {
            rigidbody2d.position = Vector2.MoveTowards(rigidbody2d.position, newPosI,speed * Time.fixedDeltaTime);
            yield return null;
        }
        rigidbody2d.position = newPosI;
        isWalking = false;
    }

    Vector2 movement;
    Vector3 moveToPosition;

    //Metodo que cambia la posicion del personaje en la escena
    void FixedUpdate()
    {
        //Verificar que el rigibody del personaje esta activo
        if (rigidbody2d != null)
        {

            if(!isWalking)
            {
                movement.x = CrossPlatformInputManager.GetAxis("Horizontal");
                movement.y = CrossPlatformInputManager.GetAxis("Vertical");

                if (movement.x > 0)
                {
                    movement.x = 0.77f;
                    movement.y = 0;
                    print("X: " + movement.x + " Y: " + movement.y);
                }
                else if (movement.x < 0)
                {
                    movement.x = -0.77f;
                    movement.y = 0;
                    print("X: " + movement.x + " Y: " + movement.y);
                }

                if (movement.y > 0)
                {
                    movement.x = 0;
                    movement.y = 0.67f;
                }
                else if(movement.y < 0)
                {
                    movement.x = 0;
                    movement.y = -0.67f;
                }


               if (movement != Vector2.zero)
               {
                    print("TOCO: " + isTochingTheObstacle());
                    moveToPosition = rigidbody2d.position + new Vector2(movement.x, movement.y);

                    if (!isTochingTheObstacle())
                    {
                        StartCoroutine(MoveI(moveToPosition));
                    }
                    else
                    {
                        moveToPosition = rigidbody2d.position;
                    }
                }

                
                /*if(isTochingTheObstacle() == 1)
                {
                    moveToPosition = rigidbody2d.position + new Vector2(movement.x,0.67f);
                    StartCoroutine(MoveI(moveToPosition));
                }
                else if (isTochingTheObstacle() == 2)
                {
                    moveToPosition = rigidbody2d.position + new Vector2(movement.x,-0.67f);
                    StartCoroutine(MoveI(moveToPosition));
                }
                else if (isTochingTheObstacle() == 3)
                {
                    moveToPosition = rigidbody2d.position + new Vector2(-0.77f, movement.y);
                    StartCoroutine(MoveI(moveToPosition));
                }
                else if (isTochingTheObstacle() == 4)
                {
                    moveToPosition = rigidbody2d.position + new Vector2(0.77f, movement.y);
                    StartCoroutine(MoveI(moveToPosition));
                }
                else
                {
                    moveToPosition = rigidbody2d.position + new Vector2(movement.x, movement.y);
                    StartCoroutine(MoveI(moveToPosition));
                }*/

               
            }

            






            // rigidbody2d.MovePosition(rigidbody2d.position + move * speed * Time.deltaTime);

            //starPos = rigidbody2d.position;
            //nextPos = starPos + direction_1;

            //Vector3 position = Vector3.Lerp(starPos, nextPos, (elapsedTime / timeToMove));
            //rigidbody2d.position = Vector3.MoveTowards(starPos, nextPos, Time.deltaTime * speed);
            //elapsedTime += Time.deltaTime;
            //rigidbody2d.position = position;            

            //movement.x = CrossPlatformInputManager.GetAxis("Horizontal");
            //movement.y = CrossPlatformInputManager.GetAxis("Vertical");

            /*
            // Si el jugador está quieto.
            if (starPos == nextPos)
            {
                isWalking = false;
            }

            // Si el jugador quiere moverse y no está caminando.
            // Se calcula la posición futura.
            if (horizontalMovement != 0 && !isWalking)
            {
                nextPos += Vector2.right * horizontalMovement;
            }
            else if (verticalMovement != 0 && !isWalking)
            {
                nextPos += Vector2.up * verticalMovement;
            }

            // Si la posición futura es distinta de la actual es porque el jugador quiere mover al personaje...
            if (nextPos != starPos)
            {
                print("SI ES DIFERENTE. NEXT: "+nextPos+" START: "+starPos);

                Vector2 dir = nextPos - starPos;
                RaycastHit2D hit = Physics2D.Raycast(starPos, dir, dir.sqrMagnitude, 6);

                // Se verifica mediante un raycast que no se interponga nada al movimiento.
                if (hit.collider == null)
                {
                    rigidbody2d.position = Vector3.MoveTowards(starPos, nextPos, Time.deltaTime * speed);
                    if (!isWalking) 
                    { isWalking = true; }
                }
                else
                {
                    Debug.LogError("There is an obstacle, the player can't move");
                    nextPos = starPos;
                }
            }
            */
        }
    }

}
