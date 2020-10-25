using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField]
    float waitForKey = 2f;
    [SerializeField]    
    GameObject keyText;
    [SerializeField]
    string mainMenuScene;

    private WaitForSeconds waitForKeyTimer;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        if (waitForKey > 0)
        {
            keyText.SetActive(false);
            waitForKeyTimer = new WaitForSeconds(waitForKey);
            StartCoroutine(ShowKeyTextAfter());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (keyText.activeSelf && Input.anyKeyDown)
        {
            SceneManager.LoadScene(mainMenuScene);
        }
    }

    private IEnumerator ShowKeyTextAfter()
    {
        yield return waitForKeyTimer;
        keyText.SetActive(true);
    }
}
