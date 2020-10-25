using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LevelDirection
{
    up,
    right,
    down,
    left
}

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject layoutRoom;
    [SerializeField]
    Color startColor, endColor;
    [SerializeField]
    int distanceToEnd;
    [SerializeField]
    Transform generationPoint;
    [SerializeField]
    LevelDirection selectedDirection;
    [SerializeField]
    float xOffset = 18f, yOffset = 10f;
    [SerializeField]
    LayerMask whatIsRoom;
    [SerializeField]
    RoomPrefabs roomPrefabs;

    private GameObject endRoom;
    private List<GameObject> layoutRoomObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject room = Instantiate(layoutRoom, generationPoint.position, generationPoint.rotation);
        room.GetComponent<SpriteRenderer>().color = startColor;

        for (int i = 0; i < distanceToEnd; i++)
        {
            selectedDirection = (LevelDirection)Random.Range(0, 4);

            while (Physics2D.OverlapCircle(generationPoint.position, 0.2f, whatIsRoom)) MoveGenerationPoint();

            GameObject r = Instantiate(layoutRoom, generationPoint.position, generationPoint.rotation);
            if (i == distanceToEnd - 1)
            {
                r.GetComponent<SpriteRenderer>().color = endColor;
                endRoom = r;
            } else
            {
                layoutRoomObjects.Add(r); // All except first and last rooms.
            }
        }

        CreateRoomOutline(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void MoveGenerationPoint()
    {
        switch (selectedDirection)
        {
            case LevelDirection.up:
                generationPoint.position += new Vector3(0f, yOffset, 0f);
                break;
            case LevelDirection.right:
                generationPoint.position += new Vector3(xOffset, 0f, 0f);
                break;
            case LevelDirection.down:
                generationPoint.position += new Vector3(0f, -yOffset, 0f);
                break;
            case LevelDirection.left:
                generationPoint.position += new Vector3(-xOffset, 0f, 0f);
                break;
            default:
                break;
        }
    }

    public void CreateRoomOutline(Vector3 roomPosition)
    {
        
    }
}

[System.Serializable]
public class RoomPrefabs
{
    [SerializeField]
    GameObject singleTop, singleBottom, singleLeft, singleRight,
        doubleTB, doubleTL, doubleTR, doubleLR, doubleBR, doubleBL,
        tripleTBR, tripleBLR, tripleTBL, tripleTLR, fourway;
}
