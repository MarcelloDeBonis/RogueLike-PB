using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items / Strument / Primary Struments /New Alone Strument")]
public class ScriptableAlonePrimaryStrument : ScriptableObject, ICloneable<AlonePrimaryStrument>
{

#region Variables & Properties

[SerializeField] public AlonePrimaryStrument alonePrimaryStrument;

#endregion

public AlonePrimaryStrument Clone()
{
    //TODO BETTER CLONABLE
    AlonePrimaryStrument cloneAlonePrimaryStrument = new AlonePrimaryStrument();
    cloneAlonePrimaryStrument.damage = alonePrimaryStrument.damage;
    cloneAlonePrimaryStrument.moveList = alonePrimaryStrument.moveList;
    cloneAlonePrimaryStrument.soundTyping = alonePrimaryStrument.soundTyping;

    return cloneAlonePrimaryStrument;
}

}

