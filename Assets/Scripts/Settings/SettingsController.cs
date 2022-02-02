using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SettingsController : Reference
{
    public FileLists file;
    public void ResetValues()
    {
        //Borrar los datos de todos los PlayerPrefs
        PlayerPrefs.DeleteAll();

        Debug.Log("ESTADO CreateLists: " + PlayerPrefs.GetInt("CreateLists", 0));

        //Crear nuevamente las listas de cada juego limitado

        //Lista juego 1
        file.classificationGameList = new List<int>();

        //Listas del juego 2
        file.imageListGame2_1 = new List<int>();
        file.imageListGame2_2 = new List<int>();
        file.imageListGame2_3 = new List<int>();

        file.imageListGame8_1_1 = new List<int>();
        file.imageListGame8_1_2 = new List<int>();
        file.imageListGame8_1_3 = new List<int>();

        file.imageListGame8_2_1 = new List<int>();
        file.imageListGame8_2_2 = new List<int>();
        file.imageListGame8_2_3 = new List<int>();

        //Lista de los logros
        file.achievementsList = new List<int>();

        //Borrar el archivo.data
        File.Delete(file.GetPath("P"));

        MusicManager.audioManager.ActivateMusic();
    }
    public void ChangeMusicStatus()
    {
        Debug.Log("Muscia: "+ PlayerPrefs.GetInt("Music", 1));
        if (PlayerPrefs.GetInt("Music",1) == 1)
        {
            MusicManager.audioManager.DisableMusic();
            App.generalView.settingsView.musicOn.enabled = false;
            App.generalView.settingsView.musicOff.enabled = true;
            PlayerPrefs.SetInt("Music", 0);
        }
        else
        {
            MusicManager.audioManager.ActivateMusic();
            App.generalView.settingsView.musicOn.enabled = true;
            App.generalView.settingsView.musicOff.enabled = false;
            PlayerPrefs.SetInt("Music", 1);
        }
    }
    public void ChangeEffectsStatus()
    {
        Debug.Log("Efectos: " + PlayerPrefs.GetInt("Effects", 1));
        if (PlayerPrefs.GetInt("Effects", 1) == 1)
        {
            SoundManager.soundManager.DisableSoundEffects();
            App.generalView.settingsView.effectsOn.enabled = false;
            App.generalView.settingsView.effectsOff.enabled = true;
            PlayerPrefs.SetInt("Effects", 0);
        }
        else
        {
            SoundManager.soundManager.ActivateSoundEffects();
            App.generalView.settingsView.effectsOn.enabled = true;
            App.generalView.settingsView.effectsOff.enabled = false;
            PlayerPrefs.SetInt("Effects", 1);
        }
    }
}
