using UnityEngine;

public class UserData : Reference
{
    public static UserData userData;

    public string userName;
    public string userAge;
    
    public void ReadStringUserName(string name)
    {
        userName = name;
        Debug.Log(userName);
    }
    public void ReadStringUserAge(string age)
    {
        userAge = age;
        Debug.Log(userAge);
    }
    void Awake()
    {
        if (userData == null)
        {
            userData = this;
            if (PlayerPrefs.GetInt("Login", 0) == 1)
            {
                App.generalController.uiController.loginCanvas.enabled = false;
                App.generalController.uiController.mainMenuCanvas.enabled = true;

            }
            else
            {
                //ActivateSoundEffects();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
