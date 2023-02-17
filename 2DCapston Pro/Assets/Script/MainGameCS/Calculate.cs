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
                if (pCoin[i] > eCoin[i]){   // 플레이어 코인이 더 많다.
                    if (count[i] < eCoin[i]){
                        Destroy(GameObject.Find(deleteCoinName[i]));
                        Destroy(GameObject.Find(deleteEnemyCoinName[i]));
                        count[i]++;
                    }
                }
                else if (pCoin[i] < eCoin[i]){  // 적 코인이 더 많다.
                    if (count[i] < pCoin[i]){
                        Destroy(GameObject.Find(deleteCoinName[i]));
                        Destroy(GameObject.Find(deleteEnemyCoinName[i]));
                        count[i]++;
                    }
                }
                else if (pCoin[i] == eCoin[i]){ // 적 코인과 플레이어 코인이 동일하다.
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
        Debug.Log("플레이어 주사위1:" + pCoin[0] + "  주사위2:" + pCoin[1] + "  주사위3:" + pCoin[2] + "  주사위4:" + pCoin[3] + "  주사위5:" + pCoin[4] + "  주사위6:" + pCoin[5] + "  주사위-:" + pCoin[6]);
        Debug.Log("적 주사위1:" + eCoin[0] + "  주사위2:" + eCoin[1] + "  주사위3:" + eCoin[2] + "  주사위4:" + eCoin[3] + "  주사위5:" + eCoin[4] + "  주사위6:" + eCoin[5] + "  주사위-:" + eCoin[6]);

        for(int i = 0; i < 7; i++){
            if (pCoin[i] > eCoin[i]){   // 플레이어 코인이 더 많다.
                coinLeft = pCoin[i] - eCoin[i];
                Debug.Log("플레이어" + coinName[i] + " 코인이 " + coinLeft + "개 더 많다.");
                pCoinLeft[i] = coinLeft;
            }
            else if (pCoin[i] < eCoin[i]){  // 적 코인이 더 많다.
                coinLeft = eCoin[i] - pCoin[i];
                Debug.Log("적" + coinName[i] + "코인이 " + coinLeft + "개 더 많다.");
                eCoinLeft[i] = coinLeft;
            }
            else if (pCoin[i] == eCoin[i] && 0< pCoin[i] && 0 < eCoin[i]){
                Debug.Log(coinName[i] + "코인의 개수는 같다.");
                pCoinLeft[i] = 0;
                eCoinLeft[i] = 0;
            }
        }
        deleteOK = true;
    }

    void OnMouseUp(){
        Debug.Log("-------------------------------------------------------------------------------");
        Debug.Log("남아 있는 플레이어 주사위1:" + pCoinLeft[0] + "  주사위2:" + pCoinLeft[1] + "  주사위3:" + pCoinLeft[2] + "  주사위4:" + pCoinLeft[3] + "  주사위5:" + pCoinLeft[4] + "  주사위6:" + pCoinLeft[5] + "  주사위-:" + pCoinLeft[6]);
        Debug.Log("남아 있는 적 주사위1:" + eCoinLeft[0] + "  주사위2:" + eCoinLeft[1] + "  주사위3:" + eCoinLeft[2] + "  주사위4:" + eCoinLeft[3] + "  주사위5:" + eCoinLeft[4] + "  주사위6:" + eCoinLeft[5] + "  주사위-:" + eCoinLeft[6]);

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
