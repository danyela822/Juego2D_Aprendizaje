using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassificationGameView : Reference
{
    public Button [] buttons;
    public Text text;
    public GameObject choise;
    public GameObject box;
    public Canvas startCanvas;
    int i = 0;
    private void Awake()
    {
        
    }
    public void OnClickButtons(Button button)
    {
        string nameButton = button.image.sprite.name;

        button.GetComponentInChildren<Text>().text = nameButton.ToUpper();
        
        button.enabled = false;

        App.generalController.classificationGameController.saveChoise(nameButton);

        GameObject texto = Instantiate(choise, new Vector3(choise.transform.position.x, choise.transform.position.y+(100*i),0),choise.transform.rotation);
        i++;
        texto.GetComponent<Text>().text = nameButton.ToUpper();

        texto.transform.SetParent(box.transform);


    }
    public void activatedPanel(GameObject panel)
    {
        panel.GetComponent<Image>().enabled = true;
    }
    public void CheckAnswer()
    {
        bool isWin = App.generalController.classificationGameController.CheckAnswer();
        if (isWin)
        {
            Debug.Log("GANO");
        }
        else
        {
            Debug.Log("PERDIO");
        }
    }
    public void PutPictures()
    {
        App.generalController.classificationGameController.PutPictures();
    }
    public void StartGame()
    {
        startCanvas.GetComponent<Canvas>().enabled = false;
    }
}
