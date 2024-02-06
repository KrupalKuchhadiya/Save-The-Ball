using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject ParentObject;
    Vector3 StartPosition, DifferentPosition;
    [SerializeField]
    GameObject[] AllPrefeb;
    GameObject G;


    
    [SerializeField]
    Button MusicBtn, SoundBtn;
    [SerializeField]
    Sprite musicON, musicOFF, soundON, soundOFF;
    [SerializeField]
    AudioSource musicSource, soundSource;


   

    public void Start()
    {
        GeneratorPrefeb();

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
    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            StartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            DifferentPosition = StartPosition - Input.mousePosition;
            ParentObject.transform.Rotate(new Vector3(0, DifferentPosition.x * 0.4f, 0));
            StartPosition = Input.mousePosition;
        }
       
    }
    int LevelRings;
    public void GeneratorPrefeb()
    {
        ///////////////////////////
        //limited Block(Rings)   //
        ///////////////////////////
        LevelRings = 9;
        LevelRings = LevelRings + 1;
        LevelRings = /*10;*/PlayerPrefs.GetInt("Level", LevelRings);
        for (int i = 0; i < LevelRings; i++)
        {
            if (i == 0)
            {
                G = Instantiate(AllPrefeb[0], ParentObject.transform);
            }
            else if (i == LevelRings - 1)
            {
                G = Instantiate(AllPrefeb[i - 1], ParentObject.transform);
            }
            else
            {
                G = Instantiate(AllPrefeb[Random.Range(1, AllPrefeb.Length - 1)], ParentObject.transform);
            }
            G.transform.Translate(new Vector3(0, -i * 4f, 0));
            G.transform.Rotate(new Vector3(0, 360, 0));
        }



        /////////////////////////////
        // Unlimited Block(Rings)  //
        /////////////////////////////
        //GameObject U;
        //int K = Random.Range(0, 360);
        //U = Instantiate(AllPrefeb[0], ParentObject.transform);
        //U.transform.Translate(new Vector3(0, 1 + 4f, 0));
        //do
        //{
        //    for (int i = 0; i < 998; i++)
        //    {
        //        G = Instantiate(AllPrefeb[Random.Range(1, AllPrefeb.Length - 1)], ParentObject.transform);
        //        G.transform.Translate(new Vector3(0, -i * 4f, 0));
        //        G.transform.Rotate(new Vector3(0, K, 0));
        //    }
        //} while (G.transform.childCount.Equals(10));
        //U = Instantiate(AllPrefeb[LevelRings - 1], ParentObject.transform);
        //U.transform.Translate(new Vector3(0, 999 * 4f, 0));

    }
    public void RETRYButton()
    {
        PlayerPrefs.SetInt("Level",LevelRings);
        PlayerManager.RetryButton();
    }
    public void HomeButton()
    {
        SceneManager.LoadScene("HomeScene");
    }
  

}
