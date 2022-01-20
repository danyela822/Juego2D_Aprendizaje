using UnityEngine;
using UnityEngine.SceneManagement;
public class GamesMenuController : Reference
{
    public FileLists file;
    public void Play(string gameName)
    {
        //Debug.Log("Nombre del juego: " + gameName);

        switch (gameName)
        {
            case "Descubre el conjunto":
                if(file.classificationGameList.Count==0)
                {
                    App.generalView.gamesMenuView.playButtons[0].interactable = false;
                    App.generalView.gamesMenuView.finishedCanvas.enabled = true;
                }
                else
                {
                    SceneManager.LoadScene("ClassificationGameScene");
                }
                break;
            case "¿Quien soy?":
                if (file.imageListGame2_3.Count == 0)
                {
                    App.generalView.gamesMenuView.playButtons[1].interactable = false;
                    App.generalView.gamesMenuView.finishedCanvas.enabled = true;
                }
                else
                {
                    SceneManager.LoadScene("CharacteristicsGameScene");
                }
                break;
            case "Sigue la secuencia":
                SceneManager.LoadScene("SequenceGameScene");
                break;

            case "Conectados":
                SceneManager.LoadScene("ConnectedGameScene");

                break;

            case "Encuentra el camino":
                SceneManager.LoadScene("CategoriesScene");

                break;

            case "Desifra el operando":
                SceneManager.LoadScene("AdditionGameScene");
                break;

            case "Union e Interseccion":
                if (file.imageListGame8_2_3.Count == 0)
                {
                    App.generalView.gamesMenuView.playButtons[6].interactable = false;
                    App.generalView.gamesMenuView.finishedCanvas.enabled = true;
                }
                else
                {
                    SceneManager.LoadScene("SetsGameScene");
                }
                break;

            case "Juego de equivalencias":
                SceneManager.LoadScene("EquialityGameScene");
                break;
            default:
                Debug.Log("Escena no encontrada");
                break;
        }
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void GetPointsGame()
    {
        //int points = App.generalModel.characteristicsGameModel.GetPoints();
    }
}
