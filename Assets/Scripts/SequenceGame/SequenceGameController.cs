using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SequenceGameController : Reference{

    //lista que contiene los id de los objetos 
    readonly List<int> iconNumber = new List<int>();    
    //objeto que contiene el prefab
    public GameObject objectRow;
    //variable que me almacena el tamaño del objeto principal
    Vector2 size;

    //-------------------------------Sprite---------------------------//
    //lista publica que contiene los sprites del juego
    public List<Sprite> sprites = new List<Sprite>();

    //lista publica que contiene los sprites del juego nivel 1
    public List<Sprite> spritesLevelOne = new List<Sprite>();

    //lista publica que contiene los sprites del juego nivel 1
    public List<Sprite> spritesLevelTwo = new List<Sprite>();

    //-------------------------------Sprite---------------------------//

    //lista que contiene la respuesta correcta a cada escenario
    readonly List<int> correctListAnswer = new List<int>();

    //lista que contiene las posibles respuestas que tiene el 
    //usuario para escoger
    readonly List<int> possibleAnswer = new List<int>();

    //lista auxiliar que contiene las repsutas que escoge el usuario
    readonly List<int> answerToUserButton = new List<int>();
    //matriz que dibuja el escenario
    public GameObject [,] options = new GameObject[3, 4];

    //lista auxiliar que me indica si se puede pintar o despintar
    //la respuesta que da el usuario
    readonly List<int> auxState = new List<int>();
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

    //NIVEL EN QUE SE ENCUENTRA EL USUARIO 
    int levelUser;

    //Numero de intentos que tiene el jugador para ganar el juego
    int attempts = 3;

    //Numero para indicar el numero de veces que verifica la respuesta correcta
    public int counter;

    //Numero de veces que ha jugado
    int countPlay;

    //
    bool isLastLevel;

    //--------------- mostrar el canvas de solucion -------------------//
    public List<Image> imageList = new List<Image>();
    public GameObject sequenceWindow;
    public GameObject additionEqualityWindow;

    //--------------- mostrar el canvas de solucion -------------------//

    // Start is called before the first frame update
    void Start(){

        levelUser = App.generalModel.sequenceGameModel.GetLevel();
        Debug.Log("NIVEL ACTUAL: " + levelUser);
        size = objectRow.GetComponent<BoxCollider2D>().size;
        contToPaint = 0;

        //ciclo auxiliar que me permite saber el estado de los caramelos
        //si puedeser o no seleccionado 
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

        switch (levelUser){
            //nivel 1
            case 1:
                for (int i = 0; i < 4; i++){
                    int value = GetContainsNumber();
                    iconNumber.Add(value);
                }
            break;
            //nivel 2
            case 2:
                for (int i = 0; i < 6; i++){
                    int value = GetContainsNumber();
                    iconNumber.Add(value);
                }
            break;
            //nivel 3
            default:
                for (int i = 0; i < 6; i++){
                    int value = GetContainsNumber();
                    iconNumber.Add(value);
                }
            break;
        }

        
    }

    //metodo que me permite saber si el numero que se genera aleatoriamente
    //ya esta dentro del array, si lo esta me genera otro
    //retorna el numero a almacenar
    int GetContainsNumber(){

        int num;
        if (levelUser == 1){
            num = 8;
        }else if(levelUser == 2){
            num = 12;
        }else{
            num = 22;
        }

        bool condition = true;
        int number = 0;
        while(condition){
            number = Random.Range(0, num);
            if (!iconNumber.Contains(number)) condition = false;
        }

        return number;
    }

    //metodo que crea el tablero de la escena a partir de una matriz
    void CreateTableGame(){

        float x = -1.8f;
        float y = 1.6f;
        GameObject sequence;
        // int cont = 0;
        // int aux = 0;

        for (int i = 0; i < 3; i++){
            for (int j = 0; j < 4; j++){

                sequence = Instantiate(objectRow, new Vector3(x + ((size.x - 0.25f) * j), y - (size.y * i), 0), objectRow.transform.rotation);

                options[i,j] = sequence;
            }   
        }
        PaintTable();
    }

    //Metodo que permite pintar el tablero de acuerdo al nivel que se 
    //encuentra el usuario 
    void PaintTable(){

        int cont = 0;
        int aux = 0;
        
        switch (levelUser){

            //Nivel uno 
            case 1: 
                for (int i = 0; i < 3; i++){
                    for (int j = 0; j < 4; j++){
                        if (cont < 4){
                            options[i,j].GetComponent<SpriteRenderer>().sprite = spritesLevelOne[iconNumber[cont]];
                            cont++;
                        }

                        else if (i == 1 && j == 0){
                            options[i,j].GetComponent<SpriteRenderer>().sprite = spritesLevelOne[iconNumber[0]];
                        }

                        else if (i == 2){
                            cont = 0;
                            options[i,j].GetComponent<SpriteRenderer>().sprite = spritesLevelOne[iconNumber[cont]];
                            cont ++;
                        }
                    }
                    
                }
            break;
            //nivel 2
            case 2:

                for (int i = 0; i < 3; i++){
                    for (int j = 0; j < 4; j++){

                        //se le asigna la imagen a los primeros 6 game object
                        if (cont < 6){

                            options[i,j].GetComponent<SpriteRenderer>().sprite = spritesLevelTwo[iconNumber[cont]];
                            cont++;
                        }

                        //se le asigna la imagen a los dos primeros game objects que se
                        //repiten, los demas quedan en blanco
                        else if (i == 1 && j > 1 && aux < 2){
                            options[i,j].GetComponent<SpriteRenderer>().sprite = spritesLevelTwo[iconNumber[aux]];
                            aux ++;
                        }

                        //se le asigna la imagen a los dos primeros game objects que se
                        //repiten, los demas quedan en blanco
                        else if (i == 2 && j > 1){
                            cont = 4;
                            options[i,j].GetComponent<SpriteRenderer>().sprite = spritesLevelTwo[iconNumber[cont]];
                            cont ++;
                        }     
                    }
                }
               

            break;
            //nivel 3
            default:

                for (int i = 0; i < 3; i++){
                    for (int j = 0; j < 4; j++){

                        //se le asigna la imagen a los primeros 6 game object
                        if (cont < 6){
                            options[i,j].GetComponent<SpriteRenderer>().sprite = sprites[iconNumber[cont]];
                            cont++;
                        }

                        //se le asigna la imagen a los dos primeros game objects que se
                        //repiten, los demas quedan en blanco
                        else if (cont == 6 && aux < 2){
                            options[i,j].GetComponent<SpriteRenderer>().sprite = sprites[iconNumber[aux]];
                            aux ++;
                        }

                    }
                } 
            break;
        }
    }

    //me llena la lista que contiene las repsuestas correctas del 
    //los juegos
    void GetListAnswer(){

        switch (levelUser){
            //nivel 1
            case 1:
                for (int i = 1; i < iconNumber.Count; i++){
                    correctListAnswer.Add(iconNumber[i]);

                }
            break;
            //nivel 2
            case 2:
                correctListAnswer.Add(iconNumber[2]);
                correctListAnswer.Add(iconNumber[3]);
            break;
            //nivel 3
            default:
                for (int i = 2; i < iconNumber.Count; i++){
                    correctListAnswer.Add(iconNumber[i]);
                }
            break;
        }

    }

    //metodo que llena una lista con las resouestas correctas y le 
    //agrega una mas, esta lista es la que se le muestra al usuario
    //para que escoja la secuencia correcta
    void GetPossibleAnswer(){

        //ciclo que agrega las respuestas correctas
        for (int i = 0; i < correctListAnswer.Count; i++){
            possibleAnswer.Add(correctListAnswer[i]);
        }

        //ciclo que completa con numeros aleatorios
        for (int j = correctListAnswer.Count; j < 5; j++){
            int value = GetContainsNumberButton();
            possibleAnswer.Add(value);
        }

    }
    
    //metodo que me permite obtener un numero aleatorio y agregarlo como
    //opcion de seleccion para el usuario 
    int GetContainsNumberButton(){

        int num;
        if (levelUser == 1){
            num = 8;
        }else if(levelUser == 2){
            num = 12;
        }else{
            num = 22;
        }

        bool condition = true;
        int number = 0;
        while(condition){
            number = Random.Range(0, num);
            if (!possibleAnswer.Contains(number)) condition = false;
        }
        return number;
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
        
        switch (levelUser){
            
            case 1:
                
                for (int i = 0; i < App.generalView.sequenceGameView.buttons.Count; i++){
                    App.generalView.sequenceGameView.buttons[i].image.sprite = spritesLevelOne[answerToPaint[i]];
                    App.generalView.sequenceGameView.buttons[i].GetComponentInChildren<Text>().text = "" + answerToPaint[i];
                }
            break;

            case 2:
                
                for (int i = 0; i < App.generalView.sequenceGameView.buttons.Count; i++){
                    App.generalView.sequenceGameView.buttons[i].image.sprite = spritesLevelTwo[answerToPaint[i]];
                    App.generalView.sequenceGameView.buttons[i].GetComponentInChildren<Text>().text = "" + answerToPaint[i];
                }
            break;

            default:
                
                for (int i = 0; i < App.generalView.sequenceGameView.buttons.Count; i++){
                    App.generalView.sequenceGameView.buttons[i].image.sprite = sprites[answerToPaint[i]];
                    App.generalView.sequenceGameView.buttons[i].GetComponentInChildren<Text>().text = "" + answerToPaint[i];
                }
            break;
        }

    }

    //metodo que verifica si la secuencia que selecciono el usuair
    //es la correcta
    public void CheckAnswerForUser(){

        counter++;

        //Verificar si ya jugo un nivel de este juego
        countPlay = App.generalModel.sequenceGameModel.GetTimesPlayed();

        App.generalModel.sequenceGameModel.UpdateTimesPlayed(++countPlay);
        print("HA JUGADO: " + countPlay);

        //indica que perdio 
        int i;
        for (i = 0; i < answerToUserButton.Count; i++)
        {
            if (answerToUserButton[i] != correctListAnswer[i])
            {
                Debug.Log("You lose");
                break;
            }
        }
        //me indica que si selecciona correctamente los caramelos, gana el usuario
        switch (levelUser){
            //nivel 1
            case 1: 
                //indica que ganó
                if (i == 3){
                    Debug.Log("You win");
                    App.generalModel.sequenceGameModel.UpdateLevel(2);
                    SetPointsAndStars();
                }
                else
                {
                    CheckAttempt();
                }
            break;
            //nivel 2
            case 2:
                if (i == 2){
                    Debug.Log("You win");
                    App.generalModel.sequenceGameModel.UpdateLevel(3);
                    SetPointsAndStars();
                }
                else
                {
                    CheckAttempt();
                }
                break;
            //nivel 3
            default:
                if (i == 4){
                    Debug.Log("You win");
                    App.generalModel.sequenceGameModel.UpdateLevel(1);
                    isLastLevel = true;
                    SetPointsAndStars();
                }
                else
                {
                    CheckAttempt();
                }
                break;
        }

    }


    //metodo que pemrite seleccionar y deseleccionar los caramelos
    //que escoge el usuario (pinta y despinta)
    public void AnswerForUser(string text){

        //posicion del caramelo que se escoge
        int position = FindChange(text);

        var contCandy = levelUser switch
        {
            //nivel 1
            1 => 3,
            //nivel 2
            2 => 2,
            //nivel 3
            _ => 4,
        };

        //pasos que ocurren si se sellecciona el caramelo
        if (auxState[position] == 1 && answerToUserButton.Count < contCandy+1 && contToPaint < contCandy){
            
            auxState[position] = 0; 
            ReturnPosition(position, 0);
            int value = int.Parse(text);
            answerToUserButton.Add(value);
            switch (levelUser){
                //nivel 1
                case 1:
                    options[1, contToPaint + 1].GetComponent<SpriteRenderer>().sprite = spritesLevelOne[value];
                break;
                //nivel 2
                case 2:
                    options[2, contToPaint].GetComponent<SpriteRenderer>().sprite = spritesLevelTwo[value];
                break;
                //nivel 3
                default:
                    options[2, contToPaint].GetComponent<SpriteRenderer>().sprite = sprites[value];
                break;
            }
            
            contToPaint++;
            centinela = true;
            
        //pasos que ocurren si se desellecciona el caramelo
        }
        else{
            if (contToPaint == answerToUserButton.Count && auxState[position] == 0 && 
            int.Parse(App.generalView.sequenceGameView.buttons[position].GetComponentInChildren<Text>().text) == answerToUserButton[answerToUserButton.Count-1]){

                centinela = false;
                lastPosition = answerToUserButton[answerToUserButton.Count-1];
                int aux = int.Parse(App.generalView.sequenceGameView.buttons[position].GetComponentInChildren<Text>().text);
                ReturnPosition(position, aux);
                auxState[position] = 1;
                switch (levelUser){
                    //nivel 1
                    case 1:
                        options[1, contToPaint].GetComponent<SpriteRenderer>().sprite = objectRow.GetComponent<SpriteRenderer>().sprite;
                    break;
                    //nivel 2
                    case 2:
                        options[2, contToPaint-1].GetComponent<SpriteRenderer>().sprite = objectRow.GetComponent<SpriteRenderer>().sprite;
                    break;
                    //nivel 3
                    default:
                        options[2, contToPaint-1].GetComponent<SpriteRenderer>().sprite = objectRow.GetComponent<SpriteRenderer>().sprite;
                    break;
                }
                
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
    public int FindChange(string text){

        int position = 0;
        for (int i = 0; i < App.generalView.sequenceGameView.buttons.Count; i++)
        {
            if (int.Parse(text) == int.Parse(App.generalView.sequenceGameView.buttons[i].GetComponentInChildren<Text>().text))
            {
                position = i;
                i = App.generalView.sequenceGameView.buttons.Count;
            }
        }
        return position;
    }

    //metodo que me pemrite activar le panel que le indica al 
    //usuario que ya sleecciono el caramelo
    public void ActivePanel(GameObject panel){

        var contCandy = levelUser switch
        {
            //nivel 1
            1 => 3,
            //nivel 2
            2 => 2,
            //nivel 3
            _ => 4,
        };

        //pasos que pemriten activar el panel
        if (auxState[auxPosition] == 0 && contToUnPaint < contCandy){
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
    /// <summary>
    /// Metodo que asigna los puntos y estrellas que ha ganado el jugador
    /// </summary>
    public void SetPointsAndStars()
    {
        SoundManager.soundManager.PlaySound(4);

        //Declaracion de los puntos y estrellas que ha ganado el juegador
        int points, stars, canvasStars;

        //Declaracion del mensaje a mostrar
        string winMessage;

        //Si gana el juego con 3 intentos suma 30 puntos y gana 3 estrellas
        if (counter == 1)
        {
            points = App.generalModel.sequenceGameModel.GetPoints() + 30;
            stars = App.generalModel.sequenceGameModel.GetStars() + 3;
            canvasStars = 3;
            winMessage = App.generalController.gameOptionsController.winMessages[2];

            //Actualizar las veces que ha ganado 3 estrellas
            App.generalModel.sequenceGameModel.UpdatePerfectWins(App.generalModel.sequenceGameModel.GetPerfectWins() + 1);

            //Actualizar las veces que ha ganado sin errores
            App.generalModel.sequenceGameModel.UpdatePerfectGame(App.generalModel.sequenceGameModel.GetPerfectGame() + 1);
        }
        //Si gana el juego mas de 3 y menos de 9 intentos suma 20 puntos y gana 2 estrellas
        else if (counter == 2)
        {
            points = App.generalModel.sequenceGameModel.GetPoints() + 20;
            stars = App.generalModel.sequenceGameModel.GetStars() + 2;
            canvasStars = 2;
            winMessage = App.generalController.gameOptionsController.winMessages[1];

            //Actualizar las veces que ha ganado sin errores -LE FALTAN DETALLES
            App.generalModel.sequenceGameModel.UpdatePerfectGame(0);
        }
        //Si gana el juego con mas de 9 intentos suma 10 puntos y gana 1 estrella
        else
        {
            points = App.generalModel.sequenceGameModel.GetPoints() + 10;
            stars = App.generalModel.sequenceGameModel.GetStars() + 1;
            canvasStars = 1;
            winMessage = App.generalController.gameOptionsController.winMessages[0];

            //Actualizar las veces que ha ganado sin errores
            App.generalModel.sequenceGameModel.UpdatePerfectGame(0);
        }

        //Actualiza los puntos y estrellas obtenidos
        App.generalModel.sequenceGameModel.UpdatePoints(points);
        App.generalModel.sequenceGameModel.UpdateStars(stars);

        //Mostrar el canvas que indica cuantas estrellas gano
        App.generalView.gameOptionsView.ShowWinCanvas(canvasStars,winMessage,isLastLevel);
    }
    /// <summary>
    /// Metodo para contar y verificar los errores
    /// </summary>
    void CheckAttempt()
    {
        //Dismuir la cantidad de intentos
        attempts--;

        //Si se queda sin intentos pierde el juego
        if (attempts == 0)
        {
            App.generalView.gameOptionsView.ShowLoseCanvas();
        }
        //Si aun le quedan intentos se muestra un mensaje en pantalla
        else
        {
            //Obtener el mensaje de intento
            string attemptMessages = App.generalController.gameOptionsController.attemptMessages[attempts - 1];
            App.generalView.gameOptionsView.ShowMistakeCanvas(attemptMessages);
        }
    }

    public void ShowSolution(){

        //Si hay tickets muestra la solucion
        Debug.Log("HAY: " + App.generalModel.ticketModel.GetTickets() + " TICKETS");
        if (App.generalModel.ticketModel.GetTickets() >= 1)
        {
            Debug.Log("HAS USADO UN PASE");

            App.generalController.ticketController.DecraseTickets();
            App.generalView.gameOptionsView.HideBuyCanvas();
            App.generalView.gameOptionsView.ShowSolutionCanvas();

            //App.generalView.setsGameView.solutionPanel.SetActive(true);
            //App.generalView.setsGameView.solutionImage.sprite = correctAnswer;
            sequenceWindow.SetActive(true);

            switch (levelUser)
            {

                case 1:
                    for (int i = 0; i < imageList.Count - 1; i++)
                    {
                        imageList[i].enabled = true;
                        imageList[i].GetComponent<Image>().sprite = spritesLevelOne[correctListAnswer[i]];
                    }
                    break;

                case 2:
                    for (int i = 1; i < imageList.Count - 1; i++)
                    {
                        imageList[i].enabled = true;
                        imageList[i].GetComponent<Image>().sprite = spritesLevelTwo[correctListAnswer[i - 1]];
                    }
                    break;

                default:
                    for (int i = 0; i < imageList.Count; i++)
                    {
                        imageList[i].enabled = true;
                        imageList[i].GetComponent<Image>().sprite = sprites[correctListAnswer[i]];
                    }
                    break;
            }
        }
        else
        {
            App.generalView.gameOptionsView.ShowTicketsCanvas(3);
        }

        /*bool aux = true;

        if (aux){     
            App.generalView.gameOptionsView.ShowSolutionCanvas();
            sequenceWindow.SetActive(true);
            additionEqualityWindow.SetActive(false);

            switch (levelUser){
        
                case 1:
                    for (int i = 0; i < imageList.Count-1; i++){
                        imageList[i].enabled = true;
                        imageList[i].GetComponent<Image>().sprite = spritesLevelOne[correctListAnswer[i]];
                    } 
                break;

                case 2: 
                    for (int i = 1; i < imageList.Count-1; i++){
                        imageList[i].enabled = true;
                        imageList[i].GetComponent<Image>().sprite = spritesLevelTwo[correctListAnswer[i-1]];            
                    }
                break;

                default:
                    for (int i = 0; i < imageList.Count; i++){
                        imageList[i].enabled = true;
                        imageList[i].GetComponent<Image>().sprite = sprites[correctListAnswer[i]];
                    }
                break;
            }
            
        }*/
    }

}
