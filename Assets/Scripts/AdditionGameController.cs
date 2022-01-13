using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdditionGameController : Reference{

    //lista que contienen las respuestas a mostrar en desorden
    List<int> possibleAnswer = new List<int>();
    //lista que tiene los resultaods a pintar
    List<int> results = new List<int>();
    //lista de los posibles sprites que seran utilizados
    public List <Sprite> images = new List<Sprite>();

    //sprite del +
    public Sprite signPlus;
    //sprite del -
    public Sprite signLess;
    //sprite del =
    public Sprite signEquals;

    //lista que permite colocar las imagenes
    List<Icon> icons = new List<Icon>();
    //lista que contiene id de iconos que permite que no se repitan
    List<int> valueReapetIcon = new List<int>();
    //Lista de las operaciones del juego
    List<Operation> operationes = new List<Operation>();
    //lista de los signos que se van a pintar
    List<int> signs = new List<int>();
    //numero incial para las operaciones
    int initial = 8;
    //varibale que almacena la respuesta correcta
    int correctAnswer;
    //objeto que contiene el prefab
    public GameObject objectRow;

    public GameObject objectSign;
    //variable que me almacena el tamaño del objeto principal
    Vector2 size;

    Vector2 sizeSign;

    //lsitas que contienen los campos de textos en donde aparece
    //si es mas, menos o igual
    public List<Text> textListLevelOne = new List<Text>();
    public List<Text> textListLevelTwo = new List<Text>();
    public List<Text> textListLevelThree = new List<Text>();

    //coordenadas en las que aparece los objetos
    float y;
    float x;

    //coordenadas donde aparecen los signo
    float signX;
    float signY;

    //nivel en el que el usuario aparece
    int levelUser = 3;
    //numero que me indica cuantos niveles tendra el juego
    int level = 0;


    // Start is called before the first frame update
    void Start()
    {
        sizeSign = objectSign.GetComponent<BoxCollider2D>().size;

        size = objectRow.GetComponent<BoxCollider2D>().size;
        chooseLevel();
        Build(level, initial);
        //Debug.Log(OwnToString());
        GetResult();
        FillAnswer();
       
    }

    //metodo que verifica en que nivel entra el usuario y dependiendo de eso
    //cambian las coorcenadas de los signos y objetos
    void chooseLevel(){
        switch (levelUser){
            //nivel 1
            case 1: 
                level = 2;
                y = -0.1f; x = -2f;
                signX = -1.6f; signY = -0.06f;
                //activa los campos de texto del nivel 1
                for (int i = 0; i < textListLevelOne.Count; i++){
                    textListLevelOne[i].enabled = true; 
                }

            break;
            //nivel 2
            case 2:
                level = 3;
                y = 0.1f; x = -2f;
                signX = -1.6f; signY = 0.04f;
                //activa los campos de texto del nivel 2
                for (int i = 0; i < textListLevelTwo.Count; i++){
                    textListLevelTwo[i].enabled = true; 
                }

            break;
            //nivel 3
            default:
                level = 4;
                y = 0.7f; x = -2f;
                signX = -1.6f; signY = 0.73f;
                //activa los campos de texto del nivel 3
                for (int i = 0; i < textListLevelThree.Count; i++){
                    textListLevelThree[i].enabled = true; 
                }
            break;
        }
    }

   //Metodo que contruye la estructura del juego 
    public void Build (int numLevels, int numInitial){

        int numBase = 2;
        List<int> integersUsed = new List<int>();
        for (int i = 0; i < numLevels; i++){
            switch (i){
                case 0: operationes.Add(new Operation(numBase, integersUsed, numInitial));
                break;
                default:operationes.Add(new Operation(numBase + i, integersUsed));
                break;
            }
        }
        GetImage();
    }

    //metodo que permite a partir de cada una de las operaciones
    //genera un id y verificar si esta repetido 
    public void GetImage(){
        
        int aux = 0;
        int value = 0;
        int image = 0;
        foreach (Operation operation in operationes){
            for(int i = 0; i < operation.operands.Count; i++){
                if (aux < 2 || i == operation.operands.Count-1){

                    value = operation.Operands[i].valueOperand;
                    image = GetValueImage();
                    icons.Add(new Icon(value, image));
                    aux+=1;

                }else {
                    value = operation.Operands[i].valueOperand;
                    image = GetValueImageRepeat(value);
                    icons.Add(new Icon(value, image)); 
                }   
            }
        }
        correctAnswer = icons[icons.Count -1].idValue;
        
        GetSign();
        CreateTable();
    }

    //Metodo que genera numeros aleatorio
    //que son utilizados como id en la imagenes
    public int GetValueImage(){

        int value = 0;
        bool condition = true;

        while (condition){
            value = Random.Range(0, 20);
            if (!CheckRepeated(value)) condition = false;
        }
        valueReapetIcon.Add(value);
        return value;
    }

    //metodo que verifica si el valor que se hallo, ya se encuentra en la lista
    //si lo esta, genera uno nuevo
    public bool CheckRepeated(int value){
        bool condition = false;
        foreach (int num in valueReapetIcon){
            if (value == num) condition = true;   
        }
        return condition;
    }

    //metodo que verifica si dentro de la lista de iconos
    //se repite un numero y si lo hay, esa posicion toma dicho valor
    public int GetValueImageRepeat(int value){
        
        int aux = 0;
        foreach (Icon icon in icons){
            if (value == icon.idValue){
                aux = icon.idIcon;
            }
        }
        return aux;
    }


    //metodo que crea la estructura visual del juego 
    public void CreateTable(){

        GameObject firstOption;
        GameObject sign;
        GameObject signFirts;
        GameObject newOption;

        int cont = 2;
        int auxImage = 0;
        int auxSign = 0;
        
        firstOption = Instantiate(objectRow,new Vector3(-2, 1.6f, 0), objectRow.transform.rotation);
        signFirts = Instantiate(objectRow,new Vector3(-1.4f, 1.6f, 0), objectRow.transform.rotation);
        Sprite spriteFirst = images[icons[0].idIcon];
        firstOption.GetComponent<SpriteRenderer>().sprite = spriteFirst;
        signFirts.GetComponent<SpriteRenderer>().sprite = signEquals;

        for (int i = 0; i < level; i++){
                    
            for (int j = 0; j < cont; j++){

                newOption = Instantiate(objectRow, new Vector3(x + ((size.x - 0.53f) * j), y - ((size.y - 0.5f)*i), 0), objectRow.transform.rotation);

                Sprite sprite = images[icons[auxImage].idIcon];
                newOption.GetComponent<SpriteRenderer>().sprite = sprite;

                auxImage += 1;


                sign = Instantiate(objectSign, new Vector3(signX +((sizeSign.x - 0.53f)* j), signY - ((sizeSign.y + 0.1f) * i), 0), objectSign.transform.rotation);

                switch (signs[auxSign]){
                                
                    case 1: 
                        sign.GetComponent<SpriteRenderer>().sprite = signLess;
                    break;
                    case 2:
                        sign.GetComponent<SpriteRenderer>().sprite = signPlus;
                    break;
                    default:
                        sign.GetComponent<SpriteRenderer>().sprite = signEquals;
                    break;
                } 
                auxSign++;
                        
            }
            cont+= 1;           
        }       
    }

    //metodo que recorre la lista de operaciones y obtiene
    //los simbolos de "+" o "-"
    public void GetSign(){
        int sign = 0;
        foreach (Operation operation in operationes){
            for(int i = 0; i < operation.operands.Count; i++){
                sign = operation.operands[i].operatorValue;
                signs.Add(sign);
            }
        }
    }

    //metodo que almacena en una lista las posibles respuestas
    //que tiene el usuario para escoge
    //halladas aleatoriamente
    private void FillAnswer(){

        possibleAnswer.Add(correctAnswer);
        bool condition = true;
        for (int i = 0; i < 4; i++){
            while (condition){
                int value = Random.Range(0, 40);
                if (!possibleAnswer.Contains(value)){
                    possibleAnswer.Add(value);
                    condition = false;
                } 
            }
            condition = true;
        }
        PaintAnswer();
    }

    //metodo que a partir de las lista de las posibles respuestas
    //las desordena 
    private List<int> RandomList(){

        List<int> list = possibleAnswer;
        List<int> aux = new List<int>();
        while(list.Count > 0){
            int i = Random.Range(0 , list.Count);
            aux.Add(list[i]);
            list.RemoveAt(i);
        }
        return aux;       
    }

    //metodo coloca en los botones las posibles respuestas
    private void PaintAnswer(){

        List<int> randomResult = RandomList();
        for (int i = 0; i < randomResult.Count; i++){
            App.generalView.additionGameView.answerButtons[i].GetComponentInChildren<Text>().text = "" + randomResult[i];
        }
        //return randomResult;

    }

    //metodo que verifica que la repsuesta que escogio el usuario
    //sea la correcta
    public void CheckAnswer(string text){

        int auxAnswer = int.Parse(text);
        if (auxAnswer == correctAnswer){
            Debug.Log("You win");
        }else{
            Debug.Log("You lose");
        }
    }

    //metodo que almacena los resultados para luego ser puestos en 
    //la interfax
    public void GetResult(){

        results.Add(initial);
        foreach (Operation op in operationes){
            results.Add(op.resultado);
        }
        PaintResult();
    }

    //metodo que permite colocar los resultados que cada una de las operaciones
    //en la pantalla
    void PaintResult(){

        switch (levelUser){
            case 1:
                for (int i = 0; i < textListLevelOne.Count; i++){
                    textListLevelOne[i].text = "" + results[i];     
                }
            break;
            case 2:
                for (int i = 0; i < textListLevelTwo.Count; i++){
                    textListLevelTwo[i].text = "" + results[i];     
                }
            break;
            default:
                for (int i = 0; i < textListLevelThree.Count; i++){
                    textListLevelThree[i].text = "" + results[i];     
                }
            break;
        }
    }
}
