using UnityEngine;

public class ConnectedGameController : Reference
{

    //Objeto que representa cada uno de los bloques que conforman la matriz
    public GameObject initialBlock;

    //Objeto que representa la zona del juego (Matriz)
    public GameObject gameZone;

    // Se crea una matriz de objetos
    static Objects [,] arrayObjects;
    // Se crea una matriz que contendra la solucion
    static Objects [,] arraySolution;

    // Indica la cantidad de filas de la matriz
    readonly int arrayRow = 8;

    // Cantidad de columnas de la matriz
    readonly int arrayCol = 6;
    // Matriz de GameObject
    public  GameObject[,] matrix;

    //Variable que almacena las coordenadas del punto de llegada 
    readonly int [] arrivalPoint = new int [2];
    //static int totalPoints = 0;
    //int totalSteps = 5;

    //Puntos con cada color 
    static int totalStepsLevel1;

    static int totalStepsLevel2;

    static int totalStepsLevel3;

    static int level;

    //Variables que almacenan la ultima posicion en la que quedo ubicado el objeto, con el fin de seguir desde ahi los movimientos futuros
    static Objects startPoint;
    static Objects startPoint1;
    static Objects startPoint2;
    static Objects startPoint3;

    //Variables en las que se almacena la cantidad de movimientos realizados depediendo el color
    int totalMove1 = 0;
    int totalMove2 = 0;
    int totalMove3 = 0;

    //Variables empleadas para verificar si llego al punto indicado
    static bool finish1 = false;
    static bool finish2 = false;
    static bool finish3 = false;

    //Prueba centinelas
    bool level1 = false;
    bool level2 = false;
    bool level3 = false;

    static readonly Objects[] totalStartPoints = new Objects[3];

    //
    bool isLastLevel;

