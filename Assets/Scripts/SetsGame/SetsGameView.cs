using UnityEngine;
using UnityEngine.UI;

public class SetsGameView : Reference
{
    public Text message;

    //Matriz de botones que puede pulsar el jugador
    public Button[] buttons;

    public Image[] panels;

    public Image solutionImage;

    public GameObject solutionPanel;

    public void OnClickButtons(Button button)
    {
        //Nombre de la imagen que tiene el boton
        string nameImage = button.image.sprite.name;

        //Guardar el nombre de la imagen
        App.generalController.setsGameController.CheckAnswer(nameImage);
    }
    public void ShowSolution()
    {
        App.generalController.setsGameController.ShowSolution();
    }
}
