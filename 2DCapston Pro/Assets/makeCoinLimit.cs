using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���⼭ ���� ����� ���� �����Ұ���.
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
            Debug.Log("������ �� ������: " + coinCountResult);
            Debug.Log("�ֻ��� ��: " + diceNum);

            // �ֻ����� ���� ������ ���� ������ ������ �ٽ� ���� �� �ְ� üũ
            if (coinCountResult == diceNum)
            {
                startDiceCheck = false;
                diceReplay = false;
            }
        }
        else
        {
            Debug.Log("�ֻ����� ��������");
        }
    }
}
