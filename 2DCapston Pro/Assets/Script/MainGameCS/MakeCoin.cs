using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCoin : MonoBehaviour
{
    [SerializeField]
    GameObject coinPrefab;
    [SerializeField]
    Transform spawnPoint;

    public bool CoinMakeOk => coinMakeOk;
    bool coinMakeOk;
    public int[] PCoin => pCoin;
    int[] pCoin = new int[7] { 0, 0, 0, 0, 0, 0, 0 };

    bool coinMakeClear;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        coinMakeOk = GameObject.Find("dice").GetComponent<DiceRot>().CoinMakeOk;    // 주사위를 던지면 코인을 만들 수 있다.
        coinMakeClear = GameObject.Find("playerCoin").GetComponent<ClonCoinLimit>().CoinMakeClear;
    }

    void OnMouseDown() {
        if (coinMakeOk && coinMakeClear){
            Instantiate(coinPrefab, spawnPoint.position, Quaternion.identity);
            switch (coinPrefab.name){
                case "clonCoin1":
                    pCoin[0]++;
                    break;
                case "clonCoin2":
                    pCoin[1]++;
                    break;
                case "clonCoin3":
                    pCoin[2]++;
                    break;
                case "clonCoin4":
                    pCoin[3]++;
                    break;
                case "clonCoin5":
                    pCoin[4]++;
                    break;
                case "clonCoin6":
                    pCoin[5]++;
                    break;
                case "clonCoin-":
                    pCoin[6]++;
                    break;
            }
        }
    }
}
