using UnityEngine;

//LR共通設定
public class Value : MonoBehaviour
{
    [Header("何毎秒ごと減らす処理をするのか(実行間隔)")]
    public float reset = 0.1f;
    [Header("チャージを減らす量")]
    public float d_chage = 0.3f;
    [Header("Maxチャージ量")]
    public float m_chage = 300.0f;
   
}
