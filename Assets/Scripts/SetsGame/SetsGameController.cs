using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetsGameController : Reference
{
    public static SetsGameController setsGameController;

    static List<Sprite[]> allUnionImages;
    static List<Sprite[]> allIntersectionImages;

    static List<Sprite> panelImages;
    
    static List<Sprite> buttonImages;

    Sprite correctAnswer;

    bool isUnion;

    int numberTry = 3;

    void Awake()
    {
        if(setsGameController == null)
        {
            setsGameController = this;
            DontDestroyOnLoad(gameObject);

            allUnionImages = new List<Sprite[]>();

            allIntersectionImages = new List<Sprite[]>();

            panelImages = new List<Sprite>();

            buttonImages = new List<Sprite>();

            LoadImages();

            PutImages(SelectQuestionType());

            LoadText();
        }
        else
        {

        }
    }

    public void LoadImages()
    {
        //Este numero representa la cantidad de conjuntos que hay en este caso solo 1
        int setOfImages = 1;
        for (int i = 1; i <= setOfImages; i++)
        {
            //Cargar y guardar un set de imagenes en un array
            Sprite[] spriteslist = Resources.LoadAll<Sprite>("SetsGame/1/Union_"+(i));

            //Guardar el array de imagenes en la lista
            allUnionImages.Add(spriteslist);

            //Cargar y guardar un set de imagenes en un array
            Sprite[] spriteslist1 = Resources.LoadAll<Sprite>("SetsGame/2/Intersection_"+(i));

            //Guardar el array de imagenes en la lista
            allIntersectionImages.Add(spriteslist1);

        }

    }

    public List<Sprite[]> SelectQuestionType()
    {
        int numberType = Random.Range(1,11);
        print("number "+numberType);

        if(numberType >= 1 && numberType <= 5)
        {
            print("union");
            isUnion = true;
            return allUnionImages;
        }
        else
        {
            print("Interseccion");
            isUnion = false;
            return allIntersectionImages;
        }

    }

    public void PutImages(List<Sprite[]> listImages)
    {
        //Lista que guardara las imagenes seleccionadas pero cambiando su orden dentro de la lista
        LoadLists(listImages[0]);
        
        for (int i = 0; i < panelImages.Count; i++)
        {
            //Asignar a cada boton de la vista una imagen diferente
            App.generalView.setsGameView.panels[i].sprite = panelImages[i];
        }

        for (int i = 0; i < buttonImages.Count; i++)
        {
            if(buttonImages[i].name == "correct")
            {
                correctAnswer = buttonImages[i];
            }
            //Asignar a cada boton de la vista una imagen diferente
            App.generalView.setsGameView.buttons[i].image.sprite = buttonImages[i];
        }
    }

    public void LoadLists(Sprite[] list)
    {

        for (int i = 0; i < list.Length; i++)
        {
            if(i < 3) panelImages.Add(list[i]);
            else buttonImages.Add(list[i]);
        }

    }

    public void LoadText ()
    {
        //string title;
        string message;
        if (isUnion)
        {
            //title = "Unión";
            message = "Unión\n\nRealice la unión entre las cartas presentadas y elija la respuesta correcta";

        }
        else
        {
            //title = "Intersección";
            message = "Intersección\n\nRealice la intersección entre las cartas presentadas y elija la respuesta correcta";
        }
        //App.generalView.setsGameView.title.text = title;
        App.generalView.setsGameView.message.text = message;

    }

    public void CheckAnswer(string answer)
    {
        if(answer == correctAnswer.name)
        {
            //Activar el canvas de ganar
            App.generalView.gameOptionsView.WinCanvas.enabled = true;
            print("Respuesta correcta");
        }
        else{
            print("respuesta incorrecta");
            CheckAttempt();
        }
    }

    void CheckAttempt ()
    {
        numberTry--;
        if(numberTry == 0)
        {
            App.generalView.setsGameView.correctAnswer.sprite = correctAnswer;
            App.generalView.setsGameView.loseCanvas.enabled = true;
        }
        else{

            //string messageLose = "Lo siento :( la respuesta  no es correcta \n le quedan "+numberTry+ " intentos";
            //App.generalView.setsGameView.messageLoseCanvas.text = messageLose;
            App.generalView.gameOptionsView.ShowMistakeCanvas(numberTry);
            //App.generalView.setsGameView.tryCanvas.enabled = true;  
        }

        
    }

}
