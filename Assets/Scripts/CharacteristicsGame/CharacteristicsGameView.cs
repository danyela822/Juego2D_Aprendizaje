using UnityEngine;
using UnityEngine.UI;

public class CharacteristicsGameView : Reference
{
    //Matriz de botones que puede pulsar el jugador
    public Button[] buttons;

    //Texto para mostrar el eunciado del juego
    public Text statement;

    public Canvas transition;

    /// <summary>
    /// Metodo que captura el nombre de la imagen que posee un boton para su posterior analisis
    /// </summary>
    /// <param name="button">Boton seleccionado</param>
    public void CheckAnswer(Button button)
    {
        //Nombre de la imagen que tiene el boton
        string nameImage = button.image.sprite.name;

        //Desactivar el boton que se ha presionado
        button.interactable = false;

        //Verificar si la respuesta es correcta
        App.generalController.characteristicsGameController.CheckAnswer(nameImage);
    }
    /// <summary>
    /// Metodo que reestablece los valores iniciales del juego a nivel visual
    /// </summary>
    public void RePlayGame()
    {
        //Ocultar el canvas que indica que perdio
        App.generalView.gameOptionsView.HideLoseCanvas();

        //Activar nuevamente todos los botones
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }

        //Reestablecer la cantidad de intentos
        App.generalController.characteristicsGameController.RestartAttempts();
    }
}
