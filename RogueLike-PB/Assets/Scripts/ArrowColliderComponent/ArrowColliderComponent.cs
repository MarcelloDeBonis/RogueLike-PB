using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowColliderComponent : MonoBehaviour
{

#region Variables & Properties

[SerializeField] private int pointsWhenPlayerClick;

#endregion

#region Methods

private void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.GetComponent<ArrowPoolable>() != null)
    {
        if (ArrowManager.Instance.GetAttackingEntity().GetComponent<Enemy>() != null && this.gameObject.name== "Collider Perfect")
        {
            CombatSystem.Instance.AddPointsToDamageCalculator(pointsWhenPlayerClick);
            ArrowManager.Instance.DeleteFromArrowInSceneList(other.gameObject);
        }
        
        if (pointsWhenPlayerClick != 0)
        {
            other.gameObject.GetComponent<ArrowPoolable>().SetPoints(pointsWhenPlayerClick);
        }
        else
        {
            ArrowManager.Instance.DeleteFromArrowInSceneList(other.gameObject);
        }
    }
}

#endregion

}
