using UnityEngine;

public class FunSearch : MonoBehaviour
{
    public float angle = 45f;

    bool isPlayerInRange;

    int Risk = 0;

    private float resetTime;   //�J��Ԃ��Ԋu
    private float countTime;   //�o�ߎ���

    private void Start()
    {
        resetTime = 1.0f;    //���s�Ԋu��ݒ�
        countTime = 0.0f;   //�o�ߎ��Ԃ����Z�b�g
    }
    private void Update()
    {
        countTime += Time.deltaTime;     //���Ԃ��J�E���g����
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
        if (other.gameObject.tag == "Player") //���E�͈͓̔��̓����蔻��
        {
            //���E�̊p�x���Ɏ��܂��Ă��邩
            Vector3 posDelta = other.transform.position - this.transform.position;
            float target_angle = Vector3.Angle(this.transform.forward , posDelta);

            if (target_angle < angle) //target_angle��angle�Ɏ��܂��Ă��邩�ǂ���
            {
                if (Physics.Raycast(this.transform.position, posDelta, out RaycastHit hit)) //Ray���g�p����target�ɓ������Ă��邩����
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
