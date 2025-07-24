using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI detectText;
    public float Gauge = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        detectText.text = $"Discovery rate: {Mathf.RoundToInt(Gauge)}%";
    }

    public void addGetect(float amount)
    {
        Gauge = Mathf.Clamp(Gauge + amount, 0f, 100f);
    }
}
