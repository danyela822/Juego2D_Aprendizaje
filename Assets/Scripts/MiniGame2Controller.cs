using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame2Controller : Reference
{
    Acertijo a;
    public void CargarAcertijos()
    {
        App.generalModel.miniGameModel.CreateRiddles();
        int num = Random.Range(0, 34);
        
        a = App.generalModel.miniGameModel.riddlesList[num];

        App.generalView.miniGameView.riddleText.text = a.riddle;

        int cont = 0;
        int r = 0;
        bool op_0 = true, op_1 = true, op_2 = true;

        while (cont < 3)
        {
            r = Random.Range(0, 3);
            string nom = "Option_"+r+" Button";
            print("R: " + r);
            
            if (nom == App.generalView.miniGameView.option0.name && op_0 == true)
            {
                App.generalView.miniGameView.text0.text = a.options[r].ToString();
                op_0 = false;
                cont++;
            }
            else if (nom == App.generalView.miniGameView.option1.name && op_1 == true)
            {
                App.generalView.miniGameView.text1.text = a.options[r].ToString();
                op_1 = false;
                cont++;
            }
            else if (nom == App.generalView.miniGameView.option2.name && op_2 == true)
            {
                App.generalView.miniGameView.text2.text = a.options[r].ToString();
                op_2 = false;
                cont++;
            }
        }
    }
    public void CheckAnswer(string answer)
    {
        Debug.Log("ENVIO: "+answer+" LA QUE HAY: "+a.answer);
        if(answer == a.answer)
        {
            App.generalView.miniGameView.winPanel.SetActive(true);

            print("CORRECTO");

            Sprite sprite = App.generalModel.miniGameModel.LoadSprite(a.image);


            print("IMAGEN: " + sprite);
            App.generalView.miniGameView.solutionImage.enabled = true;
            App.generalView.miniGameView.solutionImage.sprite = sprite;

            App.generalView.miniGameView.solutionText.enabled = true;
            App.generalView.miniGameView.solutionText.text =  "Correcto, la respuesta es: " + a.answer;

            App.generalView.miniGameView.riddleImage.enabled = false;
        }
        else
        {
            App.generalView.miniGameView.losePanel.SetActive(true);
            App.generalView.miniGameView.loseImage.enabled = true;
            App.generalView.miniGameView.loseText.enabled = true;
        }
    }
}
