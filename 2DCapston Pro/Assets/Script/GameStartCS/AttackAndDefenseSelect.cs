using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAndDefenseSelect : MonoBehaviour
{
    bool firstChoice;

    // Start is called before the first frame update
    void Start()
    {
        firstChoice = GameObject.Find("playerDice").GetComponent<GameStartDice>().FirstChoice;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
