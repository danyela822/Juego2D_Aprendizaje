using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameView : Reference
{
    public Text question;
    public List<Text> easyText = new List<Text>();
    public List<Button> easyButton = new List<Button>();
    public List<Text> listText = new List<Text>(); 
    public List<Button> listButton = new List<Button>();
    public List<Text> listOptions = new List<Text>();
    
    // Canvas de minigame
    public Canvas miniGame3Canvas;
    
    // Start is called before the first frame update
    
    void Start()
    {
        MiniGame3();
    }
    
    void MiniGame3()
    {
        string type = App.generalController.miniGameController.miniGame3Controller.Category(9);

        if(type == "1")
        {
            LoadOperation(type, easyText, easyButton);
        }
        else if(type == "2" || type == "3")
        {
            LoadOperation(type, listText, listButton);
        }
    }
    
    public void Question()
    {
        question = GameObject.Find("Question").GetComponent<Text>();
    }

    void LoadOperation(string type, List<Text> textList, List<Button> buttonList)
    {
        App.generalController.miniGameController.miniGame3Controller.LoadOperation(type, textList, buttonList);
    }

    public void OnclickButton(Text option)
    {
        App.generalController.miniGameController.miniGame3Controller.CheckAnswer(option.text);
    }
}
