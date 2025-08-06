using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("PlayerInfo")]
    public Image HP_bar;
    public Image SP_bar;

    [Header("Menu")]
    public GameObject Menu;
    public Slider BGM_Volume;
    public Slider SE_Volume;
    public Button ResumeButton;
    public Button QuitButton;

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
        QuitButton.onClick.AddListener(QuitGame);
    }

    private void Start()
    {
        HP_bar.fillAmount = 1;
        SP_bar.fillAmount = 1;
    }

    public void ToggleMenu(InputAction.CallbackContext context)
    {
        Menu.SetActive(Menu.activeSelf ? false : true);
    }

    public void ResumeGame()
    {
        Menu.SetActive(false);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainScene");
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
