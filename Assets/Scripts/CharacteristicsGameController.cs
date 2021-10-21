using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class CharacteristicsGameController : Reference
{
    Sprite [,] allPictures;
    string [] allTexts;
    string[] allAnswers;
    int number;
    string answer;

    private void Start()
    {
        number = Random.Range(0, 2);
        Debug.Log("N: " + number);
        LoadPictures();
        PutPictures();
        LoadTexts();
        PutText();
        LoadAnswers();
    }
    public void LoadPictures()
    {
        allPictures = new Sprite[2, 4];

        for (int i = 0; i < allPictures.GetLength(0); i++)
        {
            Sprite[] spriteslist = Resources.LoadAll<Sprite>("Characteristics/characteristics_" + (i + 1));

            for (int j = 0; j < allPictures.GetLength(1); j++)
            {

                allPictures[i, j] = spriteslist[j];
            }
        }
    }
    public Sprite[] SelectPictures(int number)
    {
        Sprite[,] pictures = allPictures;
        Sprite[] selectedPictures = new Sprite[pictures.GetLength(1)];
        for (int j = 0; j < pictures.GetLength(1); j++)
        {
            selectedPictures[j] = pictures[number, j];
        }
        return selectedPictures;
    }
    public void PutPictures()
    {
        List<Sprite> pictures = ChangeOrderList(SelectPictures(number));

        for (int i = 0; i < pictures.Count; i++)
        {
            App.generalView.characteristicsGameView.buttons[i].image.sprite = pictures[i];
        }
    }
    public void LoadTexts()
    {
        StreamReader reader = new StreamReader("Assets/Resources/Files/statements_characteristics.txt");

        //string para almacenar linea a linea el contenido del texto
        string line;
        line = reader.ReadLine();

        allTexts = new string[2];
        int index = 0;
        //Continuar leyendo hasta llegar al final del archivo
        while (line != null)
        {
            allTexts[index] = line;
            index++;
            line = reader.ReadLine();
        }
    }
    public string SelectText(int number)
    {
        string[] texts = allTexts;
        return texts[number];
    }
    public void PutText()
    {
        string text = SelectText(number);
        App.generalView.characteristicsGameView.statement.text = text;
    }
    public void LoadAnswers()
    {
        StreamReader reader = new StreamReader("Assets/Resources/Files/correct_characteristics.txt");

        //string para almacenar linea a linea el contenido del texto
        string line;
        line = reader.ReadLine();
        int i = 0;
        allAnswers = new string[2];

        //Continuar leyendo hasta llegar al final del archivo
        while (line != null)
        {
            allAnswers[i] = line;
            line = reader.ReadLine();
            i++;
        }
    }
    public void SaveOption(string selectedOption)
    {
        answer = selectedOption;
    }
    public bool CheckAnswer()
    {
        if (answer == allAnswers[number])
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public List<Sprite> ChangeOrderList(Sprite[] list)
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
