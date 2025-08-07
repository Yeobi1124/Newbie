using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private float timeCount = 0f;

    public TMP_Text time;

    [Header("PlayerInfo")]
    public Image HP_bar;
    public Image SP_bar;

    [Header("Menu")]
    public GameObject Menu;
    public Slider BGM_Volume;
    public Slider SE_Volume;
    public Button ResumeButton;
    public Button QuitButton;

    [Header("Win")]
    public GameObject Win;
    public TMP_Text HighRecord;
    public Button HomeButton;

    [Header("Lose")]
    public GameObject Lose;
    public Button RetryButton;
    public Button Quit2Button;

    private PlayerInput playerInput;

    private void Awake()
    {
        if(null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);

        Menu.SetActive(false);

        playerInput = GetComponent<PlayerInput>();
        InputActionMap inputs = playerInput.actions.FindActionMap("UI");
        inputs.FindAction("ESC").performed += ToggleMenu;

        ResumeButton.onClick.AddListener(ResumeGame);
        ResumeButton.onClick.AddListener(RetryGame);
        QuitButton.onClick.AddListener(QuitGame);
        Quit2Button.onClick.AddListener(QuitGame);
        HomeButton.onClick.AddListener(QuitGame);

        Win.SetActive(false);
        Lose.SetActive(false);
    }

    private void OnEnable()
    {
        BGM_Volume.value = StartSceneManager.Instance.BGM_Volume.value;
        SE_Volume.value = StartSceneManager.Instance.SE_Volume.value;
        StartSceneManager.Instance.enabled = false;
    }

    public void WinGame()
    {
        Win.SetActive(true);
        HighRecord.text = "Record: " + time.text;
        Time.timeScale = 0;

    }

    public void LoseGame()
    {
        Lose.SetActive(true);
        Time.timeScale = 0;
    }

    private void Start()
    {
        HP_bar.fillAmount = 1;
        SP_bar.fillAmount = 1;
    }

    private void Update()
    {
        timeCount += Time.deltaTime;
        time.text = timeCount.ToString("F2") + "s";
    }

    public void ToggleMenu(InputAction.CallbackContext context)
    {
        Menu.SetActive(Menu.activeSelf ? false : true);
        Time.timeScale = Menu.activeSelf ? 0 : 1;
    }

    public void ResumeGame()
    {
        Menu.SetActive(false);
    }

    public void RetryGame()
    {
        LoadingSceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("StartScene");
        StartSceneManager.Instance.enabled = true;
    }

    public void ChangeStatusValue(string type, float val)
    {
        switch (type)
        {
            case "HP":
                HP_bar.fillAmount += val/100f;
                if (HP_bar.fillAmount < 0.2f) HP_bar.color = Color.red;
                else if (HP_bar.fillAmount < 0.5f) HP_bar.color = Color.yellow;
                else HP_bar.color = new Color(0, 0.937255f, 0.9960785f);
                    break;
            case "SP":
                SP_bar.fillAmount += val/100f;
                break;
            default:
                Debug.Log("UI Type Error: There is no type such as " + type + "!!!");
                break;
        }
    }
}
