using UnityEngine;

public class GameOptionsController : Reference
{
    public  string [] winMessages = { "Sigue adelante", "¡Muy Bien!", "¡Excelente!" };
    public  string [] attemptMessages = { "Ya estas cerca, vamos!", "Intentalo de nuevo. ¡Tu puedes!" };

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
