using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : Reference
{
    //Lista de todos los personajes que pueden estar en un nivel
    List<Character> characters = new List<Character>();

    //Lista de los personajes que aparecen en la pantalla
    List<GameObject> screenCharacters = new List<GameObject>();

    //Metodo para seleccionar los personajes que apareceran en el nivel y ubicarlos en la pantalla.
    //Debe recibir la cantidad de persojes que apareceran y la lista de objetos de los personajes
    public void SelectCharactersLevel(int numCharacters, List<GameObject> allCh)
    {
        //Si son 3 personajes, se añaden a la lista y se ubican en la pantalla
        if (numCharacters == 3)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                //Añadir a la lista de personajes en pantalla     
                screenCharacters.Add(Instantiate(allCh[characters[i].numCharacter], new Vector3(characters[i].x, characters[i].y, 0), allCh[characters[i].numCharacter].transform.rotation));
            }
        }
        //Si son 2, se añade el principal y se elige al azar cual de los 2 restantes se ubica
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

            for (int i = 0; i < characters.Count; i++)
            {
                if (characters[i].type == 1 || characters[i].type == numType)
                {
                    //Añadir a la lista de personajes en pantalla     
                    screenCharacters.Add(Instantiate(allCh[characters[i].numCharacter], new Vector3(characters[i].x, characters[i].y, 0), allCh[characters[i].numCharacter].transform.rotation));
                }
            }
        }
    }

    //Metodo para crear todos los personajes del juego
    public void CreateCharacters(GameObject[,] drawdMatrix, string theme)
    {
        //Variables para capturar las posiciones de la matriz logica
        int posArrayX = 0;
        int posArrayY = 0;

        //Variables para capturar las posiciones de la matriz visual
        float posX = 0;
        float posY = 0;

        for (int i = 0; i < drawdMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < drawdMatrix.GetLength(1); j++)
            {
                //Se recorre la matriz para saber en que pocicion esta el inicio y poder ubicar al personaje principal en esa casilla
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

        switch (theme)
        {
            case "Castle":
                characters.Add(new Character("King", "Castle", 1, posX, posY, posArrayX, posArrayY, 0));
                characters.Add(new Character("Knight", "Castle", 2, posX, posY, posArrayX, posArrayY, 1));
                characters.Add(new Character("Miner", "Castle", 3, posX, posY, posArrayX, posArrayY, 2));
                break;
            case "Forest":
                characters.Add(new Character("Caperucita Roja", "Forest", 1, posX, posY, posArrayX, posArrayY, 3));
                characters.Add(new Character("Satyr", "Forest", 2, posX, posY, posArrayX, posArrayY, 4));
                characters.Add(new Character("Robin Hood", "Forest", 3, posX, posY, posArrayX, posArrayY, 5));
                break;
            case "Sea":
                characters.Add(new Character("Pirate", "Sea", 1, posX, posY, posArrayX, posArrayY, 6));
                characters.Add(new Character("Fish", "Sea", 2, posX, posY, posArrayX, posArrayY, 7));
                characters.Add(new Character("Ghost", "Sea", 3, posX, posY, posArrayX, posArrayY, 8));
                break;
        }
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
        for (int i = 0; i < characters.Count; i++)
        {
            //Buscar los personajes del nivel por su tema

            if (characters[i].type == type)
            {
                //Activar el rigibody y animator del personaje
                character = characters[i];
                characterTransform = screenCharacters[i].transform;
                //animator = screenCharacters[i].GetComponent<Animator>();
            }
        }
    }

    //Metodo que indica hacia que direccion debe moverse el personaje
    public void Move(string direction)
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
            if (App.generalController.gameController.CheckFigurePosition(character.posArrayX - 1, character.posArrayY))
            {
                characterTransform.position = characterTransform.position + vectorUp;
                character.posArrayX = character.posArrayX - 1;
                Debug.Log("POS CHARACTER DESPUES DE UP X: " + character.posArrayX + " Y: " + character.posArrayY);
            }
        }
        else if (direction == "down")
        {
            if (App.generalController.gameController.CheckFigurePosition(character.posArrayX + 1, character.posArrayY))
            {
                characterTransform.position = characterTransform.position + vectorDown;
                character.posArrayX = character.posArrayX + 1;
                Debug.Log("POS CHARACTER DESPUES DE DOWN X: " + character.posArrayX + " Y: " + character.posArrayY);
            }
        }
    }
    //Metodo para mover al personaje de izquierda a deracha
    void MoveRightLeft(string direction)
    {
        if (direction == "right")
        {
            if (App.generalController.gameController.CheckFigurePosition(character.posArrayX, character.posArrayY + 1))
            {
                characterTransform.position = characterTransform.position + vectorRigth;
                character.posArrayY = character.posArrayY + 1;
                Debug.Log("POS CHARACTER DESPUES DE RIGTH X: " + character.posArrayX + " Y: " + character.posArrayY);
            }
        }
        else if (direction == "left")
        {
            if (App.generalController.gameController.CheckFigurePosition(character.posArrayX, character.posArrayY - 1))
            {
                characterTransform.position = characterTransform.position + vectorLetf;
                character.posArrayY = character.posArrayY - 1;
                Debug.Log("POS CHARACTER DESPUES DE LEFT X: " + character.posArrayX + " Y: " + character.posArrayY);
            }
        }
    }

    Vector3 vectorUp;
    Vector3 vectorDown;
    Vector3 vectorRigth;
    Vector3 vectorLetf;

    private void Start()
    {
        vectorUp = new Vector3(0, 0.67f, 0);
        vectorDown = new Vector3(0, -0.67f, 0);
        vectorRigth = new Vector3(0.77f, 0, 0);
        vectorLetf = new Vector3(-0.77f, 0, 0);
    }
}