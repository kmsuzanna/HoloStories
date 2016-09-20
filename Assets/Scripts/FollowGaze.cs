using UnityEngine;

public class FollowGaze : MonoBehaviour
{
    [SerializeField]
    private float distance = 2f;
    [SerializeField]
    private float tolerance = 1f;
    [SerializeField]
    private float smoothingScale = 3f;

    private Transform playerTrans;
    private Vector3 targetPosition;

    private void Start()
    {
        this.playerTrans = Camera.main.transform;
    }

    private void Update()
    {
        SmoothFollow();
    }

    private void SmoothFollow()
    {
        this.targetPosition = this.playerTrans.position + (this.playerTrans.forward * this.distance);
        if (Vector3.Distance(targetPosition, this.transform.position) > this.tolerance)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, Time.deltaTime * this.smoothingScale);
        }
    }
}