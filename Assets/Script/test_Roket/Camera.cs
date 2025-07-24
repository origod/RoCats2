using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    public float minX;
    public float maxX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = player.transform.position + offset;
        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        transform.position = targetPos;
    }
}
