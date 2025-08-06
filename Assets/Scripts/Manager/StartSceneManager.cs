using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    public Button PlayButton;
    public Button SettingButton;
    public Button QuitButton;

    private void Awake()
    {
        PlayButton.onClick.AddListener(Play);
        SettingButton.onClick.AddListener(Setting);
        QuitButton.onClick.AddListener(Quit);
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
