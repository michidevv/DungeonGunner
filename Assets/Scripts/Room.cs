using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    bool closedWhenEntered, openWhenEnemiesCleared;
    [SerializeField]
    GameObject[] doors;
    [SerializeField]
    List<GameObject> enemies = new List<GameObject>();

    private bool isRoomActive;

    private void Update()
    {
        if (isRoomActive && openWhenEnemiesCleared && enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i -= 1;
                }
            }

            if (enemies.Count == 0)
            {
                ToggleDoors(false);
                closedWhenEntered = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isRoomActive = true;
            CameraController.instance.ChangeTarget(transform);

            if (closedWhenEntered)
            {
                ToggleDoors(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isRoomActive = false;
        }
    }

    private void ToggleDoors(bool isActive)
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(isActive);
        }
    }
}
