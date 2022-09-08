using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragToCreatCoin : MonoBehaviour
{
    // Start is called before the first frame update
    private string objName;
    void Awake()
    {
        objName = this.gameObject.name;
    }
    void Start()
    {
    }

    void OnMouseDrag()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }
}
