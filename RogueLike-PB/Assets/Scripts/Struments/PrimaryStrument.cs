using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrimaryStrument
{

#region Variables & Properties

[SerializeField] public List<ScriptableMove> moveList;
[SerializeField] public ScriptableSoundTyping soundTyping;

#endregion

}
