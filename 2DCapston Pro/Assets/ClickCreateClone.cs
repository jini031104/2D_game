using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCreateClone : MonoBehaviour
{
    [SerializeField]
    private GameObject coinPrefab;
    [SerializeField]
    private Transform spawnPoint;

    int makeNum;
    int aaaa;
    bool startDiceCheck;

    // Start is called before the first frame update
    void Start()
    {
        makeNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnMouseDown()
    {
        GameObject clone = Instantiate(coinPrefab, spawnPoint.position, Quaternion.identity);
        makeNum++;
        Debug.Log(makeNum + "개");
        startDiceCheck = GameObject.Find("dice").GetComponent<DiceRotation>().startDice;
        if (startDiceCheck)
        {
            aaaa = GameObject.Find("dice").GetComponent<DiceRotation>().indexVall;
            aaaa++;
            Debug.Log(aaaa + "개 만들 수 있음");
        }
        else
        {
            Debug.Log("아직 시작 안 됐어요.");
        }
    }
}
