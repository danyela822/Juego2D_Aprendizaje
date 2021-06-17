using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIView : Reference
{
    //variable tipo text de la interfaz
    public Text category;
    public void OnClickButtons(string name_button)
    {
        App.generalController.uiController.OnClickButtons(name_button);
    }

    //Metodo que recibe por parametro el nombre de la categoria
    //y lo cambia en la interfaz
    public void ChangeTextCategory(string nameCategory)
    {
        category = GameObject.Find("Category Text").GetComponent<Text>();
        category.text = nameCategory;
    }
}
