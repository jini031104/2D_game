using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAndDefenseSelect : MonoBehaviour
{
    // bool firstChoice;

    public static bool attack, defense;

    // Start is called before the first frame update
    void Start()
    {
        attack = false;
        defense = false;
        // firstChoice = GameObject.Find("playerDice").GetComponent<GameStartDice>().FirstChoice;
    }

    // Update is called once per frame
    void Update()
    {
        attack = GameObject.Find("attackButton").GetComponent<AttackButton>().Attack;
        defense = GameObject.Find("defenseButton").GetComponent<DefenseButton>().Defense;

        if (attack)
            attack = true;
        else if (defense)
            defense = true;

        if (attack || defense)
            GameObject.Find("playerDice").GetComponent<GameStartDice>().gameStart();
    }
}
