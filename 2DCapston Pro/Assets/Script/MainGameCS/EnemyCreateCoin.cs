using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreateCoin : MonoBehaviour
{
    bool enemyDiceTurn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyDiceTurn = GameObject.Find("dice").GetComponent<DiceRotation>().EnemyDice;

        if (enemyDiceTurn)
        {
            Debug.Log("This is enemyDiceTurn!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }
}
