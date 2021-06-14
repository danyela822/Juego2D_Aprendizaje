using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : Reference
{
    public void OnClickButtons(string name_button)
    {
        App.generalController.uiController.OnClickButtons(name_button);
    }
}
