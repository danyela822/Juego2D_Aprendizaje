using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame2Controller : Reference
{
    Riddle riddle;
    public void LoadRiddles()
    {
        //Crear los acertijos 
        App.generalModel.miniGameModel.CreateRiddles();

        //Seleccionar un numero aleatorio entre cero y la cantidad total de acertijos
        int index = Random.Range(0, 34);

        //Seleccionar un acertijo de la lista creada anteriormente
        riddle = App.generalModel.miniGameModel.riddlesList[index];

        //Asignar el enunciado del acertijo al texto de la vista
        App.generalView.miniGameView.riddleText.text = riddle.Text;

        //int cont = 0;
        //bool op_0 = true, op_1 = true, op_2 = true;
        //int aux = -1;

        index = Random.Range(0, 3);
        /*while (cont < 3)
        {

            string nom = "Option_" + index + " Button";
            print("R: " + index);

            if (nom == App.generalView.miniGameView.riddleButtons[0].name && op_0 == true)
            {
                App.generalView.miniGameView.riddleTextButtons[0].text = riddle.Options[index].ToString();
                op_0 = false;
                cont++;
            }
            else if (nom == App.generalView.miniGameView.riddleButtons[0].name && op_1 == true)
            {
                App.generalView.miniGameView.text1.text = riddle.Options[index].ToString();
                op_1 = false;
                cont++;
            }
            else if (nom == App.generalView.miniGameView.riddleButtons[0].name && op_2 == true)
            {
                App.generalView.miniGameView.text2.text = riddle.Options[index].ToString();
                op_2 = false;
                cont++;
            }
        }*/
        for (int i = 0; i < riddle.Options.Count; i++)
        {
            App.generalView.miniGameView.riddleTextButtons[i].text = riddle.Options[i].ToString();
        }
    }
    public void CheckAnswer(string answer)
    {
        Debug.Log("ENVIO: "+answer+" LA QUE HAY: "+ riddle.Answer);
        if(answer == riddle.Answer)
        {
            //Canvas winCanvas = GameObject.Find("Win Canvas").GetComponent<Canvas>();
            App.generalView.miniGameView.winCanvas.enabled = true;

            print("CORRECTO");

            //Variable que servira para cambiar el sprite de la imagen del acertijo
            Sprite sprite = App.generalModel.miniGameModel.LoadSprite(riddle.Image);


            print("IMAGEN: " + sprite);

            App.generalView.miniGameView.solutionImage.sprite = sprite;

            App.generalView.miniGameView.solutionText.text =  "Correcto, la respuesta es: " + riddle.Answer;
        }
        else
        {
            App.generalView.miniGameView.loseCanvas.enabled = true;
        }
    }
}
