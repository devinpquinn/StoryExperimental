using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RPGTalk myTalk;
    public GameObject[] backgrounds;
    private int bgIndex = 0;
    [Range(0, 1)]
    public float rainfall;

    private void Start()
    {
        myTalk.OnMadeChoice += OnMadeChoice;
    }

    private void Update()
    {
        //Debug.Log(myTalk.cutscenePosition);
        //Debug.Log(myTalk.rpgtalkElements[myTalk.cutscenePosition - 1].dialogText);
    }

    public void ChangeBackground()
    {
        backgrounds[bgIndex].GetComponent<Animator>().Play("bgFade");
        bgIndex++;
    }

    private void OnMadeChoice(string questionID, int choiceNumber)
    {

        if(questionID == "0")
        {
            myTalk.VariableReplace(myTalk.variables[0].variableValue, "option " + (choiceNumber + 1));
        }
    }
}
