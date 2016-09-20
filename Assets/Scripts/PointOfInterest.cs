using UnityEngine;
using System.Collections;

public class PointOfInterest : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicTrack;
    [SerializeField]
    private AudioSource ambienceTrack;
    [SerializeField]
    private GameObject mediaParent;
    [SerializeField]
    private AnimationCurve lowpassCurve;

    private float closedLowPass = 800;
    private float openLowPass = 22000;
    private AudioLowPassFilter lowpass;

    internal bool isActive = false;

    private void Start()
    {
        ToggleMedia(false);
        InitializeAudio();
        this.mediaParent.transform.localScale = new Vector3(0, 0, 0);
    }

    public void ActivatePoint()
    {
        this.isActive = true;
        StartCoroutine(ScaleMediaUp());
    }

    public void DeactivatePoint()
    {
        this.isActive = false;
        StartCoroutine(ScaleMediaDown());
    }

    private void InitializeAudio()
    {
        this.lowpass = this.musicTrack.GetComponent<AudioLowPassFilter>();
        this.lowpass.cutoffFrequency = this.closedLowPass;
        this.musicTrack.SetSpatializerFloat(1, 2);
        this.musicTrack.SetSpatializerFloat(3, 0);
        this.musicTrack.SetSpatializerFloat(4, 0.2f);
        this.musicTrack.SetSpatializerFloat(5, 0);
        this.ambienceTrack.SetSpatializerFloat(1, 2);
        this.ambienceTrack.SetSpatializerFloat(3, 0);
        this.ambienceTrack.SetSpatializerFloat(4, 0.2f);
        this.ambienceTrack.SetSpatializerFloat(5, 0);
    }

    private void ToggleMedia(bool toggle)
    {
        this.mediaParent.SetActive(toggle);
    }

    private IEnumerator LowPassMusic()
    {
        while (this.lowpass.cutoffFrequency > this.closedLowPass)
        {
            this.lowpass.cutoffFrequency -= Time.deltaTime * 14000f;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator UnLowPassMusic()
    {
        Debug.Log("Unlowpassing");
        while (this.lowpass.cutoffFrequency < this.openLowPass)
        {
            this.lowpass.cutoffFrequency += Time.deltaTime * 14000f;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator ScaleMediaUp()
    {
        ToggleMedia(true);
        float currentScale = this.mediaParent.transform.localScale.x;
        while (currentScale < 1)
        {
            yield return new WaitForEndOfFrame();
            currentScale += Time.deltaTime;
            this.mediaParent.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            this.lowpass.cutoffFrequency = this.lowpassCurve.Evaluate(currentScale);
        }
    }

    private IEnumerator ScaleMediaDown()
    {
        float currentScale = this.mediaParent.transform.localScale.x;
        while (currentScale > 0)
        {
            yield return new WaitForEndOfFrame();
            currentScale -= Time.deltaTime * 2;
            this.mediaParent.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            this.lowpass.cutoffFrequency = this.lowpassCurve.Evaluate(currentScale);
        }
        ToggleMedia(false);
    }

    private IEnumerator StartAudioCoroutine(AudioSource source)
    {
        while (source.volume < 1)
        {
            yield return new WaitForEndOfFrame();
            source.volume += Time.deltaTime;
        }
    }

    private IEnumerator StopAudioCoroutine(AudioSource source)
    {
        while (source.volume > 0)
        {
            yield return new WaitForEndOfFrame();
            source.volume -= Time.deltaTime;
        }
    }
}