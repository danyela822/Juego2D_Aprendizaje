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
        if(name_button == "Button Pause")
        {
            App.generalView.gameView.PauseCanvas.enabled = true;
        }
        //El boton help abre el tutorial del juego
        if (name_button == "Button Help")
        {
            App.generalView.gameView.TutorialCanvas.enabled = true;
        }
        //El boton solution abre un canvas para ir a los minijuegos
        if (name_button == "Button Solution")
        {
            App.generalView.gameView.SolutionCanvas.enabled = true;
        }

        ////////////////////////////////// Botones del menu de pausa, tutorial y solucion ////////////////////////////////

        //El boton back regresa a la partida
        if (name_button == "Button Back")
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
        if (name_button == "Button Levels")
        {
            //Falta la escena de los niveles
            //SceneManager.LoadScene("");
        }

        if (name_button == "Button Pay")
        {
            //Canjea codigo y se muestra la solucion
        }

        if (name_button == "Button MiniGames")
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

        GameObject[,] matrix = new GameObject[matrix1.GetLength(0), matrix1.GetLength(1)];

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
                //Si es 2 se pinta de rojo
                else if (matrix[i, j].GetComponent<Block>().GetID() == 2)
                {
                    matrix[i, j].GetComponent<SpriteRenderer>().color = Color.green;
                }
                //Si es 3 se pinta de verde
                else if (matrix[i, j].GetComponent<Block>().GetID() == 3)
                {
                    matrix[i, j].GetComponent<SpriteRenderer>().color = Color.green;
                }
            }
        }
    }


    ////////////////////////////////////////////////////////////////////CAMILA//////////////////////////////////////////////////////////////

    // Se crea una matriz de objetos
    Objects [,] arrayObjects;
    // Indica la cantidad de filas de la matriz
    int arrayRow = 8;
    // Cantidad de columnas de la matriz
    int arrayCol = 6;
    // Lista en la que se va a almacenar la ruta de solicion del nivel
    List<Tuple<int, int>> route = new List<Tuple<int, int>>();
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Esta en el Start de GameController");
       // CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Objects[,] CreateLevel()
    {
        GenerateArray();
        ArrivalPoint();
        LocateSolution();
        StartPoint();
        ShowArray();

        return arrayObjects;
    }

    // Metodo que ubica a el personaje en el punto de partida
    void StartPoint()
    {
        int x = route[route.Count-1].Item1;
        int y = route[route.Count-1].Item2;
        arrayObjects[x,y].type = 3;
    }
    /* Metodo que se encarga de crear la matriz y llamar 
    * al metodo que la va a llenar
    */
    void GenerateArray ()
    {
        arrayObjects = new Objects [arrayRow, arrayCol];

        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                // Llamado al metodo que llena la matriz
                FillArray(i,j);
            }
        }
        // Llamado al metodo que genera aleatoriamente un punto de llegada
        //ArrivalPoint();
    }

    /* Metodo que llena la matriz de forma aleatoria indicando
    * si es Piso (0) o Limite (1)
    */
    void FillArray (int i, int j)
    {
        // Se verifica si la posicion que entra por parametros corresponde a un borde de la matriz
        if(i == 0 || j == 0 || i == arrayRow-1 || j == arrayCol-1)
        {
            // Como corresponde a un limite se llena con el numero 1
            arrayObjects [i, j] = new Objects (i, j, 1);
        }
        else
        {
            // Se genera un numero aleatorio entre 0 y 1 para llenar la matriz
            arrayObjects [i, j] = new Objects (i, j, RamdonNumber(0, 2));
        }
    }

    /* Metodo encargado de ubicar un punto de llegada en la matriz
    */
    void ArrivalPoint ()
    {
        // Variable que sirve para identificar si ya se escogio un punto de llegada
        bool approved = false;
        // Variable en la cual se va a almacenar la fila
        int row = 0;
        // Variable donde se almacena la columna
        int col = 0;

        while(approved == false)
        {
            // Se genera un numero aleatorio entre 0 y el numero total de filas de la matriz
            row = RamdonNumber(0, arrayRow);
            // Se genera un numero aleatorio entre 0 y el numero total de columnas de la matriz
            col = RamdonNumber(0, arrayCol);

            // Se verifica si la posicion escogida se puede utilizar como punto de llegada
            approved = CheckLocation(row,col);
            Debug.Log("Posicion LLegada ( " + row + "," + col + " ) = "+ arrayObjects[row, col].type + " Aprobado: "+approved);
        }
        // Se ubica el punto de llegada en la matriz, este se representa con el numero 2
        arrayObjects[row, col].type = 2;
        ShowArray();
        // Una vez ubicado el punto de llegada se procede a generar una solucion
        // en esta se establece el punto de partida del personaje y el recorrido que debe hacer
        GenerateSolution(10, row, col);
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

    /* Metodo encargado de generar una solucion al nivel, ubica el personaje en un 
    * punto de partida y crea una ruta hasta el punto de llegada
    * minMove -> cantidad minima de movimientos desde el punto de llegada
    * row y col-> fila y columna en la que esta ubicado el punto de llegada
    */
    void GenerateSolution (int minMove, int row, int col)
    {
        // Agrega a la lista route en punto de llegada
        route.Add(Tuple.Create(row, col));
        // Almacena la cantidad de movimientos a realizar
        int cantMove = minMove;
        
        while (cantMove > 0)
        {
            // Verifica si el movimiento se realizo y resta uno a la cantidad que se deben ejecutar
            // manda un numero aleatorio entre 1 y 4 para indicar el movimiento
            if(Move(RamdonNumber(1,5))) cantMove--;
            //Debug.Log("Movimiento minimo = "+ cantMove);
        }
        ShowList();
        //LocateSolution();
        //ShowArray();
    }

    /* Metodo que ubica la lista de solucion generada en la matriz 
    * para mostrar la ruta que debe tomar el personaje para llegar al punto final
    */
    void LocateSolution()
    {
        int routeX;
        int routeY;
        // Recorre todas las posiciones de la lista
        for (int i = 1; i < route.Count; i++)
        {
            // Almacena la posicon en X del elemento en la lista
            routeX = route[i].Item1;
            // Almacena la posicon en Y del elemento en la lista
            routeY = route[i].Item2;
            // Modifica la matriz en las posiciones indicadas
            arrayObjects[routeX,routeY].type = 0;
        }
    }

    /* Este metodo se encarga de generar un movimiento segun la direccion indicada
    * 1 -> Izquierda, 2 -> Derecha, 3 -> Arriba y 4 -> Abajo, tambien agrega 
    * las posiciones nuevas generadas a la lista route encargada de almacenar la ruta de solucion
    */
    bool Move (int dir)
    {
        // Se obtiene la fila y columna en la que esta ubicado el punto de llegada
        int row = route[route.Count-1].Item1;
        int col = route[route.Count-1].Item2;
        // Variable para almacenar y retornar si se realizo el movimiento 
        bool approvedMove = false;
        
        if(dir == 1)
        {
            // Verifica que si se puede hacer el movimiento y mueve 1 a la izquierda (resto 1 a col)
            //Debug.Log("Izquierda");
            if(CheckSolutionRoute(row, col-1)) col-=1;
        }
        else if(dir == 2)
        {
            // Verifica que si se puede hacer el movimiento y mueve 1 a la derecha (sumo 1 a col)
            //Debug.Log("Derecha");
            if(CheckSolutionRoute(row, col+1)) col+=1;
        }
        else if(dir == 3)
        {
            // Verifica que si se puede hacer el movimiento y mueve 1 a hacia arriba (resto 1 a fila)
            //Debug.Log("Arriba");
            if(CheckSolutionRoute(row-1, col)) row-=1;
        }
        else
        {
            // Verifica que si se puede hacer el movimiento y mueve 1 a hacia abajo (sumo 1 a fila)
            //Debug.Log("Abajo");
            if(CheckSolutionRoute(row+1, col)) row+=1;
        }

        // Verifica que la posicion final no haya sido ya utilizada
        if(CheckSolutionRoute(row, col))
        {
            // Como la posicion es correcta procede a agregarla a la lista route
            route.Add(Tuple.Create(row, col));
            // El movimiento fue realizado correctamente entonces retorna true
            approvedMove = true;
        }
        return approvedMove;
    }

    /* Metodo que se encarga de verificar que la posicion a la que se desea mover no
    * se salga de los limites de la matriz y que no haya sido utilizada previamente
    */
    bool CheckSolutionRoute (int posX, int posY)
    {
        // Crea la tupla en la cual se almacena la posicion a la que se desea mover
        Tuple <int, int> move = Tuple.Create(posX, posY);
        
        // Verifica que la posicion este entre los limites
        if(posX > 0 && posY > 0 && posX < arrayRow-1 && posY < arrayCol-1)
        {
            // Verifica que la posicion ya no haya sido utilizada y agregada a la ruta de solucion
            if(!route.Contains(move))
            {
                return true;
            }
        }
        return false;
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

    //Metodo que imprime en consola todos los elementos agregados a la lista route
    void ShowList()
    {
        string list = "";

        for (int i = 0; i < route.Count; i++)
        {
            list = list + "Posicion " + i +" Row " + route[i].Item1 + " Col " + route[i].Item2 + "\n";
        }
        Debug.Log(list);
    }

    // Metodo que imprime en consola la matriz y sus valores correspondientes
    void ShowArray ()
    {
        string imprimir = "\n";
        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                imprimir = imprimir + arrayObjects[i, j].type + " ";
            }
            imprimir = imprimir + "\n";
        }
        Debug.Log(imprimir);
    }
}
