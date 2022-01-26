using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoadGameController : Reference
{
    public GameObject initialBlock, gameZone;
    public List<GameObject> allCharacters;

    int numCharacters = 0;

    //Variable para almacenar los pasos que da el jugador en la partida
    public int numberSteps = 0;
    // Se crea una matriz de objetos
    static Objects [,] arrayObjects;
    // Indica la cantidad de filas de la matriz
    int arrayRow = 8;
    // Cantidad de columnas de la matriz
    int arrayCol = 6;
    // Matriz de GameObject
    static GameObject[,] matrix;

    // Variables para el backtracking
    static int numSteps; // Numero de pasos para llegar de un punto a otro
    static int numLlamadas; // Numero de llamadas rercursivas
    static Objects [,] arraySolution = null; // Camino a seguir

    static int level = 1;
    int [] arrivalPoint = new int [2];
    int [] startPoint = new int [2];
    int [] obstaclePoint = new int [2];
    int [] character2Point = new int [3];

    private void Start()
    {
        Debug.Log("ENTRO AL START ROADGAME con nivel "+ level);
        CreateLevel();
    }

    public void RamdonLevel()
    {
        int ramdonCategory = RamdonNumber(1,4);
        level = ramdonCategory;

        CreateLevel();
    }
    // Metodo encargado de llamar todos los metodos necesarios para generar un nivel 
    public void CreateLevel()
    {
        // Llamado a los metodos encargados de generar la matriz y ubicar los respectivos puntos
        GenerateArray();
        GeneratePoints();
        LocateObstacle();

        // Llamado a los metodos encargados de generar la solucion mas corta entre los puntos
        GenerateSolution(obstaclePoint[0], obstaclePoint[1], 9, 3);
        LocateSolutionArray();
        GenerateSolution(startPoint[0], startPoint[1], 4, 9);
        LocateSolutionArray();
        LocateMainPoints(arraySolution);

        // Llamado a los Metodos encargados de Ubicar las figuras en la matriz
        LocateFigures();
        LocateFiguresArray();

        //Llamado a los Metodos encargados de ubicar el personaje secundario apartir del obstaculo
        LocateSecondCharacter();

        OcultarSolucion();

        print("Array objects");
        ShowArray(arrayObjects);

        print("Array solution");
        ShowArray(arraySolution);
        
        /*LocateObstacle();
        // Ubicar personaje que mueve obstaculo
        CreatePointsLevel(5,2,2);
        // Ubicar personaje principal
        CreatePointsLevel(3,5,1);
        
        ShowArray(arraySolution);
        LocateFigures();
        LocateFiguresArray();
        //posStart = GeneratePoints();
        //FiguresQuantity(figuresQuantity, figureTypeMin, figureTypeMax);
        //ShowArray(arrayObjects);
        //GenerateSolution(posStart[0],posStart[1]);
        //ShowArray(arrayObjects);
        //DrawMatrix("Sea");*/
    }

    // Metodo que se encarga de crear la matriz llenarla de 0
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

    //Metodo encargado de generar el punto de llegada y partida
    void GeneratePoints ()
    {
        arrivalPoint = Coordinates(RamdonNumber(1,5));
        LocatePoints(arrivalPoint, 9); 
        int type = 0;
        
        if(arrivalPoint[0] == 0) type = RamdonNumber(3,5);
        
        else type = RamdonNumber(1,3);

        startPoint = Coordinates(type);
        LocatePoints(startPoint, 3);
    }
    public int [] Coordinates(int type)
    {
        int [] position = new int [2];

        if(type == 1)
        {
            position[0] = 0; position[1] = 0;
        }
        else if(type == 2)
        {
            position[0] = 0; position[1] = arrayCol-1;
        } 
        else if (type == 3) 
        {
            position[0] = arrayRow-1; position[1] = 0;
        }
        else
        {  
            position[0] = arrayRow-1; position[1] = arrayCol-1;
        }

        return position;
    }
    
    //Metodo encargado de ubicar  el punto de llegada y partida
    public void LocatePoints(int [] pos, int type)
    {
        // Se establece en la matriz el punto de partida (llegada del personaje) y se cambia el valor en type
        arrayObjects[pos[0],pos[1]].type = type;

        //Se clona la matriz con el fin de  tener una donde se genera automaticamente una solucion en caso de no ser resuelto
        arraySolution = Clone(arrayObjects);
    }
    
    //Metodo encargado de ubicar el obstaculo en el centro de la matriz de forma aleatoria
    void LocateObstacle()
    {
        obstaclePoint[0] = RamdonNumber(3,5);
        obstaclePoint[1] = RamdonNumber(2,4);

        arrayObjects[obstaclePoint[0],obstaclePoint[1]].type = 4;
    }
    
    /* Metodo encargado de generar la ruta de solucion mas corta
    * recibe la fila y la columna donde se ubico el punto de partida
    */
    void GenerateSolution(int row, int col, int type, int num)
    {
        // Se crea una matriz de Objetos con la cual se empieza el proceso de backtracking
        // Esta se inicializa con una clonacion de la matriz donde ya se ubicaron los obstaculos y los Coordinates de partida y llegada
        Objects [,] map = Clone(arrayObjects);

        
        // Se ponen las variables que se utilizaran en el backtraking en cero
        ResetVariables();

        // Se hace un llamado al metodo backtracking encargado de encontrar una solucion
        Backtracking(map, row, col, type, num, 0);

        // Si el numero de pasos es mayor a 0 esto indica que se encontro una solucion 
        if(numSteps > 0)
        {
            Debug.Log("Se encontro una solucion con "+numSteps+" pasos");
            ShowArray(arraySolution);
            Debug.Log("Llamadas recursivas realizadas "+numLlamadas);
        }
        else
        {
            Debug.Log("No se pudo encontrar Solucion");
            // Como no se pudo encontrar una solucion se procede a generar un nuevo nivel
            //CreateLevel();
        }
    }

    // Metodo Backtracking encargado de buscar la solucion mas corta para llegar de un punto a otro
    void Backtracking (Objects [,] map, int row, int col, int type, int num, int nSteps)
    {
        numLlamadas++;
        // Caso base
        if(map[row,col].type == type)
        {
            // Se pone la posicion en 2 ya que este numero indica que es la ruta
            map[row,col].type = 2;

            if(numSteps == 0 || nSteps < numSteps)
            {
                arraySolution = Clone(map);
                numSteps = nSteps;
            }
        }
        else if((numSteps == 0 || nSteps < numSteps) && map[row,col].type != 1 && map[row,col].type != 2 && map[row,col].type != num)
        {
            map[row,col].type = 2;

            if(row > 0) 
                Backtracking(Clone(map), row - 1, col, type, num, nSteps + 1);
            if(row < arrayRow-1) 
                Backtracking(Clone(map), row + 1, col, type, num, nSteps + 1);
            if(col > 0) 
                Backtracking(Clone(map), row, col - 1, type, num, nSteps + 1);
            if(col < arrayCol-1) 
                Backtracking(Clone(map), row, col + 1, type, num, nSteps + 1);
        }
    }

    // Metodo encargado de ubicar la solucion en el arreglo principal
    void LocateSolutionArray()
    {
        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                arrayObjects[i,j].type = arraySolution[i,j].type;
            }
        }

        LocateMainPoints(arrayObjects);
    }

    //Ubica los puntos principales en el array indicado
    void LocateMainPoints (Objects [,] array)
    {
        array[obstaclePoint[0],obstaclePoint[1]].type = 4;
        array[arrivalPoint[0],arrivalPoint[1]].type = 9;
        array[startPoint[0],startPoint[1]].type = 3;
    }
    
    // Metodo que ubica las figuras en los puntos libres de la matriz
    void LocateFigures ()
    {
        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                bool centinela = false;
                int k = 7;
                
                while(centinela == false && k > 0)
                {
                    centinela = Figures(i,j,k);
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
        if(figureType == 1)
        {
           approvedFigure = Figure1(x,y);
        }
        else if(figureType == 2)
        {
            approvedFigure = Figure2(x,y);
        }
        else if(figureType == 3)
        {
            approvedFigure = Figure3(x,y);
        }
        else if(figureType == 4)
        {
            approvedFigure = Figure4(x,y);
        }
        else if(figureType == 5)
        {
            approvedFigure = Figure5(x,y);
        }
        else if(figureType == 6)
        {
            approvedFigure = Figure6(x,y);
        }
        else if(figureType == 7)
        {
            approvedFigure = Figure7(x,y);
        }

        return approvedFigure;
    }
    
    // Metodo encargado de genarar la figura 1
    bool Figure1(int x, int y)
    {
        // Variable donde se almacena si la figura se pudo realizar
        bool approved = false;
        
        // Condicion encargada de verificar que cada posicion que se debe usar para generar la figura este disponible
        if(CheckFigurePosition(x,y) && CheckFigurePosition(x+1,y) && CheckFigurePosition(x+2,y))
        {
            // Si todas las posiciones se pueden utilizar se procede a cambiar en la matriz dichas posiciones por sus nuevos valores
            arraySolution[x,y].type = 1;
            arraySolution[x+1,y].type = 1;
            arraySolution[x+2,y].type = 1;
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
        if(CheckFigurePosition(x,y) && CheckFigurePosition(x,y+1) && CheckFigurePosition(x,y+2))
        {
            // Si todas las posiciones se pueden utilizar se procede a cambiar en la matriz dichas posiciones por sus nuevos valores
            arraySolution[x,y].type = 1;
            arraySolution[x,y+1].type = 1;
            arraySolution[x,y+2].type = 1;
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
        if(CheckFigurePosition(x,y) && CheckFigurePosition(x+1,y) && CheckFigurePosition(x+1,y+1))
        {
            // Si todas las posiciones se pueden utilizar se procede a cambiar en la matriz dichas posiciones por sus nuevos valores
            arraySolution[x,y].type = 1;
            arraySolution[x+1,y].type = 1;
            arraySolution[x+1,y+1].type = 1;
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
        if(CheckFigurePosition(x,y) && CheckFigurePosition(x+1,y) && CheckFigurePosition(x,y+1) && CheckFigurePosition(x+1,y+1))
        {
            // Si todas las posiciones se pueden utilizar se procede a cambiar en la matriz dichas posiciones por sus nuevos valores
            arraySolution[x,y].type = 1;
            arraySolution[x+1,y].type = 1;
            arraySolution[x,y+1].type = 1;
            arraySolution[x+1,y+1].type = 1;
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
        if(CheckFigurePosition(x,y) && CheckFigurePosition(x+1,y) && CheckFigurePosition(x+1,y+1) && CheckFigurePosition(x+2,y))
        {
            // Si todas las posiciones se pueden utilizar se procede a cambiar en la matriz dichas posiciones por sus nuevos valores
            arraySolution[x,y].type = 1;
            arraySolution[x+1,y].type = 1;
            arraySolution[x+1,y+1].type = 1;
            arraySolution[x+2,y].type = 1;
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
        if(CheckFigurePosition(x,y) && CheckFigurePosition(x+1,y) && CheckFigurePosition(x,y+1) && CheckFigurePosition(x,y+2))
        {
            // Si todas las posiciones se pueden utilizar se procede a cambiar en la matriz dichas posiciones por sus nuevos valores
            arraySolution[x,y].type = 1;
            arraySolution[x+1,y].type = 1;
            arraySolution[x,y+1].type = 1;
            arraySolution[x,y+2].type = 1;
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
        if(CheckFigurePosition(x,y) && CheckFigurePosition(x+1,y-1) && CheckFigurePosition(x+1,y) && CheckFigurePosition(x+1,y+1) && CheckFigurePosition(x+2,y))
        {
            // Si todas las posiciones se pueden utilizar se procede a cambiar en la matriz dichas posiciones por sus nuevos valores
            arraySolution[x,y].type = 1;
            arraySolution[x+1,y-1].type = 1;
            arraySolution[x+1,y].type = 1;
            arraySolution[x+1,y+1].type = 1;
            arraySolution[x+2,y].type = 1;
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
        Debug.Log("POS EN X: "+posX+" POS EN Y: "+posY);
        Debug.Log("ARRAYROW: " + arrayRow + " ARRAYCOL: " + arrayCol);
        // Verifica que no se vaya salir de los limites 
        if (posX >= 0 && posY >= 0 && posX < arrayRow && posY < arrayCol)
        {
            // Verifica si la posicion indicada esta libre
            if(arrayObjects[posX,posY].type == 0) return true;
            if(arraySolution[posX,posY].type == 0) return true;
        }
        return false;
    }
    
    //Metodo encargado de Ubicar las figuras en el array visual
    void LocateFiguresArray()
    {
        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                if(arraySolution[i,j].type == 1)
                {
                    arrayObjects[i,j].type = 1;
                }
            }
        }
    }
    
    // Metodo encargado de ubicar el personaje secundario apartir de la posicion del obstaculo
    void LocateSecondCharacter ()
    {
        character2Point = GeneratePoints(2, obstaclePoint);

        if(character2Point != null)
        {
            arrayObjects[character2Point[0],character2Point[1]].type = character2Point[2];
        }
        else{
            print("null");
        }
    }


    public int [] GeneratePoints(int cantMove, int [] point)
    {
        //poner condicion de parada 
        int posX = point[0];
        int posY = point[1];
        int stop = 0;

        bool centinela = true;

        while(cantMove >= 0 && centinela)
        {
            if(stop == 15)
            {
                centinela = false;
            }

            int option = TipoMovimiento(3);
            //int option = RamdonNumber(1,5);
            if(option == 1)
            {
                if(CheckPosition(arraySolution,posX-1,posY))
                {
                    posX--;
                    cantMove --;
                } 
            }
            else if(option == 2)
            {
                if(CheckPosition(arraySolution,posX+1,posY))
                {
                    posX++;
                    cantMove --;
                } 
            }
            else if(option == 3)
            {
                if(CheckPosition(arraySolution,posX,posY-1))
                {
                    posY--;
                    cantMove --;
                } 
            }
            else{
                if(CheckPosition(arraySolution,posX,posY+1))
                {
                    posY++;
                    cantMove --;
                } 
            }
            stop ++;
        }

        if(cantMove < 0)
        {
            int [] finalPosition = new int [] {posX,posY,5};
            return finalPosition;
        }
        
        return null;
    }

    int TipoMovimiento (int movimiento)
    {
        int type = 0;

        // se puede mover a cualquier direccion
        if(movimiento == 1) type = RamdonNumber(1,5);

        // Se muevo solo arriba y abajo
        else if(movimiento == 2) type = RamdonNumber(1,3);

        // Se mueve derecha e izquierda
        else type = RamdonNumber(3,5);

        //print("Ramdom movimiento "+ type);
        return type;
    }

    /* Metodo encargado de verificar que la posicion donde se va a 
    * ubicar la figura sea parte de la ruta de solucion y que no se 
    * salga de los limites de la matriz
    */
    bool CheckPosition(Objects [,] array,int posX, int posY)
    {
        // Verifica que no se vaya salir de los limites 
        if(posX >= 0 && posY >= 0 && posX < arrayRow && posY < arrayCol)
        {
            // Verifica si la posicion indicada esta libre
            if(array[posX,posY].type == 2) return true;
        }
        return false;
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

    // Metodo encargado de resetear las variables 
    void ResetVariables ()
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
                if(arrayObjects[i,j].type == 2) arrayObjects[i,j].type = 0;
            }
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
                arrayClone[i,j] = new Objects(array[i,j].x, array[i,j].y, array[i,j].type);
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
                if(arrayObjects[i,j].type != 3 && arrayObjects[i,j].type != 4)
                {
                    arrayObjects[i,j].type = arraySolution[i,j].type;
                }
            }
        }
    }
   
    // Metodo que imprime en consola la matriz y sus valores correspondientes
    void ShowArray (Objects [,] array)
    {
        string imprimir = "\n";
        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                imprimir = imprimir + array[i, j].type + " ";
            }
            imprimir = imprimir + "\n";
        }
        Debug.Log(imprimir);
    }

       /*
     * Metodo que dibuja en la pantalla la matriz
     */
    public void DrawMatrix()
    {
        //
        //GameObject initialBlock = App.generalView.roadGameView.initialBlock;

        //Posicion inicial del bloque
        float posStarX = initialBlock.transform.position.x;
        float posStarY = initialBlock.transform.position.y;

        //Tamanio del bloque
        Vector2 blockSize = initialBlock.GetComponent<BoxCollider2D>().size;

        //
        Objects[,] matrix1 = ReturnArray();

        //Matriz de objetos que representara el mapa en la pantalla
        matrix = new GameObject[matrix1.GetLength(0), matrix1.GetLength(1)];

        //
        string [] themes = { "Sea", "Castle", "Forest" };
        string theme = themes[Random.Range(0, themes.Length)];

        //Matrices que contienen los sprites para llenar el mapa
        //Sprite[,] floorMatrix = App.generalModel.roadGameModel.GetMapFloor(theme);
        //Sprite[,] lockMatrix = App.generalModel.roadGameModel.GetMapLock(theme);

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
                matrix[i, j].GetComponent<Block>().SetId(matrix1[i,j].type);

                //Asignar la matriz al objeto de GameZone
                matrix[i, j].transform.parent = gameZone.transform;

                //Si es 0 se pinta de blanco (Azul por defecto)
                if (matrix1[i, j].type == 0)
                {
                    matrix[i, j].GetComponent<SpriteRenderer>().color = Color.gray;
                    //matrix[i, j].GetComponent<SpriteRenderer>().sprite = floorMatrix[i, j];
                }
                //Si es 1 se pinta de negro
                else if (matrix1[i, j].type == 1)
                {
                    matrix[i, j].GetComponent<SpriteRenderer>().color = Color.black;
                    //matrix[i, j].GetComponent<SpriteRenderer>().sprite = lockMatrix[i, j];

                    matrix[i, j].gameObject.layer = 6;
                    matrix[i, j].GetComponent<Collider2D>().isTrigger = false;
                }
                //Si es 3 se pinta de verde (punto Partida)
                else if (matrix1[i, j].type == 3)
                {
                    //matrix[i, j].GetComponent<SpriteRenderer>().sprite = floorMatrix[i, j];
                    matrix[i, j].GetComponent<SpriteRenderer>().color = Color.blue;
                }
                //Si es 4 se pinta de rojo (obstaculo)
                else if (matrix1[i, j].type == 4)
                {
                    //matrix[i, j].GetComponent<SpriteRenderer>().sprite = floorMatrix[i, j];
                    matrix[i, j].GetComponent<SpriteRenderer>().color = Color.red;
                }
                //Si es 5 se pinta de cyan (personaje que muevo obstaculo)
                else if (matrix1[i, j].type == 5)
                {
                    //matrix[i, j].GetComponent<SpriteRenderer>().sprite = floorMatrix[i, j];
                    matrix[i, j].GetComponent<SpriteRenderer>().color = Color.yellow;
                }
                //Si es 9 se pinta de azul (Puntp Llegada)
                else if (matrix1[i, j].type == 9)
                {
                    //matrix[i, j].GetComponent<SpriteRenderer>().sprite = floorMatrix[i, j];
                    matrix[i, j].GetComponent<SpriteRenderer>().color = Color.green;
                }
            }
        }
        numCharacters = Random.Range(2, 4);
        App.generalController.charactersController.CreateCharacters(theme, numCharacters);
        App.generalController.charactersController.SelectCharactersLevel(allCharacters);
    }
  
    //Metodo para contar los pasos que dio el jugador en la partida
    public int countSteps()
    {
        //Ciclo que recorrer toda la matriz para obtener los pasos que dio el jugador sobre cada una de las casillas
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                //Sumar los pasos de cada casilla en una sola variable
                numberSteps += matrix[i, j].GetComponent<Block>().getNumVisited();
            }
        }
        //Eliminar los pasos que da el personaje sobre la casilla donde es ubicado al inicio de la partida
        if(numCharacters == 2)
        {
            print("DOS PERSONAJEES: " + numberSteps);
            numberSteps = numberSteps - 3;
        }
        else
        {
            print("TRES PERSONAJEES: " + numberSteps);
            numberSteps = numberSteps - 4;
        }
        print("TOTAL: " + numberSteps);
        return numberSteps;
    }
   
    ////////////////////////////////////////////////////////////////////CAMILA//////////////////////////////////////////////////////////////
    public void PuntoFinal()
    {
        int totalSteps = countSteps();
        
        int totalStars = Coints(totalSteps);

        PointsLevel(totalStars);
    }
    //Metodos para calcular los puntajes y etc

    public void IncreaseTickets ()
    {
        App.generalModel.roadGameModel.IncreaseTickets();
    }

    public void PointsLevel (int totalStars)
    {
        int pointsPerStar = 10;
        int pointsLevel = totalStars*pointsPerStar;

        App.generalModel.roadGameModel.SetPoints( App.generalModel.roadGameModel.GetPoints()+pointsLevel);

        //App.generalView.roadGameView.ActivateWinCanvas(totalStars);
        //App.generalView.gameOptionsView.ShowWinCanvas(totalStars);
        print("Tiempo total "+App.generalModel.roadGameModel.GetTime());
    }

    public int Coints(int totalSteps)
    {
        int totalStars = 0;
        //Si los pasos realizados por el usuario son menor o igual a los pasos del algoritmo tiene 3 estrellas
        //Si los pasos realizados por el usuario son mayores a los pasos del algoritmo por maximo 3 pasos son 2 estrellas
        //Si son mayores por mas de 3 pasos extras tiene una estrella
        if(totalSteps <= numSteps)
        {
            totalStars = 3;
        }
        else if(totalSteps > numSteps && totalSteps <= numSteps+3)
        {
            totalStars = 2;
        }
        else
        {
            totalStars = 1;
        }
        return totalStars;
    }
   
    // Metodo encargado de Pintar la ruta de solucion
    public void DrawSolution()
    {
        LocateSolucion();
        App.generalModel.roadGameModel.DecraseTickets();
        App.generalView.gameOptionsView.SolutionCanvas.enabled = false;
        
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if(arrayObjects[i,j].type == 2)
                {
                    matrix[i, j].GetComponent<Block>().SetId(arrayObjects[i,j].type);

                    matrix[i, j].GetComponent<SpriteRenderer>().color = Color.grey;
                }
            }
        }
    }
    
    public GameObject[,] GetMatrix ()
    {
        return matrix;
    }

    public Objects[,] ReturnArray ()
    {
        return arrayObjects;
    }


    //---------------------------------------------------METODOS VIEJOS----------------------------------------------------------------

    
    // Metodo Backtracking encargado de buscar la solucion mas corta para llegar de un punto a otro
    /*void Backtracking (Objects [,] map, int row, int col, int nSteps)
    {
        numLlamadas++;
        // Caso base
        if(map[row,col].type == 4)
        {
            // Se pone la posicion en 2 ya que este numero indica que es la ruta
            map[row,col].type = 2;

            if(numSteps == 0 || nSteps < numSteps)
            {
                arraySolution = Clone(map);
                numSteps = nSteps;
            }
        }
        else if((numSteps == 0 || nSteps < numSteps) && map[row,col].type != 1 && map[row,col].type != 2)
        {
            map[row,col].type = 2;

            if(row > 0) 
                Backtracking(Clone(map), row - 1, col, nSteps + 1);
            if(row < arrayRow-1) 
                Backtracking(Clone(map), row + 1, col, nSteps + 1);
            if(col > 0) 
                Backtracking(Clone(map), row, col - 1, nSteps + 1);
            if(col < arrayCol-1) 
                Backtracking(Clone(map), row, col + 1, nSteps + 1);
        }
    }*/   
    /*public void LocateObstacle()
    {
        objectPoint = GeneratePoints(5, 4, arrivalPoint, 1);

        if(objectPoint == null)
        {
            print("Null Ubicar Obstaculos");
            CreateLevel();
        }
        else 
        {
            if(CheckDistancePoints(objectPoint[0],objectPoint[1],arrivalPoint[0],arrivalPoint[1]))
            {
                print("CUmple con la distancia");
                arrayObjects[objectPoint[0],objectPoint[1]].type = objectPoint[2];
            }
            else 
            {
                print("No cumple con los requisitos, no se puede ubicar el obstaculo");
                CreateLevel();
            }
            
        }
    
    }*/
    /*public void CreatePointsLevel(int type, int totalSteps, int movimiento)
    {
        int [] finalPoint = GeneratePoints(totalSteps, type, obstaclePoint, movimiento);

        if(finalPoint == null)
        {
            print("Null Crear Coordinates personajes");
            //LevelData();
            //CreateLevel();
        }
        else
        {        
            arrayObjects[finalPoint[0],finalPoint[1]].type = finalPoint[2];
        }   
    }

    int TipoMovimiento (int movimiento)
    {
        int type = 0;

        // se puede mover a cualquier direccion
        if(movimiento == 1) type = RamdonNumber(1,5);

        // Se muevo solo arriba y abajo
        else if(movimiento == 2) type = RamdonNumber(1,3);

        // Se mueve derecha e izquierda
        else type = RamdonNumber(3,5);

        //print("Ramdom movimiento "+ type);
        return type;
    }
    
    
    //Metodo encargado de ubicar el punto de partida y el punto final en la matriz
    /*int[] GeneratePoints()
    {
        // Variables para almacenar la posicion en la que se va a ubicar el punto de partida
        int posStarX = RamdonNumber(0, arrayRow);
        int posStarY = RamdonNumber(0, arrayCol);

        // Se establece en la matriz el punto de partida y se cambia el valor en type
        arrayObjects[posStarX,posStarY].type = 3;
        
        // Arreglo encargado de almacenar la posicion del punto de partida
        int [] pos = new int[2];
        pos[0]=posStarX;
        pos[1]=posStarY;

        // Variable encargada de verificar si se ubico un punto final (llegada) correctamente
        bool approvedFinalPoint = false;

        // Variables encargadas de almacenar la posicion donde se ubica el punto final
        int posFinalX = 0;
        int posFinalY = 0;

        // Condicion que termina cuando se a encontrado un punto final adecuado
        while(approvedFinalPoint == false)
        {
            posFinalX = RamdonNumber(0, arrayRow);
            posFinalY = RamdonNumber(0, arrayCol);

            approvedFinalPoint = CheckDistancePoints(posStarX,posStarY,posFinalX,posFinalY);
        }

        arrayObjects[posFinalX,posFinalY].type = 9;
        return pos;
    }*/
    // Metodo encargado de verificar que el punto final no quede muy cerca del punto de inicio
    /*
    bool CheckDistancePoints(int startX, int startY, int finalX, int finalY)
    {
        bool approved = false;

        if((finalX > startX+4 || finalX < startX-4) || (finalY > startY+3 || finalY < startY-3))
        {
            approved = true;
        }
        return approved;
    }*/
   
    /* Metodo que maneja la cantidad de figuras que se van a ubicar en la
    * matriz, la posicion en la cual se va a ubicar y el tipo de figura que
    * se debe poner
    */
    /*void FiguresQuantity (int quantity, int figureTypeMin, int figureTypeMax)
    {
        // Variable para verificar que la figura a sido ubicada
        bool approved = false;
        // Variable para manejar la cantidad de figuras
        int i = quantity;
        
        while(i > 0)
        {
            // Variable que almacena la posicion en X generada aleatoriamente
            int x = RamdonNumber(0, arrayRow);
            // Variable que almacena la posicion en Y generada aleatoriamente
            int y = RamdonNumber(0, arrayCol);
            // Variable que almacena la figura que se debe ubicar generada aleatoriamente
            int figureType = RamdonNumber(figureTypeMin, figureTypeMax);
            approved = Figures(x, y, figureType);
            // Verifica si la figura se ubico correctamente y disminuye uno a la cantidad
            if(approved) i--;
        }
    }*/

    /* Metodo que verifica si se puede ubicar un objeto en una posicion especifica y
    * verifica que no se salga de los Coordinates de la matriz
    */
    /*bool CheckLocation(int row, int col)
    {
        // Se verifica que la posicion escogida no se salga de los limites de la matriz
        if((row < arrayRow && col < arrayCol) && (row >= 0 && col >= 0))
        {
            // Verifica que la posicion a la que se desea mover este en 0 ya que este representa el piso
            if(arrayObjects[row, col].type == 0)
            {
                return true;
            }
        }
        return false;
    }*/
}
