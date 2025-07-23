using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
//joycon_R専用
public class ShakeManager_R : MonoBehaviour
{
    private static readonly Joycon.Button[] m_buttons =
      Enum.GetValues(typeof(Joycon.Button)) as Joycon.Button[];

    private List<Joycon> m_joycons;
    private Joycon m_joyconR;
    private Joycon.Button? m_pressedButtonR;

    private Vector3 lowPassValue;
    const float LowPassFilterFactor = 0.2f;

    public bool powerOn_R = false;

    public float chageValue_R = 0; //チャージ量
    private float resetTime;    //繰り返す間隔
    private float countTime;   //経過時間

    [SerializeField ] Value GetValue;
    private void Start()
    {
        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconR = m_joycons.Find(c => !c.isLeft);

        resetTime = GetValue.reset ;    //実行間隔を設定
        countTime = 0.0f ;   //経過時間をリセット
    }

    private void Update()
    {
        m_pressedButtonR = null;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        foreach (var button in m_buttons)
        {
            if (m_joyconR.GetButton(button))
            {
                m_pressedButtonR = button;
            }
        }


        //Vector3 movement = new Vector3(0, aveSpeed, 0);
        //transform.Translate(movement * Time.deltaTime, Space.World);

        countTime += Time.deltaTime;     //時間をカウントする

            //Rボタン
        if ((m_pressedButtonR == Joycon.Button.SHOULDER_1)&&( chageValue_R >= 0.0f))
        {
            if (countTime >= resetTime)
            {
                //ここで処理を実行
                    powerOn_R = true;

                    chageValue_R -= GetValue.d_chage;             
                Debug.Log("push!R_Button\nchage_R=" + chageValue_R);
                countTime = 0;   //経過時間をリセットする
            }
        }
        else
        {
            powerOn_R = false;
        }
        //ZRボタン
        if (m_pressedButtonR == Joycon.Button.SHOULDER_2)
        {
            if (chageValue_R <= GetValue.m_chage)
            {
                DetectNewStep(JoyCon_R_Update());
            }

            Debug.Log("push!ZR_Button\nchage_R=" + chageValue_R);
        }
        //リセット（プラスボタン）
        if(m_pressedButtonR ==Joycon .Button .PLUS )
        {
            chageValue_R = 0;

            Debug.Log("push!PLUS_Button\nchage_L=" + chageValue_R);
        }
    }
 
    float JoyCon_R_Update()
    {
        var accel = m_joyconR.GetAccel();
        Vector3 filteredAccelValue = FilterAccelValue(true, accel);
        var current_accel_value = filteredAccelValue.magnitude;
        // current_accel_valueを可視化する
        //DrawGraph
        //    .Add("current_accel_value", current_accel_value)
        //    .SetColor(Color.yellow)
        //    ;
        return current_accel_value;

    }

    Vector3 FilterAccelValue(bool smooth, Vector3 accVal)
    {
        if (smooth)
            lowPassValue = Vector3.Lerp(lowPassValue, accVal, LowPassFilterFactor);
        else
            lowPassValue = accVal;

        return lowPassValue;
    }
    bool lastStatus;
    bool bDirectionUp;
    int continueUpCount = 0;
    int continueUpFormerCount;
    float minValue = 0.0f, maxValue = 5.0f;
    float peakOfWave, valleyOfWave;
    bool DetectorPeak(float newValue, float oldValue)
    {
        lastStatus = bDirectionUp;

        // wave up
        if (newValue >= oldValue)
        {
            bDirectionUp = true;
            continueUpCount++;
        }
        // wave down
        else
        {
            continueUpFormerCount = continueUpCount;
            continueUpCount = 0;
            bDirectionUp = false;
        }
        // 山
        if (!bDirectionUp && lastStatus
                && (continueUpFormerCount >= 2 && (oldValue >= minValue && oldValue < maxValue)))
        {
            peakOfWave = oldValue;
            return true;
        }
        // 谷
        else if (!lastStatus && bDirectionUp)
        {
            valleyOfWave = oldValue;
            return false;
        }

        return false;
    }
    float last_accel_value;
    float timeOfLastPeak, timeOfThisPeak, timeOfNow;
    float threadThreshold = 1f;
    float aveSpeed;　　//振った威力
    void DetectNewStep(float values)
    {
        if (last_accel_value <= 0f)
        {
            last_accel_value = values;
        }
        else
        {
            
            if (DetectorPeak(values, last_accel_value))
            {
                timeOfLastPeak = timeOfThisPeak;
                timeOfNow = Time.time;

                if (timeOfNow - timeOfLastPeak >= 0.1f
                    && (peakOfWave - valleyOfWave >= threadThreshold))
                {
                    timeOfThisPeak = timeOfNow;               
                    aveSpeed = 1f / Peak_Valley_Thread(timeOfNow - timeOfLastPeak);
                    //ここでチャージ
                    chageValue_R += aveSpeed;
                    //chageValue++;
                }
            }
        }
        last_accel_value = values;
    }
    const int valueNum = 4;
    int tempCount;
    float[] tempValue = new float[valueNum];
    public float Peak_Valley_Thread(float value)
    {
        float tempThread = 1f;
        if (tempCount < valueNum)
        {
            tempValue[tempCount] = value;
            tempCount++;
        }
        else
        {
            tempThread = averageValue(tempValue, valueNum);
            for (int i = 1; i < valueNum; i++)
            {
                tempValue[i - 1] = tempValue[i];
            }
            tempValue[valueNum - 1] = value;
        }
        return tempThread;
    }

    public float averageValue(float[] value, int n)
    {
        float ave = 0;
        for (int i = 0; i < n; i++)
        {
            ave += value[i];
        }
        ave = ave / valueNum;
        return ave;
    }

}
