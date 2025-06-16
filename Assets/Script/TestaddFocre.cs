using System.Security.Cryptography;
using UnityEngine;

public class TestaddFocre : MonoBehaviour
{

    public GameObject LeftBottle;
    public GameObject RightBottle;
    public float forceAmount = 5f;

    private Rigidbody rdLeft;
    private Rigidbody rdRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rdLeft = LeftBottle.GetComponent<Rigidbody>();
        rdRight = RightBottle.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            rdLeft.AddForce(Vector3.up * forceAmount * Time.deltaTime);
        }

        if (Input.GetMouseButton(1))
        {
            rdRight.AddForce(Vector3.up * forceAmount * Time.deltaTime);
        }
    }
}
