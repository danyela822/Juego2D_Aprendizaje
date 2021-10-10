using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassificationGameView : Reference
{
    public Button [] buttons;
    public Text text;
    private void Awake()
    {
        
    }
    public void OnClickButtons()
    {

    }
    public void CheckAnswer()
    {

    }
    public void PutPictures()
    {
        App.generalController.classificationGameController.PutPictures();
    }
}
