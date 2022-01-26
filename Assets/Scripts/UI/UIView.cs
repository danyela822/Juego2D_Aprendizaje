using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIView : Reference
{
    //variable tipo text de la interfaz
    public Text category;

    public void OnClickButtons(Button button)
    {
        App.generalController.uiController.OnClickButtons(button.name);
    }

    //Metodo que recibe por parametro el nombre de la categoria
    //y lo cambia en la interfaz
    public void ChangeTextCategory(string nameCategory)
    {
        category = GameObject.Find("Category Text").GetComponent<Text>();
        category.text = nameCategory;
    }

    public string NameCategory()
    {
        category = GameObject.Find("Category Text").GetComponent<Text>();
        return category.text;
    }
}
