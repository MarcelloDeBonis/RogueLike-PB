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

private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.GetComponent<ArrowPoolable>() != null)
    {
        if (pointsWhenPlayerClick != 0)
        {
            collision.gameObject.GetComponent<ArrowPoolable>().SetPoints(pointsWhenPlayerClick);
        }
        else
        {
            ArrowManager.Instance.DeleteFromArrowInSceneList(collision.gameObject);
        }
    }
}

#endregion

}
