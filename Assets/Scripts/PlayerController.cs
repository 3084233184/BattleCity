using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly float ATTANK_CD = 0.4f;
    private readonly float DEFENED_TIME = 3f;
    private static readonly float MOVE_CD = 0.8f;
    //运动速度
    public float moveSpeed = 3;

    //计时器
    private float timeVal;
    private float timeDefended;
    private float timeValMove;
    //按键定义
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;
    public KeyCode attakKey;

    //图片精灵
    public Sprite[] tankSprite;

    //预制体获取
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject defendedPrefab;
    //音效文件
    public AudioSource driveAudio;
    public AudioClip bulletAudio;
    //精灵渲染器
    private  SpriteRenderer spriteRender;
    //获得钢体
    private Rigidbody2D rigidbody2D;


    //子弹欧拉角
    private Vector3 bulletEulerAngles;

    //
    private bool isDefened = true;
    // Use this for initialization
    void Start()
    {

    }
    void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        driveAudio = GetComponent<AudioSource>();
        driveAudio.Play();
        driveAudio.Pause();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    //固定物理帧
    void FixedUpdate()
    {
        if (!GameManager.Instance.IsDefeat)
        {
            PlayerCD();
            if (timeValMove <= MOVE_CD)
            {
                timeValMove += Time.deltaTime;
                return;
            }
            PlayerMove();
        }
    }


    void PlayerCD()
    {
        
        //无敌CD
        if (isDefened)
        {
            timeDefended += Time.deltaTime;
            defendedPrefab.SetActive(true);
            if ( timeDefended >= DEFENED_TIME )
            {
                isDefened = false;
                defendedPrefab.SetActive(false);
            }
        }

        //攻击CD
        if (timeVal >= ATTANK_CD)
        {
            Atttak();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
    }
    void PlayerDie() 
    {

        if (isDefened) return;
        //产生爆炸特效
        Instantiate(explosionPrefab,transform.position,transform.rotation);
     
        switch (transform.name)
        {
            case "Player1":
                GameObject.Find("GameManager").GetComponent<GameManager>().Player1IsDead = true;
                break;
            case "Player2":
                GameObject.Find("GameManager").GetComponent<GameManager>().Player2IsDead = true;
                break;
            default:
                break;
        }
        //死亡
        Destroy(gameObject);
    }
    
    private void PlayerMove()
    {
        if (Input.GetKey(upKey))
        {
            rigidbody2D.velocity = new Vector2(0, moveSpeed);
            spriteRender.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
            driveAudio.UnPause();
            goto upKeyUp;
        }
        else if (Input.GetKey(downKey))
        {
            rigidbody2D.velocity = new Vector2(0, -moveSpeed);
            spriteRender.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3(0, 0, -180);
            driveAudio.UnPause();
            goto downKeyUp;
        }
        else if (Input.GetKey(rightKey))
        {
            rigidbody2D.velocity = new Vector2(moveSpeed, 0);
            spriteRender.sprite = tankSprite[1];
            bulletEulerAngles = new Vector3(0,0,-90);
            driveAudio.UnPause();
            goto rightKeyUp;
        }
        else if (Input.GetKey(leftKey))
        {
            spriteRender.sprite = tankSprite[3];
            rigidbody2D.velocity = new Vector2(-moveSpeed, 0);
            bulletEulerAngles = new Vector3(0, 0, 90);
            driveAudio.UnPause();
            goto leftKeyUp;
        }
        else
        {
            rigidbody2D.velocity = new Vector2(0, 0);
        }
    upKeyUp:
        if (Input.GetKeyUp(upKey))
        {
            driveAudio.Pause();
            return;
        }

    downKeyUp:
        if (Input.GetKeyUp(downKey))
        {
            driveAudio.Pause();
            return;
        }

    rightKeyUp:
        if (Input.GetKeyUp(rightKey))
        {
            driveAudio.Pause();
            return;
        }
    leftKeyUp:
        if (Input.GetKeyUp(leftKey))
        {
            driveAudio.Pause();
            return;
        }
    }

    private void Atttak()
    {
        if (Input.GetKeyDown(attakKey))
        {
            //子弹产生的角度： 当前坦克的角度 + 子弹应该旋转的角度
            Instantiate(bulletPrefab,transform.position,Quaternion.Euler(transform.eulerAngles+bulletEulerAngles)).name = "PlayerBullet";
            AudioSource.PlayClipAtPoint(bulletAudio,transform.position);
            timeVal = 0;
        }
    }
}