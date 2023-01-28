using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour, IPointerClickHandler
{

#region Variables & Properties

[SerializeField] public CombatInfo combatInfo;
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

#region Clickable

public void OnPointerClick(PointerEventData pointerEventData)
{
    if(CombatSystem.Instance.GetEnumBattlePhase()==EnumBattlePhase.SelectingPhase)
    isSelected = true;
}

#endregion

public void DeactiveIsSelected()
{
    isSelected = false;
}

#endregion

}
