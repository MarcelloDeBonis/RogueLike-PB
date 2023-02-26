using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ReliantPrimaryStrument : PrimaryStrument
{

#region Variables & Properties

[SerializeField] public ScriptableSecondaryStrument secondayStrument;

#endregion

public SecondaryStrument GetSecondaryStrumentClone()
{
    return secondayStrument.Clone();
}

}
