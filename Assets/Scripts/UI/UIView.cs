using UnityEngine.UI;

public class UIView : Reference
{
    public void OnClickButtons(Button button)
    {
        App.generalController.uiController.OnClickButtons(button.name);
    }
}
