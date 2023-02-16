using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatInfo
{

#region Variables & Properties

[SerializeField] private int difficult;
[SerializeField] private int life;
[SerializeField] private Vector3 alignmentPosition;
[SerializeField] private Vector3 attackPosition;

[SerializeField] private ScriptableAlonePrimaryStrument scriptableAlonePrimaryStruments;
[SerializeField] private ScriptableReliantPrimaryStrument scriptableReliantPrimaryStruments;
[SerializeField] private ScriptableElementTyping defenceElementTyping;
[SerializeField] private ScriptableSoundTyping defenceSoundTyping;

//TODO
//TO DELETE VBECAUSE IN THAT FUNCITON THAT I WILL GET THOSE, I WILL GET FROM INSTRUMENT AND NOT FROM GENERAL MOVESET
private List<ScriptableMove> scriptableMoveList;

//TODO
//[SerializeField] private primaryStrumentSetted

#endregion

#region Methods



public List<ScriptableMove> GetScriptableMove()
{
//TODO
//PREPARE A NEW CHARACTER IF IT IS A PLAYER || ENEMY AND SET STRUMENTS NOW THE CHARACTER HAS SETTED. PLAYER CAN CHANGE, BUT ENEMY NOT (PROPOSE TO DESIGNERS HAVE A DIFFERENTS STRUMENTS THEY CAN CHANGE
    return scriptableMoveList;
}

public ScriptableMove GetRandomScriptableMove()
{
    //TODO
    return scriptableMoveList[Random.Range(0, scriptableMoveList.Count)];
}

public void ChangeLife(int newLife)
{
    life = newLife;
}

public int GetLife()
{
    return life;
}

public bool IsDied()
{
    return life == 0;
}

public Vector3 GetAlignmentPosition()
{
    return alignmentPosition;
}

public Vector3 GetAttackPosition()
{
    return attackPosition;
}

#endregion

}
