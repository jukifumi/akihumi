using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData : MonoBehaviour
{
    //カードの場所
    public int cardPlace;
    // 0 =　手札
    // 1 =　表
    // 2 =　裏

    //カードの種類
    public int cardType;
    // 0 = 黒
    // 1 = 白
    // 2 = ジョーカー

    //カードの番号
    public int cardNumber;

    // Use this for initialization
    void Start()
    {
        cardPlace = 0;
        cardType = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
