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

#endregion

}
