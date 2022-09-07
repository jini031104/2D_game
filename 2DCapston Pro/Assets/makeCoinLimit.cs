using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 여기서 코인 만들기 개수 제한할거임.
public class makeCoinLimit : MonoBehaviour
{
    GameObject[] coinTag;
    public bool startDiceCheck, diceReplay;

    int coinCountResult;

    int[] coinNum = new int[7];
    int diceNum;
    // Start is called before the first frame update
    void Start()
    {
        coinTag = GameObject.FindGameObjectsWithTag("CoinTag");
        diceReplay = false;
    }

    // Update is called once per frame
    public void Update()
    {
        startDiceCheck = GameObject.Find("dice").GetComponent<DiceRotation>().startDice;

        if (startDiceCheck)
        {
            diceReplay = true;
            diceNum = GameObject.Find("dice").GetComponent<DiceRotation>().indexVall;
            diceNum++;

            coinCountResult = 0;
            for (int i = 0; i < coinTag.Length; i++)
            {
                coinNum[i] = coinTag[i].GetComponent<ClickCreateClone>().makeNum;
                coinCountResult += coinNum[i];
            }
            Debug.Log("코인의 총 개수는: " + coinCountResult);
            Debug.Log("주사위 값: " + diceNum);

            // 주사위의 값과 생성한 코인 개수가 같으면 다시 굴릴 수 있게 체크
            if (coinCountResult == diceNum)
            {
                startDiceCheck = false;
                diceReplay = false;
            }
        }
        else
        {
            Debug.Log("주사위를 굴리세요");
        }
    }
}
