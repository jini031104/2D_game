using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    int attackResult;

    // Start is called before the first frame update
    void Start(){
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

        playerHpText.text = " " + playerHP;
        enemyHpText.text = " " + enemyHP;

        //Invoke("CoinDelete", 20);
        //CoinDelete();
        if (AttackOrDefenseChangeOk){
            AttackOrDefenseChange();
            AttackOrDefenseChangeOk = false;
        }
    }

    void OnMouseDown(){
        CoinCheck();
        HpResult();

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
        GameObject.Find("playerHP").GetComponent<Slider>().value = playerHP;
        GameObject.Find("enemyHP").GetComponent<Slider>().value = enemyHP;

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

    void CoinDelete(){
        /*
         * 플레이어 코인이 더 많다.
         * - 적의 코인 수 만큼 플레이어&적 코인을 제거한다.
         * 
         * 적 코인이 더 많다.
         * - 플레이어의 코인 수 만큼 플레이어&적 코인을 제거한다.
         * 
         * 동일하다
         * - 플레이어&적 코인을 모두 제거한다.
         */
        if (deleteOK){
            for (int i = 0; i < 7; i++){
                if (pCoinCopy[i] > eCoinCopy[i]){   // 플레이어 코인이 더 많다.
                    if (count[i] < eCoinCopy[i]){
                        Destroy(GameObject.Find(deleteCoinName[i]));
                        Destroy(GameObject.Find(deleteEnemyCoinName[i]));
                        Debug.Log(deleteCoinName[i] + "    " + deleteEnemyCoinName[i] + "    삭제!");
                        count[i]++;
                    }
                }
                else if (pCoinCopy[i] < eCoinCopy[i]){  // 적 코인이 더 많다.
                    if (count[i] < pCoinCopy[i]){
                        Destroy(GameObject.Find(deleteCoinName[i]));
                        Destroy(GameObject.Find(deleteEnemyCoinName[i]));
                        Debug.Log(deleteCoinName[i] + "    " + deleteEnemyCoinName[i] + "    삭제!");
                        count[i]++;
                    }
                }
                else if (pCoinCopy[i] == eCoinCopy[i]){ // 적 코인과 플레이어 코인이 동일하다.
                    if (count[i] < pCoinCopy[i]){
                        Destroy(GameObject.Find(deleteCoinName[i]));
                        Destroy(GameObject.Find(deleteEnemyCoinName[i]));
                        Debug.Log(deleteCoinName[i] + "    " + deleteEnemyCoinName[i] + "    삭제!");
                        count[i]++;
                    }
                }
            }
        }
    }

    void CoinCheck(){
        for (int i = 0; i < 7; i++){
            pCoinCopy[i] = pCoin[i];
            eCoinCopy[i] = eCoin[i];

            if (pCoin[i] > eCoin[i]){   // 플레이어 코인이 더 많다.
                coinLeft = pCoin[i] - eCoin[i];
                pCoinLeft[i] = coinLeft;
                eCoinLeft[i] = 0;
            }
            else if (pCoin[i] < eCoin[i]){  // 적 코인이 더 많다.
                coinLeft = eCoin[i] - pCoin[i];
                eCoinLeft[i] = coinLeft;
                pCoinLeft[i] = 0;
            }
            else if (pCoin[i] == eCoin[i]){
                pCoinLeft[i] = 0;
                eCoinLeft[i] = 0;
            }

            if (pCoin[i] == 0)
                pCoinLeft[i] = 0;
            if (eCoin[i] == 0)
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
