using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calculate : MonoBehaviour
{
    int[] pCoin = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
    int[] eCoin = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
    int[] pCoinCopy = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
    int[] eCoinCopy = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

    int[] count = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

    int[] pCoinLeft = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
    int[] eCoinLeft = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

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
    bool attack, defense;

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

        CoinDelete();
    }

    void OnMouseDown(){
        //Debug.Log("�÷��̾� ����1:" + pCoin[0] + "    ����2:" + pCoin[1] + "    ����3:" + pCoin[2] + "    ����4:" + pCoin[3] + "    ����5:" + pCoin[4] + "    ����6:" + pCoin[5] + "    ����-:" + pCoin[6]);
        //Debug.Log("��       ����1:" + eCoin[0] + "    ����2:" + eCoin[1] + "    ����3:" + eCoin[2] + "    ����4:" + eCoin[3] + "    ����5:" + eCoin[4] + "    ����6:" + eCoin[5] + "    ����-:" + eCoin[6]);
        CoinCheck();
        HpResult();

        deleteOK = true;
        deleteCoinNum = true;

        if (attackResult == 0)
            playerTurn = true;
        else if (attackResult == 1)
            playerTurn = false;
    }

    void OnMouseUp(){
        if (attackResult == 0)
            playerTurn = false;
        else if (attackResult == 1)
            playerTurn = true;
        deleteCoinNum = false;
        //Debug.Log("���� �ִ� �÷��̾� ����1:" + pCoinLeft[0] + "    ����2:" + pCoinLeft[1] + "    ����3:" + pCoinLeft[2] + "    ����4:" + pCoinLeft[3] + "    ����5:" + pCoinLeft[4] + "    ����6:" + pCoinLeft[5] + "    ����-:" + pCoinLeft[6]);
        //Debug.Log("���� �ִ� ��       ����1:" + eCoinLeft[0] + "    ����2:" + eCoinLeft[1] + "    ����3:" + eCoinLeft[2] + "    ����4:" + eCoinLeft[3] + "    ����5:" + eCoinLeft[4] + "    ����6:" + eCoinLeft[5] + "    ����-:" + eCoinLeft[6]);

        Debug.Log("playerHP: " + playerHP + "             enemyHP: " + enemyHP);
        GameObject.Find("playerHP").GetComponent<Slider>().value = playerHP;
        GameObject.Find("enemyHP").GetComponent<Slider>().value = enemyHP;
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
         * �÷��̾� ������ �� ����.
         * - ���� ���� �� ��ŭ �÷��̾�&�� ������ �����Ѵ�.
         * 
         * �� ������ �� ����.
         * - �÷��̾��� ���� �� ��ŭ �÷��̾�&�� ������ �����Ѵ�.
         * 
         * �����ϴ�
         * - �÷��̾�&�� ������ ��� �����Ѵ�.
         */
        if (deleteOK){
            for (int i = 0; i < 7; i++){
                count[i] = 0;
                if (pCoinCopy[i] > eCoinCopy[i]){   // �÷��̾� ������ �� ����.
                    if (count[i] < eCoinCopy[i]){
                        Destroy(GameObject.Find(deleteCoinName[i]));
                        Destroy(GameObject.Find(deleteEnemyCoinName[i]));
                        Debug.Log(deleteCoinName[i] + "    " + deleteEnemyCoinName[i] + "    ����!");
                        count[i]++;
                    }
                }
                else if (pCoinCopy[i] < eCoinCopy[i]){  // �� ������ �� ����.
                    if (count[i] < pCoinCopy[i]){
                        Destroy(GameObject.Find(deleteCoinName[i]));
                        Destroy(GameObject.Find(deleteEnemyCoinName[i]));
                        Debug.Log(deleteCoinName[i] + "    " + deleteEnemyCoinName[i] + "    ����!");
                        count[i]++;
                    }
                }
                else if (pCoinCopy[i] == eCoinCopy[i]){ // �� ���ΰ� �÷��̾� ������ �����ϴ�.
                    if (count[i] < pCoinCopy[i]){
                        Destroy(GameObject.Find(deleteCoinName[i]));
                        Destroy(GameObject.Find(deleteEnemyCoinName[i]));
                        Debug.Log(deleteCoinName[i] + "    " + deleteEnemyCoinName[i] + "    ����!");
                        count[i]++;
                    }
                }
                if (i == 6)
                    deleteOK = false;
            }

            AttackOrDefenseChange();
        }
    }

    void CoinCheck(){
        for (int i = 0; i < 7; i++){
            pCoinCopy[i] = pCoin[i];
            eCoinCopy[i] = eCoin[i];

            if (pCoin[i] > eCoin[i]){   // �÷��̾� ������ �� ����.
                coinLeft = pCoin[i] - eCoin[i];
                pCoinLeft[i] = coinLeft;
                eCoinLeft[i] = 0;
            }
            else if (pCoin[i] < eCoin[i]){  // �� ������ �� ����.
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
