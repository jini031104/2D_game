using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpRecovery : MonoBehaviour{
    public int PlayerHP => playerHP;
    public int EnemyHP => enemyHP;
    int playerHP, enemyHP;

    public bool CardClick => cardClick;
    bool playerTurn, cardClick;

    // Start is called before the first frame update
    void Start(){
        cardClick = false;
    }

    // Update is called once per frame
    void Update(){
    }

    void OnMouseDown(){
        cardClick = true;
        playerTurn = GameObject.Find("changeButton").GetComponent<ChangeButton>().PlayerTurn;
        if (playerTurn){
            playerHP = GameObject.Find("startButton").GetComponent<Calculate>().PlayerHP;
            playerHP += 2;
        }
        else{
            enemyHP = GameObject.Find("startButton").GetComponent<Calculate>().EnemyHP;
            enemyHP += 2;
        }
    }

    void OnMouseUp(){
        cardClick = false;
    }
}
