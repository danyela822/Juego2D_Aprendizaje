using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : Reference
{
    //variable tipo text de la interfaz
    public Text category;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //metodo que recibe por parametro el nombre de la categoria
    //y lo cambia en la interfaz
    public void ChangeTextCategory(string nameCategory)
    {
        category.text = nameCategory;
    }

    //metodo que recibe por paramentro el nombre de la escena a la 
    //que quiero cambiar y llamo al metodo del controller
    public void ChangeScene(string scene)
    {
        App.generalController.levelController.ChangeSceneTo(scene);
    }
}