    //
    int countPlay;

    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }    
    /*
     * Metodo que dibuja en la pantalla la matriz
     */
    public void DrawMatrix(Objects[,] matrix1, GameObject initialBlock, GameObject gameZone)
    {
        //Posicion inicial del bloque
        float posStarX = initialBlock.transform.position.x;
        float posStarY = initialBlock.transform.position.y;

        //Tamanio del bloque
        Vector2 blockSize = initialBlock.GetComponent<BoxCollider2D>().size;

        //Matriz de objetos que representara el mapa en la pantalla
        matrix = new GameObject[matrix1.GetLength(0), matrix1.GetLength(1)];

        int index = 0;

        for (int i = 0; i < matrix.GetLength(0); i++)
        {

            for (int j = 0; j < matrix.GetLength(1); j++)
            {

                //Crear cada bloque en la escena en una posicion data.
                //Cada bloque esta contenido en una determinada posicion de la matriz
                matrix[i, j] = Instantiate(initialBlock, new Vector3(posStarX + (blockSize.x * j),
                                                                      posStarY + (blockSize.y * -i),
                                                                      0),
                                                                      initialBlock.transform.rotation);

                matrix[i,j].name = string.Format("Block[{0}][{1}]", i, j);

                //Temporal - se asigna un 0 para indicar que es bloque disponible o 1 para indicar que es bloque obstaculo
                matrix[i, j].GetComponent<Block>().Id=(matrix1[i,j].Type);

                //Asignar la matriz al objeto de GameZone
                matrix[i, j].transform.parent = gameZone.transform;

                index += 1;

                PaintMatrix(i,j,matrix[i, j].GetComponent<Block>().Id);
            }
        }
        PaintStartPoints();
    }

    public void PaintStartPoints()
    {
        if(level == 1)
        {
            matrix[startPoint1.X, startPoint1.Y].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Map/cuadroPunto");
        }
        else if(level == 2)
        {
            matrix[startPoint1.X, startPoint1.Y].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Map/cuadroPunto");
            matrix[startPoint2.X, startPoint2.Y].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Map/cuadroPunto");
        }
        else
        {
            matrix[startPoint1.X, startPoint1.Y].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Map/cuadroPunto");
            matrix[startPoint2.X, startPoint2.Y].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Map/cuadroPunto");
            matrix[startPoint3.X, startPoint3.Y].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Map/cuadroPunto");
        }
    }
    /* 
    * Metodo que pinta la matriz dependiendo el id indicado (cada id representa un color diferente)
    */
    public void PaintMatrix(int i, int j, int id)
    {
        //Si es 0 se pinta de gris (vacio)
        if (id == 0)
        {
            matrix[i, j].GetComponent<SpriteRenderer>().color = Color.gray;
        }
        //Si es 9 se pinta de verde (punto de llegada)
        else if (id == 9)
        {
            matrix[i, j].GetComponent<SpriteRenderer>().color = Color.green;
        }
        //Si es 1 se pinta de amarillo ()
        else if (id == 1)
        {
            matrix[i, j].GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        //Si es 2 se pinta de azul ()
        else if (id == 2)
        {
            matrix[i, j].GetComponent<SpriteRenderer>().color = Color.blue;
        }
        //Si es 3 se pinta de rojo ()
        else if (id == 3)
        {
            matrix[i, j].GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
    // Metodo que retorna la matriz logica
    public Objects[,] ReturnArray ()
    {
        return arrayObjects;
    }
    // Metodo principal encargado de hacer los llamados correspondientes para generar el nivel
    public void CreateLevel()
    {
        finish1 = false;
        finish2 = false;
        finish3 = false;

        level = App.generalModel.connectedGameModel.GetLevel();
        print("Create level "+level);
        ActivateButtons();
        GenerateArray();
        GenerateArrivalPoint();
        CreatePoints();
        LocateStartPoints();
        RestartCountingMovements();

        Objects[,] matrix = arrayObjects;
        DrawMatrix(matrix,initialBlock,gameZone);
        LoadText();
       
    }
    public void ActivateButtons()
    {
        for (int i = 0; i < level; i++)
        {
            App.generalView.connectedGameView.colorButtons[i].interactable = true;
            App.generalView.connectedGameView.resetButtons[i].interactable = true;
        }
    }
    void RestartCountingMovements()
    {
        totalMove1 = 0;
        totalMove2 = 0;
        totalMove3 = 0;
    }
    //Metodo que carga el texto inicial dando indicaciones
    public void LoadText ()
    {
        string message;
        message = "Conectados\n\n Llegue al punto Verde de acuerdo a las siguientes indicaciones:\n";
        switch (level)
        {
            case 1:
                message += "- Punto Amarillo con "+totalStepsLevel1;
                break;
            case 2: 
                message += "- Punto Amarillo con "+totalStepsLevel1+
                "\n- Punto Azul con "+totalStepsLevel2;
                break;
            case 3: 
                message += "- Punto Amarillo con "+totalStepsLevel1+
                "\n- Punto Azul con "+totalStepsLevel2+
                "\n- Punto Rojo con "+totalStepsLevel3;
                break;
        }

        App.generalView.connectedGameView.message.text = message;
    }
    //Metodo encargado de inicializar los puntos de la matriz logica
    public void GenerateArray ()
    {
        arrayObjects = new Objects [arrayRow, arrayCol];

        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                arrayObjects [i, j] = new Objects (i, j, 0);
            }
        }
    }
    //Metodo encargado de generar el punto de llegada 
    public void GenerateArrivalPoint()
    {
        int posStarX = RamdonNumber(0, arrayRow);
        int posStarY = RamdonNumber(0, arrayCol);

        // Se establece en la matriz el punto de partida y se cambia el valor en type
        arrayObjects[posStarX,posStarY].Type = 9;

        arrivalPoint[0] = posStarX;
        arrivalPoint[1] = posStarY;

        //Se clona la matriz con el fin de  tener una donde se genera automaticamente una solucion en caso de no ser resuelto
        arraySolution = Clone(arrayObjects);
    }
    //Metodo encargado de generar los puntos (colores o personajes) dependiendo la cantidad indicada (2 o 3)
    public void CreatePoints()
    {
        switch (level)
        {
            case 1:
                totalStepsLevel1 = RamdonNumber(3,7);
                print("Total pasos amarillo: "+totalStepsLevel1);
                CreatePointsLevel(1, totalStepsLevel1-1, 0);
                break;
            case 2: 
                totalStepsLevel1 = RamdonNumber(6,8);
                print("Total pasos amarillo: "+totalStepsLevel1);
                
                CreatePointsLevel(1, totalStepsLevel1-1, 0);

                totalStepsLevel2 = RamdonNumber(3,7);
                print("Total pasos azul: "+totalStepsLevel2);
                
                CreatePointsLevel(2, totalStepsLevel2-1, 1);
                break;
            case 3: 
                totalStepsLevel1 = RamdonNumber(6,8);
                print("Total pasos amarillo: "+totalStepsLevel1);

                CreatePointsLevel(1, totalStepsLevel1-1, 0);

                totalStepsLevel2 = RamdonNumber(4,7);
                print("Total pasos azul: "+totalStepsLevel2);

                CreatePointsLevel(2, totalStepsLevel2-1, 1);

                totalStepsLevel3 = RamdonNumber(3,7);
                print("Total pasos rojo: "+totalStepsLevel3);
                
                CreatePointsLevel(3, totalStepsLevel3-1, 2);

                break;
        }

        CheckLevel();
    }
    // Metodo encargado de verificar si el nivel cumple con las condiciones 
    public void CheckLevel ()
    {
        bool isCorrect;
        if(level == 1) isCorrect = CheckLevel1();
        else if(level == 2) isCorrect = CheckLevel2();
        else isCorrect = CheckLevel3();

        //print("level 1 "+level1+" Level 2 "+level2+" level 3 "+level3);
        if(isCorrect)
        {
            print("Cumplio con la condicion de parada y la distancia");
        }
        else{
            print("Algun punto no cumplio con la condicion de parada y la distancia");
            CreateLevel();
            //SceneManager.LoadScene("ConnectedGameScene");
        }
    }
    bool CheckLevel1()
    {
        return level1;
    }
    bool CheckLevel2()
    {
        return level1 && level2;
    }
    bool CheckLevel3()
    {
        return level1 && level2 && level3;
    }
    // Metodo encargado de verificar si el color ya cumplio con las condiciones
    void CheckConditions (int type, bool value)
    {
        if(type == 1) level1 = value;
        else if(type == 2) level2 = value;
        else level3 = value;
    }
    public void CreatePointsLevel(int type, int totalSteps, int index)
    {
        int [] finalPoint = GenerateSolution(totalSteps, type);
        
        if(finalPoint == null)
        {
            print("Es null para el punto "+type);
            CheckConditions(type, false);
        }
        else
        {
            if(CheckDistancePoints(finalPoint[0], finalPoint[1], arrivalPoint[0], arrivalPoint[1]))
            {
                arrayObjects[finalPoint[0],finalPoint[1]].Type = finalPoint[2];

                totalStartPoints[index] = new Objects(finalPoint[0], finalPoint[1], finalPoint[2]);

                CheckConditions(type, true);

                //print("Cumplio con la distacia social");
            }
            else
            {
                //print("Nooooo cumplio con la distacia social");
                CheckConditions(type, false);
            }
        }      
    }
    // Metodo encargado de verificar que el punto final no quede muy cerca del punto de inicio
    bool CheckDistancePoints(int startX, int startY, int finalX, int finalY)
    {
        bool approved = false;

        if((finalX > startX+2 || finalX < startX-2) || (finalY > startY+2 || finalY < startY-2))
        {
            approved = true;
        }
        return approved;
    }
    //Metodo encargado de generar una solucion por cada punto(color o personaje) con la cantidad de movimientos especificados
    public int [] GenerateSolution(int cantMove, int type)
    {
        //poner condicion de parada 
        int posX = arrivalPoint[0];
        int posY = arrivalPoint[1];
        int stop = 0;

        bool centinela = true;

        while(cantMove >= 0 && centinela)
        {
            if(stop == 50)
            {
                centinela = false;
            }
            
            int option = RamdonNumber(1,5);
            if(option == 1)
            {
                if(CheckPosition(arraySolution,posX-1,posY))
                {
                    posX--;
                    arraySolution[posX,posY].Type = type;
                    cantMove --;
                } 
            }
            else if(option == 2)
            {
                if(CheckPosition(arraySolution,posX+1,posY))
                {
                    posX++;
                    arraySolution[posX,posY].Type = type;
                    cantMove --;
                } 
            }
            else if(option == 3)
            {
                if(CheckPosition(arraySolution,posX,posY-1))
                {
                    posY--;
                    arraySolution[posX,posY].Type = type;
                    cantMove --;
                } 
            }
            else{
                if(CheckPosition(arraySolution,posX,posY+1))
                {
                    posY++;
                    arraySolution[posX,posY].Type = type;
                    cantMove --;
                } 
            }
            stop ++;
            
            //print("cantidad mov "+ cantMove+" Stop "+stop);
        }

        if(cantMove < 0)
        {
            int [] finalPosition = new int [] {posX,posY,type};
            return finalPosition;
        }

        return null;
    }
    //Metodo que encuentra los puntos iniciales y inicializa las variables correspondientes de acuerdo al tipo
    public void LocateStartPoints ()
    {
        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                if(arrayObjects[i,j].Type == 1)
                {
                    startPoint1= new Objects(i,j,1);
                }
                else if(arrayObjects[i,j].Type == 2)
                {
                    startPoint2= new Objects(i,j,2);
                }
                else if(arrayObjects[i,j].Type == 3)
                {
                    startPoint3= new Objects(i,j,3);
                }
            }
        }
    }
    //Metodo empleado para verificar que objeto eligio mover el jugador
    public void SelectColor(int type)
    {
        if(type == 1)
        {
            startPoint = startPoint1;
        }
        else if(type == 2)
        {
            startPoint = startPoint2;
        }
        else if(type == 3)
        {
            startPoint = startPoint3;
        }
    }
    //Metodo encargado de toda la parte del movimiento que realiza el jugador tanto logica y visual
    public void Move(string direction)
    {
        if (startPoint == null)
        {
            App.generalView.gameOptionsView.ShowWarningCanvas();
        }
        else
        {
            if (direction == "up")
            {
                if (CheckPosition(arrayObjects, startPoint.X - 1, startPoint.Y))
                {
                    arrayObjects[startPoint.X - 1, startPoint.Y].Type = startPoint.Type;
                    PaintMatrix(startPoint.X - 1, startPoint.Y, startPoint.Type);
                    CountMoves(startPoint.Type);
                    startPoint.X--;
                }
                else if (CheckArrival(arrayObjects, startPoint.X - 1, startPoint.Y) && !CheckFinishOption(startPoint.Type))
                {
                    CheckArrivalOption(startPoint.Type);
                    CountMoves(startPoint.Type);
                }
            }
            else if (direction == "down")
            {
                if (CheckPosition(arrayObjects, startPoint.X + 1, startPoint.Y))
                {
                    arrayObjects[startPoint.X + 1, startPoint.Y].Type = startPoint.Type;
                    PaintMatrix(startPoint.X + 1, startPoint.Y, startPoint.Type);
                    CountMoves(startPoint.Type);
                    startPoint.X++;
                }
                else if (CheckArrival(arrayObjects, startPoint.X + 1, startPoint.Y) && !CheckFinishOption(startPoint.Type))
                {
                    CheckArrivalOption(startPoint.Type);
                    CountMoves(startPoint.Type);
                }

            }
            else if (direction == "right")
            {
                if (CheckPosition(arrayObjects, startPoint.X, startPoint.Y + 1))
                {
                    arrayObjects[startPoint.X, startPoint.Y + 1].Type = startPoint.Type;
                    PaintMatrix(startPoint.X, startPoint.Y + 1, startPoint.Type);
                    CountMoves(startPoint.Type);
                    startPoint.Y++;
                }
                else if (CheckArrival(arrayObjects, startPoint.X, startPoint.Y + 1) && !CheckFinishOption(startPoint.Type))
                {
                    CheckArrivalOption(startPoint.Type);
                    CountMoves(startPoint.Type);
                }
            }
            else if (direction == "left")
            {
                if (CheckPosition(arrayObjects, startPoint.X, startPoint.Y - 1))
                {
                    arrayObjects[startPoint.X, startPoint.Y - 1].Type = startPoint.Type;
                    PaintMatrix(startPoint.X, startPoint.Y - 1, startPoint.Type);
                    CountMoves(startPoint.Type);
                    startPoint.Y--;
                }
                else if (CheckArrival(arrayObjects, startPoint.X, startPoint.Y - 1) && !CheckFinishOption(startPoint.Type))
                {
                    CheckArrivalOption(startPoint.Type);
                    CountMoves(startPoint.Type);
                }
            }
            print(totalMove1 + " - " + totalMove2);
            CheckEndGame();
        }
    }
    //Metodo que cuenta la cantidad de movimientos que el jugador a realizado dependiendo el objeto que selecciono 
    public void CountMoves (int type)
    {
        if(type == 1)
        {
            totalMove1++;
        }
        else if(type == 2)
        {
            totalMove2++;
        }
        else if(type == 3)
        {
            totalMove3++;
        }
    }
    public bool CheckFinishOption(int tipo)
    {
        if (tipo == 1) return finish1;

        else if (tipo == 2) return finish2;

        else return finish3;
    }
    //Metodo que dependiendo la opcion indica cual fue el objeto que ya llego al punto final
    public void CheckArrivalOption (int tipo)
    {
        if(tipo == 1)
        {
            finish1 = true;
        }
        else if(tipo == 2)
        {
            finish2 = true;
        }
        else if(tipo == 3)
        {
            finish3 = true;
        }
    }
    void CheckEndGame()
    {
        if(level == 1)
        {
            OneTotalPoints();
        }
        else if(level == 2) 
        {
            TwoTotalPoints();
        }
        else
        {
            ThreeTotalPoints();
        }
    }
    public void ResetMoves(int type)
    {
        print("tipo para borrar "+type);
        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                if(type == arrayObjects[i,j].Type)
                {
                    arrayObjects[i,j].Type = 0;
                    PaintMatrix(i,j,0);
                }
            }
        }
        if (type == 1)
        {
            finish1 = false;
        }else if (type == 2)
        {
            finish2 = false;
        }
        else
        {
            finish3 = false;
        }
        PaintStartPoints(type);
    }
    void PaintStartPoints(int type)
    {
        if(type == 1)
        {   
            PaintMatrix(totalStartPoints[0].X,totalStartPoints[0].Y,type);
            startPoint1.X = totalStartPoints[0].X;
            startPoint1.Y = totalStartPoints[0].Y;
            totalMove1 = 0;
        }
        else if(type == 2)
        {
            PaintMatrix(totalStartPoints[1].X,totalStartPoints[1].Y,type);
            startPoint2.X = totalStartPoints[1].X;
            startPoint2.Y = totalStartPoints[1].Y;
            totalMove2 = 0;
        }
        else
        {
            PaintMatrix(totalStartPoints[2].X,totalStartPoints[2].Y,type);
            startPoint3.X = totalStartPoints[2].X;
            startPoint3.Y = totalStartPoints[2].Y;
            totalMove3 = 0;
        }
    }
    bool OneTotalPoints()
    {
        if(finish1)
        {
            print("Finalizo llego al punto verde");
            CheckTotalMoves();
            return true;
        }
        return false;
    }
    bool TwoTotalPoints()
    {
        if(finish1 && finish2)
        {
            print("Finalizo llego con todos los tipos al final");
            CheckTotalMoves();
            return true;
        }
        return false;
    }
    bool ThreeTotalPoints()
    {
        if(finish1 && finish2 && finish3)
        {
            print("Finalizo llego con todos los tipos al final");
            CheckTotalMoves();
            return true;
        }
        return false;
    }
    /* Metodo encargado de verificar que la posicion donde se va a 
    * ubicar la figura no este ya ocupada y que no se salga de los limites
    * de la matriz
    */
    bool CheckPosition(Objects [,] array,int posX, int posY)
    {
        // Verifica que no se vaya salir de los limites 
        if(posX >= 0 && posY >= 0 && posX < arrayRow && posY < arrayCol)
        {
            // Verifica si la posicion indicada esta libre
            if(array[posX,posY].Type == 0) return true;
        }
        return false;
    }
    //Metodo que verifica si la posicion a la que se va a mover corresponde al punto de llegada
    bool CheckArrival(Objects [,] array,int posX, int posY)
    {
        // Verifica que no se vaya salir de los limites 
        if(posX >= 0 && posY >= 0 && posX < arrayRow && posY < arrayCol)
        {
            // Verifica si la posicion indicada esta libre
            if(array[posX,posY].Type == 9) return true;
        }
        return false;
    }    
    void CheckTotalMoves ()
    {
        //Verificar si ya jugo un nivel de este juego
        countPlay = App.generalModel.connectedGameModel.GetTimesPlayed();

        App.generalModel.connectedGameModel.UpdateTimesPlayed(++countPlay);
        print("HA JUGADO: " + countPlay);

        if (level == 1)
        {
            CheckMovesLevel1();
        }
        else if(level == 2)
        {
            CheckMovesLevel2();
        }
        else        
        {
            CheckMovesLevel3();
        }
    }
    void CheckMovesLevel1()
    {
        int difference = totalMove1 - totalStepsLevel1;

        Debug.Log("LA RESTA ES: " + difference);

        /*if (totalMove1 == totalStepsLevel1)
        {
            print("Cumplio los pasos con el color amarillo");
            WinLevel();
        }
        else
        {
            print("No Cumplio los pasos con el color amarillo");
        }*/

        if(difference == 0)
        {
            print("Cumplio los pasos con el color amarillo");
            //WinLevel();
            SetPointsAndStars(1);
        }
        else if(difference > 0 && difference < 4)
        {
            print("No Cumplio los pasos con el color amarillo");
            SetPointsAndStars(2);
        }
        else
        {
            print("No Cumplio los pasos con el color amarillo 2");
            SetPointsAndStars(3);
        }
        App.generalModel.connectedGameModel.UpdateLevel(2);
    }    
    void CheckMovesLevel2()
    {
        int difference = totalMove1 - totalStepsLevel1;
        int difference2 = totalMove2 - totalStepsLevel2;

        if (difference == 0 && difference2 == 0)
        {
            print("Cumplio los pasos con el color amarillo y azul");
            SetPointsAndStars(1);
            //WinLevel();
        }else if ((difference == 0 && (difference2 > 0 && difference2 < 4)) || (difference2 == 0 && (difference > 0 && difference < 4)))
        {
            print("NO Cumplio los pasos con el color amarillo y azul");
            SetPointsAndStars(2);
        }
        else
        {
            print("NO Cumplio los pasos con el color amarillo y azul 2");
            SetPointsAndStars(3);
        }

        App.generalModel.connectedGameModel.UpdateLevel(3);
        /*if (totalMove1 == totalStepsLevel1 && totalMove2 == totalStepsLevel2)
        {
            print("Cumplio los pasos con el color amarillo y azul");
            WinLevel();
        }*/
    }
    void CheckMovesLevel3()
    {
        int difference = totalMove1 - totalStepsLevel1;
        int difference2 = totalMove2 - totalStepsLevel2;
        int difference3 = totalMove3 - totalStepsLevel3;

        Debug.Log("LA RESTA 1 ES: " + difference);
        Debug.Log("LA RESTA 2 ES: " + difference2);
        Debug.Log("LA RESTA 3 ES: " + difference3);

        isLastLevel = true;
        if (difference == 0 && difference2 == 0 && difference3 == 0)
        {
            print("Cumplio los pasos con el color amarillo, azul y rojo");
            SetPointsAndStars(1);
            //WinLevel();
        }
        else if ((difference == 0 && difference2 == 0 && (difference3 > 0 && difference3 < 4)) || (difference2 == 0 && difference3 == 0 && (difference > 0 && difference < 4)) || (difference == 0 && difference3 == 0 && (difference2 > 0 && difference2 < 4)))
        {
            print("NO Cumplio los pasos con el color amarillo, azul y rojo 1");
            SetPointsAndStars(2);
        }
        else if((difference == 0 && (difference2 > 0 && difference2 < 4) && (difference3 > 0 && difference3 < 4)) || (difference3 == 0 && (difference2 > 0 && difference2 < 4) && (difference > 0 && difference < 4)) || (difference2 == 0 && (difference > 0 && difference < 4) && (difference3 > 0 && difference3 < 4)))
        {
            print("NO Cumplio los pasos con el color amarillo, azul y rojo 2");
            SetPointsAndStars(2);
        }
        else
        {
            print("NO Cumplio los pasos con el color amarillo, azul y rojo 8");
            SetPointsAndStars(3);
        }

        App.generalModel.connectedGameModel.UpdateLevel(1);
    }
    public void ShowSolution()
    {
        //Si hay tickets muestra la solucion
        Debug.Log("HAY: " + App.generalModel.ticketModel.GetTickets() + " TICKETS");
        if (App.generalModel.ticketModel.GetTickets() >= 1)
        {
            Debug.Log("HAS USADO UN PASE");

            App.generalController.ticketController.DecraseTickets();
            App.generalView.gameOptionsView.HideBuyCanvas();
                       
            arrayObjects = Clone(arraySolution);

            App.generalView.connectedGameView.continueButton.interactable = true;
            for (int i = 0; i < 3; i++)
            {
                App.generalView.connectedGameView.colorButtons[i].interactable = false;
            }
            DrawMatrix(arrayObjects, initialBlock, gameZone);
        }
        else
        {
            App.generalView.gameOptionsView.ShowTicketsCanvas(3);
        }
    }
    // Metodo auxiliar para duplicar la matriz
    Objects [,] Clone (Objects [,] array)
    {
        Objects [,] arrayClone = new Objects[arrayRow, arrayCol];

        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                arrayClone[i,j] = new Objects(array[i,j].X, array[i,j].Y, array[i,j].Type);
            }
        }
        return arrayClone;
    }

    /* Metodo que genera un numero aleatorio dependiendo el rango que se le indique
    * min -> numero minimo (incluido)
    * max -> numero maximo (no incluido)
    */
    public int RamdonNumber (int min, int max)
    {
        int number = UnityEngine.Random.Range (min, max);
        return number;
    }
    /// <summary>
    /// Metodo que asigna los puntos y estrellas que ha ganado el jugador
    /// </summary>
    /// <param name="rating">Int rating que indica los puntos que deben ser asignados</param>
    public void SetPointsAndStars(int rating)
    {
        SoundManager.soundManager.PlaySound(4);

        //Declaracion de los puntos y estrellas que ha ganado el juegador
        int points, stars, totalStars, totalPoints, canvasStars;

        //Declaracion del mensaje a mostrar
        string winMessage;

        //Si gana el juego con 3 intentos suma 30 puntos y gana 3 estrellas
        if (rating == 1)
        {
            points = App.generalModel.connectedGameModel.GetPoints() + 30;
            stars = App.generalModel.connectedGameModel.GetTotalStars() + 3;

            totalPoints = App.generalModel.statsModel.GetTotalPoints() + 30;
            totalStars = App.generalModel.statsModel.GetTotalStars() + 3;

            canvasStars = 3;
            winMessage = App.generalController.gameOptionsController.winMessages[2];

            //Actualizar las veces que ha ganado 3 estrellas
            App.generalModel.connectedGameModel.UpdatePerfectWins(App.generalModel.connectedGameModel.GetPerfectWins() + 1);

            //Actualizar las veces que ha ganado sin errores
            App.generalModel.connectedGameModel.UpdatePerfectGame(App.generalModel.connectedGameModel.GetPerfectGame() + 1);
            //Debug.Log("LLEVA: " + PlayerPrefs.GetInt("PerfectGame2", 0) + " JUEGO(S) PERFECTO(S)");
        }
        //Si gana el juego mas de 3 y menos de 9 intentos suma 20 puntos y gana 2 estrellas
        else if (rating == 2)
        {
            points = App.generalModel.connectedGameModel.GetPoints() + 20;
            stars = App.generalModel.connectedGameModel.GetTotalStars() + 2;

            totalPoints = App.generalModel.statsModel.GetTotalPoints() + 20;
            totalStars = App.generalModel.statsModel.GetTotalStars() + 2;

            canvasStars = 2;
            winMessage = App.generalController.gameOptionsController.winMessages[1];

            //Actualizar las veces que ha ganado sin errores -LE FALTAN DETALLES
            App.generalModel.connectedGameModel.UpdatePerfectGame(0);
        }
        //Si gana el juego con mas de 9 intentos suma 10 puntos y gana 1 estrella
        else
        {
            points = App.generalModel.connectedGameModel.GetPoints() + 10;
            stars = App.generalModel.connectedGameModel.GetTotalStars() + 1;

            totalPoints = App.generalModel.statsModel.GetTotalPoints() + 10;
            totalStars = App.generalModel.statsModel.GetTotalStars() + 1;

            canvasStars = 1;
            winMessage = App.generalController.gameOptionsController.winMessages[0];

            //Actualizar las veces que ha ganado sin errores
            App.generalModel.connectedGameModel.UpdatePerfectGame(0);
        }

        //Actualiza los puntos y estrellas obtenidos
        App.generalModel.connectedGameModel.UpdatePoints(points);
        App.generalModel.connectedGameModel.UpdateTotalStars(stars);

        App.generalModel.statsModel.UpdateTotalStars(totalStars);
        App.generalModel.statsModel.UpdateTotalPoints(totalPoints);

        //Debug.Log("IS LAST LEVEL: " + isLastLevel);
        //Mostrar el canvas que indica cuantas estrellas gano
        App.generalView.gameOptionsView.ShowWinCanvas(canvasStars, winMessage,isLastLevel);

    }
}
