using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollCreate : MonoBehaviour
{

    [SerializeField]
    float interval;//間隔

    [SerializeField]
    GameObject[] collBox;

    //静的定数
    private const int MAX_CARDS = 64;//複製するオブジェクトの最大数

    // Start is called before the first frame update
    void Start()
    {
        //オブジェクトを複製する
        for(var i=0; i < MAX_CARDS; i++)
        {
            GameObject newCards = Instantiate(collBox[0]);
            newCards.transform.position = new Vector3(-4.3f + (i % 8 * interval), 0, -4.3f + (i / 8 * interval));//盤面のマスに合うように間隔をあける
            newCards.GetComponent<MeshRenderer>().enabled = false;//オブジェクトを見えないようにする

            //スクリプトをアタッチする
            newCards.AddComponent<CardData>();
            newCards.AddComponent<SelectPlace>();

            //cardNumberの値を変える
            newCards.GetComponent<CardData>().cardNumber = i + 1;
            newCards.GetComponent<SelectPlace>().cardNumber = i + 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
