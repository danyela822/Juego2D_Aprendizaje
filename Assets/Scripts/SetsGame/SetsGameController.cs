using System.Collections.Generic;
using UnityEngine;

public class SetsGameController : Reference
{
    public static SetsGameController setsGameController;

    static List<Sprite[]> allUnionImages;
    static List<Sprite[]> allIntersectionImages;

    // Nuevo codigo 

    static List<Sprite[]> allUnionLevel1Images;
    static List<Sprite[]> allUnionLevel2Images;
    static List<Sprite[]> allUnionLevel3Images;

    static List<Sprite[]> allIntersectionLevel1Images;
    static List<Sprite[]> allIntersectionLevel2Images;
    static List<Sprite[]> allIntersectionLevel3Images;

    static List<Sprite> panelImages;
    
    static List<Sprite> buttonImages;

    Sprite correctAnswer;

    bool isUnion;

    int numberTry = 3;

    static int level = 1;

    int numberSet = 0;

    void Awake()
    {
        if(setsGameController == null)
        {
            setsGameController = this;
            DontDestroyOnLoad(gameObject);

            allUnionImages = new List<Sprite[]>();

            allIntersectionImages = new List<Sprite[]>();

            //Codigo nuevo 

            allUnionLevel1Images = new List<Sprite[]>();

            allUnionLevel2Images = new List<Sprite[]>();

            allUnionLevel3Images = new List<Sprite[]>();

            allIntersectionLevel1Images = new List<Sprite[]>();

            allIntersectionLevel2Images = new List<Sprite[]>();

            allIntersectionLevel3Images = new List<Sprite[]>();

            //panelImages = new List<Sprite>();

            //buttonImages = new List<Sprite>();

            LoadImages();

            StartGame();

            //ActivatePanels();

            //PutImages(SelectQuestionType());

            //LoadText();
        }
        else
        {
            StartGame();
        }
    }

    public int ReturnLevel()
    {
        return level;
    }
    public void StartGame()
    {
        print("start game "+level);
        
        panelImages = new List<Sprite>();
        buttonImages = new List<Sprite>();

        ActivatePanels();
        LoadText();
        QuestionType();
    }

    public void LoadImages()
    {
        //Este numero representa la cantidad de conjuntos que hay en este caso solo 1
        int setOfImages = 3;

        Sprite[] spriteslist;

        for (int i = 1; i <= setOfImages; i++)
        {
            //Cargar y guardar un set de imagenes en un array
            //Guardar el array de imagenes en la lista

            //UNION
            spriteslist = Resources.LoadAll<Sprite>("SetsGame/1/Level1/"+(i));

            allUnionLevel1Images.Add(spriteslist);

            spriteslist = Resources.LoadAll<Sprite>("SetsGame/1/Level2/"+(i));

            allUnionLevel2Images.Add(spriteslist);

            spriteslist = Resources.LoadAll<Sprite>("SetsGame/1/Level3/"+(i));

            allUnionLevel3Images.Add(spriteslist);

            //INTERSECCION
            spriteslist = Resources.LoadAll<Sprite>("SetsGame/2/Level1/"+(i));

            allIntersectionLevel1Images.Add(spriteslist);
            
            spriteslist = Resources.LoadAll<Sprite>("SetsGame/2/Level2/"+(i));

            allIntersectionLevel2Images.Add(spriteslist);

            spriteslist = Resources.LoadAll<Sprite>("SetsGame/2/Level3/"+(i));

            allIntersectionLevel3Images.Add(spriteslist);

        }

    }

    public void ActivatePanels()
    {
        // se cambia el tres por un 4 
        if(level == 1 || level == 4)
        {
            for (int i = 0; i < 2; i++)
            {
                App.generalView.setsGameView.panels[i].enabled = true;
            }
        }
        else
        { 
            for (int i = 2; i < 5; i++)
            {
                App.generalView.setsGameView.panels[i].enabled = true;
            }

        }
    }

    public void DesativatePanels()
    {
        for (int i = 0; i < 5; i++)
        {
            App.generalView.setsGameView.panels[i].enabled = false;
        }
        
    }

    public List<Sprite[]> ImageLevel ()
    {
        List<Sprite[]> imageLevel = new List<Sprite[]>();
        switch (level)
        {
            case 1:
                imageLevel = allUnionLevel1Images;
                break;
            case 2: 
                imageLevel = allUnionLevel2Images;
                break;
            case 3: 
                imageLevel = allUnionLevel3Images;
                break;
            case 4: 
                imageLevel = allIntersectionLevel1Images;
                break;
            case 5: 
                imageLevel = allIntersectionLevel2Images;
                break;
            case 6: 
                imageLevel = allIntersectionLevel3Images;
                break;
        }
        
        return imageLevel;
    }
    /*public List<Sprite[]> ImageUnionLevel ()
    {
        if (level== 1)
        {
            return allUnionLevel1Images;
        }
        else //if(level == 2)
        {
            return allUnionLevel2Images;
        }
    }

    public List<Sprite[]> ImageIntersectionLevel ()
    {
        if (level == 1)
        {
            return allIntersectionLevel1Images;
        }
        else //if(level == 2)
        {
            return allIntersectionLevel2Images;
        }
    }*/

