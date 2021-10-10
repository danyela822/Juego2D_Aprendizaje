using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ClassificationGameController : Reference
{
    Sprite[,] allPictures;
    string[] allTexts;
    int number = -1;
    private void Start()
    {
        //Selecionar una carpeta al azar
        number = Random.Range(0, 4);
        LoadPictures();
        PutPictures();
        LoadTexts();
        PutText();
    }
    public void LoadPictures()
    {
        allPictures = new Sprite[4, 16];

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
        StreamReader reader = new StreamReader("Assets/Resources/Files/statements.txt");

        //string para almacenar linea a linea el contenido del texto
        string line;
        line = reader.ReadLine();

        allTexts = new string[4];
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
        Debug.Log("TEXT: " + text);
        App.generalView.classificationGameView.text.text = text;
    }
    public void PutPictures()
    {
        List<Sprite> pictures = ChangeOrderList(SelectPictures(number));

        for (int i = 0; i < pictures.Count; i++)
        {
            App.generalView.classificationGameView.buttons[i].image.sprite = pictures[i];
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

