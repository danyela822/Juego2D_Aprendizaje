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
                    levelCharacters.Add(new Character(allCharacters[i].nameCharacter, allCharacters[i].theme, allCharacters[i].type, allCharacters[i].x, allCharacters[i].y, allCharacters[i].posArrayX, allCharacters[i].posArrayY));


                    //Añadir a la lista de personajes en pantalla
                    screenCharacters.Add(Instantiate(allCh[i], new Vector3(allCharacters[i].x, allCharacters[i].y, 0), allCh[i].transform.rotation));

                }
            }
        }
        else
        {
            int numType = App.generalController.gameController.RamdonNumber(2, 4);
            print("NUMERO DEL PERSONAJE: " + numType);
            if (numType == 2)
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
                    levelCharacters.Add(new Character(allCharacters[i].nameCharacter, allCharacters[i].theme, allCharacters[i].type, allCharacters[i].x, allCharacters[i].y, allCharacters[i].posArrayX, allCharacters[i].posArrayY));

                    screenCharacters.Add(Instantiate(allCh[i], new Vector3(allCharacters[i].x, allCharacters[i].y, 0), allCh[i].transform.rotation));
                }
            }
        }
    }

    //Metodo para crear todos los personajes del juego
    public void CreateCharacters(GameObject[,] drawdMatrix)
    {
        int posArrayX = 0;
        int posArrayY = 0;
        float posX = 0;
        float posY = 0;
        for (int i = 0; i < drawdMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < drawdMatrix.GetLength(1); j++)
            {
                if (drawdMatrix[i, j].GetComponent<Block>().GetID() == 3)
                {
                    posArrayX = i;
                    posArrayY = j;
                    Debug.Log("INICIAL X: " + posArrayX + " INICIAL Y: " + posArrayY);
                    posX = drawdMatrix[i, j].transform.position.x;
                    posY = drawdMatrix[i, j].transform.position.y;
                }
            }
        }

        allCharacters.Add(new Character("King", "Castle", 1, posX, posY, posArrayX, posArrayY));
        allCharacters.Add(new Character("Knight", "Castle", 2, posX, posY, posArrayX, posArrayY));
        allCharacters.Add(new Character("Miner", "Castle", 3, posX, posY, posArrayX, posArrayY));

        allCharacters.Add(new Character("Caperucita Roja", "Forest", 1, posX, posY, posArrayX, posArrayY));
        allCharacters.Add(new Character("Satyr", "Forest", 2, posX, posY, posArrayX, posArrayY));
        allCharacters.Add(new Character("Robin Hood", "Forest", 3, posX, posY, posArrayX, posArrayY));

        allCharacters.Add(new Character("Pirate", "Sea", 1, posX, posY, posArrayX, posArrayY));
        allCharacters.Add(new Character("Fish", "Sea", 2, posX, posY, posArrayX, posArrayY));
        allCharacters.Add(new Character("Ghost", "Sea", 3, posX, posY, posArrayX, posArrayY));
    }

    //Variable para acceder a las animaciones de los personajes
    Animator animator;

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
    Transform characterTransform;
    Character character;
    //Metodo para activar los componentes que permiten el movimiento de los personajes
    void ActivateComponents(int type)
    {
        for (int i = 0; i < levelCharacters.Count; i++)
        {
            //Buscar los personajes del nivel por su tema
            if (levelCharacters[i].type == type)
            {
                //Activar el rigibody y animator del personaje
                character = levelCharacters[i];
                characterTransform = screenCharacters[i].transform;
                animator = screenCharacters[i].GetComponent<Animator>();
            }
        }
    }

    //Metodo que indica hacia que direccion debe moverse el personaje
    public void Move2(string direction)
    {
        //Verficar que se esta mandando una direccion para mover al personaje
        if (direction != null)
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
            Up();
            //Activar animaciones del personaje
            //animator.SetFloat("mov_y", vectorUp.y);
            //animator.SetBool("walking", true);
        }
        else if (direction == "down" )
        {
            Down();
            //Activar animaciones del personaje
            //animator.SetFloat("mov_y", vectorDown.y);
            //animator.SetBool("walking", true);
        }
    }
    //Metodo para mover al personaje de izquierda a deracha
    void MoveRightLeft(string direction)
    {
        if (direction == "right")
        {
            Right();
            //Activar animaciones del personaje
            //animator.SetFloat("mov_x", vectorRigth.x);
            //animator.SetBool("walking", true);
        }
        else if (direction == "left")
        {
            Left();
            //Activar animaciones del personaje
            //animator.SetFloat("mov_x", vectorLetf.x);
            //animator.SetBool("walking", true);
        }
    }

    Vector3 vectorUp;
    Vector3 vectorDown;
    Vector3 vectorRigth;
    Vector3 vectorLetf;

    private void Start()
    {
        vectorUp = new Vector3(0,0.67f,0);
        vectorDown = new Vector3(0, -0.67f, 0);
        vectorRigth = new Vector3(0.77f, 0, 0);
        vectorLetf = new Vector3(-0.77f, 0, 0);
    }

    public void Move(string direction)
    {
        switch(direction)
        {
            case "up": Up();
                break;
            case "down":Down();
                break;
            case "right":Right();
                break;
            case "left":Left();
                break;
        }
    }
    void Up()
    {
        Debug.Log("POS CHARACTER X: " + character.posArrayX + " Y: " + character.posArrayY);
        if (App.generalController.gameController.CheckFigurePosition(character.posArrayX - 1, character.posArrayY))
        {
            characterTransform.position = characterTransform.position + vectorUp;
            character.posArrayX = character.posArrayX - 1;
            Debug.Log("POS CHARACTER DESPUES DE UP X: " + character.posArrayX + " Y: "+character.posArrayY);
        }
    }
    void Down()
    {
        Debug.Log("POS CHARACTER X: " + character.posArrayX + " Y: " + character.posArrayY);
        if (App.generalController.gameController.CheckFigurePosition(character.posArrayX + 1, character.posArrayY ))
        {
            characterTransform.position = characterTransform.position + vectorDown;
            character.posArrayX = character.posArrayX + 1;
            Debug.Log("POS CHARACTER DESPUES DE DOWN X: " + character.posArrayX + " Y: " + character.posArrayY);
        }
    }
    void Right()
    {
        Debug.Log("POS CHARACTER X: " + character.posArrayX + " Y: " + character.posArrayY);
        if (App.generalController.gameController.CheckFigurePosition(character.posArrayX, character.posArrayY + 1))
        {
            characterTransform.position = characterTransform.position + vectorRigth;
            character.posArrayY = character.posArrayY + 1;
            Debug.Log("POS CHARACTER DESPUES DE RIGTH X: " + character.posArrayX + " Y: " + character.posArrayY);
        }
    }
    void Left()
    {
        Debug.Log("POS CHARACTER X: " + character.posArrayX + " Y: " + character.posArrayY);
        if (App.generalController.gameController.CheckFigurePosition(character.posArrayX, character.posArrayY - 1))
        {
            characterTransform.position = characterTransform.position + vectorLetf;
            character.posArrayY = character.posArrayY - 1;
            Debug.Log("POS CHARACTER DESPUES DE LEFT X: " + character.posArrayX + " Y: " + character.posArrayY);
        }
    }
}
