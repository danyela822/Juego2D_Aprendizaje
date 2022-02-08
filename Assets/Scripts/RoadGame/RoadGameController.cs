using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class RoadGameController : Reference
{
    public GameObject initialBlock, gameZone;
    public List<GameObject> allCharacters;
    public List<GameObject> obstables;
    public List<GameObject> goals;

    //Variable para almacenar los pasos que da el jugador en la partida
    public int numberSteps = 0;
    // Se crea una matriz de objetos
    static Objects[,] arrayObjects;
    // Indica la cantidad de filas de la matriz
    readonly int arrayRow = 8;
    // Cantidad de columnas de la matriz
    readonly int arrayCol = 6;
    // Matriz de GameObject
    static GameObject[,] matrix;

    // Variables para el backtracking
    static int numSteps; // Numero de pasos para llegar de un punto a otro
    static int numLlamadas; // Numero de llamadas rercursivas
    static Objects[,] arraySolution = null; // Camino a seguir

    int numFinalSteps;

    int level;
    int[] arrivalPoint = new int[2];
    int[] startPoint = new int[2];
    readonly int[] obstaclePoint1 = new int[2];
    readonly int[] obstaclePoint2 = new int[2];
    int[] character2Point = new int[3];
    int[] character3Point = new int[3];

    int prueba;
    //
    int countPlay;


    [SerializeField]
    private string BASE_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScqB4zla9KJYoouFvzU2dVA58BMFk2emP7r5IkXvQ8LcjRkQg/formResponse";
    
    IEnumerator Post()
    {
        print("Form Data");
        WWWForm form = new WWWForm();
        form.AddField("entry.1874038654", "Road Game");
        form.AddField("entry.1072500869", level+"");
        form.AddField("entry.1041397496",numFinalSteps+"");
        form.AddField("entry.360365649",App.generalController.charactersController.steps+"");

        byte[] rawData = form.data;
        WWW www = new WWW(BASE_URL,rawData);
        yield return www;
    }

    public void Send()
    {
        print("Metodo send");
        StartCoroutine(Post());
    }

    
    private void Start()
    {
        level =  App.generalModel.roadGameModel.GetLevel();
        Debug.Log("ENTRO AL START ROADGAME con nivel " + level);
        CreateLevel();
    }

    
    // Metodo encargado de llamar todos los metodos necesarios para generar un nivel 
    public void CreateLevel()
    {
        // Llamado a los metodos encargados de generar la matriz y ubicar los respectivos puntos
        GenerateArray();
        GeneratePoints();

        if (level == 1) GenerateLevel1();

        else if (level == 2) GenerateLevel2();

        else GenerateLevel3();

        // Llamado a los Metodos encargados de Ubicar las figuras en la matriz
        LocateFigures();
        LocateFiguresArray();

        OcultarSolucion();
        InvisiblePoints();

        print("Array objects");
        ShowArray(arrayObjects);

        print("Array solution");
        ShowArray(arraySolution);

        prueba++;
        if (prueba == 1)
        {
            DrawMatrix();
        }  
    }
    void GenerateLevel1()
    {
        LocateObstacleLevel(1, 4, RamdonNumber(3, 5), RamdonNumber(1, 5));
        //LocateObstacle(4, RamdonNumber(3,5), RamdonNumber(1,5));

        // Llamado a los metodos encargados de generar la solucion mas corta entre los puntos
        GenerateSolution(obstaclePoint1[0], obstaclePoint1[1], 9, 2, 3);
        numFinalSteps = numSteps;
        LocateSolutionArray();
        
        GenerateSolution(startPoint[0], startPoint[1], 4, 2, 9);
        numFinalSteps += numSteps;
        LocateSolutionArray();

        LocateMainPoints(arraySolution);

        //Ruta invisible
        InvisibleRoute();

        LocateMainPointsLevel1(arrayObjects);
        LocateMainPointsLevel1(arraySolution);
    }

    void GenerateLevel2()
    {
        LocateObstacleLevel(1, 4, RamdonNumber(3, 5), RamdonNumber(1, 5));
        //LocateObstacle(4, RamdonNumber(3,5), RamdonNumber(1,5));

        // Llamado a los metodos encargados de generar la solucion mas corta entre los puntos
        GenerateSolution(obstaclePoint1[0], obstaclePoint1[1], 9, 2, 3);
        numFinalSteps = numSteps;
        LocateSolutionArray();

        GenerateSolution(startPoint[0], startPoint[1], 4, 2, 9);
        numFinalSteps += numSteps;
        LocateSolutionArray();

        LocateMainPoints(arraySolution);

        //Ruta invisible
        InvisibleRoute();

        //Llamado a los Metodos encargados de ubicar el personaje secundario apartir del obstaculo
        character2Point = LocateSecondCharacter(character2Point, obstaclePoint1, 5);
    }

    void GenerateLevel3()
    {
        LocateObstacleLevel3();
        arraySolution[obstaclePoint1[0], obstaclePoint1[1]].Type = 4;
        arraySolution[obstaclePoint2[0], obstaclePoint2[1]].Type = 10;

        // Llamado a los metodos necesarios para crear la ruta entre el punto de llegada y el obstaculo 1
        GenerateSolution(obstaclePoint1[0], obstaclePoint1[1], 9, 2, 3);
        numFinalSteps = numSteps;
        LocateSolutionArray();

        //Llama a los metodos necesarios para crear la ruta entre el obstaculo 1 y obstaculo 2
        GenerateSolution(obstaclePoint1[0], obstaclePoint1[1], 10, 2, 3);
        numFinalSteps += numSteps;
        LocateSolutionArray();
        LocateMainPointsLevel3(arrayObjects, obstaclePoint2, 10);
        LocateMainPointsLevel3(arraySolution, obstaclePoint2, 10);

        //Llama a los metodos necesarios para crear la ruta entre el obstaculo 2 y el punto de inicio
        GenerateSolution(startPoint[0], startPoint[1], 10, 2, 9);
        numFinalSteps += numSteps;
        LocateSolutionArray();

        LocateMainPointsLevel3(arrayObjects, obstaclePoint2, 10);
        LocateMainPointsLevel3(arraySolution, obstaclePoint2, 10);
        LocateMainPoints(arraySolution);

        //Ruta invisible
        InvisibleRoute();

        //Creacion de los personajes secundarios a partir de los obstaculos correspondientes
        character2Point = LocateSecondCharacter(character2Point, obstaclePoint1, 5);
        character3Point = LocateSecondCharacter(character3Point, obstaclePoint2, 11);

    }

    void LocateObstacleLevel3()
    {
        if (arrivalPoint[0] == 0 && arrivalPoint[1] == 0)
        {
            //Obstaculo 1
            LocateObstacleLevel(1, 4, RamdonNumber(1, 3), RamdonNumber(0, 3));
            //LocateObstacle(4, RamdonNumber(2,4), RamdonNumber(3,6));

            //Obstaculo 2
            LocateObstacleLevel(2, 10, RamdonNumber(5, 7), RamdonNumber(3, 6));
            //LocateObstacle(10, RamdonNumber(4,6), RamdonNumber(0,3));
        }
        else if (arrivalPoint[0] == 0 && arrivalPoint[1] == 5)
        {
            //Obstaculo 1
            LocateObstacleLevel(1, 4, RamdonNumber(1, 3), RamdonNumber(3, 6));
            //LocateObstacle(4, RamdonNumber(2,4), RamdonNumber(0,3));

            //Obstaculo 2
            LocateObstacleLevel(2, 10, RamdonNumber(5, 7), RamdonNumber(0, 3));
            //LocateObstacle(10, RamdonNumber(4,6), RamdonNumber(3,6));
        }
        else if (arrivalPoint[0] == 7 && arrivalPoint[1] == 0)
        {
            //Obstaculo 1
            LocateObstacleLevel(1, 4, RamdonNumber(5, 7), RamdonNumber(0, 3));
            //LocateObstacle(4, RamdonNumber(4,6), RamdonNumber(3,6));

            //Obstaculo 2
            LocateObstacleLevel(2, 10, RamdonNumber(1, 3), RamdonNumber(3, 6));
            //LocateObstacle(10, RamdonNumber(2,4), RamdonNumber(0,3));
        }
        else
        {
            //Obstaculo 1
            LocateObstacleLevel(1, 4, RamdonNumber(5, 7), RamdonNumber(3, 6));
            //LocateObstacle(4, RamdonNumber(4,6), RamdonNumber(0,3));

            //Obstaculo 2
            LocateObstacleLevel(2, 10, RamdonNumber(1, 3), RamdonNumber(0, 3));
            //LocateObstacle(10, RamdonNumber(2,4), RamdonNumber(3,6));
        }
    }


    int[] InvisibleLocation()
    {
        if (startPoint[0] == 0 && startPoint[1] == 0) return new int[] { RamdonNumber(0, 3), RamdonNumber(3, 6) };

        else if (startPoint[0] == 0 && startPoint[1] == 5) return new int[] { RamdonNumber(0, 3), RamdonNumber(0, 3) };

        else if (startPoint[0] == 7 && startPoint[1] == 0) return new int[] { RamdonNumber(5, 8), RamdonNumber(3, 6) };

        else return new int[] { RamdonNumber(5, 8), RamdonNumber(0, 3) };

    }

    public void InvisibleRoute()
    {
        int[] coordinates = InvisibleLocation();
        if (arrayObjects[coordinates[0], coordinates[1]].Type == 0)
        {
            arrayObjects[coordinates[0], coordinates[1]].Type = 6;
            GenerateSolution(startPoint[0], startPoint[1], 6, 7, 7);
            LocateSolutionArray();
        }
        else
        {
            InvisibleRoute();
        }
    }

    void InvisiblePoints()
    {
        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                if (arrayObjects[i, j].Type == 7)
                {
                    arrayObjects[i, j].Type = 0;
                    arraySolution[i, j].Type = 0;
                }
            }
        }
        arraySolution[startPoint[0], startPoint[1]].Type = 3;
    }

    // Metodo que se encarga de crear la matriz llenarla de 0
    public void GenerateArray()
    {
        arrayObjects = new Objects[arrayRow, arrayCol];

        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                arrayObjects[i, j] = new Objects(i, j, 0);
            }
        }
    }

    //Metodo encargado de generar el punto de llegada y partida
    void GeneratePoints()
    {
        arrivalPoint = Coordinates(RamdonNumber(1, 5));
        LocatePoints(arrivalPoint, 9);
        int type;

        if (arrivalPoint[0] == 0 && arrivalPoint[1] == 0) type = 4;

        else if (arrivalPoint[0] == 0 && arrivalPoint[1] == 5) type = 3;

        else if (arrivalPoint[0] == 7 && arrivalPoint[1] == 0) type = 2;

        else type = 1;

        startPoint = Coordinates(type);
        LocatePoints(startPoint, 3);
    }
    public int[] Coordinates(int type)
    {
        int[] position = new int[2];

        if (type == 1)
        {
            position[0] = 0; position[1] = 0;
        }
        else if (type == 2)
        {
            position[0] = 0; position[1] = arrayCol - 1;
        }
        else if (type == 3)
        {
            position[0] = arrayRow - 1; position[1] = 0;
        }
        else
        {
            position[0] = arrayRow - 1; position[1] = arrayCol - 1;
        }

        return position;
    }

    //Metodo encargado de ubicar  el punto de llegada y partida
    public void LocatePoints(int[] pos, int type)
    {
        // Se establece en la matriz el punto de partida (llegada del personaje) y se cambia el valor en type
        arrayObjects[pos[0], pos[1]].Type = type;

        //Se clona la matriz con el fin de  tener una donde se genera automaticamente una solucion en caso de no ser resuelto
        arraySolution = Clone(arrayObjects);
    }

    //Metodo encargado de ubicar el obstaculo en el centro de la matriz de forma aleatoria
    void LocateObstacle(int type, int x, int y)
    {
        //obstaclePoint1[0] = x;
        //obstaclePoint1[1] = y;
        arrayObjects[x, y].Type = type;
    }

    void LocateObstacleLevel(int option, int type, int x, int y)
    {
        if (option == 1)
        {
            obstaclePoint1[0] = x;
            obstaclePoint1[1] = y;
            LocateObstacle(type, x, y);
        }
        else
        {
            obstaclePoint2[0] = x;
            obstaclePoint2[1] = y;
            LocateObstacle(type, x, y);
        }
    }

    /* Metodo encargado de generar la ruta de solucion mas corta
    * recibe la fila y la columna donde se ubico el punto de partida
    */
    void GenerateSolution(int row, int col, int type, int numRoute, int num)
    {
        // Se crea una matriz de Objetos con la cual se empieza el proceso de backtracking
        // Esta se inicializa con una clonacion de la matriz donde ya se ubicaron los obstaculos y los Coordinates de partida y llegada
        Objects[,] map = Clone(arrayObjects);

        // Se ponen las variables que se utilizaran en el backtraking en cero
        ResetVariables();

        // Se hace un llamado al metodo backtracking encargado de encontrar una solucion
        Backtracking(map, row, col, type, numRoute, num, 0);

        // Si el numero de pasos es mayor a 0 esto indica que se encontro una solucion 
        if (numSteps > 0)
        {
            //Debug.Log("Se encontro una solucion con " + numSteps + " pasos");
            ShowArray(arraySolution);
            Debug.Log("Llamadas recursivas realizadas " + numLlamadas);
        }
        else
        {
            Debug.Log("No se pudo encontrar Solucion");
            // Como no se pudo encontrar una solucion se procede a generar un nuevo nivel
            //CreateLevel();
        }
    }

    // Metodo Backtracking encargado de buscar la solucion mas corta para llegar de un punto a otro
    void Backtracking(Objects[,] map, int row, int col, int type, int numRoute, int num, int nSteps)
    {
        numLlamadas++;
        // Caso base
        if (map[row, col].Type == type)
        {
            // Se pone la posicion en 2 ya que este numero indica que es la ruta
            map[row, col].Type = numRoute;

            if (numSteps == 0 || nSteps < numSteps)
            {
                arraySolution = Clone(map);
                numSteps = nSteps;
            }
        }
        else if ((numSteps == 0 || nSteps < numSteps) && map[row, col].Type != 1 && map[row, col].Type != 2 && map[row, col].Type != num)
        {
            map[row, col].Type = numRoute;

            if (row > 0)
                Backtracking(Clone(map), row - 1, col, type, numRoute, num, nSteps + 1);
            if (row < arrayRow - 1)
                Backtracking(Clone(map), row + 1, col, type, numRoute, num, nSteps + 1);
            if (col > 0)
                Backtracking(Clone(map), row, col - 1, type, numRoute, num, nSteps + 1);
            if (col < arrayCol - 1)
                Backtracking(Clone(map), row, col + 1, type, numRoute, num, nSteps + 1);
        }
    }

    // Metodo encargado de ubicar la solucion en el arreglo principal
    void LocateSolutionArray()
    {
        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                arrayObjects[i, j].Type = arraySolution[i, j].Type;
            }
        }

        LocateMainPoints(arrayObjects);
    }

    //Ubica los puntos principales en el array indicado
    void LocateMainPointsLevel1(Objects[,] array)
    {
        array[obstaclePoint1[0], obstaclePoint1[1]].Type = 2;
    }

    //Ubica los puntos principales en el array indicado
    void LocateMainPointsLevel3(Objects[,] array, int[] obstacle, int type)
    {
        array[obstacle[0], obstacle[1]].Type = type;
    }


    //Ubica los puntos principales en el array indicado
    void LocateMainPoints(Objects[,] array)
    {
        array[obstaclePoint1[0], obstaclePoint1[1]].Type = 4;
        array[arrivalPoint[0], arrivalPoint[1]].Type = 9;
        array[startPoint[0], startPoint[1]].Type = 3;
    }

    // Metodo que ubica las figuras en los puntos libres de la matriz
    void LocateFigures()
    {
        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                bool centinela = false;
                int k = 7;

                while (centinela == false && k > 0)
                {
                    centinela = Figures(i, j, k);
                    k--;
                }
            }
        }

    }

    /* Metodo encargado de administrar y hacer los llamados correspondientes dependiendo
    * el tipo de figura escogido
    */
    bool Figures(int x, int y, int figureType)
    {
        // Variable que almacena si se pudo realizar la figura
        bool approvedFigure = false;

        // Condiciones encargadas de verificar que figura se debe realizar y llamar al metodo correspondiente
        if (figureType == 1)
        {
            approvedFigure = Figure1(x, y);
        }
        else if (figureType == 2)
        {
            approvedFigure = Figure2(x, y);
        }
        else if (figureType == 3)
        {
            approvedFigure = Figure3(x, y);
        }
        else if (figureType == 4)
        {
            approvedFigure = Figure4(x, y);
        }
        else if (figureType == 5)
        {
            approvedFigure = Figure5(x, y);
        }
        else if (figureType == 6)
        {
            approvedFigure = Figure6(x, y);
        }
        else if (figureType == 7)
        {
            approvedFigure = Figure7(x, y);
        }

        return approvedFigure;
    }

    // Metodo encargado de genarar la figura 1
    bool Figure1(int x, int y)
    {
        // Variable donde se almacena si la figura se pudo realizar
        bool approved = false;

        // Condicion encargada de verificar que cada posicion que se debe usar para generar la figura este disponible
        if (CheckFigurePosition(x, y) && CheckFigurePosition(x + 1, y) && CheckFigurePosition(x + 2, y))
        {
            // Si todas las posiciones se pueden utilizar se procede a cambiar en la matriz dichas posiciones por sus nuevos valores
            arraySolution[x, y].Type = 1;
            arraySolution[x + 1, y].Type = 1;
            arraySolution[x + 2, y].Type = 1;
            approved = true;
        }
        return approved;
    }

    // Metodo encargado de genarar la figura 2
    bool Figure2(int x, int y)
    {
        // Variable donde se almacena si la figura se pudo realizar
        bool approved = false;

        // Condicion encargada de verificar que cada posicion que se debe usar para generar la figura este disponible
        if (CheckFigurePosition(x, y) && CheckFigurePosition(x, y + 1) && CheckFigurePosition(x, y + 2))
        {
            // Si todas las posiciones se pueden utilizar se procede a cambiar en la matriz dichas posiciones por sus nuevos valores
            arraySolution[x, y].Type = 1;
            arraySolution[x, y + 1].Type = 1;
            arraySolution[x, y + 2].Type = 1;
            approved = true;
        }

        return approved;
    }

    // Metodo encargado de genarar la figura 3
    bool Figure3(int x, int y)
    {
        // Variable donde se almacena si la figura se pudo realizar
        bool approved = false;

        // Condicion encargada de verificar que cada posicion que se debe usar para generar la figura este disponible
        if (CheckFigurePosition(x, y) && CheckFigurePosition(x + 1, y) && CheckFigurePosition(x + 1, y + 1))
        {
            // Si todas las posiciones se pueden utilizar se procede a cambiar en la matriz dichas posiciones por sus nuevos valores
            arraySolution[x, y].Type = 1;
            arraySolution[x + 1, y].Type = 1;
            arraySolution[x + 1, y + 1].Type = 1;
            approved = true;
        }

        return approved;
    }

    // Metodo encargado de genarar la figura 4
    bool Figure4(int x, int y)
    {
        // Variable donde se almacena si la figura se pudo realizar
        bool approved = false;

        // Condicion encargada de verificar que cada posicion que se debe usar para generar la figura este disponible
        if (CheckFigurePosition(x, y) && CheckFigurePosition(x + 1, y) && CheckFigurePosition(x, y + 1) && CheckFigurePosition(x + 1, y + 1))
        {
            // Si todas las posiciones se pueden utilizar se procede a cambiar en la matriz dichas posiciones por sus nuevos valores
            arraySolution[x, y].Type = 1;
            arraySolution[x + 1, y].Type = 1;
            arraySolution[x, y + 1].Type = 1;
            arraySolution[x + 1, y + 1].Type = 1;
            approved = true;
        }

        return approved;
    }

    // Metodo encargado de genarar la figura 5
    bool Figure5(int x, int y)
    {
        // Variable donde se almacena si la figura se pudo realizar
        bool approved = false;

        // Condicion encargada de verificar que cada posicion que se debe usar para generar la figura este disponible
        if (CheckFigurePosition(x, y) && CheckFigurePosition(x + 1, y) && CheckFigurePosition(x + 1, y + 1) && CheckFigurePosition(x + 2, y))
        {
            // Si todas las posiciones se pueden utilizar se procede a cambiar en la matriz dichas posiciones por sus nuevos valores
            arraySolution[x, y].Type = 1;
            arraySolution[x + 1, y].Type = 1;
            arraySolution[x + 1, y + 1].Type = 1;
            arraySolution[x + 2, y].Type = 1;
            // Como se pudo generar la figura se retorna verdadero
            approved = true;
        }

        return approved;
    }

    // Metodo encargado de genarar la figura 6
    bool Figure6(int x, int y)
    {
        // Variable donde se almacena si la figura se pudo realizar
        bool approved = false;

        // Condicion encargada de verificar que cada posicion que se debe usar para generar la figura este disponible
        if (CheckFigurePosition(x, y) && CheckFigurePosition(x + 1, y) && CheckFigurePosition(x, y + 1) && CheckFigurePosition(x, y + 2))
        {
            // Si todas las posiciones se pueden utilizar se procede a cambiar en la matriz dichas posiciones por sus nuevos valores
            arraySolution[x, y].Type = 1;
            arraySolution[x + 1, y].Type = 1;
            arraySolution[x, y + 1].Type = 1;
            arraySolution[x, y + 2].Type = 1;
            approved = true;
        }

        return approved;
    }

    // Metodo encargado de genarar la figura 7
    bool Figure7(int x, int y)
    {
        // Variable donde se almacena si la figura se pudo realizar
        bool approved = false;

        // Condicion encargada de verificar que cada posicion que se debe usar para generar la figura este disponible
        if (CheckFigurePosition(x, y) && CheckFigurePosition(x + 1, y - 1) && CheckFigurePosition(x + 1, y) && CheckFigurePosition(x + 1, y + 1) && CheckFigurePosition(x + 2, y))
        {
            // Si todas las posiciones se pueden utilizar se procede a cambiar en la matriz dichas posiciones por sus nuevos valores
            arraySolution[x, y].Type = 1;
            arraySolution[x + 1, y - 1].Type = 1;
            arraySolution[x + 1, y].Type = 1;
            arraySolution[x + 1, y + 1].Type = 1;
            arraySolution[x + 2, y].Type = 1;
            approved = true;
        }

        return approved;
    }

    /* Metodo encargado de verificar que la posicion donde se va a 
   * ubicar la figura no este ya ocupada y que no se salga de los limites
   * de la matriz
   */
    public bool CheckFigurePosition(int posX, int posY)
    {
        // Verifica que no se vaya salir de los limites 
        if (posX >= 0 && posY >= 0 && posX < arrayRow && posY < arrayCol)
        {
            // Verifica si la posicion indicada esta libre
            //if(arrayObjects[posX,posY].type == 0) return true;
            if (arraySolution[posX, posY].Type == 0) return true;
        }
        return false;
    }

    public bool CheckFigurePositionCharacter(int posX, int posY,int type)
    {
        Debug.Log("POS EN X: " + posX + " POS EN Y: " + posY);
        Debug.Log("ARRAYROW: " + arrayRow + " ARRAYCOL: " + arrayCol);
        Debug.Log("SE MOVIO UN TYPE: "+type);

        if (type == 1)
        {
            // Verifica que no se vaya salir de los limites 
            if (posX >= 0 && posY >= 0 && posX < arrayRow && posY < arrayCol)
            {
                // Verifica si la posicion indicada esta libre
                if ((arrayObjects[posX, posY].Type == 0 || arrayObjects[posX, posY].Type == 5 || arrayObjects[posX, posY].Type == 11) && matrix[posX, posY].GetComponent<Block>().visited ==  false)
                {
                  
                    Debug.Log("No ESTA OCUPADO: " + arrayObjects[posX, posY].X + " Y: " + arrayObjects[posX, posY].Y + " TYPE: " + type);
                        return true;
                }
                else if (arrayObjects[posX, posY].Type == 9)
                {
                    Debug.Log("GANASTE: " + arrayObjects[posX, posY].Type + " TYPE: " + type);
                    PuntoFinal();

                    return true;
                }
            }
            return false;
        }
        else if(type == 2)
        {
            // Verifica que no se vaya salir de los limites 
            if (posX >= 0 && posY >= 0 && posX < arrayRow && posY < arrayCol)
            {
                // Verifica si la posicion indicada esta libre
                if ((arrayObjects[posX, posY].Type == 0 || arrayObjects[posX, posY].Type == 5 || arrayObjects[posX, posY].Type == 4 || arrayObjects[posX, posY].Type == 11 || arrayObjects[posX, posY].Type == 3) && matrix[posX, posY].GetComponent<Block>().visited == false) 
                {
                    Debug.Log("No ESTA OCUPADO: " + arrayObjects[posX, posY].X + " Y: " + arrayObjects[posX, posY].Y + " TYPE: " + type);

                    if (arrayObjects[posX, posY].Type == 4)
                    {
                        arrayObjects[posX, posY].Type = 0;
                        Debug.Log("SE CAMBIO X: "+ arrayObjects[posX, posY].X+" Y: "+ arrayObjects[posX, posY].Y);
                    }
                    return true;
                }
            }
            return false;
        }
        else
        {
            // Verifica que no se vaya salir de los limites 
            if (posX >= 0 && posY >= 0 && posX < arrayRow && posY < arrayCol)
            {
                // Verifica si la posicion indicada esta libre
                if ((arrayObjects[posX, posY].Type == 0 || arrayObjects[posX, posY].Type == 11 || arrayObjects[posX, posY].Type == 10 || arrayObjects[posX, posY].Type == 5 || arrayObjects[posX, posY].Type == 3) && matrix[posX, posY].GetComponent<Block>().visited == false)
                {
                    Debug.Log("No ESTA OCUPADO: " + arrayObjects[posX, posY].X + " Y: " + arrayObjects[posX, posY].Y + " TYPE: " + type);
                    if (arrayObjects[posX, posY].Type == 10)
                    {
                        arrayObjects[posX, posY].Type = 0;
                        Debug.Log("SE CAMBIO X: " + arrayObjects[posX, posY].X + " Y: " + arrayObjects[posX, posY].Y);
                    }
                    return true;
                }
            }
            return false;
        }
        
    }
    //Metodo encargado de Ubicar las figuras en el array visual
    void LocateFiguresArray()
    {
        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                if (arraySolution[i, j].Type == 1)
                {
                    arrayObjects[i, j].Type = 1;
                }
            }
        }
    }

    // Metodo encargado de ubicar el personaje secundario apartir de la posicion del obstaculo
    int[] LocateSecondCharacter(int[] character, int[] obstacle, int type)
    {
        character = GeneratePoints(1, obstacle, type);

        if (character != null)
        {
            arrayObjects[character[0], character[1]].Type = character[2];
        }
        else
        {
            print("null personaje secundario");
            CreateLevel();
        }

        return character;
    }

    public int[] GeneratePoints(int cantMove, int[] point, int type)
    {
        //poner condicion de parada 
        int posX = point[0];
        int posY = point[1];
        int stop = 0;

        bool centinela = true;

        while (cantMove >= 0 && centinela)
        {
            if (stop == 15)
            {
                centinela = false;
            }

            int option = TipoMovimiento(1);
            //int option = RamdonNumber(1,5);
            if (option == 1)
            {
                if (CheckPosition(arraySolution, posX - 1, posY))
                {
                    posX--;
                    cantMove--;
                }
            }
            else if (option == 2)
            {
                if (CheckPosition(arraySolution, posX + 1, posY))
                {
                    posX++;
                    cantMove--;
                }
            }
            else if (option == 3)
            {
                if (CheckPosition(arraySolution, posX, posY - 1))
                {
                    posY--;
                    cantMove--;
                }
            }
            else
            {
                if (CheckPosition(arraySolution, posX, posY + 1))
                {
                    posY++;
                    cantMove--;
                }
            }
            stop++;
        }

        if (cantMove < 0)
        {
            int[] finalPosition = new int[] { posX, posY, type };
            return finalPosition;
        }

        return null;
    }

    int TipoMovimiento(int movimiento)
    {
        int type;

        // se puede mover a cualquier direccion
        if (movimiento == 1) type = RamdonNumber(1, 5);

        // Se muevo solo arriba y abajo
        else if (movimiento == 2) type = RamdonNumber(1, 3);

        // Se mueve derecha e izquierda
        else type = RamdonNumber(3, 5);

        //print("Ramdom movimiento "+ type);
        return type;
    }

    /* Metodo encargado de verificar que la posicion donde se va a 
    * ubicar la figura sea parte de la ruta de solucion y que no se 
    * salga de los limites de la matriz
    */
    bool CheckPosition(Objects[,] array, int posX, int posY)
    {
        // Verifica que no se vaya salir de los limites 
        if (posX >= 0 && posY >= 0 && posX < arrayRow && posY < arrayCol)
        {
            // Verifica si la posicion indicada esta libre
            if (array[posX, posY].Type == 2) return true;
        }
        return false;
    }

    /* Metodo que genera un numero aleatorio dependiendo el rango que se le indique
    * min -> numero minimo (incluido)
    * max -> numero maximo (no incluido)
    */
    public int RamdonNumber(int min, int max)
    {
        int number = UnityEngine.Random.Range(min, max);
        return number;
    }

    // Metodo encargado de resetear las variables 
    void ResetVariables()
    {
        numSteps = 0;
        numLlamadas = 0;
        //arraySolution = null;
    }

    void OcultarSolucion()
    {
        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                if (arrayObjects[i, j].Type == 2) arrayObjects[i, j].Type = 0;
            }
        }
    }


    // Metodo auxiliar para duplicar la matriz
    Objects[,] Clone(Objects[,] array)
    {
        Objects[,] arrayClone = new Objects[arrayRow, arrayCol];

        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                arrayClone[i, j] = new Objects(array[i, j].X, array[i, j].Y, array[i, j].Type);
            }
        }
        return arrayClone;
    }

    // Metodo encargado de ubicar la solucion en la matriz 
    public void LocateSolucion()
    {
        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                if (arrayObjects[i, j].Type != 3 && arrayObjects[i, j].Type != 4)
                {
                    arrayObjects[i, j].Type = arraySolution[i, j].Type;
                }
            }
        }
    }

    // Metodo que imprime en consola la matriz y sus valores correspondientes
    void ShowArray(Objects[,] array)
    {
        string imprimir = "\n";
        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                imprimir = imprimir + array[i, j].Type + " ";
            }
            imprimir += "\n";
        }
        Debug.Log(imprimir);
    }

    /*
  * Metodo que dibuja en la pantalla la matriz
  */
    public void DrawMatrix()
    {
        //Posicion inicial del bloque
        float posStarX = initialBlock.transform.position.x;
        float posStarY = initialBlock.transform.position.y;

        //Tamanio del bloque
        Vector2 blockSize = initialBlock.GetComponent<BoxCollider2D>().size;

        //
        //Objects[,] matrix1 = ReturnArray();
        Objects[,] matrix1 = arrayObjects;

        //Matriz de objetos que representara el mapa en la pantalla
        matrix = new GameObject[matrix1.GetLength(0), matrix1.GetLength(1)];

        //
        string[] themes = { "Sea", "Castle", "Forest" };
        string theme = themes[Random.Range(0, themes.Length)];
        //string theme = "Forest";

        //Matrices que contienen los sprites para llenar el mapa
        Sprite[,] floorMatrix = App.generalModel.roadGameModel.GetMapFloor(theme);
        Sprite[,] lockMatrix = App.generalModel.roadGameModel.GetMapLock(theme);

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

                matrix[i, j].name = string.Format("Block[{0}][{1}]", i, j);

                //Temporal - se asigna un 0 para indicar que es bloque disponible o 1 para indicar que es bloque obstaculo
                matrix[i, j].GetComponent<Block>().Id=(matrix1[i, j].Type);

                matrix[i, j].GetComponent<Block>().visited = false;

                //Asignar la matriz al objeto de GameZone
                matrix[i, j].transform.parent = gameZone.transform;

                //Si es 0 se pinta de gris
                if (matrix1[i, j].Type == 0 || matrix1[i, j].Type == 11 || matrix1[i, j].Type == 3 || matrix1[i, j].Type == 5)
                {
                    //matrix[i, j].GetComponent<SpriteRenderer>().color = Color.gray;
                    matrix[i, j].GetComponent<SpriteRenderer>().sprite = floorMatrix[i, j];
                }
                //Si es 1 se pinta de negro - bloqueo
                else if (matrix1[i, j].Type == 1)
                {
                    //matrix[i, j].GetComponent<SpriteRenderer>().color = Color.black;
                    matrix[i, j].GetComponent<SpriteRenderer>().sprite = lockMatrix[i, j];
                    //matrix[i, j].GetComponent<Collider2D>().isTrigger = false;
                }
                //Si es 4 se pinta de rojo (obstaculo)
                else if (matrix1[i, j].Type == 4)
                {
                    matrix[i, j].GetComponent<SpriteRenderer>().sprite = floorMatrix[i, j];
                    DrawObstacle(matrix1[i, j], matrix[i, j], theme);
                }
                //Si es 9 se pinta de azul (Puntp Llegada)
                else if (matrix1[i, j].Type == 9)
                {
                    matrix[i, j].GetComponent<SpriteRenderer>().sprite = floorMatrix[i, j];
                    DrawObstacle(matrix1[i, j], matrix[i, j], theme);
                }
                //Si es 10 se pinta de cyan (Obstaculo 2)
                else if (matrix1[i, j].Type == 10)
                {
                    matrix[i, j].GetComponent<SpriteRenderer>().sprite = floorMatrix[i, j];
                    DrawObstacle(matrix1[i, j], matrix[i, j], theme);
                }
            }
        }
        App.generalController.charactersController.CreateCharacters(theme, level);
        App.generalController.charactersController.SelectCharactersLevel(allCharacters);
    }

    public void DrawObstacle(Objects logicBox,GameObject box,string theme)
    {
        GameObject obstacle = null;

        if (logicBox.Type == 4)
        {
            switch (theme)
            {
                case "Sea":
                    obstacle = Instantiate(obstables[4], new Vector3(box.transform.position.x, box.transform.position.y, box.transform.position.z), obstables[4].transform.rotation);
                    break;
                case "Castle":
                    obstacle = Instantiate(obstables[0], new Vector3(box.transform.position.x, box.transform.position.y, box.transform.position.z), obstables[0].transform.rotation);
                    break;
                case "Forest":
                    obstacle = Instantiate(obstables[2], new Vector3(box.transform.position.x, box.transform.position.y, box.transform.position.z), obstables[2].transform.rotation);
                    break;
            }
        }
        if (logicBox.Type == 10)
        {
            switch (theme)
            {
                case "Sea":
                    obstacle = Instantiate(obstables[4], new Vector3(box.transform.position.x, box.transform.position.y, box.transform.position.z), obstables[4].transform.rotation);
                    break;
                case "Castle":
                    obstacle = Instantiate(obstables[1], new Vector3(box.transform.position.x, box.transform.position.y, box.transform.position.z), obstables[1].transform.rotation);
                    break;
                case "Forest":
                    obstacle = Instantiate(obstables[3], new Vector3(box.transform.position.x, box.transform.position.y, box.transform.position.z), obstables[3].transform.rotation);
                    break;
            }
        }
        if(logicBox.Type == 9)
        {
            switch (theme)
            {
                case "Sea":
                    obstacle = Instantiate(goals[0], new Vector3(box.transform.position.x, box.transform.position.y, box.transform.position.z), goals[0].transform.rotation);
                    break;
                case "Castle":
                    obstacle = Instantiate(goals[1], new Vector3(box.transform.position.x, box.transform.position.y, box.transform.position.z), goals[1].transform.rotation);
                    break;
                case "Forest":
                    obstacle = Instantiate(goals[2], new Vector3(box.transform.position.x, box.transform.position.y, box.transform.position.z), goals[2].transform.rotation);
                    break;
            }
        }

        obstacle.transform.parent = box.transform;
        obstacle.transform.position = Vector3.zero;
        obstacle.transform.position = box.transform.position;
    }

    //Metodo para contar los pasos que dio el jugador en la partida
    public int CountSteps()
    {
        //Ciclo que recorrer toda la matriz para obtener los pasos que dio el jugador sobre cada una de las casillas
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                //Sumar los pasos de cada casilla en una sola variable
                numberSteps += matrix[i, j].GetComponent<Block>().NumVisited;
            }
        }
        //Eliminar los pasos que da el personaje sobre la casilla donde es ubicado al inicio de la partida
        if (level == 2)
        {
            print("DOS PERSONAJEES: " + numberSteps);
            numberSteps -= 3;
        }
        else
        {
            print("TRES PERSONAJEES: " + numberSteps);
            numberSteps -= 4;
        }
        print("TOTAL: " + numberSteps);
        return numberSteps;
    }

    ////////////////////////////////////////////////////////////////////CAMILA//////////////////////////////////////////////////////////////
    public void PuntoFinal()
    {
        //int totalSteps = CountSteps();

        int totalSteps = App.generalController.charactersController.steps;
        
        int totalStars = Coints(totalSteps);

        PointsLevel(totalStars);

        Send();
        //Verificar si ya jugo un nivel de este juego
        countPlay = App.generalModel.roadGameModel.GetTimesPlayed();

        App.generalModel.roadGameModel.UpdateTimesPlayed(++countPlay);
        print("HA JUGADO: " + countPlay);

        //SetPointsAndStars();
    }

    //Declaracion del mensaje a mostrar
    string winMessage;
    bool isLastLevel = false;
    public void PointsLevel(int totalStars)
    {
        int pointsPerStar = 10;
        int pointsLevel = totalStars * pointsPerStar;

       // App.generalModel.roadGameModel.SetPoints(App.generalModel.roadGameModel.GetPoints() + pointsLevel);

        
        if (level==1)
        {
            App.generalModel.roadGameModel.UpdateLevel(2);
        }
        else if (level == 2)
        {
            App.generalModel.roadGameModel.UpdateLevel(3);
        }
        else
        {
            App.generalModel.roadGameModel.UpdateLevel(1);
            isLastLevel = true;
        }
        //App.generalView.roadGameView.ActivateWinCanvas(totalStars);
        App.generalView.gameOptionsView.ShowWinCanvas(totalStars,winMessage, isLastLevel);
        //Debug.Log("GANASTE");
        //print("Tiempo total " + App.generalModel.roadGameModel.GetTime());
    }

    public int Coints(int totalSteps)
    {
        int totalStars;
        //Si los pasos realizados por el usuario son menor o igual a los pasos del algoritmo tiene 3 estrellas
        //Si los pasos realizados por el usuario son mayores a los pasos del algoritmo por maximo 3 pasos son 2 estrellas
        //Si son mayores por mas de 3 pasos extras tiene una estrella
        if (totalSteps <= numFinalSteps)
        {
            totalStars = 3;
            winMessage = App.generalController.gameOptionsController.winMessages[2];
        }
        else if (totalSteps > numFinalSteps && totalSteps <= numFinalSteps + 3)
        {
            totalStars = 2;
            winMessage = App.generalController.gameOptionsController.winMessages[1];
        }
        else
        {
            totalStars = 1;
            winMessage = App.generalController.gameOptionsController.winMessages[0];
        }
        return totalStars;
    }

    /// <summary>
    /// Metodo que asigna los puntos y estrellas que ha ganado el jugador
    /// </summary>
    /// <param name="rating">Int rating que indica los puntos que deben ser asignados</param>
    public void SetPointsAndStars(int rating)
    {
        SoundManager.soundManager.PlaySound(4);

        //Declaracion de los puntos y estrellas que ha ganado el juegador
        int points, stars, canvasStars;

        //Declaracion del mensaje a mostrar
        string winMessage;

        //Si gana el juego con 3 intentos suma 30 puntos y gana 3 estrellas
        if (rating == 1)
        {
            points = App.generalModel.roadGameModel.GetPoints() + 30;
            stars = App.generalModel.roadGameModel.GetTotalStars() + 3;
            canvasStars = 3;
            winMessage = App.generalController.gameOptionsController.winMessages[2];

            //Actualizar las veces que ha ganado 3 estrellas
            App.generalModel.roadGameModel.UpdatePerfectWins(App.generalModel.roadGameModel.GetPerfectWins() + 1);

            //Actualizar las veces que ha ganado sin errores
            App.generalModel.roadGameModel.UpdatePerfectGame(App.generalModel.roadGameModel.GetPerfectGame() + 1);
            //Debug.Log("LLEVA: " + PlayerPrefs.GetInt("PerfectGame2", 0) + " JUEGO(S) PERFECTO(S)");
        }
        //Si gana el juego mas de 3 y menos de 9 intentos suma 20 puntos y gana 2 estrellas
        else if (rating == 2)
        {
            points = App.generalModel.roadGameModel.GetPoints() + 20;
            stars = App.generalModel.roadGameModel.GetTotalStars() + 2;
            canvasStars = 2;
            winMessage = App.generalController.gameOptionsController.winMessages[1];

            //Actualizar las veces que ha ganado sin errores -LE FALTAN DETALLES
            App.generalModel.roadGameModel.UpdatePerfectGame(0);
        }
        //Si gana el juego con mas de 9 intentos suma 10 puntos y gana 1 estrella
        else
        {
            points = App.generalModel.roadGameModel.GetPoints() + 10;
            stars = App.generalModel.roadGameModel.GetTotalStars() + 1;
            canvasStars = 1;
            winMessage = App.generalController.gameOptionsController.winMessages[0];

            //Actualizar las veces que ha ganado sin errores
            App.generalModel.roadGameModel.UpdatePerfectGame(0);
        }

        //Actualiza los puntos y estrellas obtenidos
        App.generalModel.roadGameModel.UpdatePoints(points);
        App.generalModel.roadGameModel.UpdateTotalStars(stars);

        Debug.Log("IS LAST LEVEL: " + isLastLevel);
        //Mostrar el canvas que indica cuantas estrellas gano
        App.generalView.gameOptionsView.ShowWinCanvas(canvasStars, winMessage, isLastLevel);
        

    }

    // Metodo encargado de Pintar la ruta de solucion
    public void DrawSolution()
    {
        /*LocateSolucion();
        App.generalModel.roadGameModel.DecraseTickets();
        App.generalView.gameOptionsView.SolutionCanvas.enabled = false;

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (arrayObjects[i, j].Type == 2)
                {
                    matrix[i, j].GetComponent<Block>().Id=(arrayObjects[i, j].Type);

                    matrix[i, j].GetComponent<SpriteRenderer>().color = Color.grey;
                }
            }
        }*/

        //Si hay tickets muestra la solucion
        Debug.Log("HAY: " + App.generalModel.ticketModel.GetTickets() + " TICKETS");
        if (App.generalModel.ticketModel.GetTickets() >= 1)
        {
            Debug.Log("HAS USADO UN PASE");

            App.generalController.ticketController.DecraseTickets();
            App.generalView.gameOptionsView.HideBuyCanvas();

            LocateSolucion();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (arrayObjects[i, j].Type == 2)
                    {
                        matrix[i, j].GetComponent<Block>().Id = (arrayObjects[i, j].Type);

                        matrix[i, j].GetComponent<SpriteRenderer>().color = Color.grey;
                    }
                }
            }
        }
        else
        {
            App.generalView.gameOptionsView.ShowTicketsCanvas(3);
        }
    }

    public GameObject[,] GetMatrix()
    {
        return matrix;
    }

    public Objects[,] ReturnArray()
    {
        return arrayObjects;
    }
}