using UnityEngine;

public class SphereRocketController : MonoBehaviour
{
    public GameObject leftSphereObject;
    public GameObject rightSphereObject;
    private Rigidbody leftRb;
    private Rigidbody rightRb;

    public float thrustForce = 20f;

    bool inputCK = true;

    Vector3 dir = Vector3.up;

    void Start()
    {
        leftRb = leftSphereObject.GetComponent<Rigidbody>();
        rightRb = rightSphereObject.GetComponent<Rigidbody>();
        leftRb.linearDamping = 10;
        rightRb.linearDamping = 10;

        if (leftRb == null || rightRb == null)
        {
            Debug.LogError("NOアタッチ");
        }
    }

    void FixedUpdate()
    {
        //LR同時に押されたとき
        if ((Input.GetMouseButton(0) && leftRb != null) && (Input.GetMouseButton(1) && rightRb != null))
        {
            Debug.Log("LR");
            leftRb.AddForce(leftSphereObject.transform.up * thrustForce, ForceMode.Impulse);
            rightRb.AddForce(rightSphereObject.transform.up * thrustForce, ForceMode.Impulse);
            inputCK = false;
            rightRb.useGravity = false;
            leftRb.useGravity = false;

        }
        else
        {
            inputCK = true;
        }
        // Lが押されたとき
        if (Input.GetMouseButton(0) && leftRb != null)
        {
            if (inputCK)
            {
                Debug.Log("L");
                rightRb.useGravity = false;
                leftRb.useGravity = false;
                leftRb.AddForce(leftSphereObject.transform.up * (thrustForce * 1.5f), ForceMode.Impulse);
                rightRb.AddForce(rightSphereObject.transform.up * thrustForce*1.2f, ForceMode.Impulse);
            }
        }
        else if (Input.GetMouseButton(1) && rightRb != null)// Rが押されたとき
        {
            if (inputCK)
            {
                Debug.Log("R");
                rightRb.useGravity = false;
                leftRb.useGravity = false;
                rightRb.AddForce(rightSphereObject.transform.up * (thrustForce * 1.5f), ForceMode.Impulse);
                leftRb.AddForce(leftSphereObject.transform.up * thrustForce * 1.2f, ForceMode.Impulse);
            }
        }
        else
        {
            //重力を使う
            leftRb.useGravity = true;
            rightRb.useGravity = true;
        }
        
    }
}
