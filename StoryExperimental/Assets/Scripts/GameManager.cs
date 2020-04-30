using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RPGTalk myTalk;
    public GameObject[] backgrounds;
    public StressReceiver sr;
    private int bgIndex = 0;
    [Range(0, 1)]
    public float rainfall;
    private List<string> keywords;
    public AudioSource musicPlayer;
    public AudioClip[] musics;
    private int musicIndex = 0;
    private AudioSource fxSource;
    public AudioClip[] soundFX;

    private void Start()
    {
        myTalk.OnMadeChoice += OnMadeChoice;
        myTalk.OnPlayNext += OnPlayNext;
        keywords = new List<string>();
        sr.enabled = false;
        fxSource = Camera.main.GetComponent<AudioSource>();
    }

    private void Update()
    {
        //Debug.Log(myTalk.cutscenePosition);
        //Debug.Log(myTalk.rpgtalkElements[myTalk.cutscenePosition - 1].dialogText);
    }

    private void ChangeMusic()
    {
        musicPlayer.clip = musics[musicIndex];
        musicPlayer.mute = false;
        musicPlayer.Play();
        musicIndex++;
    }

    IEnumerator MusicSwitch()
    {
        musicPlayer.GetComponent<Animator>().Play("audioFade");
        yield return new WaitForSeconds(1);
        ChangeMusic();
    }

    IEnumerator MusicFadeOut()
    {
        musicPlayer.GetComponent<Animator>().Play("audioFade");
        yield return new WaitForSeconds(1);
        musicPlayer.Stop();
    }

    public void ChangeBackground()
    {
        backgrounds[bgIndex].GetComponent<Animator>().Play("bgFade");
        bgIndex++;
    }

    private void OnPlayNext()
    {
        switch(myTalk.cutscenePosition)
        {
            case 16:
                ChangeBackground();
                break;
            case 17:
                rainfall = 0;
                ChangeBackground();
                ChangeMusic();
                break;
            case 26:
                rainfall = 0.4f;
                ChangeBackground();
                StartCoroutine("MusicFadeOut");
                break;
            case 27:
                ChangeBackground();
                break;
            case 29:
                rainfall = 0.85f;
                sr.enabled = true;
                break;
            case 30:
                ChangeBackground();
                ChangeMusic();
                break;
            case 31:
                fxSource.PlayOneShot(soundFX[0]);
                break;
        }
    }

    private void OnMadeChoice(string questionID, int choiceNumber)
    {
        switch(questionID)
        {
            case "border":
                ChangeBackground();
                break;
            case "from":
                string response;
                if(choiceNumber == 0)
                {
                    response = "The sergeant spits derisively into the snow. \"Imperial bastards,\" he grunts. \"Well, I won't hold it against you, but don't go spreading any crazy ideas. You'll find no love lost for Empress Isabel or her strange gods up here in the free north.\"";
                    myTalk.VariableReplace(myTalk.variables[0].variableValue, response, 0);
                    keywords.Add("fromEldOmbyr");
                    break;
                }
                else if (choiceNumber == 1)
                {
                    response = "\"Warm Front, eh?\" the sergeant says wistfully. \"I've always wanted to go there someday, maybe retire along the coast. Worry I'd get soft in the warmth, though. No offense.\" He hawks a wad of spit into the snow. \"My respects to Goodall, anyway. Now there's a leader of men. Not some austere kook like Empress Isabel.\"";
                    myTalk.VariableReplace(myTalk.variables[0].variableValue, response, 0);
                    keywords.Add("fromWarmFront");
                    break;
                }
                else if(choiceNumber == 2)
                {
                    response = "The sergeant scoffs. \"All the way from the old sea fort, eh? Traveling this light, I'd wager you're no scientist, and the state of your clothes says you're no diplomat either. Well, whoever you are, you'll find a warm hearth wherever you go up here. We're not so keen on strange ideas about dividing people up like they do in Ghyde Light.\"";
                    myTalk.VariableReplace(myTalk.variables[0].variableValue, response, 0);
                    keywords.Add("fromGhydeLight");
                    break;
                }
                break;
            case "gender":
                string responded;
                if (choiceNumber == 0)
                {
                    responded = "\"The pleasure's all mine, <i>sir</i>.\"";
                    myTalk.VariableReplace(myTalk.variables[0].variableValue, responded, 0);
                    keywords.Add("genderMale");
                    break;
                }
                else if (choiceNumber == 1)
                {
                    responded = "\"The pleasure's all mine, miss.\"";
                    myTalk.VariableReplace(myTalk.variables[0].variableValue, responded, 0);
                    keywords.Add("genderFemale");
                    break;
                }
                else if (choiceNumber == 2)
                {
                    responded = "\"No, I suppose it doesn't.\"";
                    myTalk.VariableReplace(myTalk.variables[0].variableValue, responded, 0);
                    keywords.Add("genderOther");
                    break;
                }
                break;
            case "waiting":
                if(keywords.Contains("genderMale"))
                {
                    myTalk.VariableReplace(myTalk.variables[0].variableValue, "sir", 0);
                }
                else if (keywords.Contains("genderFemale"))
                {
                    myTalk.VariableReplace(myTalk.variables[0].variableValue, "miss", 0);
                }
                else if (keywords.Contains("genderOther"))
                {
                    myTalk.VariableReplace(myTalk.variables[0].variableValue, "pal", 0);
                }
                break;
        }
    }
}
