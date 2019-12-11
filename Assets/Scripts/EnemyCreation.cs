using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyCreation : MonoBehaviour {

    //计时器CD
    private readonly float CREATE_CD = 15f;

    //计时器
    private float timeValCreate;
    //敌人数量
    static public readonly int MAX_NUM = 50;
    static public readonly int MIN_NUM = 20;
    public int EnemyNumber;
    public int Now_Num;
    public int Remain_Num;

    private static EnemyCreation instance;

    //生成器
    public GameObject Born;

    public static EnemyCreation Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	void Awake()
    {
        Instance = this;
        EnemyNumber = Random.Range(MIN_NUM, MAX_NUM);
        timeValCreate = CREATE_CD;
    }
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        Remain_Num = EnemyNumber - Now_Num;

        if (timeValCreate >= CREATE_CD) { 
            if (Remain_Num > 0)
            {
                int randomNum = Random.Range(3, 6);
                if (randomNum > Remain_Num) randomNum = Remain_Num;
                for (int i = 0; i < randomNum; i ++)
                CreateEnemy();
            }
            timeValCreate = 0;
        }
        else
        {
            timeValCreate += Time.deltaTime;
        }
    }

    void CreateEnemy()
    {
        Instantiate(Born,GameManager.CreatePosition(), Quaternion.identity, gameObject.transform).name = "EnemyBorn";
        gameObject.transform.Find("EnemyBorn").SendMessage("BornEnemy");
        Now_Num++;
        //删除这个位置
        GameManager.DeletePos(GameManager.GetLastPos());
    }
}
