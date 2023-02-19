using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeButton : MonoBehaviour
{
    public int CleanPlayerDiceNum => cleanPlayerDiceNum;
    public int CleanEnemyDiceNum => cleanEnemyDiceNum;
    int cleanPlayerDiceNum, cleanEnemyDiceNum;

    public bool PlayerTurn => playerTurn;
    public bool DiceStart => diceStart;
    bool diceChang, playerTurn, diceStart;
    bool calculateActive;

    // Start is called before the first frame update
    void Start(){
        playerTurn = true;
    }

    // Update is called once per frame
    void Update(){
        diceChang = GameObject.Find("playerCoin").GetComponent<ClonCoinLimit>().DiceChang;
        calculateActive = GameObject.Find("playerCoin").GetComponent<ClonCoinLimit>().CalculateActive;
        if (!playerTurn){
            cleanEnemyDiceNum = -2;
            playerTurn = GameObject.Find("startButton").GetComponent<Calculate>().PlayerTurn;
        }
    }

    void OnMouseDown(){
        if (diceChang){ // �ֻ��� ���� ������ ���� ���� ������ �� �ٲ��.
            if(!calculateActive)
                if (playerTurn){
                    cleanPlayerDiceNum = -2;
                    playerTurn = false;
                    Debug.Log("Enemy!");
                }
                //else{
                //    cleanEnemyDiceNum = -2;
                //    playerTurn = true;
                //    Debug.Log("Player!");
                //}
        }
        else
            Debug.Log("������ ���ڸ���.");
    }
}
