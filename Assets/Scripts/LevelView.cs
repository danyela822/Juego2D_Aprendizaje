using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : Reference
{
    //variable tipo text de la interfaz
    private Text category;

    //metodo que recibe por parametro el nombre de la categoria
    //y lo cambia en la interfaz
    public void ChangeTextCategory(string nameCategory)
    {
        print("ENTRO A CHANGE");
        category = GameObject.Find("CategoryText").GetComponent<Text>();
        category.text = nameCategory;
    }

    //metodo que recibe por paramentro el nombre de la escena a la 
    //que quiero cambiar y llamo al metodo del controller
    public void ChangeScene(string scene)
    {
        App.generalController.levelController.ChangeSceneTo(scene);
    }
}
