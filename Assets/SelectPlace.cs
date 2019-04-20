using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPlace : CardData
{
    //ターン用アタッチするためのオブジェ
    GameObject turnCube;
    Turn turnScript;
    //リスト用アタッチするためのオブジェ
    GameObject listCube;
    ObjList objList;

    //変数
    [SerializeField]
    float selectUp, selectDown, selectRight, selectLeft;//選択している場所の周り

    int selectPosition;//選んでいる場所
    public bool select;//その場所を選択しているかどうか

    bool isPut;//置く
    bool isListAdd;//追加するとき
    bool isLiftObj;//オブジェクトを持ち上げるとき
    bool countTop;
    bool isInit;//初期化するときのフラグ

    //ベクトル
    Vector3 initPosition;//初期位置


    //静的定数
    private const int MAX_CARDS = 64;//複製するオブジェクトの最大数

    // Start is called before the first frame update
    void Start()
    {
        turnCube = GameObject.Find("turnControl");
        listCube = GameObject.Find("ListCube");
        objList = listCube.GetComponent<ObjList>();

        countTop = false;
        select = false;
        isPut = false;
        isLiftObj = false;
        isListAdd = false;
        selectPosition = 0;
        selectUp = 0;
        selectDown = 0;
        selectRight = 0;
        selectLeft = 0;
        initPosition = transform.position;


        //初期配置
        if (cardNumber == 28)
        {
            GetComponent<MeshRenderer>().enabled = true;
            cardType =2;
            cardPlace = 1;
        }
        if (cardNumber == 29)
        {
            GetComponent<MeshRenderer>().enabled = true;
            cardType =1;
            cardPlace = 1;
        }
        if (cardNumber == 36)
        {
            GetComponent<MeshRenderer>().enabled = true;
            cardType =1;
            cardPlace = 1;
        }
        if (cardNumber == 37)
        {
            GetComponent<MeshRenderer>().enabled = true;
            cardType =2;
            cardPlace = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        turnScript = turnCube.GetComponent<Turn>();

        MaterialChange();
        SelectState();
        PutTheCards();
        TurnOver();
        ChooseALocation();
    }

    //色を変える処理
    void MaterialChange()
    {
        //駒を置く場所を選んでいるときその場所を赤色にする
        if (select == true)
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<Renderer>().material.color = Color.red;
            if (isLiftObj == false)
            {//選択しているオブジェを少し上に浮かす
                gameObject.transform.position += new Vector3(0, 0.5f, 0);
                isLiftObj = true;
            }
        }
        else
        {
            isLiftObj = false;
            //浮かしたオブジェを元の位置に戻す
            gameObject.transform.position = initPosition;
        }

        //黒だったらマテリアルの色を変える
        if (cardPlace == 1)
        {
            if (select == false)
            {//黒
                GetComponent<Renderer>().material.color = Color.black;
            }
        }
        else if (cardPlace == 0)
        {
            if (select == false)
            {//白
                GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }

    //選択状態を切り替える
    void SelectState()
    {
        if (cardNumber == selectPosition)
        {//選択している
            select = true;
        }
        else
        {//選択していない
            //カードの状態
            if (cardPlace == 0)
            {//手札にあったら
                GetComponent<MeshRenderer>().enabled = false;
            }
            else if (cardPlace == 1)
            {//表に出ていたら
                GetComponent<MeshRenderer>().enabled = true;
            }
            select = false;
        }
    }

    //カードを置く
    void PutTheCards()
    {
        //Aキーを押したとき
        if (Input.GetKeyDown(KeyCode.A) == true)
        {//選択している場所が
            if (cardNumber == selectPosition)
            {//手札だったら
                if (cardPlace == 0)
                {
                    cardPlace = 1;//表において
                    GetComponent<MeshRenderer>().enabled = true;//オブジェを表示する
                    if (turnScript.turn % 2 == 0)//偶数のターン
                    {//黒色にする
                        cardType = 0;
                    }
                    else//奇数のターン
                    {//白色にする
                        cardType = 1;
                    }
                }
            }
        }
    }

    //場所を選択する処理
    void ChooseALocation()
    {
        //置く場所を選ぶ
        //右キー
        if (Input.GetKeyDown(KeyCode.RightArrow) &&
            selectPosition < MAX_CARDS)
        {
            isInit = true;
            selectPosition++;
            LookAround();
        }
        //左キー
        if (Input.GetKeyDown(KeyCode.LeftArrow) &&
            selectPosition > 1)
        {
            isInit = true;
            selectPosition--;
            LookAround();
        }
        //上キー
        if (Input.GetKeyDown(KeyCode.UpArrow) &&
            selectPosition + 8 <= MAX_CARDS)
        {
            isInit = true;
            selectPosition += 8;
            LookAround();
        }
        //下キー
        if (Input.GetKeyDown(KeyCode.DownArrow) &&
            selectPosition - 8 >= 0)
        {
            isInit = true;
            selectPosition -= 8;
            LookAround();
        }

        //初期化
        if(isInit == true)
        {
            isListAdd = false;
            countTop = false;
            objList.upFrontObj.Clear();
            objList.floatObj.Clear();
            isInit = false;
        }

        //上の残りの列を数える
        if (countTop == false)
        {
            int nowRow = (int)(selectPosition / 8 - 0.01f) + 1;

            bool flg = false;
            for (int i = 0; i < 8; i++)
            {
                int num = selectPosition + (8 * (i + 1));
                if (cardNumber == num && flg == false)
                {
                    if (cardPlace == 1)
                    {
                        objList.floatObj.Add(num);
                        Debug.Log(num);
                    }
                    else
                    {
                        flg = true;
                    }
                }
            }
            countTop = true;
        }
    }

    //選択している箇所の周りを確認する
    void LookAround()
    {
        //選んでいる位置の上下左右の値を代入する
        selectUp = selectPosition + 8;
        selectDown = selectPosition - 8;
        selectRight = selectPosition + 1;
        selectLeft = selectPosition - 1;
    }

    //ひっくり返す
    void TurnOver()
    {
        if (isListAdd == false)
        {
            foreach (var item in objList.floatObj)
            {
                if (item == cardNumber&& cardPlace == 1)
                {//オブジェクトを控える
                    objList.upFrontObj.Add(this.gameObject);
                    isListAdd = true;
                }
            }
        }

    }
}
