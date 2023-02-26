using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items / Strument / New Secondary Strument")]
public class ScriptableSecondaryStrument : ScriptableObject, ICloneable<SecondaryStrument>
{

#region Variables & Properties

[SerializeField] public SecondaryStrument secondaryStrument;

#endregion

public SecondaryStrument Clone()
{
    SecondaryStrument cloneSecondaryStrument = new SecondaryStrument();
    cloneSecondaryStrument = secondaryStrument;
    
    //TODO GOOD CLONE
    return cloneSecondaryStrument;
}

}
