using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreation : MonoBehaviour {

    public GameObject Born;
    // Use this for initialization
    void Start () {
		
	}
	void Awake()
    {

        ResetPlayer();
    }
	// Update is called once per frame
	void Update () 
    {
		
	}

    void ResetPlayer()
    {
        switch (GameManager.PlayerNum)
        {
            case 1:
                Instantiate(Born, GameManager.player1Position, Quaternion.identity, gameObject.transform).name = "Player1Born";
                gameObject.transform.Find("Player1Born").SendMessage("BornPlayer1");
                break;
            case 2:
                Instantiate(Born, GameManager.player1Position, Quaternion.identity, gameObject.transform).name = "Player1Born";
                gameObject.transform.Find("Player1Born").SendMessage("BornPlayer1");

                Instantiate(Born, GameManager.player2Position, Quaternion.identity, gameObject.transform).name = "Player2Born";
                gameObject.transform.Find("Player2Born").SendMessage("BornPlayer2");
                break;
        }
    }
}
