using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    
    public float movedspeed = 10;
    public bool isPlayerBullet;
    public AudioClip ExplosionAudio;
    public AudioClip HitAudio;
    // Use this for initialization
    void Start () {

	}
	void Awake()
    {
        BulletIdentify();
    }
    void BulletIdentify()
    {
        
        if (gameObject.tag.Equals("Tank"))
            isPlayerBullet = true;
        else
        {
            isPlayerBullet = false;
        }
    }
	// Update is called once per frame
	void Update () {
        transform.Translate(transform.up * movedspeed *Time.deltaTime,Space.World);	
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tank":
                if (!isPlayerBullet)
                {
                    collision.SendMessage("PlayerDie");
                    if (gameObject.name == "PlayerBullet")
                        AudioSource.PlayClipAtPoint(ExplosionAudio,transform.position);
                    Destroy(gameObject);
                }
                break;
            case "Enemy":
                if (isPlayerBullet)
                {
                    collision.SendMessage("EnemyDie");
                    if (gameObject.name == "PlayerBullet")
                        AudioSource.PlayClipAtPoint(ExplosionAudio, transform.position);
                    Destroy(gameObject);
                }
                    break;
            case "NormalWall":
                GameManager.DeletePos(collision.gameObject.transform.position);
                if (gameObject.name == "PlayerBullet")
                    AudioSource.PlayClipAtPoint(ExplosionAudio, transform.position);
                Destroy(collision.gameObject);
                Destroy(gameObject);
                break;
            case "MetalWall":
                if (gameObject.name == "PlayerBullet")
                    AudioSource.PlayClipAtPoint(HitAudio, transform.position);
                Destroy(gameObject); 
                break;
            case "Heart":
                collision.SendMessage("Die");
                Destroy(gameObject);
                break;
        }
    }
}
