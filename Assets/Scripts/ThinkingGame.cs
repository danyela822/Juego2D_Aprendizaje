using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThinkingGame : MonoBehaviour
{

    private List<GameObject> firstObjectRow1 = new List<GameObject>();

    private List<GameObject> firstObjectRow2 = new List<GameObject>();

    private List<GameObject> firstObjectRow3 = new List<GameObject>();

    private List <GameObject> firstObjectRow4 = new List<GameObject>();  

    private List <GameObject> allObject = new List<GameObject>();

    public List <Text> textList = new List<Text>();

    public GameObject objectRow;

    private Vector2 size;

    //lista de los posibles sprites que seran utilizados
    public List <Sprite> images = new List<Sprite>();

    //lista que almacena los id de los objetos y evita
    //que se repitan
    private List <int> idImages = new List<int>();

    //Lista que contiene los valores que aparecen en el juego    
    private List<int> values = new List<int>();

    //lista que contiene los signos que seran utilizados
    //1 "+" 0 "-"
    private List<int> signs = new List<int>();

    private Option option;

    // Start is called before the first frame update
    void Start()
    {
        size = objectRow.GetComponent<BoxCollider2D>().size;

        Debug.Log(size.x);

        CreatListAleatoriaNumbers(1);
        CreatListAleatoriaNumbers(2);
        //PaintMatches();
        CreateFirstRow();
        CreateSecondRow();
        CreateThirdRow();
        CreateFourthRow();
        GenerateSigns();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateFirstRow(){

        //cantidad de columnas de la primera fila
        int numberColumns = 2;

        float x = -2.02f;
        //float x = objectFirstRow.transform.position.x;
        float y = 0.79f;
        //float y = objectFirstRow.transform.position.y;

        GameObject newImage;

        for (int i = 0; i < numberColumns ; i++)
        {
            newImage = Instantiate(objectRow, 
            new Vector3(x + (size.x * (i-(0.18f*i))), y, 0), 
            objectRow.transform.rotation);

            Sprite sprite = images[idImages[i]];
            newImage.GetComponent<SpriteRenderer>().sprite = sprite;

            //newImage.GetComponent<Sequence>().id="jola";           

            firstObjectRow1.Add(newImage);
            
        }

    }

    public void CreateSecondRow(){

        //cantidad de columnas de la primera fila
        int numberColumns = 2;

        int aux = 2;

        float x = -2.0f;
        //float x = objectSecondRow.transform.position.x;
        float y = -0.22f;
        //float y = objectSecondRow.transform.position.y;

        GameObject newImage;

        for (int i = 0; i < numberColumns ; i++)
        {

            newImage = Instantiate(objectRow, 
            new Vector3(x + (size.x * (i-(0.18f*i))), y, 0), 
            objectRow.transform.rotation);

            Sprite sprite = images[idImages[aux]];
            newImage.GetComponent<SpriteRenderer>().sprite = sprite;

            //newImage.GetComponent<Option>().valueOption = values[i];
            

            firstObjectRow2.Add(newImage);
            aux++;
            
        }
    }

    public void CreateThirdRow(){

        //cantidad de columnas de la primera fila
        int numberColumns = 3;

        int aux = 4;

        float x = -1.97f;
        //float x = objectThirdRow.transform.position.x;
        float y = -1.3f;
        //float y = objectThirdRow.transform.position.y;

        GameObject newImage;

        for (int i = 0; i < numberColumns ; i++)
        {
            newImage = Instantiate(objectRow, 
            new Vector3(x + (size.x * (i-(0.18f*i))), y, 0), 
            objectRow.transform.rotation);

            Sprite sprite = images[idImages[aux]];
            newImage.GetComponent<SpriteRenderer>().sprite = sprite;
            //newImage.GetComponent<Option>().valueOption = values[i];
            

            firstObjectRow3.Add(newImage);
            aux++;

        }

    }

    public void CreateFourthRow(){

        //cantidad de columnas de la primera fila
        int numberColumns = 4;

        int aux = 7;

        float x = -1.97f;
        //float x = objectFourthRow.transform.position.x;
        float y = -2.4f;
        //float y = objectFourthRow.transform.position.y;

        GameObject newImage;

        for (int i = 0; i < numberColumns ; i++)
        {
            newImage = Instantiate(objectRow, 
            new Vector3(x + (size.x * (i-(0.18f*i))), y, 0), 
            objectRow.transform.rotation);

            Sprite sprite = images[idImages[aux]];
            newImage.GetComponent<SpriteRenderer>().sprite = sprite;
            //newImage.GetComponent<Option>().valueOption = values[i];
            

            firstObjectRow4.Add(newImage);
            aux++;

        }
    }

    //metodo que me permite crear la lista de valores y de id de imagenes
    //de forma aleatoria
    public void CreatListAleatoriaNumbers(int aux){

        //aux es igual a 1 se realiza la lista de los valores
        //si es igual a 2 se realisza la lista de las imagenes

        for (int i = 0; i < 11; i++)
        {
            if (aux == 1)
            {
                CheckRepeatNumbers(1);

            }else {

                CheckRepeatNumbers(2);

            }
                
        }                                                   
        
    }

    //metodo que verifica que ningun numero de las listas se repita
    private void CheckRepeatNumbers(int aux){
        
        int number = 0;
        bool o = false;

        while(o == false){
                
            //verifica los numeros que estan en la matriz de valores
            if(aux==1){

                number = Random.Range(0, 30);
                if(values.Contains(number)){
                    o = false;
                }else{
                    o = true;
                    values.Add(number);
                }

            }else{

                number = Random.Range(0, images.Count);
                if(idImages.Contains(number)){
                    o = false;
                }else{
                    o = true;
                    idImages.Add(number);
                }
            }

        }
    }

    private void GenerateSigns(){

        // for (int i = 0; i < textList.Count; i++)
        // {
        //     textList[0].text = "" + signs[i];
        // }
        int aux;
        for (int i = 0; i < 7; i++)
        {
            aux = Random.Range(0, 2);

            Debug.Log(aux);

        }

    }

}
