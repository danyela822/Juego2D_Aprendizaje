using UnityEngine.UI;
public class RoadGameView : Reference
{
    //Botones para especificar que personaje se va a mover
    public Button character_1, character_2, character_3;

    public Text cointsText, solutionTickets;

    /*private void Awake()
    {
        PointsLevel();
    }*/
    /*public void PointsLevel()
    {
        cointsText.text = " x " + App.generalModel.roadGameModel.GetPoints();
        solutionTickets.text = " x " + App.generalModel.roadGameModel.GetTickets();
    }*/
    /*public void ActivateWinCanvas(int totalStars)
    {
        Image imageWin = GameObject.Find("ImageStars").GetComponent<Image>();

        imageWin.sprite = Resources.Load<Sprite>("Stars/" + totalStars);

        App.generalView.gameOptionsView.WinCanvas.enabled = true;
    }*/
    public void DrawSolution()
    {
        App.generalController.roadGameController.DrawSolution();
        /*if( App.generalModel.roadGameModel.GetTickets() > 0)
        {
            App.generalController.roadGameController.DrawSolution();
            PointsLevel();
        }
        else{
            print("no tienes tickets de solucion");
        }*/
    }
    public void ActivateMovement(int type)
    {
        //App.generalController.charactersController.ActivateMovement(type);
        App.generalController.charactersController.ActivateComponents(type);
    }
    public void MoveCharacter(string direction)
    {
        App.generalController.charactersController.Move(direction);
    }
}
