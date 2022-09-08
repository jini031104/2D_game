using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRotation : MonoBehaviour
{
    Vector3 diceScale;
    Vector3 diceRotation;

    bool diceSmall = false;
    public bool startDice = false;
    bool makeCoinAfterDice;
    bool diceReplay;

    int[] diceVall = new int[] { 1, 2, 3, 4, 5, 6 };
    int[,] rotationVal = new int[,] { { 40, 135, 270 },
                                        {40, 45, 90 },
                                        {130, 45, 0 } };
    int w, v;
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
        diceReplay = GameObject.Find("coin").GetComponent<makeCoinLimit>().diceReplay;
        if (diceSmall == true)
        {
            if (transform.localScale.x > diceScale.x)
            {
                transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }
            else
            {
                diceSmall = false;
                transform.localEulerAngles = new Vector3(0, 0, 0);
                switch (indexVall)
                {
                    case 0:
                        transform.localEulerAngles = new Vector3(0, 180, 0);
                        break;
                    case 1:
                        transform.localEulerAngles = new Vector3(0, 90, -90);
                        break;
                    case 2:
                        transform.localEulerAngles = new Vector3(0, 270, 0);
                        break;
                    case 3:
                        transform.localEulerAngles = new Vector3(0, 90, 0);
                        break;
                    case 4:
                        transform.localEulerAngles = new Vector3(0, 90, -270);
                        break;
                    case 5:
                        transform.localEulerAngles = new Vector3(0, 0, 0);
                        break;
                }
            }
        }

        // If DiceNum and make CoinNum same, We have Dice roll replay check.
        makeCoinAfterDice = GameObject.Find("coin").GetComponent<makeCoinLimit>().startDiceCheck;
        if (!makeCoinAfterDice)
            startDice = false;
    }

    private void OnMouseDown()  // Dice Up
    {
        if (!diceReplay)
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void OnMouseUp() // Dice Rotation
    {
        if (!diceReplay)
        {
            index = Random.Range(0, w);
            transform.localEulerAngles = new Vector3(rotationVal[index, 0], rotationVal[index, 1], rotationVal[index, 2]);
            indexVall = Random.Range(0, v);
            diceSmall = true;
            startDice = true;
            // 주사위 클릭하면 생성되었던 클론들 삭제시키기
        }
    }
}
