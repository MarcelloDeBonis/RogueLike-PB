using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Move")]
public class ScriptableMove : ScriptableObject
{

#region Variables & Properties

[SerializeField] private Move move;

#endregion

#region Methods

public Move GetMove()
{
    return move;
}

public AudioClip GetClip()
{
    return move.GetAudioClip();
}

public string GetName()
{
    return move.GetName();
}

#endregion

}
