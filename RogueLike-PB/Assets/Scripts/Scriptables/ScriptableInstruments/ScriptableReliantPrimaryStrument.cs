using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items / Strument / Primary Struments /New Reliant Strument")]
public class ScriptableReliantPrimaryStrument : ScriptableObject, ICloneable<ReliantPrimaryStrument>
{

#region Variables & Properties

[SerializeField] public ReliantPrimaryStrument reliantPrimaryStrument;

#endregion

public ReliantPrimaryStrument Clone()
{
    //TODO INCORRECT FORM REWORK
    
    ReliantPrimaryStrument cloneReliantPrimaryStrument = new ReliantPrimaryStrument();
    cloneReliantPrimaryStrument.secondayStrument.secondaryStrument= reliantPrimaryStrument.GetSecondaryStrumentClone();

    return cloneReliantPrimaryStrument;
}

}
