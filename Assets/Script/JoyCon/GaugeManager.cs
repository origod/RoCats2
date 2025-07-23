using UnityEngine;
using UnityEngine.UI;

public class GaugeManager : MonoBehaviour
{
    [SerializeField] ShakeManager_L gauge_L;
    [SerializeField] ShakeManager_R gauge_R;

    [SerializeField] Slider slider_L;
    [SerializeField] Slider slider_R;

    [SerializeField] Value GetValue;

    int maxGauge = 100;
    int currentGauge_L,currentGauge_R;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider_L.value = 0;
        slider_R.value = 0;

        maxGauge = (int)GetValue.m_chage;

        currentGauge_L = currentGauge_R = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //現在チャージ量を適用
        currentGauge_L = (int)gauge_L.chageValue_L;
        currentGauge_R = (int)gauge_R.chageValue_R;

        //描写
        slider_L.value = (float)currentGauge_L / (float)maxGauge;
        slider_R.value = (float)currentGauge_R / (float)maxGauge;
    }
}
