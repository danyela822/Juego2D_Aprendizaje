using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSequence : MonoBehaviour
{
    //creamos un singleton
    public static BoardSequence sharedInstance;

    //lista que tiene las imagenes que van a tomar los objetos
    public List <Sprite> prefabs = new List<Sprite>();

    //tamaño de matriz de las imagenes de secuencia
    private int x1, y1;

    //matriz de imagenes
    private GameObject [,] images;

    private GameObject [,] matches;

    public GameObject currentImage;

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
        Vector2 offset = currentImage.GetComponent<BoxCollider2D>().size;

        CreateMatrix(offset);

        CreateMatches(offset);
    }

    //metodo que me crea la matriz donde se encuentra la secuncia
    private void CreateMatrix (Vector2 offset){

        x1 = 2;
        y1 = 3;

        images = new GameObject[x1, y1];

        //float startX = this.transform.position.x;
        float startX = -1.71f;

        //float startY = this.transform.position.y;
        float startY = 2.02f;

        for(int i = 0; i < x1; i++)
        {
            for (int j = 0; j < y1; j++)
            {
                if(i == 0 )
                {
                    GameObject newImage = Instantiate(currentImage,
                    new Vector3(startX + ((offset.x + 0.2f) * j), startY
                    + (offset.y * -i ), 0), currentImage.transform.rotation);

                    Sprite sprite = prefabs [Random.Range(0, prefabs.Count)];

                    newImage.GetComponent<SpriteRenderer>().sprite = sprite;

                    string auxName = newImage.GetComponent<SpriteRenderer>().sprite.name;

                    //newImage.GetComponent<Sequence>().id = auxName;

                    images[i, j] = newImage;
                }
                else if(i == 1 && j == 1)
                {
                    Sprite spriteAux = images[0, 0].GetComponent<SpriteRenderer>().sprite;

                    GameObject newImage = Instantiate(currentImage,
                    new Vector3(startX + ((offset.x + 0.2f) * j), startY
                    + (offset.y * -i - 1.1f), 0), currentImage.transform.rotation);

                    newImage.GetComponent<SpriteRenderer>().sprite = spriteAux;

                    string auxName = newImage.GetComponent<SpriteRenderer>().sprite.name;

                    //newImage.GetComponent<Sequence>().id = auxName;

                    images[i, j] = newImage;
                }
                else if(i == x1 - 1 && j == y1 - 1)
                {
                    GameObject newImage = Instantiate(currentImage,
                    new Vector3(startX + ((offset.x + 0.2f) * j), startY
                    + (offset.y * -i - 1.1f), 0), currentImage.transform.rotation);

                    string auxName = newImage.GetComponent<SpriteRenderer>().sprite.name;

                    //newImage.GetComponent<Sequence>().id = auxName;

                    images[i, j] = newImage;
                }
                else
                {
                    GameObject newImage = Instantiate(currentImage,
                    new Vector3(startX + ((offset.x + 0.2f) * j), startY
                    + (offset.y * -i - 1.1f), 0), currentImage.transform.rotation);

                    Sprite sprite = prefabs [Random.Range(0, prefabs.Count)];
                    newImage.GetComponent<SpriteRenderer>().sprite = sprite;
                    images[i, j] = newImage;
                }
                
            }
        }


    }

    private void CreateMatches (Vector2 offset)
    {

        x1 = 1;
        y1 = 3;

        matches = new GameObject[x1, y1];

        //float startX = this.transform.position.x;
        float startX = -1.77f;

        //float startY = this.transform.position.y;
        float startY = -3.96f;

         for(int i = 0; i < x1; i++)
        {
            for (int j = 0; j < y1; j++)
            {
      
                    GameObject newImage = Instantiate(currentImage,
                    new Vector3(startX + ((offset.x + 0.2f) * j), startY
                    + (offset.y * -i ), 0), currentImage.transform.rotation);

                    Sprite sprite = prefabs [Random.Range(0, prefabs.Count)];
                    newImage.GetComponent<SpriteRenderer>().sprite = sprite;
                    images[i, j] = newImage;
    
            }

        }
    }

    public void PutId (){

        int id = 0;

        for (int i = 0; i < prefabs.Count; i++)
        {
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


