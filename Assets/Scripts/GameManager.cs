using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    public AudioSource startAudio;
    public GameObject Born;
    public GameObject UIDefeat;
    public Text Text_Flag;
    public Text Text_DieEnemy;
    public Text Text_Player1Life;
    public Text Text_Player2Life;

    public static readonly int MAX_BLOOD = 2;
    public static Vector3 player1Position ;
    public static Vector3 player2Position ;
    public static Vector3 cameraSize;
    private Vector3 backGroundSize;

    private int player1Life;
    private int player2Life;
    private int player1Score;
    private int player2Score;
    private bool player1IsDead;
    private bool player2IsDead;
    private bool isDefeat = false;
    private bool isDefeatShow = false;
    private static int playerNum;
    private static List<Vector3> PositionList = new List<Vector3>();

    private static GameManager instance;
    public static GameManager Instance
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

    public bool Player1IsDead
    {
        get
        {
            return player1IsDead;
        }

        set
        {
            player1IsDead = value;
        }
    }

    public bool Player2IsDead
    {
        get
        {
            return player2IsDead;
        }

        set
        {
            player2IsDead = value;
        }
    }

    public bool IsDefeat
    {
        get
        {
            return isDefeat;
        }

        set
        {
            isDefeat = value;
        }
    }

    public Vector3 BackGroundSize
    {
        get
        {
            return backGroundSize;
        }

        set
        {
            backGroundSize = value;
        }
    }

    public static int PlayerNum
    {
        get
        {
            return playerNum;
        }

        set
        {
            playerNum = value;
        }
    }

    void Awake()
    {
        Instance = this;
        ResetPlayerLife();

        player1Position = new Vector3(-1.5f, -GameObject.Find("BackGround").gameObject.GetComponent<Renderer>().bounds.size.y / 2+0.5f, 0);
        player2Position = new Vector3(1.5f, -GameObject.Find("BackGround").gameObject.GetComponent<Renderer>().bounds.size.y / 2+0.5f, 0);
        cameraSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        cameraSize.y = cameraSize.y * 2;
        startAudio = gameObject.GetComponent<AudioSource>();

    }
    void Start()
    {
        ResetText();
    }
    void FixedUpdate()
    {
        if (!IsDefeat)
        { 
            Recover(); 
            UpdateText();
            return;
        }
        Defeat();
    }

    private void ResetPlayerLife()
    {
        player1Life = MAX_BLOOD;
        Instance.Player2IsDead = true;
        if(PlayerNum == 2) 
        { 
            player2Life = MAX_BLOOD;
            Instance.Player2IsDead = false;
        }
    }

    private void UpdateText()
    {/*
        Text_Flag.text = EnemyCreation.Instance.EnemyNumber.ToString();
        Text_DieEnemy.text = EnemyCreation.Instance.Remain_Num.ToString();
        Text_Player1Life.text = player1Life.ToString();
        Text_Player2Life.text = player2Life.ToString();*/
    }

    public static Vector3 GetLastPos()
    {
        return PositionList[PositionList.Count-1];
    }

    public static void AddPos(Vector3 pos)
    {
        PositionList.Add(pos);
    }
    public static void DeletePos(Vector3 pos)
    {
        for (int i = 0; i < PositionList.Count; i++)
        {
            if(pos == PositionList[i])
            {
                PositionList.RemoveAt(i);
                return;
            }
        }
    }
    public static Vector3 CreatePosition()
    {
        while (true)
        {
            Vector3 pos = new Vector3(Random.Range(-9, 9), Random.Range(-6, 7), 0);
            if (HasThePosition(pos))
                continue;
            PositionList.Add(pos);
            return pos;
        }        
    }
    private static bool HasThePosition(Vector3 pos)
    {
        for (int i = 0; i < PositionList.Count; i++)
        {
            if(pos == PositionList[i])
            {
                return true;
            }
        }
        return false;
    }

    void Recover()
    {
        if(player1IsDead && player1Life > 0)
        {
            player1Life--;
            GameObject go = Instantiate(Born, player1Position,Quaternion.identity, gameObject.transform);
            go.GetComponent<Born>().BornPlayer1();
            go.name = "Player1Born";
            player1IsDead = false;
        }
        if(player2IsDead && player2Life > 0)
        {
            player2Life--;
            GameObject go = Instantiate(Born, player2Position, Quaternion.identity, gameObject.transform);
            go.GetComponent<Born>().BornPlayer2();
            go.name = "Player2Born";
            player2IsDead = false;
        }
        if ((player1Life <= 0 && player2Life <= 0 && player1IsDead && player2IsDead))
        {
            GameManager.Instance.isDefeat = true;
            Debug.Log("GameManager.Instance.isDefeat " + GameManager.Instance.isDefeat);
            Defeat();
        }
    }
    void Defeat()
    {
        if (isDefeatShow) return;
        GameObject defeat =  Instantiate(UIDefeat, new Vector2(), Quaternion.identity, gameObject.transform);
        defeat.name = "UIDefeat";
        defeat.SetActive(true);
        isDefeatShow = true;
        Invoke("GoBack",3f);
    }

    void ResetText()
    {

    }
    void GoBack()
    {
        SceneManager.LoadScene(0);
    }
}
