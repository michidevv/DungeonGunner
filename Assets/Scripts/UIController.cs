using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum FadeState
{
    In,
    Out,
    None
}

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Slider healthSlider;
    public Text healthText;
    public GameObject gameOverScreen;
    public Image dashImage;
    public Image fadeScreen;
    public GameObject pauseMenu;

    [SerializeField]
    float fadeSpeed;
    [SerializeField]
    string newGameScene, mainMenuScene;

    private FadeState fadeState;
    [HideInInspector]
    public FadeState FadeState 
    {
        get
        {
            return fadeState;
        }
        set
        {
            fadeState = value;
            fadeScreen.raycastTarget = value != FadeState.None;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        FadeState = FadeState.In;
    }

    private void Update()
    {
        if (FadeState == FadeState.In)
        {
            FadeScreen(0f);
            if (fadeScreen.color.a == 0f) FadeState = FadeState.None;
        } else if (FadeState == FadeState.Out) {
            FadeScreen(1f);
        }
    }

    private void FadeScreen(float target)
    {
        fadeScreen.color = new Color(
                fadeScreen.color.r,
                fadeScreen.color.g,
                fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, target, Time.deltaTime * fadeSpeed)
            );
    }

    public void UpdateHealthUI(float health, float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        healthText.text = $"{health} / {maxHealth}";
    }

    public void SetDashOpacity(float opacity)
    {
        Color c = dashImage.color;
        dashImage.color = new Color(c.r, c.g, c.b, opacity);
    }

    public void NewGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(newGameScene);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        LevelManager.instance.IsPaused = false;
    }
}
