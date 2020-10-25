using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [SerializeField]
    string nextLevel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LevelManager.instance.EndLevel());
        }
    }
}
