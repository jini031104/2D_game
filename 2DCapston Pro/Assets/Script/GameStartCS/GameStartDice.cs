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
    int rVall, dVall;
    int enemyDiceRotIndex, playerDiceeRotIndex;
    int enemyDiceVal, playerDiceVal;

    bool diceSmall = false;
    bool gameStartCheck = false;
    
    public bool FirstChoice => firstChoice;
    bool firstChoice = false;
    bool diceDrop = false;

    // Start is called before the first frame update
    void Start()
    {
        // 주사위의 크기 값을 가져옴.
        playerDiceScale = playerDice.gameObject.transform.localScale;
        enemyDiceScale = enemyDice.gameObject.transform.localScale;

        dVall = diceVall.GetLength(0);
        rVall = rotationVal.GetLength(0);

        Button.SetActive(false);    // 게임 시작시, 공/방 버튼은 비활성 처리한다.
    }

    // Update is called once per frame
    void Update()
    {
        if(diceDrop)
        {
            if(enemyDiceVal < playerDiceVal){
                Button.SetActive(true);
                firstChoice = true;
                //gameStart();
            }
            else if(enemyDiceVal == playerDiceVal)
            {
                firstChoice = false;
                diceDrop = false;
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

    private void OnMouseDown(){ // 주사위를 클릭하면, 주사위가 커진다.
        playerDice.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        enemyDice.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    private void OnMouseUp(){   // 주사위를 굴린다.
        DiceDrpoAndValSet(playerDiceeRotIndex, playerDice);
        DiceDrpoAndValSet(enemyDiceRotIndex, enemyDice);
        //Debug.Log(playerDice.name + "주사위 값: " + playerDiceVal);
        //Debug.Log(enemyDice.name + "주사위 값: " + enemyDiceVal);
        diceSmall = true;
    }

    public void DiceDrpoAndValSet(int rotIndex, GameObject Dice){   // 주사위 값 세팅
        rotIndex = Random.Range(0, rVall);
        Dice.transform.localEulerAngles = new Vector3(rotationVal[rotIndex, 0], rotationVal[rotIndex, 1], rotationVal[rotIndex, 2]);

        int valIndex;
        valIndex = Random.Range(0, dVall);
        if(Dice.name == "playerDice")
            playerDiceVal = valIndex;
        if(Dice.name == "enemyDice")
            enemyDiceVal = valIndex;
    }

    void SmallDice(GameObject Dice, Vector3 diceScale){ // 주사위가 떨어지는 동안 점점 작아짐.
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
                diceDrop = true;
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
