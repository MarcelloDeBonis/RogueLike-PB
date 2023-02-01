using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowColliderComponent : MonoBehaviour
{

#region Variables & Properties

[SerializeField] private int pointsWhenPlayerClick;

#endregion

#region Methods

public int GetPoints()
{
    return pointsWhenPlayerClick;
}

#endregion

}
