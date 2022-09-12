using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartDice : MonoBehaviour
{
    [SerializeField]
    private GameObject playerDice;
    Vector3 playerDiceScale;

    [SerializeField]
    private GameObject enemyDice;
    Vector3 enemyDiceScale;

    [SerializeField]
    private GameObject Button;

    int[] diceVall = new int[] { 1, 2, 3, 4, 5, 6 };
    int[,] rotationVal = new int[,] { { 40, 135, 270 },
                                        {40, 45, 90 },
                                        {130, 45, 0 } };
    int w, v;
    int enemyDiceRotIndex, playerDiceeRotIndex;
    int enemyDiceVal, playerDiceVal;

    bool diceSmall = false;
    bool gameStartCheck = false;
    
    public bool FirstChoice => firstChoice;
    bool firstChoice = false;
    bool diceDrpo = false;

    // Start is called before the first frame update
    void Start()
    {
        // 적 주사위의 크기 값을 가져옴.
        playerDiceScale = playerDice.gameObject.transform.localScale;
        enemyDiceScale = enemyDice.gameObject.transform.localScale;

        w = rotationVal.GetLength(0);
        v = diceVall.GetLength(0);

        Button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(diceDrpo){
            if(enemyDiceVal <= playerDiceVal){
                Button.SetActive(true);
                firstChoice = true;
                //gameStart();
            }
            else
                gameStart();
        }

        SmallDice(playerDice, playerDiceScale);
        SmallDice(enemyDice, enemyDiceScale);
    }

    public void gameStart(){
        if(gameStartCheck){
            if(Input.GetMouseButtonDown(0))
                SceneManager.LoadScene("MainGameCS");
        }
    }

    private void OnMouseDown(){
        BigDice(playerDice);
        BigDice(enemyDice);
    }

    private void OnMouseUp(){
        DiceDrpoAndValSet(playerDiceeRotIndex, playerDice);
        DiceDrpoAndValSet(enemyDiceRotIndex, enemyDice);
        //Debug.Log(playerDice.name + "주사위 값: " + playerDiceVal);
        //Debug.Log(enemyDice.name + "주사위 값: " + enemyDiceVal);
        diceSmall = true;
    }

    void BigDice(GameObject Dice){
        Dice.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void DiceDrpoAndValSet(int rotIndex, GameObject Dice){
        rotIndex = Random.Range(0, w);
        Dice.transform.localEulerAngles = new Vector3(rotationVal[rotIndex, 0], rotationVal[rotIndex, 1], rotationVal[rotIndex, 2]);

        int valIndex;
        valIndex = Random.Range(0, v);
        if(Dice.name == "playerDice")
            playerDiceVal = valIndex;
        if(Dice.name == "enemyDice")
            enemyDiceVal = valIndex;
    }

    void SmallDice(GameObject Dice, Vector3 diceScale){
        if (diceSmall){
            if (Dice.transform.localScale.x > diceScale.x)
            {
                Dice.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }
            else{
                DiceResult(playerDiceVal, playerDice);
                DiceResult(enemyDiceVal, enemyDice);
                diceSmall = false;
                gameStartCheck = true;
                diceDrpo = true;
            }
        }
    }

    void DiceResult(int valIndex, GameObject Dice){
        switch (valIndex){
            case 0:
                Dice.transform.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 1:
                Dice.transform.localEulerAngles = new Vector3(0, 90, -90);
                break;
            case 2:
                Dice.transform.localEulerAngles = new Vector3(0, 270, 0);
                break;
            case 3:
                Dice.transform.localEulerAngles = new Vector3(0, 90, 0);
                break;
            case 4:
                Dice.transform.localEulerAngles = new Vector3(0, 90, -270);
                break;
            case 5:
                Dice.transform.localEulerAngles = new Vector3(0, 0, 0);
                break;
        }
    }
}
