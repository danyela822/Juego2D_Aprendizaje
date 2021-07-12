using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : MonoBehaviour
{
    //id del la imagen de la secuencia
    public string id;
    //imagen con que voy a comparar si escoge bien o no 
    //private static Sequence sequenceToCompare = null;

    //varibale que me indica si la imagen es de enunciado o para comparar
    //false es enunciado
    public bool type;

    //metodo que permite comparar el objeto seleccionado con la respuesta correcta
   /* private void SelectSequence(){

        sequenceToCompare = gameObject.GetComponent<Sequence>();
        string compare = MiniGame1Controller.sharedInstance.correctAnswer.GetComponent<Sequence>().id;

        if (compare == this.id)
        {
            Debug.Log("Esta carajada dio a la primera");
            MiniGame1Controller.sharedInstance.ChangeCorrectImage();

        }else
        {
            Debug.Log("No dio a la primera :(");
        }
    }

    //metodo que permite seleccionar el objeto
    private void OnMouseDown(){

        //verifica si el objeto puede o no ser seleccionado
        if(type == false){

            return; 

        }else{

            SelectSequence();

        }

    }*/

}