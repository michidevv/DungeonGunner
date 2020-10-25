using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSortOrder : MonoBehaviour
{
    SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.sortingOrder = Mathf.RoundToInt(transform.position.y * -10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
