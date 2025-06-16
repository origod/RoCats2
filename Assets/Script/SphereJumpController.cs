using UnityEngine;

public class SphereRocketController : MonoBehaviour
{
    public GameObject leftSphereObject;
    public GameObject rightSphereObject;
    public float thrustForce = 20f;

    private Rigidbody leftRb;
    private Rigidbody rightRb;

    void Start()
    {
        leftRb = leftSphereObject.GetComponent<Rigidbody>();
        rightRb = rightSphereObject.GetComponent<Rigidbody>();

        if (leftRb == null || rightRb == null)
        {
            Debug.LogError("スフィアに Rigidbody がアタッチされていません！");
        }
    }

    void FixedUpdate()
    {
        // 左クリック押している間、左スフィアに上方向の力を与え続ける
        if (Input.GetMouseButton(0) && leftRb != null)
        {
            leftRb.AddForce(leftSphereObject.transform.up * thrustForce, ForceMode.Force);
        }

        // 右クリック押している間、右スフィアに上方向の力を与え続ける
        if (Input.GetMouseButton(1) && rightRb != null)
        {
            rightRb.AddForce(rightSphereObject.transform.up * thrustForce, ForceMode.Force);
        }
    }
}
