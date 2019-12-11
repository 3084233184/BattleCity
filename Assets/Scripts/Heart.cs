using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

    private readonly int DIE = 1;
    private readonly int LIVE = 0;

    public Sprite[] heartSprites;
    private SpriteRenderer sr;

    public AudioClip DieAudio;
	// Use this for initialization
	void Start () {
		
	}
	
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

	// Update is called once per frame
	void Update () {
		
	}

    void Die()
    {
        sr.sprite = heartSprites[DIE];
        GameManager.Instance.IsDefeat = true;
        AudioSource.PlayClipAtPoint(DieAudio,transform.position);
    }
}
