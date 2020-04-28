using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RPGTalk myTalk;

    private void Start()
    {
        myTalk.OnMadeChoice += OnMadeChoice;
    }

    private void Update()
    {
        //Debug.Log(myTalk.cutscenePosition);
        //Debug.Log(myTalk.rpgtalkElements[myTalk.cutscenePosition - 1].dialogText);
    }

    private void OnMadeChoice(string questionID, int choiceNumber)
    {

        if(questionID == "0")
        {
            myTalk.VariableReplace(myTalk.variables[0].variableValue, "option " + (choiceNumber + 1));
        }
    }
}
