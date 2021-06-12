using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerLevelView : MonoBehaviour
{
    public Text textCategory;

    public void Start()
    {
        textCategory.text = "Principiante";
    }

    //method that allows the change scene
    public void ChangeSceneTo(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //method thaht allows the category
    public void ChangeCategory(string category)
    {
        category = "principiante";

        switch (category)
        {
            case "principiante":
                textCategory.text = "Principiante";
                break;

            case "moderado":
                textCategory.text = "Moderado";
                break;

            case "avanzado":
                textCategory.text = "Avanzado";
                break;

            default:
                break;
        }
    }
}
