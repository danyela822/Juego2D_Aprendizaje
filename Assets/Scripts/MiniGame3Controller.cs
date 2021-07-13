using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class MiniGame3Controller : Reference
{
    // Start is called before the first frame update
    static List<string[]> operationsType1 = new List<string[]>();
    static List<string[]> operationsType2 = new List<string[]>();
    static List<string[]> operationsType3 = new List<string[]>();
    string result = "";
    
    Color backgroundColor = new Color(255,255,255,255);
    void Start()
    {
        ReadCSV();
    }

    public string Category(int age)
    {
        string type = "";
        if(age == 7)
        {
            type = "1";
        }
        else if(age > 7 && age < 10)
        {
            type = "2";
        }
        else if(age > 9 && age < 12)
        {
            type = "3";
        }
        return type;
    }
    
    void ReadCSV()
    {
        string[] lines = File.ReadAllLines("Assets/Files/Operations.csv"); 

        foreach(var line in lines)
        {
            var values = line.Split(',');

            if(values[0] == "1")
            {
                operationsType1.Add(values);
            }
            else if(values[0] == "2")
            {
                operationsType2.Add(values);
            }
            else
            {
                operationsType3.Add(values);
            }  
        }
    }
    
    public void LoadOperation(string type, List<Text> listText, List<Button> listButton)
    {
        List<string[]> operations = SelectList(type);

        int position = RamdonNumber(0, operations.Count);
        
        var choose = operations[position];

        result = choose[1];

        int index = Array.IndexOf(choose,";");

        ExerciseType(index, choose.Length-1);

        for (int i = 0; i < listText.Count; i++)
        {
            listText[i].text = choose[i+2];
        }

        listText[index-2].text = " ";
        listButton[index-2].image.color = backgroundColor;
    }

    List<string[]> SelectList(string type)
    {
        List<string[]> operations = new List<string[]>();

        if(type == "1")
        {
            operations = operationsType1;
        }
        else if(type == "2")
        {
            operations = operationsType2;
        }
        else if(type == "3")
        {
            operations = operationsType3;
        }

        return operations;
    }
    
    void ExerciseType(int index, int finalPos)
    {
        App.generalView.miniGameView.Question();
        if((index % 2) == 0 && index != finalPos)
        {
            // Como el indice en el cual se encuentra el ; que indica el vacio
            // es par significa que hace falta un numero
            App.generalView.miniGameView.question.text = "¿Cual es el numero correcto para completar la operación?";
            NumbersOption();
        }
        else if(index == finalPos)
        {
            App.generalView.miniGameView.question.text = "¿Cual es el resultado correcto de esta operación?";
            NumbersOption();
        }
        else
        {
            // Como el indice en el cual se encuentra el ; que indica el vacio
            // es impar significa que hace falta un operador
            App.generalView.miniGameView.question.text = "¿Cual es el operador correcto?";
            OperatorsOption();
        }
    }

    // Metodos utilizados cuando las opciones de la operacion corresponden a un operador
    List<string> GenerateOperators()
    {
        List<string> listOperators = new List<string>();

        listOperators.Add("+");
        listOperators.Add("-");
        listOperators.Add("*");
        listOperators.Add("/");

        return listOperators;
    }
    
    void OperatorsOption()
    {
        List<Text> listOptions = App.generalView.miniGameView.listOptions;
        
        List<string> operators = RamdonList(GenerateOperators());

        for (int i = 0; i < listOptions.Count; i++)
        {
            listOptions[i].text = operators[i];
        }
    }

    // Metodos utilizados cuando las opciones de la operacion corresponden a un numero
    List<string> GenerateNumbers()
    {
        List<string> listOperators = new List<string>();
        
        int resultA = Int32.Parse(result);
        
        listOperators.Add(resultA+"");
        listOperators.Add(resultA+RamdonNumber(2,10)+"");
        listOperators.Add(resultA+RamdonNumber(1,5)+"");
        listOperators.Add(resultA-RamdonNumber(1,3)+"");

        return listOperators;
    }
    
    void NumbersOption()
    {
        List<Text> listOptions = App.generalView.miniGameView.listOptions;
        
        List<string> numbers = RamdonList(GenerateNumbers());

        for (int i = 0; i < listOptions.Count; i++)
        {
            listOptions[i].text = numbers[i];
        }
    }

    public void CheckAnswer(string answer)
    {
        bool answerResult = false;
        if(answer == result)
        {
            answerResult = true;
        }
        App.generalController.miniGameController.CorrectAnswer(answerResult);

    }
    // Metodo que desordena una lista
    List<string> RamdonList(List<string> listP)
    {
        List<string> list = listP;
        List<string> listAux = new List<string>();

        while(list.Count > 0)
        {
            int index = RamdonNumber(0, list.Count);
            listAux.Add(list[index]);
            list.RemoveAt(index);
        }
        return listAux;
    }
    
    int RamdonNumber (int min, int max)
    {
        int number = UnityEngine.Random.Range (min, max);
        return number;
    }
}


