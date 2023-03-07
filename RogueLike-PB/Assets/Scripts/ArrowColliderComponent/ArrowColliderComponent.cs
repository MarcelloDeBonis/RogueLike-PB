using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowColliderComponent : MonoBehaviour
{

#region Variables & Properties

[SerializeField] private EnumEffectiveArrow effectiveArrow;

#endregion

#region Methods

private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.GetComponent<ArrowPoolable>() != null)
    {
        if(effectiveArrow == EnumEffectiveArrow.Bad)
        {
            ArrowManager.Instance.DeleteFromArrowInSceneList(collision.gameObject);
            return;
        }
        
        //TODO AI ABOUT DIFFICULTY
        if (ArrowManager.Instance.GetAttackingEntity().GetComponent<Enemy>() != null && this.gameObject.name== "Collider Perfect")
        {
            CombatSystem.Instance.AddPointsToDamageCalculator(ArrowManager.Instance.GetPointsKnowingEffectiveArrow(EnumEffectiveArrow.Perfect));
            ArrowManager.Instance.DeleteFromArrowInSceneList(collision.gameObject);
            return;
        }
        
        if (effectiveArrow != EnumEffectiveArrow.Bad)
        {
            collision.gameObject.GetComponent<ArrowPoolable>().SetEffective(effectiveArrow);
            return;
        }
        
    }
}

#endregion

}
