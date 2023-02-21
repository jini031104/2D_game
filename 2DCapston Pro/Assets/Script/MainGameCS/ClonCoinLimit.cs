using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonCoinLimit : MonoBehaviour
{
    GameObject[] clonCoinTag;
    GameObject[] enemyClonCoinTag;

    int playerDiceNumVall, enemyDiceNumVall;
    int pDiceNum, eDiceNum;

    public bool CoinMakeClear => coinMakeClear;
    public bool EnemyCoinMakeClear => enemyCoinMakeClear;
    bool coinMakeClear, enemyCoinMakeClear;
    public bool DiceChang => diceChang;
    bool diceChang;

    public bool CalculateActive => calculateActive;
    bool calculateActive;

    // Start is called before the first frame update
    void Start(){
        coinMakeClear = true;
        enemyCoinMakeClear = true;
        calculateActive = false;
    }

    // Update is called once per frame
    void Update(){
        clonCoinTag = GameObject.FindGameObjectsWithTag("ClonCoin");
        enemyClonCoinTag = GameObject.FindGameObjectsWithTag("eClonCoin");

        playerDiceNumVall = GameObject.Find("dice").GetComponent<DiceRot>().PlayerDiceNumVall;
        enemyDiceNumVall = GameObject.Find("dice").GetComponent<DiceRot>().EnemyDiceNumVall;

        pDiceNum = playerDiceNumVall + 1;
        eDiceNum = enemyDiceNumVall + 1;

        if (clonCoinTag.Length == pDiceNum){    // �ֻ��� ���� ������ ���� ���� ������ �� �ٲ��.
            coinMakeClear = false;
            diceChang = true;  // ChangeButton ��ũ��Ʈ�� ������.
        }
        else if(enemyClonCoinTag.Length == eDiceNum){
            enemyCoinMakeClear = false;
            diceChang = true;
            //calculateActive = true;
        }
        else{
            diceChang = false;
            coinMakeClear = true;
            calculateActive = false;
        }
    }
}