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

private void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.GetComponent<ArrowPoolable>() != null)
    {
        if(pointsWhenPlayerClick == 0)
        {
            ArrowManager.Instance.DeleteFromArrowInSceneList(collision.gameObject);
            return;
        }
        
        if (ArrowManager.Instance.GetAttackingEntity().GetComponent<Enemy>() != null && this.gameObject.name== "Collider Perfect")
        {
            CombatSystem.Instance.AddPointsToDamageCalculator(pointsWhenPlayerClick);
            ArrowManager.Instance.DeleteFromArrowInSceneList(collision.gameObject);
            return;
        }
        
        if (pointsWhenPlayerClick != 0)
        {
            collision.gameObject.GetComponent<ArrowPoolable>().SetPoints(pointsWhenPlayerClick);
            return;
        }
        
    }
}

#endregion

}
