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

    bool playerDice = true;
    bool enemyDice = false;

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
        
        if(playerDice){
            Debug.Log("Player!");
            SmallDice();
        }
        else if(enemyDice){
            Debug.Log("Enemy!");
            SmallDice();
        }

        // If DiceNum and make CoinNum same, We have Dice roll replay check.
        makeCoinAfterDice = GameObject.Find("coin").GetComponent<makeCoinLimit>().startDiceCheck;
        if (!makeCoinAfterDice){
            startDice = false;
        }
    }

    private void OnMouseDown()  // Dice Up
    {
        if (!diceReplay){
            if(playerDice)
                BigDice();
            else if(enemyDice)
                BigDice();
        }
    }

    public void OnMouseUp() // Dice Rotation
    {
        if (!diceReplay)
            if(playerDice)
                DiceDrpoAndValSet();
            else if(enemyDice)
                DiceDrpoAndValSet();
            // 주사위 클릭하면 생성되었던 클론들 삭제시키기
    }

    // 주사위의 값을 결정 및 플레이어 <-> 적 주사위 교체
    void SmallDice(){
        if(diceSmall){
            if(transform.localScale.x > diceScale.x)
                transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            else{
                diceSmall = false;
                //transform.localEulerAngles = new Vector3(0, 0, 0);
                DiceResult(indexVall);  // 주사위 값을 보여줌.
                //DicePlayerOrEnemyChange();  // 플레이어 <-> 적 변경
            }
        }
    }

    void DiceDrpoAndValSet(){
        index = Random.Range(0, w);
        transform.localEulerAngles = new Vector3(rotationVal[index, 0], rotationVal[index, 1], rotationVal[index, 2]);
        indexVall = Random.Range(0, v); // 1~6 사이의 주사위 값 결정.
        diceSmall = true;
        startDice = true;
    }

    void DicePlayerOrEnemyChange(){
        if (playerDice){
            playerDice = false;
            enemyDice = true;
        }
        else if(enemyDice){
            playerDice = true;
            enemyDice = false;
        }
    }

    void DiceResult(int valIndex){
        switch (valIndex){
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

    void BigDice(){
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
}
