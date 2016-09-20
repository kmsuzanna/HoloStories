using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField]
    private LayerMask gazeLayer;

    private float smoothingScale = 4f;
    private float defaultGazeDistance = 2f;
    private float maxGazeDistance = 5f;
    private bool isPlaced = false;
    private Transform playerTrans;

    void Start()
    {
        this.playerTrans = Camera.main.transform;
    }

    void Update()
    {
        if (!this.isPlaced)
        {
            SmoothToLocation(GetGazeLocation());
        }
    }

    public void OnSelect()
    {
        this.isPlaced = true;
        MediaMenuControl.ShowMenu();
    }

    private Vector3 GetGazeLocation()
    {
        RaycastHit tempHit = new RaycastHit();
        if (Physics.Raycast(this.playerTrans.position, this.playerTrans.forward, out tempHit, this.maxGazeDistance, this.gazeLayer))
        {
            return tempHit.point;
        }
        else
        {
            return this.playerTrans.position + (this.playerTrans.forward * this.defaultGazeDistance);
        }
    }

    private void SmoothToLocation(Vector3 newLocation)
    {
        this.transform.position = Vector3.Lerp(this.transform.position, newLocation, Time.deltaTime * this.smoothingScale);
    }
}