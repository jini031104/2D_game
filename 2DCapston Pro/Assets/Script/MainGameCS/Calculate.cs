using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Calculate : MonoBehaviour
{
    int[] pCoin = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
    int[] eCoin = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
    int[] pCoinCopy = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
    int[] eCoinCopy = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

    int[] count = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

    int[] pCoinLeft = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
    int[] eCoinLeft = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

    [SerializeField]
    TextMeshProUGUI playerHpText;
    [SerializeField]
    TextMeshProUGUI enemyHpText;
    [SerializeField]
    TextMeshProUGUI turnCountText;

    public int PlayerHP => playerHP;
    public int EnemyHP => enemyHP;
    int coinLeft, playerHP, enemyHP, damage;

    string[] coinName = new string[] { "coin1", "coin2", "coin3", "coin4", "coin5", "coin6", "coin-" };
    string[] deleteCoinName = new string[] { "clonCoin1(Clone)", "clonCoin2(Clone)", "clonCoin3(Clone)", "clonCoin4(Clone)", "clonCoin5(Clone)", "clonCoin6(Clone)", "clonCoin-(Clone)" };
    string[] deleteEnemyCoinName = new string[] { "eClon1(Clone)", "eClon2(Clone)", "eClon3(Clone)", "eClon4(Clone)", "eClon5(Clone)", "eClon6(Clone)", "eClon-(Clone)" };

    public bool DeleteOK => deleteOK;
    public bool PlayerTurn => playerTurn;
    public bool DeleteCoinNum => deleteCoinNum;
    bool deleteOK, playerTurn, deleteCoinNum;

    public bool Attack => attack;
    public bool Defense => defense;
    bool attack, defense, AttackOrDefenseChangeOk;

    public bool CoinRotation => coinRotation;
    bool coinRotation;

    public static int turnCount;
    int attackResult, clickCount;

    public static bool playerWin, enemyWin;
    bool cardClick, playerTurnCheck;

    // Start is called before the first frame update
    void Start(){
        playerWin = false;
        enemyWin = false;
        clickCount = 0;
        turnCount = 0;
        coinLeft = 0;
        playerHP = (int)GameObject.Find("playerHP").GetComponent<Slider>().value;
        enemyHP = (int)GameObject.Find("enemyHP").GetComponent<Slider>().value;
        damage = 0;
        deleteOK = false;
        deleteCoinNum = false;
        attackResult = GameObject.Find("changeButton").GetComponent<ChangeButton>().AttackResult;
        if (attackResult == 0)
            playerTurn = false;
        else if (attackResult == 1)
            playerTurn = true;
        AttackOrDefenseChangeOk = false;
    }

    // Update is called once per frame
    void Update(){
        attack = GameObject.Find("delete_Coin").GetComponent<delete_Coin>().Attack;

        if (attack)
            for (int i=0; i<6; i++)
                pCoin[i] = GameObject.Find(coinName[i]).GetComponent<MakeCoin>().PCoin[i];
        else
            for (int i=0; i<7; i++)
                pCoin[i] = GameObject.Find(coinName[i]).GetComponent<MakeCoin>().PCoin[i];

        eCoin = GameObject.Find("enemyCoin").GetComponent<EnemyMakeCoin>().ECoin;

        cardClick = GameObject.Find("hpRecoveryCard").GetComponent<HpRecovery>().CardClick;
        playerTurnCheck = GameObject.Find("changeButton").GetComponent<ChangeButton>().PlayerTurn;
        if (cardClick){
            if (playerTurnCheck)
                playerHP = GameObject.Find("hpRecoveryCard").GetComponent<HpRecovery>().PlayerHP;
            else
                enemyHP = GameObject.Find("hpRecoveryCard").GetComponent<HpRecovery>().EnemyHP;
        }

        playerHpText.text = " " + playerHP;
        enemyHpText.text = " " + enemyHP;
        turnCountText.text = "Turn: " + turnCount;
        GameObject.Find("playerHP").GetComponent<Slider>().value = playerHP;
        GameObject.Find("enemyHP").GetComponent<Slider>().value = enemyHP;

        if (AttackOrDefenseChangeOk){
            AttackOrDefenseChange();
            AttackOrDefenseChangeOk = false;
        }

        if (playerHP <= 0){     // 적이 이김
            playerHP = 0;
            enemyWin = true;
            if (Input.GetMouseButtonDown(1))
                SceneManager.LoadScene("GameOver");
        }
        else if(enemyHP <= 0){  // 플레이어가 이김
            enemyHP = 0;
            playerWin = true;
            if (Input.GetMouseButtonDown(1))
                SceneManager.LoadScene("GameOver");
        }
    }

    void OnMouseDown(){
        CoinCheck();
        HpResult();
        clickCount++;
        if (clickCount % 2 == 0)
            turnCount++;

        deleteOK = true;
        deleteCoinNum = true;
        AttackOrDefenseChangeOk = true;
        coinRotation = true;

        if (attackResult == 0)
            playerTurn = true;
        else if (attackResult == 1)
            playerTurn = false;

        for (int i = 0; i < 7; i++)
            count[i] = 0;
    }

    void OnMouseUp(){
        if (attackResult == 0)
            playerTurn = false;
        else if (attackResult == 1)
            playerTurn = true;

        deleteCoinNum = false;
        coinRotation = false;

        Debug.Log("playerHP: " + playerHP + "             enemyHP: " + enemyHP);

        deleteOK = false;
    }

    void AttackOrDefenseChange(){
        if (attack){
            attack = false;
            defense = true;
        }
        else{
            attack = true;
            defense = false;
        }
    }

    void CoinCheck(){
        for (int i = 0; i < 7; i++){
            if (pCoin[6] == 0)
                pCoinCopy[i] = pCoin[i];
            else if (pCoin[6] == 1)
                if (pCoin[i] > 0)
                    pCoinCopy[i] = 6;

            if (eCoin[6] == 0)
                eCoinCopy[i] = eCoin[i];
            else if (eCoin[6] == 1)
                if (eCoin[i] > 0)
                    eCoinCopy[i] = 6;

            if (pCoinCopy[i] > eCoinCopy[i]){   // 플레이어 코인이 더 많다.
                coinLeft = pCoinCopy[i] - eCoinCopy[i];
                pCoinLeft[i] = coinLeft;
                eCoinLeft[i] = 0;
            }
            else if (pCoinCopy[i] < eCoinCopy[i]){  // 적 코인이 더 많다.
                coinLeft = eCoinCopy[i] - pCoinCopy[i];
                eCoinLeft[i] = coinLeft;
                pCoinLeft[i] = 0;
            }
            else if (pCoinCopy[i] == eCoinCopy[i]){
                pCoinLeft[i] = 0;
                eCoinLeft[i] = 0;
            }

            if (pCoinCopy[i] == 0)
                pCoinLeft[i] = 0;
            if (eCoinCopy[i] == 0)
                eCoinLeft[i] = 0;
        }
    }

    void HpResult(){
        if (attack)
            for (int i = 0; i < 6; i++){
                damage = (i + 1) * pCoinLeft[i];
                enemyHP -= damage;
            }
        else
            for (int i = 0; i < 6; i++){
                damage = (i + 1) * eCoinLeft[i];
                playerHP -= damage;
            }
    }
}
