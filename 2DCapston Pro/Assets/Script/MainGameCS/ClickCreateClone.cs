using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCreateClone : MonoBehaviour
{
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private Transform spawnPoint;

    public int makeNum;
    bool startDiceCheck;
    bool makeCoinAfterDice;

    // Start is called before the first frame update
    void Start()
    {
        makeNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // If DiceNum and make CoinNum same, We have Dice roll replay check.
        makeCoinAfterDice = GameObject.Find("coin").GetComponent<makeCoinLimit>().startDiceCheck;
        if (!makeCoinAfterDice)
            makeNum = 0;
    }
    public void OnMouseDown()
    {
        if(makeCoinAfterDice){
            GameObject clone = Instantiate(coinPrefab, spawnPoint.position, Quaternion.identity);
            makeNum++;
        }
    }
}
