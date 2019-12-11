using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour {

    private static readonly int NUM = 20;

    private GameObject RightAirWall;
    private GameObject LeftAirWall;
    private GameObject TopAirWall;
    private GameObject DownAirWall;

    public GameObject Grass;
    public GameObject Heart;
    public GameObject Born;
    public GameObject MetalWall;
    public GameObject NormalWall;
    public GameObject River;
    public GameObject AirWall;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Awake()
    {
        ResetAirWall();
        ResetHeart();
        CreateMap_1();
    }

    void ResetHeart()
    {

        //reset Heart
        Instantiate(Heart, new Vector3(0,-7.5f,0), Quaternion.identity,transform).name = "Heart";
        //reset wall to warp heart up
        Instantiate(NormalWall, new Vector3(-0.5f, -7.5f, 0), Quaternion.identity,transform).name = "HeartWall_LD";
        Instantiate(NormalWall, new Vector3(-0.5f, -7f, 0), Quaternion.identity, transform).name = "HeartWall_LU";
        Instantiate(NormalWall, new Vector3(0, -7f, 0), Quaternion.identity, transform).name = "HeartWall_U";
        Instantiate(NormalWall, new Vector3(0.5f, -7f, 0), Quaternion.identity, transform).name = "HeartWall_RU";
        Instantiate(NormalWall, new Vector3(0.5f, -7.5f, 0), Quaternion.identity, transform).name = "HeartWall_RD";
    }
    void ResetAirWall()
    {
        BoxCollider2D topWallCollider2D;
        BoxCollider2D downWallCollider2D;
        BoxCollider2D leftWallCollider2D;
        BoxCollider2D rightWallCollider2D;
        GameManager.Instance.BackGroundSize = GameObject.Find("BackGround").gameObject.GetComponent<Renderer>().bounds.size; 
        Vector3 tempPosition = GameManager.Instance.BackGroundSize;

        RightAirWall = Instantiate(AirWall, new Vector3(tempPosition.x/2, 0, 0), Quaternion.identity, transform);
        LeftAirWall = Instantiate(AirWall, new Vector3(-tempPosition.x/2, 0, 0), Quaternion.identity, transform);
        TopAirWall = Instantiate(AirWall, new Vector3(0, tempPosition.y/2, 0), Quaternion.identity, transform);
        DownAirWall = Instantiate(AirWall, new Vector3(0, -tempPosition.y/2, 0), Quaternion.identity, transform);

        RightAirWall.name = "RightAirWall";
        LeftAirWall.name = "LeftAirWall";
        TopAirWall.name = "TopAirWall";
        DownAirWall.name = "DownAirWall";

        rightWallCollider2D = RightAirWall.GetComponent<BoxCollider2D>();
        leftWallCollider2D = LeftAirWall.GetComponent<BoxCollider2D>();
        topWallCollider2D = TopAirWall.GetComponent<BoxCollider2D>();
        downWallCollider2D = DownAirWall.GetComponent<BoxCollider2D>();

        topWallCollider2D.size = new Vector2(tempPosition.x , 0.1f);
        downWallCollider2D.size = new Vector2(tempPosition.x , 0.1f);
        rightWallCollider2D.size = new Vector2(0.1f, tempPosition.y );
        leftWallCollider2D.size = new Vector2(0.1f, tempPosition.y );
    }

    void CreateMap_1()
    {
        for (int i = 0; i < NUM; i++)
        {
            Instantiate(Grass, GameManager.CreatePosition(), Quaternion.identity, transform).name = "Grass" + i;
        }
        for (int i = 0; i < NUM+25; i++)
        { 
            Instantiate(NormalWall, GameManager.CreatePosition(), Quaternion.identity, transform).name = "NormalWall" + i;
        }
        for (int i = 0; i < NUM; i++)
        {
            Instantiate(MetalWall, GameManager.CreatePosition(), Quaternion.identity, transform).name = "MetalWall" + i;
        }
        for (int i = 0; i < NUM; i++)
        {
            Instantiate(River, GameManager.CreatePosition(), Quaternion.identity, transform).name = "River" + i;
        }
    }
}
