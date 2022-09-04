using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRotation : MonoBehaviour
{
    Vector3 diceScale;
    Vector3 diceRotation;
    bool diceSmall = false;
    public bool startDice = false;
    int[] diceVall = new int[] { 1, 2, 3, 4, 5, 6 };
    int[,] rotationVal = new int[,] { { 40, 135, 270 },
                                        {40, 45, 90 },
                                        {130, 45, 0 } };
    int w, h, v;
    public int index, indexVall;

    // Start is called before the first frame update
    void Start()
    {
        diceScale = this.gameObject.transform.localScale;

        w = rotationVal.GetLength(0);
        v = diceVall.GetLength(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (diceSmall == true)
        {
            if (transform.localScale.x > diceScale.x)
            {
                transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
                // index = Random.Range(0, w);
                // transform.localEulerAngles = new Vector3(rotationVal[index, 0], rotationVal[index, 1], rotationVal[index, 2]);
            }
            else
            {
                diceSmall = false;
                transform.localEulerAngles = new Vector3(0, 0, 0);
                switch(indexVall) {
                    case 0:
                        Debug.Log("1");
                        transform.localEulerAngles = new Vector3(0, 180, 0);
                        break;
                    case 1:
                        Debug.Log("2");
                        transform.localEulerAngles = new Vector3(0, 90, -90);
                        break;
                    case 2:
                        Debug.Log("3");
                        transform.localEulerAngles = new Vector3(0, 270, 0);
                        break;
                    case 3:
                        Debug.Log("4");
                        transform.localEulerAngles = new Vector3(0, 90, 0);
                        break;
                    case 4:
                        Debug.Log("5");
                        transform.localEulerAngles = new Vector3(0, 90, -270);
                        break;
                    case 5:
                        Debug.Log("6");
                        transform.localEulerAngles = new Vector3(0, 0, 0);
                        break;
                }
            }
        }
    }

    private void OnMouseDown()  // 주사위 들어올림
    {
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void OnMouseUp() // 주사위 회전
    {
        index = Random.Range(0, w);
        transform.localEulerAngles = new Vector3(rotationVal[index, 0], rotationVal[index, 1], rotationVal[index, 2]);
        indexVall = Random.Range(0, v);
        diceSmall = true;
        startDice = true;
    }
}
