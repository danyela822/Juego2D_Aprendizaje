using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceController : MonoBehaviour
{
    //creamos un singleton
    public static SequenceController sharedInstance;
    //lista que tiene las imagenes que van a tomar los objetos
    public List <Sprite> prefabs = new List<Sprite>();
    //lista auxiliar la cual es utilizada para verificar que no se
    //repitan los objetos en la secuencia principal
    private List <int> listNumber = new List<int>();
    //lista auxiliar la cual es utilizada para verificar que no se
    //repitan los objetos en la secuencia de respuesta
    private List <int> possibleNumber;
    //lista auxliar donde se almacenas los sprites que seran utilizados
    //para las repsuestas
    private List <Sprite> spritesList = new List<Sprite>();
    //lista de las posibles soulciones a la secuencia
    private List<GameObject> matches;
    //matriz de imagenes
    private GameObject [,] images;
    //tamaño de matriz de las imagenes de secuencia
    private int x1, y1;
    //variable que se utiliza como boceto para la creacion de la matriz
    public GameObject currentImage;
    //me permite saber cual se selecciono
    private Sequence sequence;
    //objeto en el cual se almacena la solucion correcta
    public GameObject correctAnswer;
    private Vector2 offset ;

 

    // Start is called before the first frame update
    void Start()
    {
        if (sharedInstance  == null)
        {
            sharedInstance = this;
        }   
        else{
            Destroy(gameObject);
        }
        //vector que me permite obtener las dimensiones para colocar cada una
        //de las imagenes de la secuencia
        offset = currentImage.GetComponent<BoxCollider2D>().size;

        CreateMatrix(offset);;
    }

    //metodo que crea y pinta la secuencia principal
    private void CreateMatrix (Vector2 offset){

        possibleNumber = new List<int>();

        x1 = 2;
        y1 = 3;

        images = new GameObject[x1, y1];

        //float startX = this.transform.position.x;
        float startX = -1.71f;

        //float startY = this.transform.position.y;
        float startY = 2.02f;

        string auxName;

        GameObject newImage;

        for(int i = 0; i < x1; i++)
        {
            for (int j = 0; j < y1; j++)
            {
                if(i == x1 - 1 && j == y1 - 1)
                {
                    newImage = Instantiate(currentImage,
                    new Vector3(startX + ((offset.x + 0.2f) * j), startY
                    + (offset.y * -i - 1.1f), 0), currentImage.transform.rotation);

                }
                else if(i == 1 && j == 1)
                {
                    Sprite spriteAux = images[0, 0].GetComponent<SpriteRenderer>().sprite;

                    newImage = Instantiate(currentImage,
                    new Vector3(startX + ((offset.x + 0.2f) * j), startY
                    + (offset.y * -i - 1.1f), 0), currentImage.transform.rotation);

                    newImage.GetComponent<SpriteRenderer>().sprite = spriteAux;

                    auxName = newImage.GetComponent<SpriteRenderer>().sprite.name;
                    newImage.GetComponent<Sequence>().id = auxName;

                    newImage.GetComponent<Sequence>().type = false;
                    images[i, j] = newImage;

                }
                else
                {
                    if(i == 0 )
                    {
                        newImage = Instantiate(currentImage,
                        new Vector3(startX + ((offset.x + 0.2f) * j), startY
                        + (offset.y * -i ), 0), currentImage.transform.rotation);

                    }
                    else
                    {
                        newImage = Instantiate(currentImage,
                        new Vector3(startX + ((offset.x + 0.2f) * j), startY
                        + (offset.y * -i - 1.1f), 0), currentImage.transform.rotation);

                    }

                    int num = CheckNumbers(1);

                    if(i == 0 && j == 1){
                        possibleNumber.Add(num);
                    }

                    Sprite sprite = prefabs[num];
                    spritesList.Add(sprite);
                    newImage.GetComponent<SpriteRenderer>().sprite = sprite;

                    auxName = newImage.GetComponent<SpriteRenderer>().sprite.name;
                    newImage.GetComponent<Sequence>().id = auxName;

                    newImage.GetComponent<Sequence>().type = false;
                    images[i, j] = newImage;
                }
                
            }
        }

        correctAnswer = images[0, 1];
        CreateMatches(offset);

    }

    //metodo que crea los objetos en donde se encuentra la posible solucion
    private void CreateMatches (Vector2 offset)
    {        
        y1 = 3;
        matches = new List<GameObject>();
        GameObject newImage;

        float startX = -1.77f;

        float startY = -3.96f;

        for (int i = 0; i < y1; i++)
        {
            newImage = Instantiate(currentImage,
            new Vector3(startX + ((offset.x + 0.2f) * i), startY
            + (offset.y * -0.05f ), 0), currentImage.transform.rotation);

            if (i != 0){

                CheckNumbers(2);
            }
            matches.Add(newImage);
            
        }

        PaintMatrix();
    }

    //metodo que permite desordenar la lista dinde se encuentra la solucion
    private List<int> RandomList(){

        List<int> list = possibleNumber;
        List<int> aux = new List<int>();

        while(list.Count > 0){

            int i = Random.Range(0 , list.Count);
            aux.Add(list[i]);
            list.RemoveAt(i);
        }

        return aux;

    }

    //metoso que pinta los objetos donde se encuentra la posible solucion
    public void PaintMatrix(){

        string auxId;

        List<int> aux = RandomList();

        for (int i = 0; i < matches.Count; i++)
        {
            Sprite sprite = prefabs[aux[i]];
            matches[i].GetComponent<SpriteRenderer>().sprite = sprite;
            auxId = matches[i].GetComponent<SpriteRenderer>().sprite.name;
            matches[i].GetComponent<Sequence>().id = auxId;
            matches[i].GetComponent<Sequence>().type = true;
        }

    }

    //metodo que verifica que en ninguna de las dos secuencias se repita un objeto
    public int CheckNumbers(int option)
    { 
        int num = 0;
        bool aux = false;
        //verifica la matriz principal
        if(option == 1){

            while (aux == false)
            {   
                num = Random.Range(0, prefabs.Count);
                if (listNumber.Contains(num)){
                    aux = false;
                }
                else{   
                    aux = true;
                    listNumber.Add(num);
                }    
            }
        }
        //verifica la lista donde esta la solucion
        else
        {
            while (aux == false)
            {
                num = Random.Range(0, spritesList.Count);

                if (possibleNumber.Contains(num))
                {
                    aux = false;
                }
                else
                {   
                    aux = true;
                    possibleNumber.Add(num);
                }
            }
        }
        return num;      
    }
}


