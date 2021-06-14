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

    public void ChangeTextCategory(string nameCategory)
    {
        category.text = nameCategory;
    }

    public void ChangeScene(string scene)
    {
        App.generalController.levelController.ChangeSceneTo(scene);
    }
}
