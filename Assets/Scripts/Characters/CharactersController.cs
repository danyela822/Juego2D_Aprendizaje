using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : Reference
{
    //Lista de todos los personajes que pueden estar en un nivel
    List<Character> characters = new List<Character>();

    //Lista de todos los personajes que aparecen en la pantalla
    List<GameObject> screenCharacters = new List<GameObject>();

    //
    Transform characterTransform;

    //
    Character character;

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

    //Metodo para seleccionar los personajes que apareceran en el nivel y ubicarlos en la pantalla.
    //Debe recibir la cantidad de persojes que apareceran y la lista de objetos de los personajes
    public void SelectCharactersLevel(List<GameObject> allCh)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            //Añadir a la lista de personajes en pantalla     
            screenCharacters.Add(Instantiate(allCh[characters[i].numCharacter], new Vector3(characters[i].x, characters[i].y, 0), allCh[characters[i].numCharacter].transform.rotation));
        }
    }

    //Metodo para crear todos los personajes del juego
    public void CreateCharacters(string theme, int numCharacters)
    {
        //Variables para capturar las posiciones de la matriz logica
        int posArrayX = 0;
        int posArrayY = 0;

        //Variables para capturar las posiciones de la matriz visual
        float posX = 0;
        float posY = 0;

        GameObject [,] drawnMatrix = App.generalController.roadGameController.GetMatrix();
        Objects [,] logicMatrix = App.generalController.roadGameController.ReturnArray();

        //TEMPORAL
        int posArrayX1 = 0, posArrayY1 = 0, posArrayX2 = 0, posArrayY2 = 0;
        float posX1 = 0, posY1 = 0, posX2 = 0, posY2 = 0;

        for (int i = 0; i < logicMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < logicMatrix.GetLength(1); j++)
            {
                //Se recorre la matriz para saber en que pocicion esta el inicio y poder ubicar al personaje principal en esa casilla
                if (logicMatrix[i, j].type == 3)
                {
                    posArrayX = i;
                    posArrayY = j;
                    
                    posX = drawnMatrix[i, j].transform.position.x;
                    posY = drawnMatrix[i, j].transform.position.y;
                    Debug.Log("P1 Array -> X: " + posArrayX + " Y: " + posArrayY + " Transform -> X: " + posX + "Y: " + posY);
                }
                if (logicMatrix[i, j].type == 5)
                {
                    posArrayX1 = i;
                    posArrayY1 = j;

                    posX1 = drawnMatrix[i, j].transform.position.x;
                    posY1 = drawnMatrix[i, j].transform.position.y;
                    Debug.Log("P2 Array -> X: " + posArrayX1 + " Y: " + posArrayY1 + " Transform -> X: " + posX1 + "Y: " + posY1);
                }
                if (logicMatrix[i, j].type == 11)
                {
                    posArrayX2 = i;
                    posArrayY2 = j;

                    posX2 = drawnMatrix[i, j].transform.position.x;
                    posY2 = drawnMatrix[i, j].transform.position.y;
                    Debug.Log("P3 Array -> X: " + posArrayX2+ " Y: " + posArrayY2 + " Transform -> X: " + posX2 + "Y: " + posY2);
                }
            }
        }
        switch (theme)
        {
            case "Castle":
                if(numCharacters == 3)
                {
                    characters.Add(new Character("King", "Castle", 1, posX, posY, posArrayX, posArrayY, 0));
                    characters.Add(new Character("Knight", "Castle", 2, posX1, posY1, posArrayX1, posArrayY1, 1));
                    characters.Add(new Character("Miner", "Castle", 3, posX2, posY2, posArrayX2, posArrayY2, 2));
                }
                else if(numCharacters == 2)
                {
                    characters.Add(new Character("King", "Castle", 1, posX, posY, posArrayX, posArrayY, 0));
                    characters.Add(new Character("Knight", "Castle", 2, posX1, posY1, posArrayX1, posArrayY1, 1));
                    App.generalView.roadGameView.character_3.interactable = false;
                }
                else
                {
                    characters.Add(new Character("King", "Castle", 1, posX, posY, posArrayX, posArrayY, 0));
                    App.generalView.roadGameView.character_2.interactable = false;
                    App.generalView.roadGameView.character_3.interactable = false;
                }

                break;
            case "Forest":
                if (numCharacters == 3)
                {
                    characters.Add(new Character("LittleRedRidingHood", "Forest", 1, posX, posY, posArrayX, posArrayY, 3));
                    characters.Add(new Character("Centaur", "Forest", 2, posX1, posY1, posArrayX1, posArrayY1, 4));
                    characters.Add(new Character("RobinHood", "Forest", 3, posX2, posY2, posArrayX2, posArrayY2, 5));
                }
                else if(numCharacters == 2)
                {
                    characters.Add(new Character("LittleRedRidingHood", "Forest", 1, posX, posY, posArrayX, posArrayY, 3));
                    characters.Add(new Character("Centaur", "Forest", 2, posX1, posY1, posArrayX1, posArrayY1, 4));
                    App.generalView.roadGameView.character_3.interactable = false;
                }
                else
                {
                    characters.Add(new Character("LittleRedRidingHood", "Forest", 1, posX, posY, posArrayX, posArrayY, 3));
                    App.generalView.roadGameView.character_2.interactable = false;
                    App.generalView.roadGameView.character_3.interactable = false;
                }

                break;
            case "Sea":
                if (numCharacters == 3)
                {
                    characters.Add(new Character("Pirate", "Sea", 1, posX, posY, posArrayX, posArrayY, 6));
                    characters.Add(new Character("Fish", "Sea", 2, posX1, posY1, posArrayX1, posArrayY1, 7));
                    characters.Add(new Character("Ghost", "Sea", 3, posX2, posY2, posArrayX2, posArrayY2, 8));
                }
                else if(numCharacters == 2)
                {
                    characters.Add(new Character("Pirate", "Sea", 1, posX, posY, posArrayX, posArrayY, 6));
                    characters.Add(new Character("Fish", "Sea", 2, posX1, posY1, posArrayX1, posArrayY1, 7));
                    App.generalView.roadGameView.character_3.interactable = false;
                }
                else
                {
                    characters.Add(new Character("Pirate", "Sea", 1, posX, posY, posArrayX, posArrayY, 6));
                    App.generalView.roadGameView.character_2.interactable = false;
                    App.generalView.roadGameView.character_3.interactable = false;
                }
                break;
        }
    }

    //Metodo para activar los componentes que permiten el movimiento de los personajes
    public void ActivateComponents(int type)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            //Activar el transform y animator del personaje
            if (characters[i].type == type)
            {
                character = characters[i];
                characterTransform = screenCharacters[i].transform;
                Debug.Log("CHARACTER: "+character.nameCharacter+" UBICACION: "+characterTransform.position);
            }
        }
    }

    //Metodo que indica hacia que direccion debe moverse el personaje
    public void Move(string direction)
    {
        Debug.Log("MOVIMIENTO: " + direction);
        //Verficar que se esta mandando una direccion para mover al personaje
        if (direction != null)
        {
            if (direction == "up")
            {
                Debug.Log("MOVIMIENTO: " + direction);
                if (App.generalController.roadGameController.CheckFigurePositionCharacter(character.posArrayX - 1, character.posArrayY, character.type))
                {
                    characterTransform.position = characterTransform.position + vectorUp;
                    character.posArrayX = character.posArrayX - 1;
                    Debug.Log("POS CHARACTER UP X: " + character.posArrayX + " Y: " + character.posArrayY);
                }
            }
            else if (direction == "down")
            {
                if (App.generalController.roadGameController.CheckFigurePositionCharacter(character.posArrayX + 1, character.posArrayY, character.type))
                {
                    characterTransform.position = characterTransform.position + vectorDown;
                    character.posArrayX = character.posArrayX + 1;
                    Debug.Log("POS CHARACTER DOWN X: " + character.posArrayX + " Y: " + character.posArrayY);
                }
            }
            else if (direction == "right")
            {
                if (App.generalController.roadGameController.CheckFigurePositionCharacter(character.posArrayX, character.posArrayY + 1, character.type))
                {
                    characterTransform.position = characterTransform.position + vectorRigth;
                    character.posArrayY = character.posArrayY + 1;
                    Debug.Log("POS CHARACTER RIGTH X: " + character.posArrayX + " Y: " + character.posArrayY);
                }
            }
            else if (direction == "left")
            {
                if (App.generalController.roadGameController.CheckFigurePositionCharacter(character.posArrayX, character.posArrayY - 1, character.type))
                {
                    characterTransform.position = characterTransform.position + vectorLetf;
                    character.posArrayY = character.posArrayY - 1;
                    Debug.Log("POS CHARACTER LEFT X: " + character.posArrayX + " Y: " + character.posArrayY);
                }
            }
        }

    }
}