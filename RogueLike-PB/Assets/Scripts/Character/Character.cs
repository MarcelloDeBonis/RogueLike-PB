using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

#region Variables & Properties

[SerializeField] protected ScriptableCombactInfo combactInfoReference;
protected CombatInfo combatInfo;
private bool isSelected=false;
[SerializeField] private Text lifeText;
[SerializeField] private Text nameText;

#endregion

#region MonoBehaviour

    // Awake is called when the script instance is being loaded
    void Awake()
    {
	    
    }

    // Start is called before the first frame update
    void Start()
    {
        UpgradeLife();
        UpgradeName();
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
    combatInfo = combactInfoReference.GetCombactInfo().Clone();
    combatInfo.SetPositions(allignmentPosition, attackPosition);
    combatInfo.InitPrimaryStrument();
}

#region Clickable

public void OnMouseDown()
{
    isSelected = true;
}

#endregion

private void UpgradeLife()
{
    lifeText.text = combatInfo.GetLife().ToString();
}

private void UpgradeName()
{
    nameText.text = combatInfo.GetName();
}

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

public void TakeDamage(int damage)
{
    int life = combatInfo.GetLife();
    
    if (damage > life)
    {
        combatInfo.ChangeLife(0);
        UpgradeLife();
    }
    else
    {
        combatInfo.ChangeLife(life - damage);
    }
}

#endregion

}
