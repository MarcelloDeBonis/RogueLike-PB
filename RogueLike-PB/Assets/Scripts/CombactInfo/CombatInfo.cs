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

[SerializeField] private List<ScriptableMove> scriptableMoveList;


#endregion

#region Methods

public List<ScriptableMove> GetScriptableMove()
{
    return scriptableMoveList;
}

public ScriptableMove GetRandomScriptableMove()
{
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
