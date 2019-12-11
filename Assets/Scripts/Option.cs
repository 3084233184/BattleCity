using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Option : MonoBehaviour {

    public Transform[] PosArr;
    private int choice = 1;

	// Use this for initialization
	void Start () {
		
	}
	void Awake()
    {
        
    }
	// Update is called once per frame
	void Update () {

        int num = Mathf.Abs(choice) % PosArr.Length;
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            transform.position = PosArr[num].position;
            choice--;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            transform.position = PosArr[num].position;
            choice++;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Return))
        {
            switch (num)
            {
                case 0:
                    GameManager.PlayerNum = 2;
                    SceneManager.LoadScene(1);
                    break;
                case 1:
                    GameManager.PlayerNum = 1;
                    SceneManager.LoadScene(1);
                    break;

            }
        }
    }
}
