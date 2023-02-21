using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRot : MonoBehaviour
{
    Vector3 diceScale;

    int[] diceVall = new int[] { 1, 2, 3, 4, 5, 6 };
    int[,] rotationVal = new int[,] { { 40, 135, 270},
                                        {40, 45, 90 },
                                        {130, 45, 0 }};
    int rVall, dVall;
    int cleanPlayerDiceNum, cleanEnemyDiceNum;

    public int PlayerDiceNumVall => playerDiceNumVall;
    public int EnemyDiceNumVall => enemyDiceNumVall;
    int playerDiceNumVall, enemyDiceNumVall;
    int diceRotIndex, diceNumVall;

    bool diceSmall = false;

    public bool CoinMakeOk => coinMakeOk;
    public bool EnemyCoinMakeOk => enemyCoinMakeOk;
    bool coinMakeOk, enemyCoinMakeOk;

    bool playerTurn, diceStart;
    bool diceChang, reOK;
    bool diceRePlay = true;

    //bool startDice;

    // Start is called before the first frame update
    void Start(){
        diceScale = this.gameObject.transform.localScale;
        dVall = diceVall.GetLength(0);
        rVall = rotationVal.GetLength(0);

        Debug.Log("Player!");

        coinMakeOk = false;
        enemyCoinMakeOk = false;
        diceStart = true;
    }

    // Update is called once per frame
    void Update(){
        playerTurn = GameObject.Find("changeButton").GetComponent<ChangeButton>().PlayerTurn;
        if (playerTurn){
            cleanEnemyDiceNum = GameObject.Find("changeButton").GetComponent<ChangeButton>().CleanEnemyDiceNum;
            if (cleanEnemyDiceNum < 0){
                enemyDiceNumVall = cleanEnemyDiceNum;
                if (!diceRePlay){
                    diceRePlay = true;
                    diceStart = true;
                }
            }
        }
        else{
            cleanPlayerDiceNum = GameObject.Find("changeButton").GetComponent<ChangeButton>().CleanPlayerDiceNum;
            if (cleanPlayerDiceNum < 0){
                playerDiceNumVall = cleanPlayerDiceNum;
                if (!diceStart){
                    if (diceRePlay){
                        diceStart = true;
                    }
                }
            }
        }
        SmallDice();
    }
    void OnMouseDown(){
        if (diceStart){
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            coinMakeOk = true;
            if (!playerTurn){
                enemyCoinMakeOk = true;
                diceRePlay = false;
            }
        }
    }

    void OnMouseUp(){
        if (diceStart){
            DiceDrpoAndValSet();
            diceStart = false;
        }
    }

    void DiceDrpoAndValSet(){    // ȸ���� ���� ������.
        diceRotIndex = Random.Range(0, rVall);
        transform.localEulerAngles = new Vector3(rotationVal[diceRotIndex, 0], rotationVal[diceRotIndex, 1], rotationVal[diceRotIndex, 2]);

        diceSmall = true;
        if (playerTurn){
            playerDiceNumVall = Random.Range(0, dVall);
            diceNumVall = playerDiceNumVall;
        }
        else{
            enemyDiceNumVall = Random.Range(0, dVall);
            diceNumVall = enemyDiceNumVall;
        }
    }

    void SmallDice(){
        if (diceSmall){
            if(transform.localScale.x > diceScale.x){
                transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }
            else{
                DiceResult(diceNumVall);
                diceSmall = false;
            }
        }
    }

    void DiceResult(int valIndex){      // �ֻ��� ���� ���� ���� ������
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
}