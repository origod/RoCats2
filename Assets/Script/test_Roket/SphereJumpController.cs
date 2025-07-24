using UnityEngine;

public class SphereRocketController : MonoBehaviour
{
    public GameObject player;
    public GameObject leftSphereObject;
    public GameObject rightSphereObject;
    private Rigidbody leftRb;
    private Rigidbody rightRb;

    public float thrustForce = 20f;

    bool inputCK = true;

    Vector3 dir = Vector3.up;

    [SerializeField] ShakeManager_L leftSm;
    [SerializeField] ShakeManager_R rightSm;

    void Start()
    {
        leftRb = leftSphereObject.GetComponent<Rigidbody>();
        rightRb = rightSphereObject.GetComponent<Rigidbody>();
        leftRb.linearDamping = 10;
        rightRb.linearDamping = 10;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ;

        if (leftRb == null || rightRb == null)
        {
            Debug.LogError("NOアタッチ");
        }
    }

    void FixedUpdate()
    {
        //LR同時に押されたとき
        if ((Input.GetMouseButton(0) && leftRb != null) && (Input.GetMouseButton(1) && rightRb != null)||
            (leftSm .powerOn_L &&leftRb !=null)&&(rightSm.powerOn_R &&rightRb!=null))
        {
            Debug.Log("LR");
            leftRb.AddForce(leftSphereObject.transform.up * (thrustForce*1.5f), ForceMode.Impulse);
            rightRb.AddForce(rightSphereObject.transform.up * (thrustForce*1.5f), ForceMode.Impulse);
            inputCK = false;
            rightRb.useGravity = false;
            leftRb.useGravity = false;

        }
        else
        {
            inputCK = true;
        }
        // Lが押されたとき
        if ((Input.GetMouseButton(0) && leftRb != null)||(leftSm.powerOn_L && leftRb != null))
        {
            if (inputCK)
            {
                Debug.Log("L");
                rightRb.useGravity = false;
                leftRb.useGravity = false;
                leftRb.AddForce(leftSphereObject.transform.up * (thrustForce * 1f), ForceMode.Impulse);
                rightRb.AddForce(rightSphereObject.transform.up * thrustForce*0.8f, ForceMode.Impulse);
            }
        }
        else if ((Input.GetMouseButton(1) && rightRb != null)||(rightSm.powerOn_R && rightRb != null))// Rが押されたとき
        {
            if (inputCK)
            {
                Debug.Log("R");
                rightRb.useGravity = false;
                leftRb.useGravity = false;
                rightRb.AddForce(rightSphereObject.transform.up * (thrustForce * 1f), ForceMode.Impulse);
                leftRb.AddForce(leftSphereObject.transform.up * thrustForce * 0.8f, ForceMode.Impulse);
            }
        }
        else
        {
            Debug.Log("AnLR");
            rightRb.AddForce(Vector3.down * (thrustForce*1.5f), ForceMode.Impulse);
            leftRb.AddForce(Vector3.down * (thrustForce*1.5f), ForceMode.Impulse);
            //rightRb.linearVelocity = Vector3.zero;
            //leftRb.linearVelocity = Vector3.zero;
            //重力を使う
            leftRb.useGravity = true;
            rightRb.useGravity = true;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            player.transform.position += Vector3.up * 0.5f;
            player.transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
