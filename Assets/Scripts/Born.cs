using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour {

    static public readonly int STATUS_ENEMY = 0;
    static public readonly int STATUS_PLAYER1 = 1;
    static public readonly int STATUS_PLAYER2 = 2;
    static public readonly string QUICK_ENEMY_NAME = "QuickEnemy";
    static public readonly string NORMAL_ENEMY_NAME = "NormalEnemy";

    private readonly int NUMBER_ENEMYS = 2;
    public GameObject player1Prefab;
    public GameObject player2Prefab;


    // 1.NormalEnemy  2.QuickEnemy
    public GameObject[] enemyPrefabs;
    public int Status;

	// Use this for initialization
	void Start ()
    {
    }
    void Awake()
    {
        //延时销毁Born特效
        Destroy(gameObject, 0.8f);
    }
    void BornGameObject()
    {
        switch (Status)
        {
            case 1:
                //延时调用函数
                Invoke("BornPlayer1", 0.8f);
                break;
            case 2:
                //延时调用函数
                Invoke("BornPlayer2", 0.8f);
                break;
            case 0:
                //延时调用函数
                Invoke("BornEnemy", 0.8f);
                break;

        }
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void BornPlayer1()
    {
        Instantiate(player1Prefab,transform.position,Quaternion.identity
            ,GameObject.Find("PlayerCreation").transform).name="Player1";
    }

    public void BornPlayer2()
    {
        Instantiate(player2Prefab, transform.position, Quaternion.identity
            ,GameObject.Find("PlayerCreation").transform).name = "Player2";
    }
    void BornEnemy()
    {
        int randomNum = Random.Range(0, NUMBER_ENEMYS);
        string EnemyName = "";
        switch (randomNum)
        {
            case 0:
                EnemyName = NORMAL_ENEMY_NAME;
                break;
            case 1:
                EnemyName = QUICK_ENEMY_NAME;
                break;
            default:
                EnemyName = "UnnamedEnemy";
                break;
        }
        Instantiate(enemyPrefabs[randomNum], GameManager.GetLastPos(), Quaternion.identity, 
            GameObject.Find("EnemyCreation").transform).name = EnemyName;
    }
}
