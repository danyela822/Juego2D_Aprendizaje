using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquialityGameController : Reference{
    //id maximo que puede tomar una figura
    readonly int NUM_POSIBLES = 10;
    //lista de figuras (niveles) que cintiene el juego
    List<Figure> figures;
    //lista que evita tener numeros repetidos en los objetos
    List<int> usedIntegers;
    //lista de sprite que se utilizan 
    public List<Sprite> images;
    //
    public Sprite equialitySprite;

    //permite almacenar los id de las figuras izquierda derecha
    readonly List<int> numPaint = new List<int>();
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
    readonly List<int> possibleAnswerGame = new List<int>();

    //nivel en que esta el usuario 
    int levelUser;// = App.generalModel.equialityGameModel.GetLevel();
    int level = 0;

    float y;
    float x;

    float signX;
    float signY;

    //Numero para indicar el numero de veces que verifica la respuesta correcta
    public int counter;

    //Numero de veces que ha jugado
    int countPlay;

    //
    bool isLastLevel;

    //Numero de intentos que tiene el jugador para ganar el juego
    int attempts = 3;

    // Start is called before the first frame update
    void Start(){
        levelUser = App.generalModel.equialityGameModel.GetLevel();
        size = objRow.GetComponent<BoxCollider2D>().size;
        ChangeLevel();

        usedIntegers = new List<int>();
        Buid(level);
        //Debug.Log(PrintText());
        GetNumOperation();
        GetNum();
        DrawTable();
        FillAnswer();
    }
    //metodo que verifica en que nivel entra el usuario y dependiendo de eso
    //cambian las coorcenadas de los signos y objetos
    void ChangeLevel(){

        switch (levelUser){
            //nivel 1
            case 1: 
                level = 2;
                y = 0.35f; x = -1.5f;
                signX = -0.8f; signY = 0.33f;
            break;
            //nivel 2
            case 2:
                level = 3;
                y = 0.6f; x = -1.5f;
                signX = -0.8f; signY = 0.63f;
            break;
            //nivel 3
            default:
                level = 4;
                y = 1.25f; x = -1.5f;
                signX = -0.8f; signY = 1.23f;
            break;
        }

    }

    //metodo que pemrite la construccion de cada uno de los niveles
    public void Buid(int levels){

        int numberThree = 0;
        figures = new List<Figure>();
        int value;

        for (int i = 0; i < levels; i++){
            
            switch (i){
                case 0:

                    int principal = Random.Range(1, NUM_POSIBLES);
                    usedIntegers.Add(principal);

                    value = Random.Range(1, 4);
                    if (value == 3){
                        numberThree++;
                    }  

                    int figure = GetNewValue();
                    usedIntegers.Add(figure);

                    AddFigure(new Figure(principal, value, figure));
                    break;

                default:

                    int val;
                    int valueP = usedIntegers[usedIntegers.Count -1];

                    if (numberThree == 1){
                        val = Random.Range(1, 3);
                    }else{
                        
                        val = Random.Range(1, 4);
                        if (val == 3){
                            numberThree++;
                        }
                    }
                    
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
            numPaint.Add(figure.Principal);
            for (int i = 0; i < figure.ValueC; i++){
                numPaint.Add(figure.Fig);
            }
            
        }
    }
    
    /*private string PrintNumbers(){

        GetNum();
        string res = "";
        for (int i = 0; i < numPaint.Count; i++){
            res += numPaint[i];
        }   
        return res;
    }*/

    //metodo que permite la contrucción del tablero del juego
    private void DrawTable(){

        GameObject option;
        GameObject sign;
        
        int icon = 0;
        int p = 1;
        for (int i = 0; i < level; i++){

            int k = numOperation[i] + 1;
            int j = 0;

            
            //sign = Instantiate(objSing,new Vector3(signX, signY - (0.9f*i), 0), objSing.transform.rotation);
            sign = Instantiate(objSing, new Vector3(signX, signY - i*0.9f, 0), objSing.transform.rotation);
            sign.GetComponent<SpriteRenderer>().sprite = equialitySprite;

            //y = 1.25f;
            //x = -1.5f;
            while (j < k)
            {

                if (j == 0)
                {
                    option = Instantiate(objRow, new Vector3(x, y - i*0.9f, 0), objRow.transform.rotation);
                    //option = Instantiate(objRow, new Vector3(x, y - (1f * i), 0), objRow.transform.rotation);
                    Sprite sprite = images[numPaint[icon]];
                    option.GetComponent<SpriteRenderer>().sprite = sprite;
                    p += 1;
                }
                //if(j == 1){
                    //KELLY
                    //option = Instantiate(objRow,new Vector3(x + (1.6f*j),y - (0.9f*i), 0), objRow.transform.rotation);
                    //DANY
                    //option = Instantiate(objRow, new Vector3(x + (size.x * j), y - (0.9f * i), 0), objRow.transform.rotation);
                    //Sprite sprite = images[numPaint[icon]];
                    //option.GetComponent<SpriteRenderer>().sprite = sprite;
                    
                else{
                    //KELLYoption = Instantiate(objRow,new Vector3(x + (1.2f*j),y - (0.9f*i), 0), objRow.transform.rotation);
                    //KELLY
                    //option = Instantiate(objRow,new Vector3(x + ((size.x - 0.08f) * j),y - (0.9f*i), 0), objRow.transform.rotation);
                    //DANY
                    //option = Instantiate(objRow,new Vector3((size.x/2 * j)-1f,y - (1f*i), 0), objRow.transform.rotation);
                    option = Instantiate(objRow, new Vector3((size.x / 2 * j) - 1f, y - i*0.9f, 0), objRow.transform.rotation);
                    Sprite sprite = images[numPaint[icon]];
                    option.GetComponent<SpriteRenderer>().sprite = sprite;  
                } 
                
                icon += 1;
                j +=1 ;

            }
        }

        //pinta y crea el objeto
        GameObject firstOption = Instantiate(objRow,
        new Vector3(-0.2f, -2.6f, 0), objRow.transform.rotation);
        Sprite spriteF = images[numPaint[numPaint.Count - 1]];
        firstOption.GetComponent<SpriteRenderer>().sprite = spriteF;
        firstOption.transform.localScale = new Vector3(0.45f,0.45f,0);

        //pinta y crea el objeto por el que se esta preguntando
        GameObject lastOption = Instantiate(objRow,
        new Vector3(0.75f, -3.25f, 0), objRow.transform.rotation);
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
            numOperation.Add(figure.ValueC);
            correctAnswerGame*=figure.ValueC;
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
        counter++;

        //Verificar si ya jugo un nivel de este juego
        countPlay = App.generalModel.equialityGameModel.GetTimesPlayed();

        App.generalModel.equialityGameModel.UpdateTimesPlayed(++countPlay);

        if (auxAnswer == correctAnswerGame)
        {
            Debug.Log("You win");

            SoundManager.soundManager.PlaySound(4);

            //Si ya paso el nivel 1 puede pasar al 2
            if (App.generalModel.equialityGameModel.GetLevel() == 1)
            {
                App.generalModel.equialityGameModel.UpdateLevel(2);
            }
            //Si ya paso el nivel 2 puede pasar al 3
            else if (App.generalModel.equialityGameModel.GetLevel() == 2)
            {
                App.generalModel.equialityGameModel.UpdateLevel(3);
            }
            //Si ya termino los 3 niveles de ese Set, se comienza de nuevo
            else if (App.generalModel.equialityGameModel.GetLevel() == 3)
            {
                Debug.Log("Termino los 3");
                isLastLevel = true;
                //Actualizar el nivel a 1 para empezar otra nueva ronda
                App.generalModel.equialityGameModel.UpdateLevel(1);
            }
            Debug.Log("CONTADOR: "+counter);
            SetPointsAndStars();
        }
        else
        {
            Debug.Log("You lose");
            CheckAttempt();
        }
    }
    /// <summary>
    /// Metodo que asigna los puntos y estrellas que ha ganado el jugador
    /// </summary>
    public void SetPointsAndStars()
    {
        //Declaracion de los puntos y estrellas que ha ganado el juegador
        int points, stars, totalStars, totalPoints, canvasStars;

        //Declaracion del mensaje a mostrar
        string winMessage;

        //Si gana el juego con 3 intentos suma 30 puntos y gana 3 estrellas
        if (counter == 1)
        {
            points = App.generalModel.equialityGameModel.GetPoints() + 30;
            stars = App.generalModel.equialityGameModel.GetStars() + 3;

            totalPoints = App.generalModel.statsModel.GetTotalPoints() + 30;
            totalStars = App.generalModel.statsModel.GetTotalStars() + 3;

            canvasStars = 3;
            winMessage = App.generalController.gameOptionsController.winMessages[2];

            //Actualizar las veces que ha ganado 3 estrellas
            App.generalModel.equialityGameModel.UpdatePerfectWins(App.generalModel.equialityGameModel.GetPerfectWins() + 1);

            //Actualizar las veces que ha ganado sin errores
            App.generalModel.equialityGameModel.UpdatePerfectGame(App.generalModel.equialityGameModel.GetPerfectGame() + 1);
        }
        //Si gana el juego mas de 3 y menos de 9 intentos suma 20 puntos y gana 2 estrellas
        else if (counter == 2)
        {
            points = App.generalModel.equialityGameModel.GetPoints() + 20;
            stars = App.generalModel.equialityGameModel.GetStars() + 2;

            totalPoints = App.generalModel.statsModel.GetTotalPoints() + 20;
            totalStars = App.generalModel.statsModel.GetTotalStars() + 2;

            canvasStars = 2;
            winMessage = App.generalController.gameOptionsController.winMessages[1];

            //Actualizar las veces que ha ganado sin errores -LE FALTAN DETALLES
            App.generalModel.equialityGameModel.UpdatePerfectGame(0);
        }
        //Si gana el juego con mas de 9 intentos suma 10 puntos y gana 1 estrella
        else
        {
            points = App.generalModel.equialityGameModel.GetPoints() + 10;
            stars = App.generalModel.equialityGameModel.GetStars() + 1;

            totalPoints = App.generalModel.statsModel.GetTotalPoints() + 10;
            totalStars = App.generalModel.statsModel.GetTotalStars() + 1;

            canvasStars = 1;
            winMessage = App.generalController.gameOptionsController.winMessages[0];

            //Actualizar las veces que ha ganado sin errores
            App.generalModel.equialityGameModel.UpdatePerfectGame(0);
        }

        //Actualiza los puntos y estrellas obtenidos
        App.generalModel.equialityGameModel.UpdatePoints(points);
        App.generalModel.equialityGameModel.UpdateStars(stars);

        App.generalModel.statsModel.UpdateTotalStars(totalStars);
        App.generalModel.statsModel.UpdateTotalPoints(totalPoints);

        //Mostrar el canvas que indica cuantas estrellas gano
        App.generalView.gameOptionsView.ShowWinCanvas(canvasStars, winMessage,isLastLevel);

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

    public string PrintText(){

        string res = "";
        foreach (Figure fig in figures){
            res += "figuraUno " + fig.Principal + " cantidad " +
            fig.ValueC + " figuraDos " + fig.Fig + "\n";
        }

        return res;
    }

    public void ShowSolution(){

        /*bool aux = true;
        
        if(aux){            
            
            App.generalView.gameOptionsView.ShowSolutionCanvas();
            App.generalView.gameOptionsView.HideBuyCanvas();
            sequenceWindow.SetActive(false);
            additionEqualityWindow.SetActive(true);

            numberText.text = "" + correctAnswerGame;
        }*/

        //Si hay tickets muestra la solucion
        Debug.Log("HAY: " + App.generalModel.ticketModel.GetTickets() + " TICKETS");
        if (App.generalModel.ticketModel.GetTickets() >= 1)
        {
            Debug.Log("HAS USADO UN PASE");

            App.generalController.ticketController.DecraseTickets();
            App.generalView.gameOptionsView.HideBuyCanvas();
            App.generalView.gameOptionsView.ShowSolutionCanvas();

            App.generalView.equialityGameView.solutionPanel.SetActive(true);
            App.generalView.equialityGameView.solutionText.text = " " + correctAnswerGame;
        }
        else
        {
            App.generalView.gameOptionsView.ShowTicketsCanvas(3);
        }
    }

}
