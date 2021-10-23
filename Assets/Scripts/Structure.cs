using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour
{
    //------------------------ variables interfaz --------------------//
    public List<GameObject> listOneRow = new  List<GameObject>();
    public List<GameObject> listTwoRow = new  List<GameObject>();
    public List<GameObject> listThreeRow = new  List<GameObject>();
    public List<GameObject> listFourRow = new  List<GameObject>();
    //lista de los posibles sprites que seran utilizados
    public List <Sprite> images = new List<Sprite>();
    public GameObject objectRow;
    private Vector2 size;
    int aux;
    int levels = 4;
    //----------------------------------------------------------------//

    //------------------------ variables logica ----------------------//
    List<Operation> operationes = new List<Operation>();

    //----------------------------------------------------------------//


    void Start()
    {
        //Build(3);
        size = objectRow.GetComponent<BoxCollider2D>().size;
        Debug.Log(OwnToString());
        CreateTable();

    }

    public void Build (int numLevels){
        
        int numBase = 2;
        List<int> integersUsed = new List<int>();
        for (int i = 0; i < numLevels; i++)
        {
            switch (i)
            {
                case 0: operationes.Add(new Operation(numBase, integersUsed, 8));
                break;
                default:operationes.Add(new Operation(numBase + i, integersUsed));
                break;
            }
        }
    }

    public string OwnToString(){

        string ret = "";
        foreach (Operation opt in operationes)
        {
            ret += opt.OwnToString()+"\n";
        }

        return ret;
    }

//------------------- construccion de la interfaz --------------------------------------------//

    public void CreateTable(){

        aux = 2;

        GameObject newOption;
        float x, y;

        for (int i = 0; i < levels; i++){

            switch (i){
                case 0:
                    x = -2.31f; y = 0.63f;
                    for (int j = 0; j < aux; j++){
                        newOption = Instantiate(objectRow,
                            new Vector3(x +(size.x * (j-(0.3f*j))), 
                            y, 0), objectRow.transform.rotation);
                    }
                    aux+=1;
                break;

                case 1:
                    x = -2.26f; y = -0.27f;
                    for (int j = 0; j < aux; j++){
                        newOption = Instantiate(objectRow,
                            new Vector3(x +(size.x * (j-(0.3f*j))), 
                            y, 0), objectRow.transform.rotation);
                    }
                    aux+=1;
                break;

                case 2:
                    x = -2.21f; y = -1.17f;
                    for (int j = 0; j < aux; j++){
                        newOption = Instantiate(objectRow,
                            new Vector3(x +(size.x * (j-(0.3f*j))), 
                            y, 0), objectRow.transform.rotation);
                    }
                    aux+=1;
                break;

                default:
                    x = -2.21f; y = -2.09f;
                    for (int j = 0; j < aux; j++){
                        newOption = Instantiate(objectRow,
                            new Vector3(x +(size.x * (j-(0.3f*j))), 
                            y, 0), objectRow.transform.rotation);
                    }
                    aux+=1;
                break;
            }
            
        }

    }

}
