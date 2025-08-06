using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StartSceneManager : MonoBehaviour
{
    public Button PlayButton;
    public Button SettingButton;
    public Button QuitButton;

    public TMP_Text time;

    public float speed;

    public GameObject[] BG = new GameObject[2];
    private void Awake()
    {
        PlayButton.onClick.AddListener(Play);
        SettingButton.onClick.AddListener(Setting);
        QuitButton.onClick.AddListener(Quit);
    }

    private void Update()
    {
        time.text = DateTime.Now.ToString("HH:mm:ss");

        foreach(GameObject go in BG)
        {
            go.transform.Translate(-speed * Time.deltaTime, 0, 0);
            if (go.transform.position.x < -22) go.transform.Translate(45, 0, 0);
        }
    }

    public void Play()
    {
        LoadingSceneManager.LoadScene("MainScene");
    }

    public void Setting()
    {

    }

    public void Quit()
    {
        
    }
}
