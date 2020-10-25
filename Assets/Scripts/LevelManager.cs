using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField]
    float waitBeforeLoad = 4f;
    [SerializeField]
    string nextLevel;

    private bool isPaused;
    public bool IsPaused
    {
        get => isPaused;
        set 
        {
            isPaused = value;
            UIController.instance.pauseMenu.SetActive(value);
            Time.timeScale = value ? 0f : 1f;
        }
    }

    private WaitForSeconds nextLevelWait;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        nextLevelWait = new WaitForSeconds(waitBeforeLoad);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) IsPaused = !IsPaused;
    }

    public IEnumerator EndLevel()
    {
        PlayerController.instance.StopMovement();
        AudioManager.instance.PlayLevelWin();
        UIController.instance.FadeState = FadeState.Out;

        yield return nextLevelWait;

        SceneManager.LoadScene(nextLevel);
    }
}
