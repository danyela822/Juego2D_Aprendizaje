using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : Reference
{
    public void OnClickButtons(string name_button)
    {
        //Botones del menu principal
        if (name_button == "Button Play")
        {
            SceneManager.LoadScene("CategoriesScene");
        }
        if (name_button == "Button Settings")
        {
            SceneManager.LoadScene("SettingsScene");
        }
        if (name_button == "Button Stats")
        {
            SceneManager.LoadScene("StatsScene");
        }
        if (name_button == "Button Info")
        {
            SceneManager.LoadScene("InfoScene");
        }


        //Boton de regreso (Puede servir para cualquier escena)
        if (name_button == "Button Back")
        {
            SceneManager.LoadScene("MainMenuScene");
        }

        //Botones de la vista de categorias
        if(name_button == "Button Begginer")
        {
            //LLamado al metodo para mostrar la escena de los niveles
        }
        if (name_button == "Button Medium")
        {
            //LLamado al metodo para mostrar la escena de los niveles
        }
        if (name_button == "Button Advanced")
        {
            //LLamado al metodo para mostrar la escena de los niveles
        }

        //Botones de la vista de Configuracion
        if (name_button == "Button Music")
        {
            //LLamar al metodo de la musica
        }
        if (name_button == "Button Sound")
        {
            //LLamar al metodo del sonido
        }
    }   
}
