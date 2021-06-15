using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : Reference
{
    private string nameCategory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //method that allows the change scene
    public void ChangeSceneTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //method thaht allows the category
    public void ChangeCategory(string category)
    {
        nameCategory = "";

        switch (category)
        {
            case "principiante":
                nameCategory = "Principiante";
                break;

            case "moderado":
                nameCategory = "Medio";
                break;

            case "avanzado":
                nameCategory = "Avanzado";
                break;

            default:
                break;
        }

        App.generalView.levelView.ChangeTextCategory(nameCategory);
    }
}
