using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsView : Reference
{
    public Canvas resetCanvas;

    public void ShowResetCanvas()
    {
        resetCanvas.enabled = true;
    }
    public void HideResetCanvas()
    {
        resetCanvas.enabled = false;
    }
}
