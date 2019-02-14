using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;


public class ColorMatchScript : MonoBehaviour
{
    public KMAudio Audio;

    public KMSelectable ButtonRed;
    public KMSelectable ButtonBlue;
    public KMSelectable ButtonGreen;
    public KMSelectable ButtonYellow;

    public Material[] ledOptions;
    public Renderer led;
    private int ledIndex = 0;

    //Twitch Plays
#pragma warning disable 414
    private readonly string TwitchHelpMessage = "Type '!{0} press red', '!{0} press r', '!{0} red' or '!{0} r' to press red";
#pragma warning restore 414

    public KMSelectable[] ProcessTwitchCommand(string command)
    {
        if (Regex.IsMatch(command, @"^\s*(press|)\s*(red|r)\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            return new KMSelectable[] { ButtonRed };
        }
        else if (Regex.IsMatch(command, @"^\s*(press|)\s*(blue|b)\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            return new KMSelectable[] { ButtonBlue };
        }
        else if (Regex.IsMatch(command, @"^\s*(press|)\s*(green|g)\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            return new KMSelectable[] { ButtonGreen };
        }
        else if (Regex.IsMatch(command, @"^\s*(press|)\s*(yellow|y)\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            return new KMSelectable[] { ButtonYellow };
        }
        return null;
    }

    void Awake()
    {
        GetComponent<KMNeedyModule>().OnNeedyActivation += OnNeedyActivation;
        GetComponent<KMNeedyModule>().OnNeedyDeactivation += OnNeedyDeactivation;
        GetComponent<KMNeedyModule>().OnTimerExpired += OnTimerExpired;
    }

    void Start()
    {
        ButtonRed.OnInteract += delegate () { AnsweredRed(); return false; };
        ButtonBlue.OnInteract += delegate () { AnsweredBlue(); return false; };
        ButtonGreen.OnInteract += delegate () { AnsweredGreen(); return false; };
        ButtonYellow.OnInteract += delegate () { AnsweredYellow(); return false; };
    }

    protected void OnNeedyActivation()
    {
        PickLEDColour();
    }

    void PickLEDColour()
    {
        ledIndex = UnityEngine.Random.Range(0, 4);
        led.material = ledOptions[ledIndex];
    }

    void AnsweredRed()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        ButtonRed.AddInteractionPunch();
        if (ledIndex == 0)
        {
            Solve();
        }
        else
        {
            GetComponent<KMNeedyModule>().OnStrike();
        }
    }

    void AnsweredBlue()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        ButtonBlue.AddInteractionPunch();
        if (ledIndex == 1)
        {
            Solve();
        }
        else
        {
            GetComponent<KMNeedyModule>().OnStrike();
        }
    }

    void AnsweredGreen()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        ButtonGreen.AddInteractionPunch();
        if (ledIndex == 2)
        {
            Solve();
        }
        else
        {
            GetComponent<KMNeedyModule>().OnStrike();
        }
    }

    void AnsweredYellow()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        ButtonYellow.AddInteractionPunch();
        if (ledIndex == 3)
        {
            Solve();
        }
        else
        {
            GetComponent<KMNeedyModule>().OnStrike();
        }
    }

    protected void OnNeedyDeactivation()
    {

    }

    protected void OnTimerExpired()
    {
        GetComponent<KMNeedyModule>().OnStrike();
        ledIndex = 4;
        led.material = ledOptions[ledIndex];
    }

    protected bool Solve()
    {
        GetComponent<KMNeedyModule>().OnPass();
        ledIndex = 4;
        led.material = ledOptions[ledIndex];

        return false;
    }
}