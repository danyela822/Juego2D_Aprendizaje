using UnityEngine.UI;

public class CharacteristicsGameView : Reference
{
    //Matriz de botones que puede pulsar el jugador
    public Button[] buttons;

    //Texto para mostrar el eunciado del juego
    public Text statement;

    /// <summary>
    /// Metodo que captura el nombre de la imagen que posee un boton para su posterior analisis
    /// </summary>
    /// <param name="button">Boton seleccionado</param>
    public void CheckAnswer(Button button)
    {
        //Nombre de la imagen que tiene el boton
        string nameImage = button.image.sprite.name;

        //Verificar si la respuesta es correcta
        App.generalController.characteristicsGameController.CheckAnswer(nameImage);
    }
    public void ViewSolution()
    {
        App.generalController.characteristicsGameController.ShowSolution();
    }
}
