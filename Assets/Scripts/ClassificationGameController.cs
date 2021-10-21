using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ClassificationGameController : Reference
{
    Sprite[,] allPictures;
    string[] allTexts;
    List<string[]> allAnswers;
    int number = -1;
    List<string>  choises;
    private void Start()
    {
        //Selecionar una carpeta al azar
        number = Random.Range(0,9);
        LoadPictures();
        PutPictures();
        LoadTexts();
        PutText();
        LoadAnswers();
        choises = new List<string>();
    }
    public void LoadPictures()
    {
        allPictures = new Sprite[10, 16];

        for (int i = 0; i < allPictures.GetLength(0); i++)
        {
            Sprite[] spriteslist = Resources.LoadAll<Sprite>("Sets/set_"+(i+1));

            for (int j = 0; j < allPictures.GetLength(1); j++)
            {

                allPictures[i,j] = spriteslist[j];
            }            
        }
    }
    public Sprite[,] getPictures()
    {
        return allPictures;
    }
    public Sprite[] SelectPictures(int number)
    {
        Sprite[,] pictures = getPictures();
        Sprite[] selectedPictures = new Sprite[pictures.GetLength(1)];
        for (int j = 0; j < pictures.GetLength(1); j++)
        {
            selectedPictures[j] = pictures[number, j];
        }
        return selectedPictures;
    }
    public void LoadTexts()
    {
        StreamReader reader = new StreamReader("Assets/Resources/Files/statements_sets.txt");

        //string para almacenar linea a linea el contenido del texto
        string line;
        line = reader.ReadLine();

        allTexts = new string[10];
        int index = 0;
        //Continuar leyendo hasta llegar al final del archivo
        while (line != null)
        {
            allTexts[index] = line;
            index++;
            line = reader.ReadLine();
        }
    }
    public string[] getTexts()
    {
        return allTexts;
    }
    public string SelectText(int number)
    {
        string[] texts = getTexts();

        return texts[number];
    }
    public void PutText()
    {
        string text = SelectText(number);
        App.generalView.classificationGameView.statement.text = text;
    }
    public void PutPictures()
    {
        List<Sprite> pictures = ChangeOrderList(SelectPictures(number));

        for (int i = 0; i < pictures.Count; i++)
        {
            App.generalView.classificationGameView.buttons[i].image.sprite = pictures[i];
        }
    }
    public List<string[]> GetAnswers()
    {
        return allAnswers;
    }
    public void LoadAnswers()
    {
        StreamReader reader = new StreamReader("Assets/Resources/Files/correct_sets.txt");

        //string para almacenar linea a linea el contenido del texto
        string line;
        line = reader.ReadLine();

        allAnswers = new List<string[]>();

        //Continuar leyendo hasta llegar al final del archivo
        while (line != null)
        {
            char separator = ',';
            string[] values = line.Split(separator);
            allAnswers.Add(values);
            line = reader.ReadLine();        
        }
    }
    public string[] SelectAnswers(int number)
    {
        string[] correctAnsers = GetAnswers()[number];
        return correctAnsers;
    }
    public void saveChoise(string choise)
    {
        choises.Add(choise);
    }
    public bool CheckAnswer()
    {
        string[] answers = SelectAnswers(number);
        int count = 0;
        if (choises.Count == answers.Length)
        {
            for (int i = 0; i < answers.Length; i++)
            {
                if (choises.Contains(answers[i]))
                {
                    Debug.Log("R: " + answers[i]);
                    count++;
                    Debug.Log("COUNT: " + count);
                }
            }
            if(count == answers.Length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public List<Sprite> ChangeOrderList (Sprite[] list)
    {
        List<Sprite> originalList = new List<Sprite>();

        for (int i = 0; i < list.Length; i++)
        {
            originalList.Add(list[i]);
        }

        List<Sprite> newList = new List<Sprite>();


        while (originalList.Count > 0)
        {
            int index = Random.Range(0, originalList.Count - 1);
            newList.Add(originalList[index]);
            originalList.RemoveAt(index);
        }

        return newList;
    }
}

