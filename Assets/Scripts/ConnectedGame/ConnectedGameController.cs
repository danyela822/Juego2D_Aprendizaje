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
    //static int totalPoints = 0;
    //int totalSteps = 5;

    //Puntos con cada color 
    static int totalStepsLevel1;

    static int totalStepsLevel2;

    static int totalStepsLevel3;

    static int level = 1;

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

    static Objects[] totalStartPoints = new Objects[3];
    

    /*void Awake()
    {
        print("Awake");
        if(connectedGameController == null)
        {
            connectedGameController = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (connectedGameController != this)
        {
            Destroy(gameObject);
        }
    }*/
    
    public int ReturnLevel()
    {
        return level;
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
        print("Create level "+level);
        GenerateArray();
        GenerateArrivalPoint();
        CreatePoints();
        LocateStartPoints();
        ReiniciarConteoMovimiento();
       
    }

    void ReiniciarConteoMovimiento()
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
        arrayObjects[posStarX,posStarY].type = 9;

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
                totalStepsLevel1 = RamdonNumber(5,8);
                print("Total pasos amarillo: "+totalStepsLevel1);
                CreatePointsLevel(1, totalStepsLevel1, 0);
                break;
            case 2: 
                totalStepsLevel1 = RamdonNumber(5,7);
                print("Total pasos amarillo: "+totalStepsLevel1);
                
                CreatePointsLevel(1, totalStepsLevel1, 0);

                totalStepsLevel2 = RamdonNumber(3,6);
                print("Total pasos azul: "+totalStepsLevel2);
                
                CreatePointsLevel(2, totalStepsLevel1, 1);
                break;
            case 3: 
                totalStepsLevel1 = RamdonNumber(5,7);
                print("Total pasos amarillo: "+totalStepsLevel1);

                CreatePointsLevel(1, totalStepsLevel1, 0);

                totalStepsLevel2 = RamdonNumber(3,6);
                print("Total pasos azul: "+totalStepsLevel2);

                CreatePointsLevel(2, totalStepsLevel1, 1);

                totalStepsLevel3 = RamdonNumber(3,6);
                print("Total pasos rojo: "+totalStepsLevel3);
                
                CreatePointsLevel(3, totalStepsLevel1, 2);
                break;
        }
    }

    public void CreatePointsLevel(int type, int totalSteps, int index)
    {
        int [] finalPoint = GenerateSolution(totalSteps, type);

        if(finalPoint == null)
        {
            print("Null");
            CreateLevel();
        }
        else
        {        
            //if(CheckDistancePoints(finalPoint[0], finalPoint[1], arrivalPoint[0], arrivalPoint[1]))
            //{
            arrayObjects[finalPoint[0],finalPoint[1]].type = finalPoint[2];

            totalStartPoints[index] = new Objects(finalPoint[0], finalPoint[1], finalPoint[2]);
            /*}
            else
            {
                print("no cumple con el requisito ");
                CreateLevel();
            }*/
        }
        
           
    }

    // Metodo encargado de verificar que el punto final no quede muy cerca del punto de inicio
    bool CheckDistancePoints(int startX, int startY, int finalX, int finalY)
    {
        bool approved = false;

        if((finalX > startX+3 || finalX < startX-3) || (finalY > startY+2 || finalY < startY-2))
        {
            approved = true;
        }
        print("Hello "+approved);
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
            if(stop == 15)
            {
                centinela = false;
            }
            
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
            stop ++;
            
            print("cantidad mov "+ cantMove+" stop "+stop);
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

    public void ReturnMoves(int type)
    {
        print("tipo para borrar "+type);
        for (int i = 0; i < arrayRow; i++)
        {
            for (int j = 0; j < arrayCol; j++)
            {
                if(type == arrayObjects[i,j].type)
                {
                    arrayObjects[i,j].type = 0;
                    PaintMatrix(i,j,0);
                }
            }
        }
        PaintStartPoints(type);
    }

    void PaintStartPoints(int type)
    {
        if(type == 1)
        {   
            PaintMatrix(totalStartPoints[0].x,totalStartPoints[0].y,type);
            startPoint1.x = totalStartPoints[0].x;
            startPoint1.y = totalStartPoints[0].y;
            totalMove1 = 0;
        }
        else if(type == 2)
        {
            PaintMatrix(totalStartPoints[1].x,totalStartPoints[1].y,type);
            startPoint2.x = totalStartPoints[1].x;
            startPoint2.y = totalStartPoints[1].y;
            totalMove2 = 0;
        }
        else
        {
            PaintMatrix(totalStartPoints[2].x,totalStartPoints[2].y,type);
            startPoint3.x = totalStartPoints[2].x;
            startPoint3.y = totalStartPoints[2].y;
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
    
    void WinLevel()
    {
        App.generalView.gameOptionsView.WinCanvas.enabled = true;
        level++;
    }

    void CheckTotalMoves ()
    {
        if(level == 1)
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
        if(totalMove1 == totalStepsLevel1)
        {
            print("Cumplio los pasos con el color amarillo");
            WinLevel();
        }
    }
    
    void CheckMovesLevel2()
    {
        if(totalMove1 == totalStepsLevel1 && totalMove2 == totalStepsLevel2)
        {
            print("Cumplio los pasos con el color amarillo y azul");
            WinLevel();
        }
    }

    void CheckMovesLevel3()
    {
        if(totalMove1 == totalStepsLevel1 && totalMove2 == totalStepsLevel2 && totalMove3 == totalStepsLevel3)
        {
            print("Cumplio los pasos con el color amarillo y azul");
            WinLevel();
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
