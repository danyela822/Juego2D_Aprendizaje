using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructureSequence : MonoBehaviour
{
    //lista que contiene los id de los objetos 
    List<int> iconNumber = new List<int>();    
    //objeto que contiene el prefab
    public GameObject objectRow;
    //variable que me almacena el tamaño del objeto principal
    Vector2 size;
    public List<Sprite> sprites = new List<Sprite>();
    List<int> correctListAnswer = new List<int>();
    List<int> possibleAnswer = new List<int>();
    public List<Button> buttons = new List<Button>();
    List<int> answerToUserButton = new List<int>();
    public GameObject [,] options = new GameObject[3, 4];


    // Start is called before the first frame update
    void Start(){
        size = objectRow.GetComponent<BoxCollider2D>().size;
        CreateListIcon();
        CreateTableGame();
        GetListAnswer();
        GetPossibleAnswer();
        PaintButtons();
    }

    void CreateListIcon(){

        for (int i = 0; i < 6; i++){
            int value = GetContainsNumber();
            iconNumber.Add(value);
            Debug.Log(value);
        }
        
    }

    int GetContainsNumber(){

        bool condition = true;
        int number = 0;
        while(condition){
            number = Random.Range(0, 10);
            if (!iconNumber.Contains(number)) condition = false;
        }

        return number;
    }

    void CreateTableGame(){

        float x = -1.8f;
        float y = 1.93f;
        GameObject sequence;
        int cont = 0;
        int aux = 0;

        for (int i = 0; i < 3; i++){
            for (int j = 0; j < 4; j++){

                //sequence = Instantiate(objectRow,new Vector3(x + (size.x * (j-(0.15f*j))), y - (size.y * (i+(0.05f*i))), 0), objectRow.transform.rotation);
                sequence = Instantiate(objectRow, new Vector3(x + ((size.x-0.25f) *j), y - (size.y * (i + (0.05f * i))), 0), objectRow.transform.rotation);

                if (cont < 6){
                    sequence.GetComponent<SpriteRenderer>().sprite = sprites[iconNumber[cont]];
                    cont++;
                }
                else if (cont == 6 && aux < 2){
                    sequence.GetComponent<SpriteRenderer>().sprite = sprites[iconNumber[aux]];
                    aux ++;
                }
                options[i,j] = sequence;
            }
            
        }

    }

    void GetListAnswer(){

        //correctListAnswer = new List<int>();
        for (int i = 2; i < iconNumber.Count; i++){
            correctListAnswer.Add(iconNumber[i]);
        }
    }

    void GetPossibleAnswer(){

        for (int i = 0; i < correctListAnswer.Count; i++){
            possibleAnswer.Add(correctListAnswer[i]);
        }
        int value = GetContainsNumber();
        possibleAnswer.Add(value);

    }

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

    public void PaintButtons(){

        List<int> answerToPaint = RandomList();
        for (int i = 0; i < buttons.Count; i++){
            //App.generalView.miniGameView.sequenceButtons[i].image.sprite = matches[i].GetComponent<SpriteRenderer>().sprite;
            buttons[i].image.sprite = sprites[answerToPaint[i]];
            buttons[i].GetComponentInChildren<Text>().text = "" + answerToPaint[i];
        }
        
    }

    public void answerForUser(GameObject text){

        int value = int.Parse(text.GetComponent<Text>().text);
        answerToUserButton.Add(value);
        Debug.Log(value);
    }

    public void CheckAnswerForUser(){

        int i = 0;
        for (i = 0; i < answerToUserButton.Count; i++){
            if (answerToUserButton[i] != correctListAnswer[i]){
                Debug.Log("You lose");
                break;
            }
        }

        if (i == 4){
            int aux = 2;
            Debug.Log("You win");
            for (int j = 0; j < 4; j++){
                options[2,j].GetComponent<SpriteRenderer>().sprite = sprites[iconNumber[aux]];
                aux++;
            }
            
        }
        
    }
}
