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
    public const string ATTRIBUTE_POSITIONX = "rangeX";
    public const string ATTRIBUTE_POSITIONY = "rangeY";

    /// <summary>
	/// The amount of time that needs to pass before the player can respond without being penalized.
	/// </summary>
    private float guessTimeLimit = 0;
    /// <summary>
	/// The amount of time that the user has to respond; 
	/// Starts when input becomes enabled during a Trial. 
	/// Responses that fall within this time constraint will be marked as Successful.
	/// </summary>
    private float responseTimeLimit = 0;
    /// <summary>
	/// The visibility Duration for the Stimulus.
	/// </summary>
    private float duration = 0;
    /// <summary>
    /// The range values for random stimulus positions from the session file
    /// </summary>
    private string rangeSX;
    private string rangeSY;
    private int[] rangeX;
    private int[] rangeY;

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
    public int[] RandomRangeX
    {
        get
        {
            return rangeX;
        }
    }
    public int[] RandomRangeY
    {
        get
        {
            return rangeY;
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
        XMLUtil.ParseAttribute(elem, ATTRIBUTE_POSITIONX, ref rangeSX);
        XMLUtil.ParseAttribute(elem, ATTRIBUTE_POSITIONY, ref rangeSY);
        rangeX = SplitPosition(rangeSX);
        rangeY = SplitPosition(rangeSY);
    }

    public override void WriteOutputData(ref XElement elem)
    {
        base.WriteOutputData(ref elem);
        XMLUtil.CreateAttribute(ATTRIBUTE_GUESS_TIMELIMIT, guessTimeLimit.ToString(), ref elem);
        XMLUtil.CreateAttribute(ATTRIBUTE_RESPONSE_TIMELIMIT, responseTimeLimit.ToString(), ref elem);
        XMLUtil.CreateAttribute(ATTRIBUTE_DURATION, duration.ToString(), ref elem);
    }
    
    public int[] SplitPosition(string x)
    {
        string[] s = x.Split(' ');
        int[] sp = new int[s.Length];
        for(int i = 0; i < s.Length; i++)
        {
            sp[i] = int.Parse(s[i]);
        }
        return sp;
    }

}
