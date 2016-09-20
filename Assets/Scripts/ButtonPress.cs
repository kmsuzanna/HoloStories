using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
    [SerializeField]
    private Image buttonImage;
    [SerializeField]
    private GameObject objectPrefab;

    public void Start()
    {
        Color tempColor = this.buttonImage.color;
        tempColor.a = 0.5f;
        this.buttonImage.color = tempColor;
    }

    public void OnGazeEnter()
    {
        Color tempColor = this.buttonImage.color;
        tempColor.a = 1f;
        this.buttonImage.color = tempColor;
    }

    public void OnGazeLeave()
    {
        Color tempColor = this.buttonImage.color;
        tempColor.a = 0.5f;
        this.buttonImage.color = tempColor;
    }

    public void OnSelect()
    {
        if (MediaMenuControl.MenuEnabled())
        {
            SpawnPrefab();
        }
    }

    private void SpawnPrefab()
    {
        if (this.objectPrefab == null)
        {
            return;
        }

        MediaMenuControl.HideMenu();

        Vector3 objectPos = Camera.main.transform.position + Camera.main.transform.forward;
        Instantiate(this.objectPrefab, objectPos, Quaternion.identity);
    }
}