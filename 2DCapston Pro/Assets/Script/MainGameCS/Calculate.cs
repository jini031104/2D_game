using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calculate : MonoBehaviour
{
    int[] pCoin = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
    int[] eCoin = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
    int[] count = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

    int[] pCoinLeft = new int[7] { 0, 0, 0, 0, 0, 0, 0 };
    int[] eCoinLeft = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

    int coinLeft, playerHP, enemyHP, damage;

    string[] coinName = new string[] { "coin1", "coin2", "coin3", "coin4", "coin5", "coin6", "coin-" };
    string[] deleteCoinName = new string[] { "clonCoin1(Clone)", "clonCoin2(Clone)", "clonCoin3(Clone)", "clonCoin4(Clone)", "clonCoin5(Clone)", "clonCoin6(Clone)", "clonCoin-(Clone)" };
    string[] deleteEnemyCoinName = new string[] { "eClon1(Clone)", "eClon2(Clone)", "eClon3(Clone)", "eClon4(Clone)", "eClon5(Clone)", "eClon6(Clone)", "eClon-(Clone)" };

    public bool DeleteOK => deleteOK;
    bool attack, deleteOK;

    // Start is called before the first frame update
    void Start(){
        coinLeft = 0;
        playerHP = (int)GameObject.Find("playerHP").GetComponent<Slider>().value;
        enemyHP = (int)GameObject.Find("enemyHP").GetComponent<Slider>().value;
        damage = 0;
        deleteOK = false;
    }

    // Update is called once per frame
    void Update(){
        attack = GameObject.Find("delete_Coin").GetComponent<delete_Coin>().Attack;

        if(attack)
            for (int i=0; i<6; i++)
                pCoin[i] = GameObject.Find(coinName[i]).GetComponent<MakeCoin>().PCoin[i];
        else
            for (int i = 0; i < 7; i++)
                pCoin[i] = GameObject.Find(coinName[i]).GetComponent<MakeCoin>().PCoin[i];

        eCoin = GameObject.Find("enemyCoin").GetComponent<EnemyMakeCoin>().ECoin;

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
                if (pCoin[i] > eCoin[i]){   // �÷��̾� ������ �� ����.
                    if (count[i] < eCoin[i]){
                        Destroy(GameObject.Find(deleteCoinName[i]));
                        Destroy(GameObject.Find(deleteEnemyCoinName[i]));
                        count[i]++;
                    }
                }
                else if (pCoin[i] < eCoin[i]){  // �� ������ �� ����.
                    if (count[i] < pCoin[i]){
                        Destroy(GameObject.Find(deleteCoinName[i]));
                        Destroy(GameObject.Find(deleteEnemyCoinName[i]));
                        count[i]++;
                    }
                }
                else if (pCoin[i] == eCoin[i]){ // �� ���ΰ� �÷��̾� ������ �����ϴ�.
                    if (count[i] < pCoin[i]){
                        Destroy(GameObject.Find(deleteCoinName[i]));
                        Destroy(GameObject.Find(deleteEnemyCoinName[i]));
                        count[i]++;
                    }
                }
                if (i == 6)
                    deleteOK = false;
            }
        }
    }

    void OnMouseDown(){
        Debug.Log("�÷��̾� �ֻ���1:" + pCoin[0] + "  �ֻ���2:" + pCoin[1] + "  �ֻ���3:" + pCoin[2] + "  �ֻ���4:" + pCoin[3] + "  �ֻ���5:" + pCoin[4] + "  �ֻ���6:" + pCoin[5] + "  �ֻ���-:" + pCoin[6]);
        Debug.Log("�� �ֻ���1:" + eCoin[0] + "  �ֻ���2:" + eCoin[1] + "  �ֻ���3:" + eCoin[2] + "  �ֻ���4:" + eCoin[3] + "  �ֻ���5:" + eCoin[4] + "  �ֻ���6:" + eCoin[5] + "  �ֻ���-:" + eCoin[6]);

        for(int i = 0; i < 7; i++){
            if (pCoin[i] > eCoin[i]){   // �÷��̾� ������ �� ����.
                coinLeft = pCoin[i] - eCoin[i];
                Debug.Log("�÷��̾�" + coinName[i] + " ������ " + coinLeft + "�� �� ����.");
                pCoinLeft[i] = coinLeft;
            }
            else if (pCoin[i] < eCoin[i]){  // �� ������ �� ����.
                coinLeft = eCoin[i] - pCoin[i];
                Debug.Log("��" + coinName[i] + "������ " + coinLeft + "�� �� ����.");
                eCoinLeft[i] = coinLeft;
            }
            else if (pCoin[i] == eCoin[i] && 0< pCoin[i] && 0 < eCoin[i]){
                Debug.Log(coinName[i] + "������ ������ ����.");
                pCoinLeft[i] = 0;
                eCoinLeft[i] = 0;
            }
        }
        deleteOK = true;
    }

    void OnMouseUp(){
        Debug.Log("-------------------------------------------------------------------------------");
        Debug.Log("���� �ִ� �÷��̾� �ֻ���1:" + pCoinLeft[0] + "  �ֻ���2:" + pCoinLeft[1] + "  �ֻ���3:" + pCoinLeft[2] + "  �ֻ���4:" + pCoinLeft[3] + "  �ֻ���5:" + pCoinLeft[4] + "  �ֻ���6:" + pCoinLeft[5] + "  �ֻ���-:" + pCoinLeft[6]);
        Debug.Log("���� �ִ� �� �ֻ���1:" + eCoinLeft[0] + "  �ֻ���2:" + eCoinLeft[1] + "  �ֻ���3:" + eCoinLeft[2] + "  �ֻ���4:" + eCoinLeft[3] + "  �ֻ���5:" + eCoinLeft[4] + "  �ֻ���6:" + eCoinLeft[5] + "  �ֻ���-:" + eCoinLeft[6]);

        if (attack)
            for (int i = 0; i < 6; i++){
                damage = (i + 1) * pCoinLeft[i];
                enemyHP -= damage;
            }
        else
            for(int i=0; i<6; i++){
                damage = (i + 1) * eCoinLeft[i];
                playerHP -= damage;
            }

        Debug.Log("playerHP: " + playerHP);
        GameObject.Find("playerHP").GetComponent<Slider>().value = playerHP;

        Debug.Log("enemyHP: " + enemyHP);
        GameObject.Find("enemyHP").GetComponent<Slider>().value = enemyHP;
    }
}
