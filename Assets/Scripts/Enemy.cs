using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private static readonly float CHANGE_DIRECTION_CD = 3f;
    private static readonly float ATTANK_CD = 0.2f;
    private static readonly float MOVE_CD = 0.8f;
    //运动速度
    public float moveSpeed = 3;
    private int direction;
    //计时器
    private float timeValAttack;
    private float timeValChangeDirection;
    private float timeValMove;

    //图片精灵
    public Sprite[] tankSprite;

    //预制体获取
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    //音效文件
    public AudioSource driveAudio;

    //精灵渲染器
    private SpriteRenderer spriteRender;
    //获得钢体
    private Rigidbody2D rigidbody2D;


    //子弹欧拉角
    private Vector3 bulletEulerAngles;

    // Use this for initialization
    void Start()
    {

    }
    void Awake()
    {
        driveAudio = GetComponent<AudioSource>();
        spriteRender = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        driveAudio.Play();
        driveAudio.Pause();
    }
    // Update is called once per frame
    void Update()
    {
        EnemyAttackCD();
    }
    //固定物理帧
    void FixedUpdate()
    {
        if(timeValMove<= MOVE_CD)
        {
            timeValMove += Time.deltaTime;
            return;
        }
        EnemyMove();
    }


    void EnemyAttackCD()
    {
        //攻击CD
        if (timeValAttack >= ATTANK_CD)
        {
            Attack();
        }
        else
        {
            timeValAttack += Time.deltaTime;
        }
    }
    void EnemyDie()
    {
        //产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);
    }


    //神奇Enemy移动AI
    private void EnemyMove()
    {
        switch (direction)
        {
            case 0:
                rigidbody2D.velocity = new Vector2(0, moveSpeed);
                spriteRender.sprite = tankSprite[0];
                bulletEulerAngles = new Vector3(0, 0, 0);
                driveAudio.UnPause();
                break;
            case 1:
                rigidbody2D.velocity = new Vector2(0, -moveSpeed);
                spriteRender.sprite = tankSprite[2];
                bulletEulerAngles = new Vector3(0, 0, -180);
                driveAudio.UnPause();
                break;
            case 2:
                rigidbody2D.velocity = new Vector2(moveSpeed, 0);
                spriteRender.sprite = tankSprite[1];
                bulletEulerAngles = new Vector3(0, 0, -90);
                driveAudio.UnPause();
                break;
            case 3:
                spriteRender.sprite = tankSprite[3];
                rigidbody2D.velocity = new Vector2(-moveSpeed, 0);
                bulletEulerAngles = new Vector3(0, 0, 90);
                driveAudio.UnPause();
                break;
            case 4:
                rigidbody2D.velocity = new Vector2(0, 0);
                break;
        }
        if (timeValChangeDirection >= CHANGE_DIRECTION_CD)
        {
            direction = Random.Range(0, 5);
            timeValChangeDirection = 0;
        }
        else
        {
            timeValChangeDirection += Time.deltaTime * (Random.Range(0, 10));
        }
        driveAudio.Pause();
    }


    void AIMove(Vector2 ObjectPosition,Vector2 EnemyPosition)
    {            //x轴
        if ((int)ObjectPosition.x > (int)EnemyPosition.x)
        {
            spriteRender.sprite = tankSprite[1];
            transform.Translate(Vector3.right * 1 * moveSpeed * Time.fixedDeltaTime, Space.World);
        }
        else if ((int)ObjectPosition.x < (int)EnemyPosition.x)
        {
            spriteRender.sprite = tankSprite[3];
            transform.Translate(Vector3.right * -1 * moveSpeed * Time.fixedDeltaTime, Space.World);
        }
        else { }

        //y轴
        if ((int)ObjectPosition.y > (int)EnemyPosition.y)
        {
            spriteRender.sprite = tankSprite[0];
            transform.Translate(Vector3.up * 1 * moveSpeed * Time.fixedDeltaTime, Space.World);
        }
        else if ((int)ObjectPosition.y < (int)EnemyPosition.y)
        {
            spriteRender.sprite = tankSprite[2];
            transform.Translate(Vector3.up * -1 * moveSpeed * Time.fixedDeltaTime, Space.World);
        }
        else { }
    }
    private void Attack()
    {
        if(((int)Random.Range(1,1500))/8 == 0) { 
        //子弹产生的角度： 当前坦克的角度 + 子弹应该旋转的角度
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
        timeValAttack = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.gameObject.tag;
        switch (collisionTag)
        {
            case "Enemy":
                timeValChangeDirection = CHANGE_DIRECTION_CD;
                break;
            case "Tank":
                //0s 1x 2y 3z
                
                Vector3 TankPositon =  collision.gameObject.transform.position;
                if ((int)TankPositon.x > (int)transform.position.x)
                {
                    direction = 2;
                }
                if ((int)TankPositon.x < (int)transform.position.x)
                {
                    direction = 3;
                }
                if ((int)TankPositon.y > (int)transform.position.y)
                {
                    direction = 0;
                }
                if ((int)TankPositon.y < (int)transform.position.y)
                {
                    direction = 1;
                }
                Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
                break;
            default:
                break;
        }
    }
}
