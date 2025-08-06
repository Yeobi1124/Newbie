using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("PlayerInfo")]
    public Slider HP_bar;
    public Slider MP_bar;

    [Header("Menu")]
    public GameObject Menu;
    public Slider BGM_Volume;
    public Slider SE_Volume;
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

        QuitButton.onClick.AddListener(QuitGame);
    }

    public void ToggleMenu(InputAction.CallbackContext context)
    {
        Menu.SetActive(Menu.activeSelf ? false : true);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
