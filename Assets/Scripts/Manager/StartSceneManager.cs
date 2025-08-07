using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using DG.Tweening;

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

    private RectTransform[] rects = new RectTransform[3];
    private void Awake()
    {
        if (null == Instance) Instance = this;
        else Destroy(gameObject);

        PlayButton.onClick.AddListener(Play);
        SettingButton.onClick.AddListener(Setting);
        QuitButton.onClick.AddListener(Quit);
        Quit2Button.onClick.AddListener(Quit);
        ResumeButton.onClick.AddListener(Setting);

        ChosenButton = QuitButton;
        Line.rectTransform.sizeDelta = new Vector2(Line.rectTransform.sizeDelta.x, 373);

        IsSetting = false;
        SettingMenu.SetActive(false);

        rects[0] = PlayButton.GetComponent<RectTransform>();
        rects[1] = SettingButton.GetComponent<RectTransform>();
        rects[2] = QuitButton.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        //AudioManager.Instance.PlayBGM(AudioManager.BGMType.Title);

        BGM_Volume.value = AudioManager.Instance.BGM_Volume;
        SE_Volume.value = AudioManager.Instance.SE_Volume;
    }

    private void Update()
    {
        AudioManager.Instance.BGM_Volume = BGM_Volume.value;
        AudioManager.Instance.SE_Volume = SE_Volume.value;

        time.text = DateTime.Now.ToString("HH:mm:ss");

        foreach(GameObject go in BG)
        {
            go.transform.Translate(-speed * Time.deltaTime, 0, 0);
            if (go.transform.position.x < -22) go.transform.Translate(45, 0, 0);
        }
    }

    public void Play()
    {
        AudioManager.Instance.PlaySE(AudioManager.SEType.UIButton);
        if (ChosenButton != PlayButton)
        {
            Line.rectTransform.sizeDelta = new Vector2(35, -rects[0].anchoredPosition.y + 60);
            ChosenButton = PlayButton;
            return;
        }
        StartCoroutine(PlayCoroutine());
    }

    public void Setting()
    {
        AudioManager.Instance.PlaySE(AudioManager.SEType.UIButton);
        if (ChosenButton != SettingButton)
        {
            Line.rectTransform.sizeDelta = new Vector2(35, -rects[1].anchoredPosition.y + 60);
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
        AudioManager.Instance.PlaySE(AudioManager.SEType.UIButton);
        if (ChosenButton != QuitButton)
        {
            Line.rectTransform.sizeDelta = new Vector2(35, -rects[2].anchoredPosition.y + 60);
            ChosenButton = QuitButton;
            return;
        }
        Application.Quit();
    }

    public IEnumerator PlayCoroutine()
    {
        Debug.Log(Time.timeScale);

        StartCoroutine(WipeUp(Line));
        yield return new WaitForSeconds(0.3F); // Timescale에 구애받지 않는 시간 측정
        StartCoroutine(WipeLeft(Title.rectTransform));
        yield return new WaitForSeconds(0.1f);

        foreach (RectTransform rect in rects)
        {
            StartCoroutine(WipeLeft(rect));
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.5f);

        LoadingSceneManager.LoadScene("MainScene");
    }

    public IEnumerator WipeUp(Image var)
    {
        float Myspeed = wipeSpeed;
        while(var.rectTransform.anchoredPosition.y < var.rectTransform.sizeDelta.y)
        {
            var.rectTransform.anchoredPosition =
                new Vector2(var.rectTransform.anchoredPosition.x, var.rectTransform.anchoredPosition.y - Myspeed);
            Myspeed -= 0.05f;
            yield return null;
        }
    }

    public IEnumerator WipeLeft(RectTransform var)
    {
        float Myspeed = wipeSpeed;
        while (var.anchoredPosition.x > -var.sizeDelta.x)
        {
            var.anchoredPosition =
                new Vector2(var.anchoredPosition.x + Myspeed, var.anchoredPosition.y);
            Myspeed -= 0.1f;
            yield return null;
        }
    }
}
