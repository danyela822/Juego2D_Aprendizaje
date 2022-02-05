using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SettingsController : Reference
{
    public FileLists file;

    /// <summary>
    /// Metodo para reestablecer todos los datos del juego
    /// </summary>
    public void ResetValues()
    {
        //Borrar los datos de todos los PlayerPrefs
        PlayerPrefs.DeleteAll();

        Debug.Log("ESTADO CreateLists: " + PlayerPrefs.GetInt("CreateLists", 0));

        //Crear nuevamente las listas de cada juego (NO RANDOM)

        //Lista juego 1
        file.imageListGame1 = new List<int>();

        //Listas del juego 2
        file.imageListGame2_1 = new List<int>();
        file.imageListGame2_2 = new List<int>();
        file.imageListGame2_3 = new List<int>();

        //Listas del juego 8
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

        //Activar la musica
        MusicManager.audioManager.ActivateMusic();
    }
    /// <summary>
    /// Metodo para cambiar el estado de la musica del juego
    /// </summary>
    public void ChangeMusicStatus()
    {
        Debug.Log("Muscia: "+ PlayerPrefs.GetInt("Music", 1));
        if (PlayerPrefs.GetInt("Music",1) == 1)
        {
            //Desactivar la musica
            MusicManager.audioManager.DisableMusic();

            //Desactivar el sprite de musica encendida
            App.generalView.settingsView.musicOn.enabled = false;

            //Activar el sprite de musica apagada
            App.generalView.settingsView.musicOff.enabled = true;

            //Cambiar el estado de la musica a desactivada
            PlayerPrefs.SetInt("Music", 0);
        }
        else
        {
            //Activar la musica
            MusicManager.audioManager.ActivateMusic();

            //Activar el sprite de musica encendida
            App.generalView.settingsView.musicOn.enabled = true;

            //Desactivar el sprite de musica apagada
            App.generalView.settingsView.musicOff.enabled = false;

            //Cambiar el estado de la musica a activada
            PlayerPrefs.SetInt("Music", 1);
        }
    }
    /// <summary>
    /// Metodo para cambiar el estado de los efectos de sonido
    /// </summary>
    public void ChangeEffectsStatus()
    {
        Debug.Log("Efectos: " + PlayerPrefs.GetInt("Effects", 1));
        if (PlayerPrefs.GetInt("Effects", 1) == 1)
        {
            //Desactivar los efectos
            SoundManager.soundManager.DisableSoundEffects();

            //Desactivar el sprite de efectos encendidos
            App.generalView.settingsView.effectsOn.enabled = false;

            //Activar el sprite de efectos apagados
            App.generalView.settingsView.effectsOff.enabled = true;

            //Cambiar el estado de los efectos a desactivados
            PlayerPrefs.SetInt("Effects", 0);
        }
        else
        {
            //Activar los efectos
            SoundManager.soundManager.ActivateSoundEffects();

            //Activar el sprite de efectos encendidos
            App.generalView.settingsView.effectsOn.enabled = true;

            //Desactivar el sprite de efectos apagados
            App.generalView.settingsView.effectsOff.enabled = false;

            //Cambiar el estado de los efectos a activados
            PlayerPrefs.SetInt("Effects", 1);
        }
    }
}
