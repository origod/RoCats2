using UnityEngine;

public class FunSearch : MonoBehaviour
{
    public float angle = 45f;

    bool isPlayerInRange;

    int Risk = 0;

    private float resetTime;   //ŒJ‚è•Ô‚·ŠÔŠu
    private float countTime;   //Œo‰ßŽžŠÔ

    private void Start()
    {
        resetTime = 1.0f;    //ŽÀsŠÔŠu‚ðÝ’è
        countTime = 0.0f;   //Œo‰ßŽžŠÔ‚ðƒŠƒZƒbƒg
    }
    private void Update()
    {
        countTime += Time.deltaTime;     //ŽžŠÔ‚ðƒJƒEƒ“ƒg‚·‚é
        if (isPlayerInRange)
        {
            if (countTime >= resetTime)
            {
                Risk++;
                Debug.Log("Risk:" + Risk + "%");
                countTime = 0.0f;
            }
        }
        Debug.DrawRay(transform.position, transform.forward,Color.red );
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") //Ž‹ŠE‚Ì”ÍˆÍ“à‚Ì“–‚½‚è”»’è
        {
            //Ž‹ŠE‚ÌŠp“x“à‚ÉŽû‚Ü‚Á‚Ä‚¢‚é‚©
            Vector3 posDelta = other.transform.position - this.transform.position;
            float target_angle = Vector3.Angle(this.transform.forward , posDelta);

            if (target_angle < angle) //target_angle‚ªangle‚ÉŽû‚Ü‚Á‚Ä‚¢‚é‚©‚Ç‚¤‚©
            {
                if (Physics.Raycast(this.transform.position, posDelta, out RaycastHit hit)) //Ray‚ðŽg—p‚µ‚Ätarget‚É“–‚½‚Á‚Ä‚¢‚é‚©”»•Ê
                {
                    if (hit.collider == other)
                    {
                        isPlayerInRange = true;
                        Debug.Log("range of view");
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject .tag == "Player")
        {
            isPlayerInRange = false;
        }
    }
}
