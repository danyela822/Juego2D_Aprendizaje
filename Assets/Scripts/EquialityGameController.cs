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
    //
    public Sprite equialitySprite;
    //permite almacenar los id de las figuras izquierda derecha
    List<int> numPaint = new List<int>();
    //objeto que contiene el prefab
    public GameObject objRow;
    //objeto que contiene el prefab del signo
    public GameObject objSing;
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


    //nivel en que esta el usuario 
    int levelUser = 3;
    int level = 0;

    float y;
    float x;

    float signX;
    float signY;

    // Start is called before the first frame update
    void Start(){

        size = objRow.GetComponent<BoxCollider2D>().size;
        ChangeLevel();

        usedIntegers = new List<int>();
        Buid(level);
        Debug.Log(PrintText());
        GetNumOperation();
        GetNum();
        DrawTable();
        FillAnswer();
    }

    void ChangeLevel(){

        switch (levelUser){
            
            case 1: 
                level = 2;
                y = 0.35f;
                x = -1.5f;
                signX = -0.7f;
                signY = 0.33f;
            break;

            case 2:
                level = 3;
                y = 0.6f;
                x = -1.5f;
                signX = -0.7f;
                signY = 0.63f;
            break;

            default:
                level = 4;
                y = 1.25f;
                x = -1.5f;
                signX = -0.7f;
                signY = 1.23f;
            break;
        }

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

    //metodo que permite obtenes un id nuevo para
    //las iguras nuevas
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

    //metodo que permite verificar que no exista un id repetido de las
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
    //para luego poder colocarlos en la pantalla
    private void GetNum(){

        // numPaint = new List<int>();
        
        foreach (Figure figure in figures){
            numPaint.Add(figure.principal);
            for (int i = 0; i < figure.valueC; i++){
                numPaint.Add(figure.fig);
            }
            
        }
    }
    
    private string PrintNumbers(){

        GetNum();
        string res = "";
        for (int i = 0; i < numPaint.Count; i++){
            res += numPaint[i];
        }
        
        return res;
    }


    //metodo que permite la contrucción del tablero del juego
    private void DrawTable(){

        GameObject option;
        GameObject sign;
        
        int icon = 0;

        for (int i = 0; i < level; i++){

            int k = numOperation[i] + 1;
            int j = 0;
            
            sign = Instantiate(objSing,new Vector3(signX, signY - (0.9f*i), 0), objSing.transform.rotation);
            sign.GetComponent<SpriteRenderer>().sprite = equialitySprite;
            

            while(j != k){
        
                if(j == 1){
                    option = Instantiate(objRow,new Vector3(x + (1.6f*j),y - (0.9f*i), 0), objRow.transform.rotation);
                    Sprite sprite = images[numPaint[icon]];
                    option.GetComponent<SpriteRenderer>().sprite = sprite;
                    
                }else{
                    //option = Instantiate(objRow,new Vector3(x + (1.2f*j),y - (0.9f*i), 0), objRow.transform.rotation);
                    option = Instantiate(objRow,new Vector3(x + ((size.x - 0.08f) * j),y - (0.9f*i), 0), objRow.transform.rotation);  
                    Sprite sprite = images[numPaint[icon]];
                    option.GetComponent<SpriteRenderer>().sprite = sprite;  
                } 
                
                icon += 1;
                j +=1 ;
            }
        }

        GameObject firstOption = Instantiate(objRow,
        new Vector3(0f, -2.4f, 0), objRow.transform.rotation);
        Sprite spriteF = images[numPaint[numPaint.Count - 1]];
        firstOption.GetComponent<SpriteRenderer>().sprite = spriteF;
        firstOption.transform.localScale = new Vector3(0.45f,0.45f,0);

        GameObject lastOption = Instantiate(objRow,
        new Vector3(1f, -3.08f, 0), objRow.transform.rotation);
        Sprite spriteL = images[numPaint[0]];
        lastOption.GetComponent<SpriteRenderer>().sprite = spriteL;
        lastOption.transform.localScale = new Vector3(0.45f, 0.45f, 0);
    }


    //metodo que a partir de los valor de las figuras, halla la 
    //respuesta correcta
    void GetNumOperation(){

        //lista que obtiene la cantidad que se va a tener de cada figura
        numOperation = new List<int>();
        correctAnswerGame = 1;

        foreach (Figure figure in figures){
            numOperation.Add(figure.valueC);
            correctAnswerGame*=figure.valueC;
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

    //metodo que verifica si la respuesta que da el usuario 
    //es la correcta 
    public void CheckAnswer(string text){

        int auxAnswer = int.Parse(text);
        if (auxAnswer == correctAnswerGame){
            Debug.Log("You win");
        }else{
            Debug.Log("You lose");
        }
    }

    public string PrintText(){

        string res = "";
        foreach (Figure fig in figures){
            res += "figuraUno " + fig.principal + " cantidad " +
            fig.valueC + " figuraDos " + fig.fig + "\n";
        }

        return res;
    }

}
