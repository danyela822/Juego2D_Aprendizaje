using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class RoadGameView : Reference
{
    //Botones para especificar que personaje se va a mover
    public Button character_1, character_2, character_3;

    public Text cointsText, solutionTickets;

    bool isGameStarted;
    private void Awake()
    {
        isGameStarted = true;
        PointsLevel();
    }

    public void PointsLevel()
    {
        /*Text cointsText = GameObject.Find("Coins Text").GetComponent<Text>();
        Text solutionTickets = GameObject.Find("SolutionTickets Text").GetComponent<Text>();*/

        cointsText.text = " x " + App.generalModel.roadGameModel.GetPoints();
        solutionTickets.text = " x " + App.generalModel.roadGameModel.GetTickets();
    }

    /*public void ActivateWinCanvas(int totalStars)
    {
        Image imageWin = GameObject.Find("ImageStars").GetComponent<Image>();

        imageWin.sprite = Resources.Load<Sprite>("Stars/" + totalStars);

        App.generalView.gameOptionsView.WinCanvas.enabled = true;
    }*/
    /*
     * Metodo que captura el nombre del boton el cual se esta pulsando
     */
    public void OnClickButtons(string name_button)
    {
        App.generalController.roadGameController.OnClickButtons(name_button);
    }

    public void MiniGame()
    {
        SceneManager.LoadScene("MiniGamesScene");
    }

    public void DrawSolution()
    {

        if( App.generalModel.roadGameModel.GetTickets() > 0)
        {
            App.generalController.roadGameController.DrawSolution();
            PointsLevel();
        }
        else{
            print("no tienes tickets de solucion");
        }
    }

    public void ActivateMovement(int type)
    {
        App.generalController.charactersController.ActivateMovement(type);
    }

    public void MoveCharacter(string direction)
    {
        App.generalController.charactersController.Move(direction);
    }
    public void StartGame()
    {
        App.generalView.gameOptionsView.TutorialCanvas.enabled = false;

        if(isGameStarted)
        {
            App.generalController.roadGameController.DrawMatrix();
            isGameStarted = false;
        }
    }
}
