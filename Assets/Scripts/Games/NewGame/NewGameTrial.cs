using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

public class NewGameTrial : Trial {


    public float duration = 0;


    #region ACCESSORS

    public float Duration
    {
        get
        {
            return duration;
        }
    }

    #endregion


    public NewGameTrial(SessionData data, XmlElement n = null)
        : base(data, n)
    {

    }

    public override void ParseGameSpecificVars(XmlNode n, SessionData session)
    {
        base.ParseGameSpecificVars(n, session);

        NewGameData data = (NewGameData)(session.gameData);
        if(!XMLUtil.ParseAttribute(n, ReactData.ATTRIBUTE_DURATION, ref duration, true))
        {
            duration = data.GeneratedDuration;
        }
    }

    public override void WriteOutputData(ref XElement elem)
    {
        base.WriteOutputData(ref elem);
        XMLUtil.CreateAttribute(NewGameData.ATTRIBUTE_DURATION, duration.ToString(), ref elem);
    }

}
