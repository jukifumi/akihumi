using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour {
    public float turn;//ターン数
    float time;
    public bool onePush;//

    [SerializeField]
    GameObject Even, Odd;//奇数、偶数
    // Use this for initialization
    void Start()
    {//初期化
        time = 0;
        turn = 1;
        onePush = false;
    }

    // Update is called once per frame
    void Update()
    {
       if(turn%2==0)
        {
            Odd.gameObject.SetActive(false);
            Even.gameObject.SetActive(true);
        }
        else
        {
            Even.gameObject.SetActive(false);
            Odd.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.A)==true || Input.GetKeyDown(KeyCode.S) == true || Input.GetKeyDown(KeyCode.D) == true)
        {
            //if (onePush == false)
            //{
                turn++;

            //}
        }

    }
    private void FixedUpdate()
    {
        
        
    }
}
