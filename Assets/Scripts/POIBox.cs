using UnityEngine;
using UnityEngine.Windows.Speech;
using HoloToolkit.Unity;
using System.Collections.Generic;

public class POIBox : MonoBehaviour
{
    [SerializeField]
    private PointOfInterest point;
    [SerializeField]
    private TapToPlace placement;
    [SerializeField]
    private ParticleSystem particles;
    [SerializeField]
    private AudioSource boxSounds;
    [SerializeField]
    private AudioClip openSound;
    [SerializeField]
    private AudioClip closeSound;
    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color highlightColor;
    [SerializeField]
    private bool keywordListener = false;

    private AudioSource source;
    private static bool inEditMode = false;
    private KeywordRecognizer keywords;
    private Animator anim;
    private List<Material> mats;

    private const string KeywordEdit = "Edit";
    private const string KeywordExit = "Play";
    private const string OpenTrigger = "Open";
    private const string CloseTrigger = "Close";

    private void Start()
    {
        if (this.keywordListener)
        {
            RegisterKeywords();
        }
        this.anim = GetComponent<Animator>();
        this.source = GetComponent<AudioSource>();
        ToggleEditMode(false);
        GatherMats();
        OnGazeLeave();
    }

    private void GatherMats()
    {
        this.mats = new List<Material>();
        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
        Debug.Log("MESHES: " + meshes.Length);
        for (int i = 0; i < meshes.Length; i++)
        {
            Material tempMat = meshes[i].material;
            if (tempMat != null)
            {
                this.mats.Add(tempMat);
            }
        }
    }

    private void RegisterKeywords()
    {
        string[] keywerds = new string[2];
        keywerds[0] = KeywordEdit;
        keywerds[1] = KeywordExit;
        this.keywords = new KeywordRecognizer(keywerds);
        this.keywords.OnPhraseRecognized += this.OnKeyword;
        this.keywords.Start();
    }

    private void OnKeyword(PhraseRecognizedEventArgs args)
    {
        this.source.Play();
        if (args.text == KeywordEdit)
        {
            ToggleEditMode(true);
        }
        else if (args.text == KeywordExit)
        {
            ToggleEditMode(false);
        }
    }

    private void ToggleEditMode(bool toggle)
    {
        inEditMode = toggle;
        TapToPlace.CanPlace = toggle;
    }

    private void SetBoxColor(Color newColor)
    {
        for (int i = 0; i < this.mats.Count; i++)
        {
            this.mats[i].color = newColor;
        }
    }

    public void OnGazeEnter()
    {
        SetBoxColor(this.highlightColor);
    }

    public void OnGazeLeave()
    {
        SetBoxColor(this.defaultColor);
    }

    public void OnSelect()
    {
        if (inEditMode)
        {

        }
        else
        {
            if (this.point.isActive)
            {
                this.point.DeactivatePoint();
                this.anim.SetTrigger(CloseTrigger);
                this.boxSounds.PlayOneShot(this.closeSound);
            }
            else
            {
                this.point.ActivatePoint();
                this.anim.SetTrigger(OpenTrigger);
                this.particles.Play();
                this.boxSounds.PlayOneShot(this.openSound);
            }
        }
    }
}