using UnityEngine;

public class MediaIcon : MonoBehaviour
{
    private Transform playerTrans;
    private float currentDistance = 0;

    void Start()
    {
        this.playerTrans = Camera.main.transform;
    }

    private void Update()
    {
        this.currentDistance = Vector3.Distance(this.playerTrans.position, this.transform.position) * 4;
        this.transform.localScale = new Vector3(this.currentDistance, this.currentDistance, this.currentDistance);
        this.transform.Rotate(0, Time.deltaTime * 10f, 0, Space.World);
    }
}