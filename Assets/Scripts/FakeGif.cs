using UnityEngine;
using System.Collections;

public class FakeGif : MonoBehaviour
{
    [SerializeField]
    private Texture[] stills;
    [SerializeField]
    private float frameHold = 0.2f;

    private float currentTime = 0;
    private int currentFrame = 0;
    private Material mat;

    void Start()
    {
        this.mat = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        if (this.currentTime < this.frameHold)
        {
            this.currentTime += Time.deltaTime;
            return;
        }

        this.currentTime = 0;
        this.mat.mainTexture = this.stills[this.currentFrame];
        this.currentFrame++;
        if (this.currentFrame >= this.stills.Length)
        {
            this.currentFrame = 0;
        }
    }
}