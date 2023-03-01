using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePiusOne : MonoBehaviour{
    public int PlayerDiceNumVall => playerDiceNumVall;
    public int EnemyDiceNumVall => enemyDiceNumVall;
    int playerDiceNumVall, enemyDiceNumVall, count;

    public bool Click => click;
    bool playerTurn, click;

    // Start is called before the first frame update
    void Start(){
        count = 0;
        click = false;
    }

    // Update is called once per frame
    void Update(){
    }

    void OnMouseDown(){
        click = true;
        playerTurn = GameObject.Find("changeButton").GetComponent<ChangeButton>().PlayerTurn;

        if(count == 0){
            if (playerTurn){
                playerDiceNumVall = GameObject.Find("dice").GetComponent<DiceRot>().PlayerDiceNumVall;
                if (playerDiceNumVall < 5)
                    playerDiceNumVall++;
                else if(playerDiceNumVall == 5)
                    Debug.Log("더이상 추가할 수 없습니다.");
                Debug.Log("playerDice" + playerDiceNumVall);
            }
            else{
                enemyDiceNumVall = GameObject.Find("dice").GetComponent<DiceRot>().EnemyDiceNumVall;
                if (enemyDiceNumVall < 5)
                    enemyDiceNumVall++;
                else if (enemyDiceNumVall == 5)
                    Debug.Log("더이상 추가할 수 없습니다.");
                Debug.Log("enemyDice" + enemyDiceNumVall);
            }
            count++;
        }
    }
}
