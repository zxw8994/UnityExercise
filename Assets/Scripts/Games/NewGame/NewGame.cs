using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NewGame : GameBase {

    const string INSTRUCTIONS = "Press <color=cyan>SpaceBar</color> only when you see a <color=green>Green Square</color>.";
    const string FINISHED = "FINISHED";
    const string RESPONSE_GUESS = "No Guessing";
    const string RESPONSE_CORRECT = "GOOD";
    const string RESPONSE_TIMEOUT = "Missed it";
    const string RESPONSE_SLOW = "Too Slow";
    const string RESPONSE_WRONG = "Not the Red Square.";
    const string RESPONSE_PASSED = "Not Fooled"; // Is it Necessary??
    Color RESPONSE_COLOR_GOOD = Color.green;
    Color RESPONSE_COLOR_BAD = Color.red;

    public GameObject uiCanvas;

    public GameObject stimulus;

    public GameObject feedbackTextPrefab;

    public Text instructionsText;

    public override GameBase StartSession(TextAsset sessionFile)
    {
        return base.StartSession(sessionFile);
    }

    /*protected virtual IEnumerator RunTrials(SessionData data)
    {

    }
    

    protected virtual IEnumerator DisplayStimulus(Trial t)
    {

    }
    */

    protected override void FinishedSession()
    {
        base.FinishedSession();
    }

    public override void PlayerResponded(KeyCode key, float time)
    {
        base.PlayerResponded(key, time);
    }

    protected override void AddResult(Trial t, float time)
    {
        TrialResult r = new TrialResult(t);
        r.responseTime = time;
        if(time == 0)
        {
            DisplayFeedBack(RESPONSE_TIMEOUT, RESPONSE_COLOR_BAD);
            GUILog.Log("Fail! No response!");
        }
        else
        {
            if (IsGuessResponse(time))
            {
                // Responded before stimulus 

            }
            else if (IsValidResponse(time))
            {
                // Responded Correctly

            }
            else if (IsWrongResponse(time))
            {
                // Responded to the Red Square

            }
            else
            {
                // Responed too slow

            }
        }
        sessionData.results.Add(r);
    }

    private void DisplayFeedBack(string text, Color color)
    {

    }

    protected float GetAccuracy(Trial t, float time)
    {

        return 0;
    }

    protected bool IsGuessResponse(float time)
    {

        return true;
    }

    protected bool IsValidResponse(float time)
    {

        return true;
    }

    protected bool IsWrongResponse(float time)
    {

        return true;
    }

}
