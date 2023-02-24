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

    public int AttackResult => attackResult;
    int attackResult;

    // Start is called before the first frame update
    void Start(){
        attackResult = 1;
        if (attackResult == 0)
            playerTurn = true;
        else if (attackResult == 1)
            playerTurn = false;
    }

    // Update is called once per frame
    void Update(){
        diceChang = GameObject.Find("playerCoin").GetComponent<ClonCoinLimit>().DiceChang;
        calculateActive = GameObject.Find("playerCoin").GetComponent<ClonCoinLimit>().CalculateActive;
        if (attackResult == 0)
            if (!playerTurn){
                cleanEnemyDiceNum = -2;
                playerTurn = GameObject.Find("startButton").GetComponent<Calculate>().PlayerTurn;
            }
        else if(attackResult == 1)
            if(playerTurn){
                cleanPlayerDiceNum = -2;
                playerTurn = GameObject.Find("startButton").GetComponent<Calculate>().PlayerTurn;
            }
    }

    void OnMouseDown(){
        if (diceChang){ // 주사위 값과 생성된 코인 수가 동일할 때 바뀐다.
            if(!calculateActive)
                if (attackResult == 0){
                    if (playerTurn){
                        cleanPlayerDiceNum = -2;
                        playerTurn = false;
                        Debug.Log("Enemy!");
                    }
                }
                else if (attackResult == 1){
                    if (!playerTurn){
                        cleanEnemyDiceNum = -2;
                        playerTurn = true;
                        Debug.Log("Player!");
                    }
                }
        }
        else
            Debug.Log("코인이 모자르다.");
    }
}
