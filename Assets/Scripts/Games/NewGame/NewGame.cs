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
        base.StartSession(sessionFile);

        instructionsText.text = INSTRUCTIONS;
        StartCoroutine(RunTrials(SessionData));

        return this;
    }

    protected virtual IEnumerator RunTrials(SessionData data)
    {
        foreach(Trial t in data.trials)
        {
            StartTrial(t);
            yield return StartCoroutine(DisplayStimulus(t));
            EndTrial(t);
        }
        FinishedSession();
        yield break;
    }
    
    // Will need to tell this which stimulus to display Red/Green
    protected virtual IEnumerator DisplayStimulus(Trial t)
    {
        GameObject stim = stimulus;
        stim.SetActive(false);

        yield return new WaitForSeconds(t.delay);

        StartInput();
        stim.SetActive(true);

        //yield return new WaitForSeconds(((NewGameTrial)t).duration);
        stim.SetActive(false);
        EndInput();

        yield break;

    }
    

    protected override void FinishedSession()
    {
        base.FinishedSession();
        instructionsText.text = FINISHED;
    }

    public override void PlayerResponded(KeyCode key, float time)
    {
        if (!listenForInput)
        {
            return;
        }
        base.PlayerResponded(key, time);
        if(key == KeyCode.Space)
        {
            EndInput();
            AddResult(CurrentTrial, time);
        }
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
                DisplayFeedBack(RESPONSE_GUESS, RESPONSE_COLOR_BAD);
                GUILog.Log("Fail! Guess response! responseTime = {0}", time);

            }
            else if (IsValidResponse(time))
            {
                // Responded Correctly
                DisplayFeedBack(RESPONSE_CORRECT, RESPONSE_COLOR_GOOD);
                r.success = true;
                r.accuracy = GetAccuracy(t, time);
                GUILog.Log("Success! responseTime = {0}", time);
            }
            else if (IsWrongResponse(time))
            {
                // Responded to the Red Square
                DisplayFeedBack(RESPONSE_WRONG, RESPONSE_COLOR_BAD);
                GUILog.Log("Fail! Wrong response! responseTime = {0}",time);
            }
            else
            {
                // Responed too slow
                DisplayFeedBack(RESPONSE_SLOW, RESPONSE_COLOR_BAD);
                GUILog.Log("Fail! Slow response! responseTime = {0}", time);
            }
        }
        sessionData.results.Add(r);
    }

    private void DisplayFeedBack(string text, Color color)
    {
        GameObject g = Instantiate(feedbackTextPrefab);
        g.transform.SetParent(uiCanvas.transform);
        g.transform.localPosition = feedbackTextPrefab.transform.localPosition;
        Text t = g.GetComponent<Text>();
        t.text = text;
        t.color = color;
    }

    protected float GetAccuracy(Trial t, float time)
    {
        NewGameData data = sessionData.gameData as NewGameData;
        bool hasResponseTimeLimit = data.ResponseTimeLimit > 0;

        float rTime = time - data.GuessTimeLimit;
        float totalTimeWindow = hasResponseTimeLimit ? data.ResponseTimeLimit :
            (t as NewGameTrial).duration;

        return 1f - (rTime / (totalTimeWindow - data.GuessTimeLimit));
    }

    protected bool IsGuessResponse(float time)
    {
        NewGameData data = sessionData.gameData as NewGameData;
        return data.GuessTimeLimit > 0 && time < data.GuessTimeLimit;
    }

    protected bool IsValidResponse(float time)
    {
        NewGameData data = sessionData.gameData as NewGameData;
        return data.ResponseTimeLimit <= 0 || time < data.ResponseTimeLimit;
    }

    // will need to have a ___TimeLimit for when it is bad to respond ie during red square
    // or do something different
    protected bool IsWrongResponse(float time)
    {
        NewGameData data = sessionData.gameData as NewGameData;
        return true;
    }

}
