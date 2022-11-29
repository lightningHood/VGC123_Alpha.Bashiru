using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [Header("Button")]
    public Button startButton;
    public Button settingsButton;
    public Button quitButton;
    public Button returnToMenuButton;
    public Button returnToGameButton;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject pauseMenu;

    [Header("Text")]
    public Text livesText;
    public Text volSliderText;

    [Header("Slider")]
    public Slider volSlider;


    void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    void ShowSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    void ShowMainMenu()
    {
        if (SceneManager.GetActiveScene().name == "Level")
        {
            SceneManager.LoadScene("Title");
        }
        else
        {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
        }
    }

    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }

    void OnSliderValueChanged(float value)
    {
        if (volSliderText)
            volSliderText.text = value.ToString();
    }

    void ResumeGame()
    {
        //unpause the game
        pauseMenu.SetActive(false);
    }

    void UpdateLifeText(int value)
    {
        livesText.text = value.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (startButton)
            startButton.onClick.AddListener(StartGame);

        if (settingsButton)
            settingsButton.onClick.AddListener(ShowSettingsMenu);

        if (quitButton)
            quitButton.onClick.AddListener(QuitGame);

        if (returnToMenuButton)
            returnToMenuButton.onClick.AddListener(ShowMainMenu);

        if (returnToGameButton)
            returnToGameButton.onClick.AddListener(ResumeGame);

        if (volSlider)
            volSlider.onValueChanged.AddListener(OnSliderValueChanged);

        if (livesText)
            GameManager.instance.onLifeValueChanged.AddListener(UpdateLifeText);
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenu)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);

                //HINT
                if (pauseMenu.activeSelf)
                {
                    //do something to pause
                }
                else
                {
                    //do something to unpause
                }
            }
        }
    }
}
