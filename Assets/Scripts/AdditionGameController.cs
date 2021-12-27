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
    //lista que permite colocar las imagenes
    List<Icon> icons = new List<Icon>();
    //lista que contiene id de iconos que permite que no se repitan
    List<int> valueReapetIcon = new List<int>();
    //Lista de las operaciones del juego
    List<Operation> operationes = new List<Operation>();
    //lista de los signos que se van a pintar
    List<int> signs = new List<int>();
    //numero de niveles
    int levels = 4;
    //numero incial para las operaciones
    int initial = 8;
    //varibale que almacena la respuesta correcta
    int correctAnswer;
    //objeto que contiene el prefab
    public GameObject objectRow;
    //variable que me almacena el tamaño del objeto principal
    Vector2 size;

    //variable que me indica en que nivel se encuentra el usuario 
    int levelPosition = 3;


    // Start is called before the first frame update
    void Start()
    {
        size = objectRow.GetComponent<BoxCollider2D>().size;

        Build(levels, initial);
        //Debug.Log(OwnToString());
        GetResult();
        FillAnswer();
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
        CreateTable();
    }

    //Metodo que genera numeros aleatorio
    //que son utilizados como id en la imagenes
    public int GetValueImage(){

        int value = 0;
        bool condition = true;

        while (condition){
            value = Random.Range(0, 30);
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

    //nivel uno 
    //x = -2 y = 1.07

    //nivel dos 
    //x = -2 y = 0.99

    //nivel tres
    //x = -2 y = 2.25
    //distancia de 1.26

    //metodo que crea la estructura visual del juego 
    public void CreateTable(){

        GameObject firstOption;
        GameObject newOption;
        float x, y;
        int firstLevel = 2;
        int secondLevel = 3;
        int thirdLevel = 4;
        int cont = 2;
        int auxImage = 0;


        switch (levelPosition){
            
            case 1:

                firstOption = Instantiate(objectRow,new Vector3(-2, 1.07f, 0), objectRow.transform.rotation);
                
                x = -2; y = -0.1f;

                for (int i = 0; i < firstLevel; i++){

                    for (int j = 0; j < cont; j++){

                        newOption = Instantiate(objectRow, new Vector3(x +(size.x * j), y - (size.y*i), 0), objectRow.transform.rotation);

                        Sprite sprite = images[icons[auxImage].idIcon];
                        newOption.GetComponent<SpriteRenderer>().sprite = sprite;

                        auxImage += 1;
                        
                    }
                    cont+= 1;
                    
                }

            break;

            case 2:

                firstOption = Instantiate(objectRow,new Vector3(-2, 0.99f, 0), objectRow.transform.rotation);
                
                x = -2; y = -0.1f;

                for (int i = 0; i < secondLevel; i++){

                    for (int j = 0; j < cont; j++){

                        newOption = Instantiate(objectRow, new Vector3(x +(size.x * j), y - (size.y*i), 0), objectRow.transform.rotation);

                        Sprite sprite = images[icons[auxImage].idIcon];
                        newOption.GetComponent<SpriteRenderer>().sprite = sprite;

                        auxImage += 1;
                        
                    }
                    cont+= 1;
                    
                }
            break;
            case 3:

                firstOption = Instantiate(objectRow,new Vector3(-2, 1.5f, 0), objectRow.transform.rotation);
                
                x = -2; y = 0.7f;

                for (int i = 0; i < thirdLevel; i++){

                    for (int j = 0; j < cont; j++){

                        newOption = Instantiate(objectRow, new Vector3(x +(size.x * j), y - (size.y*i), 0), objectRow.transform.rotation);
                        
                        Sprite sprite = images[icons[auxImage].idIcon];
                        newOption.GetComponent<SpriteRenderer>().sprite = sprite;

                        auxImage += 1;
                        
                    }
                    cont+= 1;
                    
                }
            break;
            default:
            break;
        }


        // GameObject gameZone = GameObject.Find("GameZone");

        // int cont = 0;
        // int aux = 2;

        // GameObject firstOption;
        // GameObject newOption;
        // float x, y;

        // firstOption = Instantiate(objectRow,new Vector3(-1, 1.5f, 0), objectRow.transform.rotation);

        // Sprite spriteFirst = images[icons[0].idIcon];
        // firstOption.GetComponent<SpriteRenderer>().sprite = spriteFirst;

        // for (int i = 0; i < levels; i++){

        //     switch (i){
        //         case 0:

        //             x = -2; y = 0.63f;
        //             for (int j = 0; j < 2; j++){

        //                 newOption = Instantiate(objectRow, new Vector3(x +(size.x * j), y, 0), objectRow.transform.rotation);

        //                 Sprite sprite = images[icons[j].idIcon];
        //                 newOption.GetComponent<SpriteRenderer>().sprite = sprite;
        //                 newOption.transform.parent = gameZone.transform;
        //                 cont +=1;
        //             }
        //             aux+=1;
        //         break;

        //         case 1:
        //             //int cont = aux - 1;
        //             x = -2; y = -0.27f;
        //             for (int j = 0; j < 3; j++){
        //                 newOption = Instantiate(objectRow, new Vector3(x + (size.x * j), y, 0), objectRow.transform.rotation);

        //                 Sprite sprite = images[icons[cont].idIcon];
        //                 newOption.GetComponent<SpriteRenderer>().sprite = sprite;
        //                 newOption.transform.parent = gameZone.transform;
        //                 cont += 1;
        //             }
        //             aux+=1;
        //         break;

        //         case 2:

        //             x = -2; y = -1.17f;
        //             for (int j = 0; j < 4; j++){
        //                 newOption = Instantiate(objectRow,new Vector3(x + (size.x * j), y, 0), objectRow.transform.rotation);

        //                 Sprite sprite = images[icons[cont].idIcon];
        //                 newOption.GetComponent<SpriteRenderer>().sprite = sprite;
        //                 newOption.transform.parent = gameZone.transform;
        //                 cont += 1;
        //             }
        //             aux+=1;
        //         break;

        //         default:
        //             x = -2; y = -2.09f;
        //             for (int j = 0; j < 5; j++){
        //                 newOption = Instantiate(objectRow,new Vector3(x + (size.x * j), y, 0), objectRow.transform.rotation);

        //                 Sprite sprite = images[icons[cont].idIcon];
        //                 newOption.GetComponent<SpriteRenderer>().sprite = sprite;
        //                 newOption.transform.parent = gameZone.transform;
        //                 cont += 1;
        //             }
        //         break;
        //     }       
        // }
        GetSign();
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
        PaintSigns();
    }

    //metodo que coloca en la interzas los simbolos hallados en el 
    //metodo getsign
    public void PaintSigns(){

        App.generalView.additionGameView.principalText.text =  "=   " + initial;
        for(int i = 0; i < signs.Count; i++){
            if (signs[i] == 1){
                App.generalView.additionGameView.listText[i].text = "-";
            }else if(signs[i] == 2){
                App.generalView.additionGameView.listText[i].text = "+";
            }else{
                App.generalView.additionGameView.listText[i].text = "=";
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

        foreach (Operation op in operationes){
            results.Add(op.resultado);
        }
        PaintResult();
    }

    //metodo que permite colocar los resultados que cada una de las operaciones
    //en la pantalla
    void PaintResult(){

        for (int i = 0; i < App.generalView.additionGameView.resultsText.Count; i++){
            App.generalView.additionGameView.resultsText[i].text = "" + results[i];     
        }

    }
}
