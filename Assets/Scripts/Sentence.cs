using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sentence : MonoBehaviour
{
    int NUM_POSIBLES = 10;
    List<Figure> figures;
    List<int> usedIntegers;

    //------------------ interfaz ------------------------------//
    //lista de sprite que se utilizan 
    public List<Sprite> images;
    List<int> numPaint;
    //objeto que contiene el prefab
    public GameObject objRow;
    Vector2 size;
    int correctAnswerGame;
    List<int> numOperation;
    public List<Text> listTextNum = new List<Text>();
    List<int> possibleAnswerGame = new List<int>();
    public List<Button> answerButtonsGame = new List<Button>();

    // Start is called before the first frame update
    void Start(){

        size = objRow.GetComponent<BoxCollider2D>().size;

        usedIntegers = new List<int>();
        Buid(4);
        Debug.Log(OwnToString());
        GetNum();
        DrawTable();
        PaintNum();
        FillAnswer();
    }

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

    private bool ContainsValue(int value){

        for (int i = 0; i < usedIntegers.Count; i++){
            if (usedIntegers[i] == value) return true;
        }
        return false;
    }

    private void AddFigure(Figure figure){
        figures.Add(figure);
    }

    public string OwnToString(){
        string cadena = "";
		foreach (Figure figure in figures) {
			cadena += figure.principal + " = " 
					+ figure.valueC + " " + figure.fig + "\n";
		}
		
		return cadena;
    }

    //me permite obtener los numeros y poder pintarlos
    private void GetNum(){

        numPaint = new List<int>();
        
        foreach (Figure figure in figures){
            numPaint.Add(figure.principal);
            numPaint.Add(figure.fig);
        }
    }

    //metodo que pinta el tablero 
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

    //metodo que halla la respuesta correcta
    void GetNumOperation(){

        numOperation = new List<int>();
        correctAnswerGame = 1;

        foreach (Figure figure in figures){
            numOperation.Add(figure.valueC);
            correctAnswerGame*=figure.valueC;
        }
    }

    private void PaintNum(){

        GetNumOperation();
        for (int i = 0; i < numOperation.Count; i++){
            listTextNum[i].text = "" + numOperation[i];
        }
    }

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

    private void PaintAnswer(){

        List<int> randomResult = RandomList();
        for (int i = 0; i < randomResult.Count; i++){
            answerButtonsGame[i].GetComponentInChildren<Text>().text = "" + randomResult[i];
        }

    }
 

}
