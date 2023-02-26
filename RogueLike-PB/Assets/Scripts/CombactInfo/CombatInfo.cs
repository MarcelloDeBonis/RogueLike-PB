using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class CombatInfo : ICloneable<CombatInfo>
{

    #region Variables & Properties

    [SerializeField] private string name;
    [SerializeField] private int difficult;
    [SerializeField] private int life;
    [SerializeField] private int startDefence;
    private int defence;
    private Vector3 alignmentPosition;
    private Vector3 attackPosition;

    [SerializeField] public ScriptableAlonePrimaryStrument scriptableAlonePrimaryStruments;
    [SerializeField] public ScriptableReliantPrimaryStrument scriptableReliantPrimaryStruments;
    [SerializeField] public ScriptableElementTyping defenceElementTyping;
    [SerializeField] public ScriptableSoundTyping defenceSoundTyping;

    private PrimaryStrument primaryStrumentEquipped = new PrimaryStrument();
    private SecondaryStrument secondaryStrument = new SecondaryStrument();

    #endregion

    #region Methods

    public CombatInfo Clone()
    {
        CombatInfo clone = new CombatInfo();
        clone.name = this.name;
        clone.difficult = this.difficult;
        clone.life = this.life;
        clone.alignmentPosition = this.alignmentPosition;
        clone.attackPosition = this.attackPosition;
        clone.startDefence = this.startDefence;
        //TODO BE careful with defence, That is modified if someone could use a move to upgrade this, or by the player adding/ removing armor
        defence = startDefence;

        //TODO Should be clonable
        clone.scriptableAlonePrimaryStruments = this.scriptableAlonePrimaryStruments;
        clone.scriptableReliantPrimaryStruments = this.scriptableReliantPrimaryStruments;
        clone.defenceElementTyping = this.defenceElementTyping;
        clone.defenceSoundTyping = this.defenceSoundTyping;

        return clone;
    }

    public void InitPrimaryStrument()
    {
        if (scriptableAlonePrimaryStruments != null)
        {
            SetNewPrimaryStrument(scriptableAlonePrimaryStruments.Clone());
        }
        else if (scriptableReliantPrimaryStruments != null)
        {
            SetNewPrimaryStrument(scriptableReliantPrimaryStruments.Clone());
            SetNewSecondaryStrument(
                scriptableReliantPrimaryStruments.reliantPrimaryStrument.GetSecondaryStrumentClone());
        }
        else if (scriptableAlonePrimaryStruments == null && scriptableReliantPrimaryStruments == null)
        {
            Debug.Log("Attention! There is one enemy that hasn't strument so there isn't any strument assigned");
        }
        else if (scriptableAlonePrimaryStruments != null && scriptableReliantPrimaryStruments != null)
        {
            Debug.Log("Attention! There are multiple instrument and no one of those is assigned!");
        }
    }

    public void SetNewPrimaryStrument(PrimaryStrument strument)
    {
        primaryStrumentEquipped = strument;

    }

    public void SetNewSecondaryStrument(SecondaryStrument strument)
    {
        secondaryStrument = strument;
    }

    public void SetPositions(Vector3 _alignmentPosition, Vector3 _attackPosition)
    {
        alignmentPosition = _alignmentPosition;
        attackPosition = _attackPosition;
    }

    public List<ScriptableMove> GetScriptableMove()
    {
        return primaryStrumentEquipped.moveList;
    }

    public ScriptableMove GetRandomScriptableMove()
    {
        return primaryStrumentEquipped.moveList[Random.Range(0, primaryStrumentEquipped.moveList.Count)];
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

    public int GetStrumentDamage()
    {
        if (primaryStrumentEquipped is ReliantPrimaryStrument)
        {
            return secondaryStrument.GetDamage();
        }
        else
        {
            AlonePrimaryStrument strument = (AlonePrimaryStrument)primaryStrumentEquipped;
            return strument.damage;
        }
    }

    public ScriptableSoundTyping GetStrumentSoundTyping()
    {
        return primaryStrumentEquipped.soundTyping;
    }
    
    public Vector3 GetAlignmentPosition()
    {
        return alignmentPosition;
    }

    public Vector3 GetAttackPosition()
    {
        return attackPosition;
    }

    public string GetName()
    {
        return name;
    }

    public int GetDefence()
    {
        return defence;
    }

#endregion

}
