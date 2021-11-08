using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectedGameController : Reference
{
    public static ConnectedGameController connectedGameController;

    // Se crea una matriz de objetos
    static Objects [,] arrayObjects;
    // Se crea una matriz que contendra la solucion
    static Objects [,] arraySolution;
    // Indica la cantidad de filas de la matriz
    int arrayRow = 8;
    // Cantidad de columnas de la matriz
    int arrayCol = 6;
    // Matriz de GameObject
    public  GameObject[,] matrix;
    //Variable que almacena las coordenadas del punto de llegada 
    int [] arrivalPoint = new int [2];
    static int totalPoints = 0;
    int totalSteps = 5;

    //Variables que almacenan la ultima posicion en la que quedo ubicado el objeto, con el fin de seguir desde ahi los movimientos futuros
    static Objects startPoint;
    static Objects startPoint1;
    static Objects startPoint2;
    static Objects startPoint3;

    //Variables en las que se almacena la cantidad de movimientos realizados depediendo el color
    static int totalMove1 = 0;
    static int totalMove2 = 0;
    static int totalMove3 = 0;

    //Variables empleadas para verificar si llego al punto indicado
    static bool finish1 = false;
    static bool finish2 = false;
    static bool finish3 = false;
    

    void Awake()
    {
        if(connectedGameController == null)
        {
            connectedGameController = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (connectedGameController != this)
        {
            Destroy(gameObject);
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
                matrix[i, j].GetComponent<Block>().SetId(matrix1[i,j].type);

                //Asignar la matriz al objeto de GameZone
                matrix[i, j].transform.parent = gameZone.transform;

                index += 1;

                PaintMatrix(i,j,matrix[i, j].GetComponent<Block>().GetID());
            }
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
        GenerateArray();
        GenerateArrivalPoint();
        totalPoints = RamdonNumber(2,4);
        CreatePoints(totalPoints);
        LocateStartPoints();
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
        arrayObjects[posStarX,posStarY].type = 9;

        arrivalPoint[0] = posStarX;
        arrivalPoint[1] = posStarY;

        //Se clona la matriz con el fin de  tener una donde se genera automaticamente una solucion en caso de no ser resuelto
        arraySolution = Clone(arrayObjects);
    }

    //Metodo encargado de generar los puntos (colores o personajes) dependiendo la cantidad indicada (2 o 3)
    public void CreatePoints(int cant)
    {
        for (int i = 1; i < cant+1; i++)
        {
            int [] finalPoint = GenerateSolution(totalSteps,i);
            arrayObjects[finalPoint[0],finalPoint[1]].type = finalPoint[2];
        }
    }

    //Metodo encargado de generar una solucion por cada punto(color o personaje) con la cantidad de movimientos especificados
    public int [] GenerateSolution(int cantMove, int type)
    {
        int posX = arrivalPoint[0];
        int posY = arrivalPoint[1];

        while(cantMove >= 0)
        {
            int option = RamdonNumber(1,5);
            if(option == 1)
            {
                if(CheckPosition(arraySolution,posX-1,posY))
                {
                    posX--;
                    arraySolution[posX,posY].type = type;
                    cantMove --;
                } 
            }
            else if(option == 2)
            {
                if(CheckPosition(arraySolution,posX+1,posY))
                {
                    posX++;
                    arraySolution[posX,posY].type = type;
                    cantMove --;
                } 
            }
            else if(option == 3)
            {
                if(CheckPosition(arraySolution,posX,posY-1))
                {
                    posY--;
                    arraySolution[posX,posY].type = type;
                    cantMove --;
                } 
            }
            else{
                if(CheckPosition(arraySolution,posX,posY+1))
                {
                    posY++;
                    arraySolution[posX,posY].type = type;
                    cantMove --;
                } 
            }
        }

        int [] finalPosition = new int [] {posX,posY,type};

        return finalPosition;
    }

    //Metodo que encuentra los puntos iniciales y inicializa las variables correspondientes de acuerdo al tipo
    public void LocateStartPoints ()
    {
        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                if(arrayObjects[i,j].type == 1)
                {
                    startPoint1= new Objects(i,j,1);
                }
                else if(arrayObjects[i,j].type == 2)
                {
                    startPoint2= new Objects(i,j,2);
                }
                else if(arrayObjects[i,j].type == 3)
                {
                    startPoint3= new Objects(i,j,3);
                }
            }
        }
    }

    //Metodo empleado para verificar que objeto eligio mover el jugador
    public void SelectedObject(int type)
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
        if (direction == "up")
        {
            if(CheckPosition(arrayObjects, startPoint.x-1, startPoint.y))
            {
                arrayObjects[startPoint.x-1,startPoint.y].type = startPoint.type;
                PaintMatrix(startPoint.x-1,startPoint.y, startPoint.type);
                CountMoves(startPoint.type);
                startPoint.x = startPoint.x-1;
            }
            else if(CheckArrival(arrayObjects, startPoint.x-1, startPoint.y))
            {
                CheckArrivalOption(startPoint.type);
            }
            /*else if(CheckPositionVolver(arrayObjects, puntoInicial.x-1, puntoInicial.y, puntoInicial.type))
            {
                arrayObjects[puntoInicial.x,puntoInicial.y].type = 0;
                PintarColores(puntoInicial.x,puntoInicial.y, 0);
                puntoInicial.x = puntoInicial.x-1;
            }*/
        }
        else if (direction == "down")
        {
            if(CheckPosition(arrayObjects, startPoint.x+1, startPoint.y))
            {
                arrayObjects[startPoint.x+1,startPoint.y].type = startPoint.type;
                PaintMatrix(startPoint.x+1,startPoint.y, startPoint.type);
                CountMoves(startPoint.type);
                startPoint.x = startPoint.x+1;
            }
            else if(CheckArrival(arrayObjects, startPoint.x+1, startPoint.y))
            {
                CheckArrivalOption(startPoint.type);
            }
            /*else if(CheckPositionVolver(arrayObjects, puntoInicial.x+1, puntoInicial.y, puntoInicial.type))
            {   
                arrayObjects[puntoInicial.x,puntoInicial.y].type = 0;
                PintarColores(puntoInicial.x,puntoInicial.y, 0);
                puntoInicial.x = puntoInicial.x+1;
            }*/

        }
        else if (direction == "right")
        {
            if(CheckPosition(arrayObjects, startPoint.x, startPoint.y+1))
            {
                arrayObjects[startPoint.x,startPoint.y+1].type = startPoint.type;
                PaintMatrix(startPoint.x,startPoint.y+1, startPoint.type);
                CountMoves(startPoint.type);
                startPoint.y = startPoint.y+1;
            }
            else if(CheckArrival(arrayObjects, startPoint.x, startPoint.y+1))
            {
                CheckArrivalOption(startPoint.type);
            }
            /*else if(CheckPositionVolver(arrayObjects, puntoInicial.x, puntoInicial.y+1, puntoInicial.type))
            {
                arrayObjects[puntoInicial.x,puntoInicial.y].type = 0;
                PintarColores(puntoInicial.x,puntoInicial.y, 0);
                puntoInicial.y = puntoInicial.y+1;
            }*/
        }
        else if (direction == "left")
        {
            if(CheckPosition(arrayObjects, startPoint.x, startPoint.y-1))
            {
                arrayObjects[startPoint.x,startPoint.y-1].type = startPoint.type;
                PaintMatrix(startPoint.x,startPoint.y-1, startPoint.type);
                CountMoves(startPoint.type);
                startPoint.y = startPoint.y-1;
            }
            else if(CheckArrival(arrayObjects, startPoint.x, startPoint.y-1))
            {
                CheckArrivalOption(startPoint.type);
            }
            /*else if(CheckPositionVolver(arrayObjects, puntoInicial.x, puntoInicial.y-1, puntoInicial.type))
            {
                arrayObjects[puntoInicial.x,puntoInicial.y].type = 0;
                PintarColores(puntoInicial.x,puntoInicial.y, 0);
                puntoInicial.y = puntoInicial.y+1;
            }*/
        }
        print(totalMove1+" - "+totalMove2);
        CheckEndGame();
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
        if(totalPoints == 2) 
        {
            TwoTotalPoints();
        }
        else
        {
            ThreeTotalPoints();
        }
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
            if(array[posX,posY].type == 0) return true;
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
            if(array[posX,posY].type == 9) return true;
        }
        return false;
    }
    
    void CheckTotalMoves ()
    {
        if(totalMove1 == totalSteps)
        {
            print("Cumplio los pasos con el color amarillo");
        }
        if(totalMove2 == totalSteps)
        {
            print("Cumplio los pasos con el color azul");
        }
        if(totalMove3 == totalSteps)
        {
            print("Cumplio los pasos con el color rojo");
        }
    }
    bool CheckPositionVolver(Objects [,] array,int posX, int posY, int type)
    {
        // Verifica que no se vaya salir de los limites 
        if(posX >= 0 && posY >= 0 && posX < arrayRow && posY < arrayCol)
        {
            // Verifica si la posicion indicada esta libre
            if(array[posX,posY].type == type) return true;
        }
        return false;
    }
    public void MostrarSolucion()
    {
        arrayObjects = Clone(arraySolution);
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

    /* Metodo que genera un numero aleatorio dependiendo el rango que se le indique
    * min -> numero minimo (incluido)
    * max -> numero maximo (no incluido)
    */
    public int RamdonNumber (int min, int max)
    {
        int number = UnityEngine.Random.Range (min, max);
        return number;
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
