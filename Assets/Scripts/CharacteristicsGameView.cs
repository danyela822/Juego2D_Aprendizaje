using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicsGameView : Reference
{
    public Button[] buttons;
    public Text statement;
    public Canvas startCanvas;

    public void OnClickButtons(Button button)
    {
        string nameButton = button.image.sprite.name;

        App.generalController.characteristicsGameController.SaveOption(nameButton);
    }
    public void CheckAnswer()
    {
        bool isWin = App.generalController.characteristicsGameController.CheckAnswer();
        if (isWin)
        {
            App.generalView.menuGamesView.WinCanvas.enabled = true;
            Debug.Log("GANO");
        }
        else
        {
            Debug.Log("PERDIO");
        }
    }
    public void StartGame()
    {
        startCanvas.GetComponent<Canvas>().enabled = false;
    }
}
