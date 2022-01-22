using UnityEngine;
using UnityEngine.UI;

public class ConnectedGameView : Reference
{
    public Button[] colorButtons,resetButtons;

    public Canvas colorsCanvas;

    public Text message;

    public void ReturnButton()
    {
        colorsCanvas.enabled = true;
    }
    public void ResetMoves (int type)
    {
        App.generalController.connectedGameController.ResetMoves(type);
    }
    public void HideColorCanvas()
    {
        colorsCanvas.enabled = false;
    }
    public void SelectColor(int tipo)
    {
        App.generalController.connectedGameController.SelectColor(tipo);
    }
    public void Move(string direction)
    {
        App.generalController.connectedGameController.Move(direction);
    }
    public void ShowSolution()
    {
        App.generalController.connectedGameController.ShowSolution();
    }
}