    public void QuestionType()
    {
        List<Sprite[]> listImages = new List<Sprite[]>();
        //Se cambia el 3 por un 4
        listImages = ImageLevel();
        PutImages(listImages);
    }

    /*public void SelectQuestionType(int numberType)
    {
        print("number "+numberType);

        List<Sprite[]> listImages = new List<Sprite[]>();
        if(numberType == 1)
        {
            print("union");
            isUnion = true;
            listImages = ImageLevel();
        }
        else
        {
            print("Interseccion");
            isUnion = false;
            listImages = ImageLevel();
        }
        LoadText();
        PutImages(listImages);
    }*/

    public void PutImages(List<Sprite[]> listImages)
    {
        //Lista que guardara las imagenes seleccionadas pero cambiando su orden dentro de la lista
        numberSet = Random.Range(0, listImages.Count);
        print("Count "+listImages.Count+" numberSet "+numberSet);
        LoadLists(listImages[numberSet]);
        int i = 0; 
        int limit = 2;

        //Se cambia el 3 por un 4
        if(level != 1 && level !=4) 
        {
            i = 2; 
            limit = 5;
        }
        
        int k = 0;
        for (int m = i; m < limit; m++)
        {
            //Asignar a cada boton de la vista una imagen diferente
            App.generalView.setsGameView.panels[m].sprite = panelImages[k];
            k++;
        }

        for (int j = 0; j < buttonImages.Count; j++)
        {
            if(buttonImages[j].name == "correct")
            {
                correctAnswer = buttonImages[j];
            }
            //Asignar a cada boton de la vista una imagen diferente
            App.generalView.setsGameView.buttons[j].image.sprite = buttonImages[j];
        }
        
    }

    /*public void DeleteSet()
    {
        print("number set delete "+numberSet);
        switch (level)
        {
            case 1:
                allUnionLevel1Images.RemoveAt(numberSet);
                break;
            case 2: 
                allUnionLevel2Images.RemoveAt(numberSet);
                break;
            case 3: 
                allIntersectionLevel1Images.RemoveAt(numberSet);
                break;

            case 4: 
                allIntersectionLevel2Images.RemoveAt(numberSet);
                break;
        }
    }*/

    public void LoadLists(Sprite[] list)
    {
        int limit = 2;
        // Se cambia el 3 por un 4
        if(level != 1 && level != 4) limit = 3;

        for (int i = 0; i < list.Length; i++)
        { 
            if(i < limit) 
            {
                panelImages.Add(list[i]);
            }
            else buttonImages.Add(list[i]);
        }

    }

    public void LoadText ()
    {
        string message ="";
        switch (level)
        {
            case 1:
                message = "Unión\n\nRealice la unión entre las cartas presentadas y elija la respuesta correcta.";
                break;
            case 2: 
                message = "Unión\n\nRealice la unión entre las cartas presentadas y elija la respuesta correcta";
                break;
            case 3: 
                message = "Unión\n\nRealice la unión entre las cartas y elija la respuesta correcta, no se fije en el orden";
                break;
            case 4: 
                message = "Intersección\n\nRealice la intersección entre las cartas presentadas y elija la respuesta correcta";
                break;
            case 5: 
                message = "Intersección\n\nRealice la intersección entre las cartas presentadas y elija la respuesta correcta";
                break;
            case 6: 
                message = "Intersección\n\nRealice la intersección entre las cartas y elija la respuesta correcta, no se fije en el orden";
                break;
        }
        App.generalView.setsGameView.TextGame(message);
    }

    /*public void LoadText ()
    {
        string message;
        if (isUnion)
        {
            message = "Unión\n\nRealice la unión entre las cartas presentadas y elija la respuesta correcta";
        }
        else
        {
            message = "Intersección\n\nRealice la intersección entre las cartas presentadas y elija la respuesta correcta";
        }
        App.generalView.setsGameView.message.text = message;
    }*/

    public void CheckAnswer(string answer)
    {
        if(answer == correctAnswer.name)
        {
            //Activar el canvas de ganar
            App.generalView.gameOptionsView.WinCanvas.enabled = true;
            level++;

            numberTry = 3;

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
            App.generalView.setsGameView.loseCanvas.enabled = true;
            numberTry = 3;
            print("Lose canvas");
        }
        else{

            //string messageLose = "Lo siento :( la respuesta  no es correcta \n le quedan "+numberTry+ " intentos";
            //App.generalView.setsGameView.messageLoseCanvas.text = messageLose;
            App.generalView.gameOptionsView.ShowMistakeCanvas(numberTry);
            //App.generalView.setsGameView.tryCanvas.enabled = true;  
        }

        
    }

}
