using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Structure : MonoBehaviour
{
    //------------------------ variables interfaz --------------------//
    //lista de los posibles sprites que seran utilizados
    public List <Sprite> images = new List<Sprite>();
    public GameObject objectRow;
    Vector2 size;
    int aux;
    List<Icon> icons = new List<Icon>();
    List<int> valueReapetIcon = new List<int>();
    public List<Text> listText = new List<Text>();
    List<int> signs = new List<int>();
    public List<Text> resultsText = new List<Text>();
    public List<int> results = new List<int>();
    public List<Button> answerButtons = new List<Button>();
    List<int> possibleAnswer = new List<int>();
    int correctAnswer;
    int levels = 4;
    int initial = 8;
    public Text principalText;
    //----------------------------------------------------------------//

    //------------------------ variables logica ----------------------//
    List<Operation> operationes = new List<Operation>();

    //----------------------------------------------------------------//


    void Start()
    {
        size = objectRow.GetComponent<BoxCollider2D>().size;

        Build(levels, initial);
        Debug.Log(OwnToString());
        GetResult();
        FillAnswer();
        Debug.Log(OwnToString());

    }

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

    public string OwnToString(){

        string ret = "";
        foreach (Operation opt in operationes) {
            ret += opt.OwnToString()+"\n";
        }
        return ret;
    }

    //-----------------------------------------------------------------------//

    public void GetResult(){

        foreach (Operation op in operationes){
            results.Add(op.resultado);
        }

        PaintResult();
    }

    void PaintResult(){

        for (int i = 0; i < resultsText.Count; i++){
            resultsText[i].text = "" + results[i];
        }

    }

//------------------- construccion de la interfaz --------------------------------------------//

    public void CreateTable(){

        int cont = 0;
        aux = 2;

        GameObject firstOption;
        GameObject newOption;
        float x, y;

        firstOption = Instantiate(objectRow,
                    new Vector3(-1.421f, 2.121f, 0), objectRow.transform.rotation);

        Sprite spriteFirst = images[icons[0].idIcon];
        firstOption.GetComponent<SpriteRenderer>().sprite = spriteFirst;

        for (int i = 0; i < 4; i++){

            switch (i){
                case 0:

                    x = -2.31f; y = 0.63f;
                    for (int j = 0; j < 2; j++){
                        newOption = Instantiate(objectRow,
                            new Vector3(x +(size.x * (j-(0.3f*j))), 
                            y, 0), objectRow.transform.rotation);

                        Sprite sprite = images[icons[j].idIcon];
                        newOption.GetComponent<SpriteRenderer>().sprite = sprite;

                        cont+=1;
                    }
                    aux+=1;
                break;

                case 1:
                    //int cont = aux - 1;
                    x = -2.26f; y = -0.27f;
                    for (int j = 0; j < 3; j++){
                        newOption = Instantiate(objectRow,
                            new Vector3(x +(size.x * (j-(0.3f*j))), 
                            y, 0), objectRow.transform.rotation);

                        Sprite sprite = images[icons[cont].idIcon];
                        newOption.GetComponent<SpriteRenderer>().sprite = sprite;

                        cont += 1;
                    }
                    aux+=1;
                break;

                case 2:

                    x = -2.21f; y = -1.17f;
                    for (int j = 0; j < 4; j++){
                        newOption = Instantiate(objectRow,
                            new Vector3(x +(size.x * (j-(0.3f*j))), 
                            y, 0), objectRow.transform.rotation);

                        Sprite sprite = images[icons[cont].idIcon];
                        newOption.GetComponent<SpriteRenderer>().sprite = sprite;

                        cont += 1;
                    }
                    aux+=1;
                break;

                default:
                    x = -2.21f; y = -2.09f;
                    for (int j = 0; j < 5; j++){
                        newOption = Instantiate(objectRow,
                            new Vector3(x +(size.x * (j-(0.3f*j))), 
                            y, 0), objectRow.transform.rotation);

                        Sprite sprite = images[icons[cont].idIcon];
                        newOption.GetComponent<SpriteRenderer>().sprite = sprite;

                        cont += 1;
                    }
                break;
            }       
        }
        GetSign();
    }

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
        Debug.Log("respuesta " +correctAnswer);
        CreateTable();
    }

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

    public bool CheckRepeated(int value)
    {
        bool condition = false;
        foreach (int num in valueReapetIcon){
            if (value == num) condition = true;   
        }
        return condition;
    }

    public int GetValueImageRepeat(int value){
        
        int aux = 0;
        foreach (Icon icon in icons){
            if (value == icon.idValue){
                aux = icon.idIcon;
            }
        }
        return aux;
    }

    public void GetSign(){

        int sign = 0;
        foreach (Operation operation in operationes){
            for(int i = 0; i < operation.operands.Count; i++){
                sign = operation.operands[i].operatorValue;
                signs.Add(sign);
                PaintSigns();
            }
        }
    }

    public void PaintSigns(){

        principalText.text = "=   " + initial;

        for(int i = 0; i < signs.Count; i++){
            if (signs[i] == 1){
                listText[i].text = "-";
            }else if(signs[i] == 2){
                listText[i].text = "+";
            }else{
                listText[i].text = "=";
            }

        }
    }

    public string OwnToStringIcon(){
        
        string cadena = "";
        foreach (Icon icon in icons){
            cadena += icon.idIcon +", " + icon.idValue + "\n";
        }
        return cadena;
    }

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

    private List<int> RandomList(){

        List<int> list = possibleAnswer;
        List<int> aux = new List<int>();
        while(list.Count > 0){
            int i = Random.Range(0 , list.Count);
            aux.Add(list[i]);
            list.RemoveAt(i);
        }

        //correctAnswer = results[results.Count-1];
        return aux;
        
    }

    private void PaintAnswer(){

        List<int> randomResult = RandomList();
        for (int i = 0; i < randomResult.Count; i++){
            answerButtons[i].GetComponentInChildren<Text>().text = "" + randomResult[i];
        }

    }

    public void CheckAnswer(GameObject text){

        string answer = text.GetComponent<Text>().text;
        int auxAnswer = int.Parse(answer);
        if (auxAnswer == correctAnswer)
        {
            Debug.Log("You win");
        }else{
            Debug.Log("You lose");
        }
    }
    
}
