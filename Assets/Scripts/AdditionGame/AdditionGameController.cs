using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdditionGameController : Reference{

    //lista que contienen las respuestas a mostrar en desorden
    readonly List<int> possibleAnswer = new List<int>();

    //lista que tiene los resultaods a pintar
    readonly List<int> results = new List<int>();
    //lista de los posibles sprites que seran utilizados
    public List <Sprite> images = new List<Sprite>();

    //sprite del +
    public Sprite signPlus;
    //sprite del -
    public Sprite signLess;
    //sprite del =
    public Sprite signEquals;

    //lista que permite colocar las imagenes
    readonly List<Icon> icons = new List<Icon>();

    //lista que contiene id de iconos que permite que no se repitan
    readonly List<int> valueReapetIcon = new List<int>();

    //Lista de las operaciones del juego
    readonly List<Operation> operationes = new List<Operation>();

    //lista de los signos que se van a pintar
    readonly List<int> signs = new List<int>();

    //numero incial para las operaciones
    //readonly int initial = Random.Range(0, 5);// 8;
    int initial;

    //varibale que almacena la respuesta correcta
    int correctAnswer;
    //objeto que contiene el prefab
    public GameObject objectRow;

    public GameObject objectSign;



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
    int levelUser;

    //numero que me indica cuantos niveles tendra el juego
    int level = 0;

    //Numero para indicar el numero de veces que verifica la respuesta correcta
    int counter;

    //Numero de veces que ha jugado
    int countPlay;

    //
    bool isLastLevel;

    //Numero de intentos que tiene el jugador para ganar el juego
    int attempts = 3;

    // Start is called before the first frame update
    void Start()
    {
        //variable que me almacena el tamaño del objeto principal

        //Vector2 sizeSign = objectSign.GetComponent<BoxCollider2D>().size;

        //Vector2 size = objectRow.GetComponent<BoxCollider2D>().size;
        initial = Random.Range(1, 8);
        ChooseLevel();
        Build(level, initial);
        //Debug.Log(OwnToString());
        GetResult();
        FillAnswer();
       
    }

    //metodo que verifica en que nivel entra el usuario y dependiendo de eso
    //cambian las coorcenadas de los signos y objetos
    void ChooseLevel(){

        levelUser = App.generalModel.additionGameModel.GetLevel();
        switch (levelUser){
            //nivel 1
            case 1: 
                level = 2;
                //= -0.1f; x = -2f;
                //gnX = -1.63f; signY = -0.06f;
                y = 0f; x=-1.2f;                  
                signX = -0.83f; signY = 0f;
                //activa los campos de texto del nivel 1
                for (int i = 0; i < textListLevelOne.Count; i++){
                    textListLevelOne[i].enabled = true; 
                }

            break;
            //nivel 2
            case 2:
                level = 3;
                y = 0.1f; x = -1.72f;
                signX = -1.35f; signY = 0.1f;
                //activa los campos de texto del nivel 2
                for (int i = 0; i < textListLevelTwo.Count; i++){
                    textListLevelTwo[i].enabled = true; 
                }

            break;
            //nivel 3
            default:
                level = 4;
                y = 0.7f; x = -2f;
                signX = -1.63f; signY = 0.7f;
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
        int value;
        int image;
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
        correctAnswer = icons[icons.Count -1].IdValue;
        
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
            if (value == icon.IdValue){
                aux = icon.IdIcon;
            }
        }
        return aux;
    }

    public GameObject gameZone;
    //metodo que crea la estructura visual del juego 
    public void CreateTable(){

        GameObject firstOption;
        GameObject sign;
        GameObject signFirts;
        GameObject newOption;

        int cont = 2;
        int auxImage = 0;
        int auxSign = 0;

        /*firstOption = Instantiate(objectRow,new Vector3(-2, 1.6f, 0), objectRow.transform.rotation);
        signFirts = Instantiate(objectRow,new Vector3(-1.4f, 1.6f, 0), objectRow.transform.rotation);*/

        firstOption = Instantiate(objectRow, new Vector3(x, y+0.9f, 0), objectRow.transform.rotation);
        signFirts = Instantiate(objectSign, new Vector3(signX+0.08f, signY+0.9f, 0), objectSign.transform.rotation);

        Sprite spriteFirst = images[icons[0].IdIcon];

        firstOption.GetComponent<SpriteRenderer>().sprite = spriteFirst;
        signFirts.GetComponent<SpriteRenderer>().sprite = signEquals;

        firstOption.transform.parent = gameZone.transform;
        signFirts.transform.parent = gameZone.transform;

        for (int i = 0; i < level; i++){
                    
            for (int j = 0; j < cont; j++){

                /*newOption = Instantiate(objectRow, new Vector3(x + ((size.x - 0.53f) * j), y - ((size.y - 0.5f)*i), 0), objectRow.transform.rotation);

                Sprite sprite = images[icons[auxImage].idIcon];
                newOption.GetComponent<SpriteRenderer>().sprite = sprite;

                newOption.transform.parent = gameZone.transform;*/

                newOption = Instantiate(objectRow, new Vector3(x + (0.75f * j), y + (-0.9f* i), 0), objectRow.transform.rotation);

                Sprite sprite = images[icons[auxImage].IdIcon];
                newOption.GetComponent<SpriteRenderer>().sprite = sprite;

                newOption.transform.parent = gameZone.transform;

                auxImage += 1;


                /*sign = Instantiate(objectSign, new Vector3(signX +((sizeSign.x - 0.53f)* j), signY - ((sizeSign.y + 0.1f) * i), 0), objectSign.transform.rotation);

                sign.transform.parent = gameZone.transform;*/

                sign = Instantiate(objectSign, new Vector3(signX + (0.75f* j), signY + (-0.9f * i), 0), objectSign.transform.rotation);

                sign.transform.parent = gameZone.transform;

                sign.GetComponent<SpriteRenderer>().sprite = signs[auxSign] switch
                {
                    1 => signLess,
                    2 => signPlus,
                    _ => signEquals,
                };
                auxSign++;
                        
            }
            cont+= 1;           
        }       
    }

    //metodo que recorre la lista de operaciones y obtiene
    //los simbolos de "+" o "-"
    public void GetSign(){
        //int sign = 0;
        foreach (Operation operation in operationes){
            for(int i = 0; i < operation.operands.Count; i++){
                int sign = operation.operands[i].operatorValue;
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
                int value = Random.Range(0, 10);
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

        counter++;

        //Verificar si ya jugo un nivel de este juego
        countPlay = App.generalModel.additionGameModel.GetTimesPlayed();

        App.generalModel.additionGameModel.UpdateTimesPlayed(++countPlay);
        print("HA JUGADO: " + countPlay);

        int auxAnswer = int.Parse(text);
        if (auxAnswer == correctAnswer){

            Debug.Log("COUNTER: "+counter);

            Debug.Log("You win");

            SoundManager.soundManager.PlaySound(4);

            //Si ya paso el nivel 1 puede pasar al 2
            if (App.generalModel.additionGameModel.GetLevel() == 1)
            {
                App.generalModel.additionGameModel.UpdateLevel(2);
            }
            //Si ya paso el nivel 2 puede pasar al 3
            else if (App.generalModel.additionGameModel.GetLevel() == 2)
            {
                App.generalModel.additionGameModel.UpdateLevel(3);
            }
            //Si ya termino los 3 niveles de ese Set, se comienza de nuevo
            else if (App.generalModel.additionGameModel.GetLevel() == 3)
            {
                Debug.Log("Termino los 3");
                isLastLevel = true;
                //Actualizar el nivel a 1 para empezar otra nueva ronda
                App.generalModel.additionGameModel.UpdateLevel(1);
            }
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
            points = App.generalModel.additionGameModel.GetPoints() + 30;
            stars = App.generalModel.additionGameModel.GetStars() + 3;

            totalPoints = App.generalModel.statsModel.GetTotalPoints() + 30;
            totalStars = App.generalModel.statsModel.GetTotalStars() + 3;

            canvasStars = 3;
            winMessage = App.generalController.gameOptionsController.winMessages[2];

            //Actualizar las veces que ha ganado 3 estrellas
            App.generalModel.additionGameModel.UpdatePerfectWins(App.generalModel.additionGameModel.GetPerfectWins() + 1);

            //Actualizar las veces que ha ganado sin errores
            App.generalModel.additionGameModel.UpdatePerfectGame(App.generalModel.additionGameModel.GetPerfectGame() + 1);
        }
        //Si gana el juego mas de 3 y menos de 9 intentos suma 20 puntos y gana 2 estrellas
        else if (counter == 2)
        {
            points = App.generalModel.additionGameModel.GetPoints() + 20;
            stars = App.generalModel.additionGameModel.GetStars() + 2;

            totalPoints = App.generalModel.statsModel.GetTotalPoints() + 20;
            totalStars = App.generalModel.statsModel.GetTotalStars() + 2;

            canvasStars = 2;
            winMessage = App.generalController.gameOptionsController.winMessages[1];

            //Actualizar las veces que ha ganado sin errores -LE FALTAN DETALLES
            App.generalModel.additionGameModel.UpdatePerfectGame(0);
        }
        //Si gana el juego con mas de 9 intentos suma 10 puntos y gana 1 estrella
        else
        {
            points = App.generalModel.additionGameModel.GetPoints() + 10;
            stars = App.generalModel.additionGameModel.GetStars() + 1;

            totalPoints = App.generalModel.statsModel.GetTotalPoints() + 10;
            totalStars = App.generalModel.statsModel.GetTotalStars() + 1;

            canvasStars = 1;
            winMessage = App.generalController.gameOptionsController.winMessages[0];

            //Actualizar las veces que ha ganado sin errores
            App.generalModel.additionGameModel.UpdatePerfectGame(0);
        }

        //Actualiza los puntos y estrellas obtenidos
        App.generalModel.additionGameModel.UpdatePoints(points);
        App.generalModel.additionGameModel.UpdateStars(stars);

        App.generalModel.statsModel.UpdateTotalStars(totalStars);
        App.generalModel.statsModel.UpdateTotalPoints(totalPoints);

        //Mostrar el canvas que indica cuantas estrellas gano
        App.generalView.gameOptionsView.ShowWinCanvas(canvasStars, winMessage, isLastLevel);

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

    public void ShowSolution(){
       
        /*bool aux = true;
        
        if(aux){            
            
            App.generalView.gameOptionsView.ShowSolutionCanvas();
            App.generalView.gameOptionsView.HideBuyCanvas();
            sequenceWindow.SetActive(false);
            additionEqualityWindow.SetActive(true);

            numberText.text = "" + correctAnswer;
        }*/

        //Si hay tickets muestra la solucion
        Debug.Log("HAY: " + App.generalModel.ticketModel.GetTickets() + " TICKETS");
        if (App.generalModel.ticketModel.GetTickets() >= 1)
        {
            Debug.Log("HAS USADO UN PASE");

            App.generalController.ticketController.DecraseTickets();
            App.generalView.gameOptionsView.HideBuyCanvas();
            App.generalView.gameOptionsView.ShowSolutionCanvas();

            App.generalView.additionGameView.solutionPanel.SetActive(true);
            App.generalView.additionGameView.solutionText.text = " " + correctAnswer;
        }
        else
        {
            App.generalView.gameOptionsView.ShowTicketsCanvas(3);
        }
    }
}
