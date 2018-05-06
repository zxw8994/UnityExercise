using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

/// <summary>
/// Contains general Game data for the NewGame gameType
/// </summary>


public class NewGameData : GameData {

    const string ATTRIBUTE_GUESS_TIMELIMIT = "guessTimeLimit";
    const string ATTRIBUTE_RESPONSE_TIMELIMIT = "responseTimeLimit";
    public const string ATTRIBUTE_DURATION = "duration";

    private float guessTimeLimit = 0;

    private float responseTimeLimit = 0;

    // How long player has to wait for red square before it passes
    // ? could just be a const ? or just use duration
    private float waitTimeLimit = 0;

    private float duration = 0;


    // If not a const, waitTimeLimit will need a GET here
    #region ACCESSORS

    public float GuessTimeLimit
    {
        get
        {
            return guessTimeLimit;
        }
    }
    public float ResponseTimeLimit
    {
        get
        {
            return responseTimeLimit;
        }
    }
    public float GeneratedDuration
    {
        get
        {
            return duration;
        }
    }

    #endregion


    public NewGameData(XmlElement elem) : base(elem)
    {
    }


    public override void ParseElement(XmlElement elem)
    {
        base.ParseElement(elem);
        XMLUtil.ParseAttribute(elem, ATTRIBUTE_DURATION, ref duration);
        XMLUtil.ParseAttribute(elem, ATTRIBUTE_RESPONSE_TIMELIMIT, ref responseTimeLimit);
        XMLUtil.ParseAttribute(elem, ATTRIBUTE_GUESS_TIMELIMIT, ref guessTimeLimit);
    }

    public override void WriteOutputData(ref XElement elem)
    {
        base.WriteOutputData(ref elem);
        XMLUtil.CreateAttribute(ATTRIBUTE_GUESS_TIMELIMIT, guessTimeLimit.ToString(), ref elem);
        XMLUtil.CreateAttribute(ATTRIBUTE_RESPONSE_TIMELIMIT, responseTimeLimit.ToString(), ref elem);
        XMLUtil.CreateAttribute(ATTRIBUTE_DURATION, duration.ToString(), ref elem);
    }
    

}
