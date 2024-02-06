using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class HomeManager : MonoBehaviour
{
    [SerializeField]
    GameObject HomePanel, ExitPanel, LoadingPanel;
    [SerializeField]
    Image LoadingSliderImage;
    [SerializeField]
    float Speed;
    bool LoadingSliderBool;

    [SerializeField]
    Button MusicBtn, SoundBtn;
    [SerializeField]
    Sprite musicON, musicOFF, soundON, soundOFF;
    [SerializeField]
    AudioSource musicSource, soundSource;

    void Update()
    {
        if (LoadingSliderBool == true)
        {
            if (LoadingSliderImage.fillAmount < 1)
            {
                LoadingSliderImage.fillAmount += Speed * Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene("PlayScene");
            }
        }
    }
    void Start()
    {

        if (AudioManager.instance.music)
        {

            MusicBtn.GetComponent<Image>().sprite = musicON;
            musicSource.mute = false;
            AudioManager.instance.music = true;
        }
        else
        {

            MusicBtn.GetComponent<Image>().sprite = musicOFF;
            musicSource.mute = true;
            AudioManager.instance.music = false;
        }




        if (AudioManager.instance.sound)
        {

            SoundBtn.GetComponent<Image>().sprite = soundON;
            soundSource.mute = false;
            AudioManager.instance.sound = true;
        }
        else
        {

            SoundBtn.GetComponent<Image>().sprite = soundOFF;
            soundSource.mute = true;
            AudioManager.instance.sound = false;
        }
    }

    public void SoundClick()
    {
        soundSource.Play();
    }

    public void SoundManagement()
    {
        SoundClick();
        if (AudioManager.instance.sound)
        {
            SoundBtn.GetComponent<Image>().sprite = soundOFF;
            soundSource.mute = true;
            AudioManager.instance.sound = false;
        }
        else
        {
            SoundBtn.GetComponent<Image>().sprite = soundON;
            soundSource.mute = false;
            AudioManager.instance.sound = true;
        }
    }


    public void MusicManagement()
    {
        SoundClick();
        if (AudioManager.instance.music)
        {
            MusicBtn.GetComponent<Image>().sprite = musicOFF;
            musicSource.mute = true;
            AudioManager.instance.music = false;
        }
        else
        {
            MusicBtn.GetComponent<Image>().sprite = musicON;
            musicSource.mute = false;
            AudioManager.instance.music = true;
        }
    }

    public void OnSoundClick()
    {
        
    }
    public void PlayButtonClick()
    {
        OnSoundClick();
        HomePanel.SetActive(false);
        LoadingPanel.SetActive(true);
        LoadingSliderBool = true;
    }
    public void ExitPanelYesButton()
    {
        OnSoundClick();
        Application.Quit();
    }
    public void ExitPanelNoButton()
    {
        OnSoundClick();
        HomePanel.SetActive(true);
        ExitPanel.SetActive(false);
    }
    public void ExitPanelOpen()
    {
        OnSoundClick();
        HomePanel.SetActive(false);
        ExitPanel.SetActive(true);
    }
}
