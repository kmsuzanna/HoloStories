using UnityEngine;
using System.Collections;

public class MediaMenuControl : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    private bool canAdd = true;
    private static MediaMenuControl Instance;

    private float ScaleTime = 4f;

    private void Start()
    {
        Instance = this;
    }

    public static void HideMenu()
    {
        Instance.StartCoroutine(Instance.ScaleMenuDown());
    }

    public static void ShowMenu()
    {
        Instance.StartCoroutine(Instance.ScaleMenuUp());
    }

    public static bool MenuEnabled()
    {
        return Instance.canAdd;
    }

    private IEnumerator ScaleMenuDown()
    {
        Instance.canAdd = false;
        float currentScale = this.transform.localScale.x;
        while (currentScale > 0)
        {
            currentScale -= Time.deltaTime * this.ScaleTime;
            this.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator ScaleMenuUp()
    {
        float currentScale = this.transform.localScale.x;
        while (currentScale < 1)
        {
            currentScale += Time.deltaTime * this.ScaleTime;
            this.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            yield return new WaitForEndOfFrame();
        }
        Instance.canAdd = true;
    }
}