using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour
{

#region Variables & Properties

[SerializeField] protected ScriptableCombactInfo combactInfoReference;
protected CombatInfo combatInfo;
private bool isSelected=false;

#endregion

#region MonoBehaviour

    // Awake is called when the script instance is being loaded
    void Awake()
    {
	    
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

#endregion

#region Methods

public void InitCombactInfo(Vector3 allignmentPosition, Vector3 attackPosition)
{
    combatInfo = new CombatInfo();
    combatInfo = combactInfoReference.GetCombactInfo();
    combatInfo.SetPositions(allignmentPosition, attackPosition);
    combatInfo.InitPrimaryStrumentEnemies();
}

#region Clickable

public void OnMouseDown()
{
    if (CombatSystem.Instance.GetEnumBattlePhase() == EnumBattlePhase.SelectingPhase)
    {
        isSelected = true;
    }
}

#endregion

public void DeactiveIsSelected()
{
    isSelected = false;
}

public bool GetIsSelected()
{
    return isSelected;
}

public CombatInfo GetCombatInfo()
{
    return combatInfo;
}

#endregion

}
