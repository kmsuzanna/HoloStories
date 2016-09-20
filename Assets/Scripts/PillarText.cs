using UnityEngine;

public class PillarText : MonoBehaviour
{
    [SerializeField]
    private Transform pillar;

    private Transform playerTrans;
    private float startY = 0;

    private void Start()
    {
        this.playerTrans = Camera.main.transform;
        this.startY = this.transform.position.y;
    }

    private void Update()
    {
        float playerDist = Vector3.Distance(this.pillar.position, this.playerTrans.position);
        Vector3 tempPos = this.transform.position;
        tempPos.y = this.startY + playerDist / 8;
        this.transform.position = tempPos;
    }
}