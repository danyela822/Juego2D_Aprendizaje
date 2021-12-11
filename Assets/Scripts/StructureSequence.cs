using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StructureSequence : MonoBehaviour{
    
    //lista que contiene los id de los objetos 
    List<int> iconNumber = new List<int>();    
    //objeto que contiene el prefab
    public GameObject objectRow;
    //variable que me almacena el tamaño del objeto principal
    Vector2 size;
    //lista publica que contiene los sprites del juego
    public List<Sprite> sprites = new List<Sprite>();
    //lista que contiene la respuesta correcta a cada escenario
    List<int> correctListAnswer = new List<int>();
    //lista que contiene las posibles respuestas que tiene el 
    //usuario para escoger
    List<int> possibleAnswer = new List<int>();
    //lista de los botones de la interfaz
    public List<Button> buttons = new List<Button>();
    //lista auxiliar que contiene las repsutas que escoge el usuario
    List<int> answerToUserButton = new List<int>();
    //matriz que dibuja el escenario
    public GameObject [,] options = new GameObject[3, 4];
    //lista auxiliar que me indica si se puede pintar o despintar
    //la respuesta que da el usuario
    List<int> auxState = new List<int>();
    //contador que me permite saber cuantas opciones a seleccionado
    //el usuario 
    int contToPaint;
    //varibale global que me permite obtener la posicion de la 
    //repsuesta que el usuario a seleccionado 
    int auxPosition = 0;
    //variable auxiliar que me guarda la respuesta del boton que
    //es seleccionado 
    int auxText = 0;
    //varibale auxiliar que me almacena la ultima posicion y poder
    //asi compararla 
    int lastPosition = 0;
    //variable auxiliar que me permite cambiar el estado del panel
    //si puede o no ser ativado
    bool centinela = true;
    //varibel auxiliar que pemrite saber cuantos paneles han sido 
    //activados
    int contToUnPaint = 0;


    // Start is called before the first frame update
    void Start(){
        size = objectRow.GetComponent<BoxCollider2D>().size;
        contToPaint = 0;

        for (int i = 0; i < 5; i++){
            auxState.Add(1);
        }

        CreateListIcon();
        CreateTableGame();
        GetListAnswer();
        GetPossibleAnswer();
        PaintButtons();
    }

    //metodo que gerena una lista de nuemros aleatorios los cuales
    // seran utilizados para la seleccion de imagenes
    void CreateListIcon(){

        for (int i = 0; i < 6; i++){
            int value = GetContainsNumber();
            iconNumber.Add(value);
        }
        
    }

    //metodo que me permite saber si el numero que se genera aleatoriamente
    //ya esta dentro del array, si lo esta me genera otro
    //retorna el numero a almacenar
    int GetContainsNumber(){

        bool condition = true;
        int number = 0;
        while(condition){
            number = Random.Range(0, 10);
            if (!iconNumber.Contains(number)) condition = false;
        }

        return number;
    }

    //metodo que crea el tablero de la escena a partir de una matriz
    void CreateTableGame(){

        float x = -1.83f;
        float y = 1.93f;
        GameObject sequence;
        int cont = 0;
        int aux = 0;

        for (int i = 0; i < 3; i++){
            for (int j = 0; j < 4; j++){

                //se crea la base de todos los game objects del tablero de juego
                sequence = Instantiate(objectRow, 
                    new Vector3(x + (size.x * (j-(0.15f*j))), 
                    y - (size.y * (i+(0.05f*i))), 0), objectRow.transform.rotation);    
                
                //se le asigna la imagen a los primeros 6 game object
                if (cont < 6){
                    sequence.GetComponent<SpriteRenderer>().sprite = sprites[iconNumber[cont]];
                    cont++;
                }

                //se le asigna la imagen a los dos primeros game objects que se
                //repiten, los demas quedan en blanco
                else if (cont == 6 && aux < 2){
                    sequence.GetComponent<SpriteRenderer>().sprite = sprites[iconNumber[aux]];
                    aux ++;
                }
                options[i,j] = sequence;
            }
            
        }

    }

    //me llena la lista que contiene las repsuestas correctas del 
    //los juegos
    void GetListAnswer(){

        for (int i = 2; i < iconNumber.Count; i++){
            correctListAnswer.Add(iconNumber[i]);
        }
    }

    //metodo que llena una lista con las resouestas correctas y le 
    //agrega una mas, esta lista es la que se le muestra al usuario
    //para que escoja la secuencia correcta
    void GetPossibleAnswer(){

        for (int i = 0; i < correctListAnswer.Count; i++){
            possibleAnswer.Add(correctListAnswer[i]);
        }
        int value = GetContainsNumber();
        possibleAnswer.Add(value);

    }

    //metodo que desordena la lista donde esta las repsuesta que 
    //se le muestran al usuario
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

    //metodo que asigna una imagen y un valor a los botones que le 
    //muestran al usuario y poder asi ser comparados
    public void PaintButtons(){

        List<int> answerToPaint = RandomList();
        for (int i = 0; i < buttons.Count; i++){
            buttons[i].image.sprite = sprites[answerToPaint[i]];
            buttons[i].GetComponentInChildren<Text>().text = "" + answerToPaint[i];
        }
        
    }

    //metodo que pemrite seleccionar y deseleccionar los caramelos
    //que escoge el usuario (pinta y despinta)
    public void answerForUser(GameObject text){

        //posicion del caramelo que se escoge
        int position = FindChange(text);
        //pasos que ocurren si se sellecciona el caramelo
        if (auxState[position] == 1 && answerToUserButton.Count < 5 && contToPaint < 4){
            
            auxState[position] = 0; 
            ReturnPosition(position, 0);
            int value = int.Parse(text.GetComponent<Text>().text);
            answerToUserButton.Add(value);
            options[2, contToPaint].GetComponent<SpriteRenderer>().sprite = sprites[value];
            contToPaint++;
            centinela = true;
            
        //pasos que ocurren si se desellecciona el caramelo
        }
        else{
            if (contToPaint == answerToUserButton.Count && auxState[position] == 0 && 
            int.Parse(buttons[position].GetComponentInChildren<Text>().text) == answerToUserButton[answerToUserButton.Count-1]){

                centinela = false;
                lastPosition = answerToUserButton[answerToUserButton.Count-1];
                int aux = int.Parse(buttons[position].GetComponentInChildren<Text>().text);
                ReturnPosition(position, aux);
                auxState[position] = 1;
                options[2, contToPaint-1].GetComponent<SpriteRenderer>().sprite = objectRow.GetComponent<SpriteRenderer>().sprite;
                contToPaint--;
                answerToUserButton.RemoveAt(answerToUserButton.Count-1);  
            }
        }
    }

    public void ReturnPosition(int position, int text){

        auxPosition = position;
        auxText = text;
    }

    //metodo que me permite obtener la posicion del boton 
    //que eligio el usuario
    public int FindChange(GameObject text){

        int position = 0;
        for (int i = 0; i < buttons.Count; i++){
            if (int.Parse(text.GetComponent<Text>().text) == int.Parse(buttons[i].GetComponentInChildren<Text>().text)){
                position = i;
                i = buttons.Count;
            }
            
        }
        return position;
    }

    //metodo que me pemrite activar le panel que le indica al 
    //usuario que ya sleecciono el caramelo
    public void ActivePanel(GameObject panel){

        //pasos que pemriten activar el panel
        if (auxState[auxPosition] == 0 && contToUnPaint < 4){
            panel.GetComponent<Image>().enabled = true;
            contToUnPaint++;

        }
        //pasos que me permiten desactivar el panel
        else if (auxState[auxPosition] == 1 && auxText == lastPosition && centinela == false){   
            centinela = true;
            panel.GetComponent<Image>().enabled = false;
            contToUnPaint--;

        }
    }

    //metodo que verifica si la secuencia que selecciono el usuair
    //es la correcta
    public void CheckAnswerForUser(){

        //indica que perdio 
        int i = 0;
        for (i = 0; i < answerToUserButton.Count; i++){
            if (answerToUserButton[i] != correctListAnswer[i]){
                Debug.Log("You lose");
                break;
            }
        }

        //indica que ganó
        if (i == 4){
            Debug.Log("You win");            
        }
        
    }
}
