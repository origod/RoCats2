using UnityEngine;

public class Window : MonoBehaviour
{

    public float highRate = 10f;
    public float lowTate = 5f;
    public Transform player;
    public UI gaugeUI;

    private BoxCollider triggerCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggerCollider = GetComponent<BoxCollider>();
        if(!triggerCollider.isTrigger)
        {
            triggerCollider.isTrigger = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("��������");
        if(other.transform==player)
        {
            Vector3 closestPoint = triggerCollider.ClosestPoint(player.position);
            float distance = Vector3.Distance(player.position, closestPoint);

            //���̔��]
            if(distance<0.2f)
            {
                DiscoveryGauge(highRate);
            }
            else
            {
                DiscoveryGauge(lowTate);
            }
        }
    }
    void DiscoveryGauge(float amount)
    {
        //�������ꂽ�Ƃ��̏����������B
        gaugeUI.addGetect(amount*Time.deltaTime);
        Debug.Log($"������+{amount}");
    }
}
