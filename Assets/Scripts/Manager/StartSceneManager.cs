using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class StartSceneManager : MonoBehaviour
{
    public static StartSceneManager Instance;

    public GameObject MainMenu;
    public GameObject SettingMenu;

    public float speed;
    public float wipeSpeed;

    private bool IsSetting;

    [Header("MainMenu")]
    public TMP_Text Title;

    public Button PlayButton;
    public Button SettingButton;
    public Button QuitButton;

    private Button ChosenButton;

    public Image Line;

    public TMP_Text time;

    [Header("Setting")]
    public Slider BGM_Volume;
    public Slider SE_Volume;

    public Button ResumeButton;
    public Button Quit2Button;


    public GameObject[] BG = new GameObject[2];
    private void Awake()
    {
        if(null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        PlayButton.onClick.AddListener(Play);
        SettingButton.onClick.AddListener(Setting);
        QuitButton.onClick.AddListener(Quit);
        Quit2Button.onClick.AddListener(Quit);
        ResumeButton.onClick.AddListener(Setting);


        ChosenButton = QuitButton;
        IsSetting = false;
        SettingMenu.SetActive(false);
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
        if(ChosenButton != PlayButton)
        {
            Line.rectTransform.sizeDelta = new Vector2(35, 240);
            ChosenButton = PlayButton;
            return;
        }
        StartCoroutine(PlayCoroutine());
    }

    public void Setting()
    {
        if(ChosenButton != SettingButton)
        {
            Line.rectTransform.sizeDelta = new Vector2(35, 307);
            ChosenButton = SettingButton;
            return;
        }
        IsSetting = !IsSetting;

        if (IsSetting)
        {
            MainMenu.SetActive(false);
            SettingMenu.SetActive(true);
        }
        else
        {
            MainMenu.SetActive(true);
            SettingMenu.SetActive(false);
        }
    }

    public void Quit()
    {
        if(ChosenButton != QuitButton)
        {
            Line.rectTransform.sizeDelta = new Vector2(35, 373);
            ChosenButton = QuitButton;
            return;
        }
        Application.Quit();
    }

    public IEnumerator PlayCoroutine()
    {
        StartCoroutine(WipeUp(Line));
        yield return new WaitForSeconds(0.3F);
        StartCoroutine(WipeTitleLeft(Title));
        yield return new WaitForSeconds(0.1f);

        StartCoroutine(WipeButtonLeft(PlayButton));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(WipeButtonLeft(SettingButton));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(WipeButtonLeft(QuitButton));
        yield return new WaitForSeconds(1);

        LoadingSceneManager.LoadScene("MainScene");
    }

    public IEnumerator WipeUp(Image var)
    {
        float Myspeed = wipeSpeed;
        while(var.rectTransform.anchoredPosition.y < 550)
        {
            var.rectTransform.anchoredPosition =
                new Vector2(var.rectTransform.anchoredPosition.x, var.rectTransform.anchoredPosition.y - Myspeed);
            Myspeed -= 0.05f;
            yield return null;
        }
    }

    public IEnumerator WipeTitleLeft(TMP_Text var)
    {
        float Myspeed = wipeSpeed;
        while (var.rectTransform.anchoredPosition.x > -500)
        {
            var.rectTransform.anchoredPosition =
                new Vector2(var.rectTransform.anchoredPosition.x + Myspeed, var.rectTransform.anchoredPosition.y);
            Myspeed -= 0.1f;
            yield return null;
        }
    }
    public IEnumerator WipeButtonLeft(Button var)
    {
        RectTransform rect = var.GetComponent<RectTransform>();
        float Myspeed = wipeSpeed;
        while (rect.anchoredPosition.x > -500)
        {
            rect.anchoredPosition =
                new Vector2(rect.anchoredPosition.x + Myspeed, rect.anchoredPosition.y);
            Myspeed -= 0.1f;
            yield return null;
        }
    }
}
