using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquialityGameController : Reference{
    //id maximo que puede tomar una figura
    int NUM_POSIBLES = 10;
    //lista de figuras (niveles) que cintiene el juego
    List<Figure> figures;
    //lista que evita tener numeros repetidos en los objetos
    List<int> usedIntegers;


    //lista de sprite que se utilizan 
    public List<Sprite> images;
    //permite almacenar los id de las figuras izquierda derecha
    List<int> numPaint;
    //objeto que contiene el prefab
    public GameObject objRow;
    //tamaño que tiene el prefab
    Vector2 size;
    //variable que almacena la respuesta correcta
    int correctAnswerGame;
    //lista que almacena los valores que pemriten hallar la 
    //respuesta correcta
    List<int> numOperation;
    //lista que almacena los numeros que se muestran como posible
    //respuestas del juego
    List<int> possibleAnswerGame = new List<int>();


    // Start is called before the first frame update
    void Start(){

        size = objRow.GetComponent<BoxCollider2D>().size;

        usedIntegers = new List<int>();
        Buid(4);
        GetNum();
        DrawTable();
        PaintNum();
        FillAnswer();
    }

    //metodo que pemrite la construccion de cada uno de los niveles
    public void Buid(int levels){
        figures = new List<Figure>();

        for (int i = 0; i < levels; i++){
            
            switch (i){
                case 0:
                    int principal = Random.Range(1, NUM_POSIBLES);
                    usedIntegers.Add(principal);
                    int value = Random.Range(1, 4);
                    int figure = GetNewValue();
                    usedIntegers.Add(figure);
                    AddFigure(new Figure(principal, value, figure));
                    break;

                default:
                    int valueP = usedIntegers[usedIntegers.Count -1];
                    int val = Random.Range(1, 4);
                    int fig = GetNewValue();
                    usedIntegers.Add(fig);
                    AddFigure(new Figure(valueP, val, fig));
                    break;
            }
        }
    }

    //metodo que permite obtenes un valor nuevo para las figuras
    private int GetNewValue(){

        bool noFound = true;
        int ret = 0;
        while(noFound){
            int value = Random.Range(1, NUM_POSIBLES);
            if (!ContainsValue(value)){
                noFound = false;
                ret = value;
            }
        }
        return ret;
    }

    //metodo que permite verificar que no exista un valor repetido de las
    //figuras
    private bool ContainsValue(int value){

        for (int i = 0; i < usedIntegers.Count; i++){
            if (usedIntegers[i] == value) return true;
        }
        return false;
    }

    //metodo que permite agregar una figura
    private void AddFigure(Figure figure){
        figures.Add(figure);
    }

    //me permite obtener los id de cada figura,
    //para luego poder pintarlos
    private void GetNum(){

        numPaint = new List<int>();
        
        foreach (Figure figure in figures){
            numPaint.Add(figure.principal);
            numPaint.Add(figure.fig);
        }
    }


    //metodo que permite la contrucción del tablero del juego
    private void DrawTable(){

        GameObject option;
        
        float y = 1.27f;
        float x = -1.55f; ;
        int aux = 0;
        for (int i = 0; i < 4; i++){
            for (int j = 0; j < 2; j++){
                
                option = Instantiate(objRow, 
                    new Vector3(x + (size.x * (j+(0.65f*j))), 
                    y - (size.y * (i-(0.3f*i))), 0), objRow.transform.rotation);

                Sprite sprite = images[numPaint[aux]];
                option.GetComponent<SpriteRenderer>().sprite = sprite;
                aux++;
            }
        }

        GameObject firstOption = Instantiate(objRow,
        new Vector3(-0.95f, -3.14f, 0), objRow.transform.rotation);
        Sprite spriteF = images[numPaint[numPaint.Count - 1]];
        firstOption.GetComponent<SpriteRenderer>().sprite = spriteF;

        GameObject lastOption = Instantiate(objRow,
        new Vector3(2.0f, -3.2f, 0), objRow.transform.rotation);
        Sprite spriteL = images[numPaint[0]];
        lastOption.GetComponent<SpriteRenderer>().sprite = spriteL;
    }


    //metodo que a partir de los valor de las figuras, halla la 
    //respuesta correcta
    void GetNumOperation(){

        numOperation = new List<int>();
        correctAnswerGame = 1;

        foreach (Figure figure in figures){
            numOperation.Add(figure.valueC);
            correctAnswerGame*=figure.valueC;
        }
    }

    //metodo que pemrite colocar los valores que son obtenidos 
    //de cada figura
    private void PaintNum(){

        GetNumOperation();
        for (int i = 0; i < numOperation.Count; i++){
            App.generalView.equialityGameView.listTextNum[i].text = "" + numOperation[i];
        }
    }

    //metodo que pemrite obtener numeros aleatorios que seran opcion
    //de respusta a la solucion del problema 
    private void FillAnswer(){

        possibleAnswerGame.Add(correctAnswerGame);
        bool condition = true;
        for (int i = 0; i < 4; i++){
            while (condition){
                int value = Random.Range(0, 20);
                if (!possibleAnswerGame.Contains(value)){
                    possibleAnswerGame.Add(value);
                    condition = false;
                } 
            }
            condition = true;
        }
        PaintAnswer();
    }


    //metodo que permite desordenar los opciones de respuesta
    //que tienen el usuario para la solucion del priblema
    private List<int> RandomList(){

        List<int> list = possibleAnswerGame;
        List<int> aux = new List<int>();
        while(list.Count > 0){
            int i = Random.Range(0 , list.Count);
            aux.Add(list[i]);
            list.RemoveAt(i);
        }
        return aux;
        
    }

    //metodo que coloca las posibles repsutas en los botones de la
    //interfaz
    private void PaintAnswer(){

        List<int> randomResult = RandomList();
        for (int i = 0; i < randomResult.Count; i++){
            App.generalView.equialityGameView.answerButtonsGame[i].GetComponentInChildren<Text>().text = "" + randomResult[i];
        }

    }

    public void CheckAnswer(string text){

        int auxAnswer = int.Parse(text);
        if (auxAnswer == correctAnswerGame){
            Debug.Log("You win");
        }else{
            Debug.Log("You lose");
        }
    }

}
