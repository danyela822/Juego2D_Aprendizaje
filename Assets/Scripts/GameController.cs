using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameController : Reference
{
    /*
     * Metodo determina que accion realizar al oprimir un bot�n
     * en la interfaz de la vista del juego
     */
    public void OnClickButtons(string name_button)
    {
        //El boton pause abre el canvas del menu de pause
        if(name_button == "Pause Button")
        {
            App.generalView.gameView.PauseCanvas.enabled = true;
        }
        //El boton help abre el tutorial del juego
        if (name_button == "Help Button")
        {
            App.generalView.gameView.TutorialCanvas.enabled = true;
        }
        //El boton solution abre un canvas para ir a los minijuegos
        if (name_button == "Solution Button")
        {
            App.generalView.gameView.SolutionCanvas.enabled = true;
        }

        ////////////////////////////////// Botones del menu de pausa, tutorial y solucion ////////////////////////////////

        //El boton back regresa a la partida
        if (name_button == "Back Button")
        {
            if (App.generalView.gameView.PauseCanvas.enabled == true)
            {
                App.generalView.gameView.PauseCanvas.enabled = false;
            }

            if (App.generalView.gameView.SolutionCanvas.enabled == true)
            {
                App.generalView.gameView.SolutionCanvas.enabled = false;
            }

            if (App.generalView.gameView.TutorialCanvas.enabled == true)
            {
                App.generalView.gameView.TutorialCanvas.enabled = false;
            }
        }
        //El boton back regresa al menu de niveles
        if (name_button == "Levels Button")
        {
            //Cargar escena de los niveles
            SceneManager.LoadScene("CategoriesScene");
        }

        if (name_button == "Pay Button")
        {
            //Canjea codigo y se muestra la solucion
        }

        if (name_button == "MiniGames Button")
        {
            //Falta la escena de los minijuegos
            //SceneManager.LoadScene("");
        }
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

        matrix = new GameObject[matrix1.GetLength(0), matrix1.GetLength(1)];

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
                if (matrix[i, j].GetComponent<Block>().GetID() == 0)
                {
                    matrix[i, j].GetComponent<SpriteRenderer>().color = Color.white;
                }
                //Si es 1 se pinta de negro
                else if (matrix[i, j].GetComponent<Block>().GetID() == 1)
                {
                    matrix[i, j].GetComponent<SpriteRenderer>().color = Color.black;
                }
                //Si es 3 se pinta de verde (punto Partida)
                else if (matrix[i, j].GetComponent<Block>().GetID() == 3)
                {
                    matrix[i, j].GetComponent<SpriteRenderer>().color = Color.green;
                }
                //Si es 4 se pinta de azul (Puntp Llegada)
                else if (matrix[i, j].GetComponent<Block>().GetID() == 4)
                {
                    matrix[i, j].GetComponent<SpriteRenderer>().color = Color.blue;
                }
            }
        }
    }

    ////////////////////////////////////////////////////////////////////CAMILA//////////////////////////////////////////////////////////////
    
    //
    public static GameController gameController;

    // Metodo encargado de Pintar la ruta de solucion
    public void DrawSolution()
    {
        App.generalView.gameView.SolutionCanvas.enabled = false;
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
    
    // Se crea una matriz de objetos
    static Objects [,] arrayObjects;
    // Indica la cantidad de filas de la matriz
    int arrayRow = 8;
    // Cantidad de columnas de la matriz
    int arrayCol = 6;
    // Matriz de GameObject
    GameObject[,] matrix;

    // Variables para el backtracking
    static int numSteps; // Numero de pasos para llegar de un punto a otro
    static int numLlamadas; // Numero de llamadas rercursivas
    static Objects [,] arraySolution = null; // Camino a seguir

    void Awake()
    {
        if(gameController == null)
        {
            gameController = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (gameController != this)
        {
            Destroy(gameObject);
        }
    }
    
    public Objects[,] ReturnArray ()
    {
        return arrayObjects;
    }

    public void RamdonLevel()
    {
        int ramdonCategory = RamdonNumber(1,4);
        string category = "";
        
        if(ramdonCategory == 1) category = "Principiante";
        
        else if (ramdonCategory == 2) category = "Medio";
        
        else if(ramdonCategory == 3) category = "Avanzado";

        LevelData(category);
    }
    public void LevelData(string category)
    {
        int figuresQuantity = 0;
        int figureTypeMin = 0;
        int figureTypeMax = 0;
        if(category == "Principiante")
        {
            figuresQuantity = RamdonNumber(3,6);
            figureTypeMin = 1;
            figureTypeMax = 4;
        }
        else if(category == "Medio")
        {
            figuresQuantity = RamdonNumber(3,6);
            figureTypeMin = 3;
            figureTypeMax = 6;
        }
        else if(category == "Avanzado")
        {
            figuresQuantity = RamdonNumber(3,6);
            figureTypeMin = 5;
            figureTypeMax = 8;
        }

        CreateLevel(category, figuresQuantity, figureTypeMin, figureTypeMax);
        
    }

    // Metodo encargado de llamar todos los metodos necesarios para generar un nivel aleatorio
    public void CreateLevel(string category, int figuresQuantity, int figureTypeMin, int figureTypeMax)
    {
        GenerateArray();
        int [] posStart = LocatePoints();
        FiguresQuantity(figuresQuantity, figureTypeMin, figureTypeMax);
        //ShowArray(arrayObjects);
        GenerateSolution(category, posStart[0],posStart[1]);
        //ShowArray(arrayObjects);
        
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

        ShowArray(arrayObjects);
    }

    //Metodo encargado de ubicar el punto de partida y el punto final en la matriz
    int[] LocatePoints()
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

        arrayObjects[posFinalX,posFinalY].type = 4;
        return pos;
    }

    // Metodo encargado de verificar que el punto final no quede muy cerca del punto de inicio
    bool CheckDistancePoints(int startX, int startY, int finalX, int finalY)
    {
        bool approved = false;

        if((finalX > startX+4 || finalX < startX-4) || (finalY > startY+3 || finalY < startY-3))
        {
            approved = true;
        }
        return approved;
    }

    /* Metodo que genera un numero aleatorio dependiendo el rango que se le indique
    * min -> numero minimo (incluido)
    * max -> numero maximo (no incluido)
    */
    int RamdonNumber (int min, int max)
    {
        int number = UnityEngine.Random.Range (min, max);
        return number;
    }

    /* Metodo encargado de verificar que la posicion donde se va a 
    * ubicar la figura no este ya ocupada y que no se salga de los limites
    * de la matriz
    */
    bool CheckFigurePosition(int posX, int posY)
    {
        // Verifica que no se vaya salir de los limites 
        if(posX >= 0 && posY >= 0 && posX < arrayRow && posY < arrayCol)
        {
            // Verifica si la posicion indicada esta libre
            if(arrayObjects[posX,posY].type == 0) return true;
        }
        return false;
    }
    
    /* Metodo que maneja la cantidad de figuras que se van a ubicar en la
    * matriz, la posicion en la cual se va a ubicar y el tipo de figura que
    * se debe poner
    */
    void FiguresQuantity (int quantity, int figureTypeMin, int figureTypeMax)
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
            arrayObjects[x,y].type = 1;
            arrayObjects[x+1,y].type = 1;
            arrayObjects[x+2,y].type = 1;
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
            arrayObjects[x,y].type = 1;
            arrayObjects[x,y+1].type = 1;
            arrayObjects[x,y+2].type = 1;
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
            arrayObjects[x,y].type = 1;
            arrayObjects[x+1,y].type = 1;
            arrayObjects[x+1,y+1].type = 1;
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
            arrayObjects[x,y].type = 1;
            arrayObjects[x+1,y].type = 1;
            arrayObjects[x,y+1].type = 1;
            arrayObjects[x+1,y+1].type = 1;
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
            arrayObjects[x,y].type = 1;
            arrayObjects[x+1,y].type = 1;
            arrayObjects[x+1,y+1].type = 1;
            arrayObjects[x+2,y].type = 1;
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
            arrayObjects[x,y].type = 1;
            arrayObjects[x+1,y].type = 1;
            arrayObjects[x,y+1].type = 1;
            arrayObjects[x,y+2].type = 1;
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
            arrayObjects[x,y].type = 1;
            arrayObjects[x+1,y-1].type = 1;
            arrayObjects[x+1,y].type = 1;
            arrayObjects[x+1,y+1].type = 1;
            arrayObjects[x+2,y].type = 1;
            approved = true;
        }

        return approved; 
    }
    
    /* Metodo encargado de generar la ruta de solucion mas corta
    * recibe la fila y la columna donde se ubico el punto de partida
    */
    void GenerateSolution(string category, int row, int col)
    {
        // Se crea una matriz de Objetos con la cual se empieza el proceso de backtracking
        // Esta se inicializa con una clonacion de la matriz donde ya se ubicaron los obstaculos y los puntos de partida y llegada
        Objects [,] map = Clone(arrayObjects);

        // Se ponen las variables que se utilizaran en el backtraking en cero
        ResetVariables();

        // Se hace un llamado al metodo backtracking encargado de encontrar una solucion
        Backtracking(map, row, col, 0);

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
            LevelData(category);
        }
        
    }
    
    // Metodo Backtracking encargado de buscar la solucion mas corta para llegar de un punto a otro
    void Backtracking (Objects [,] map, int row, int col, int nSteps)
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
    }

    // Metodo encargado de resetear las variables 
    void ResetVariables ()
    {
        numSteps = 0;
        numLlamadas = 0;
        arraySolution = null;
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
    

    /* Metodo que verifica si se puede ubicar un objeto en una posicion especifica y
    * verifica que no se salga de los puntos de la matriz
    */
    bool CheckLocation(int row, int col)
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
}
