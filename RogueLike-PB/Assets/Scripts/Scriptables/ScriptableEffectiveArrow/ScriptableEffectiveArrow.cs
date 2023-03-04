using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Effective Arrow Struct")]
public class ScriptableEffectiveArrow : ScriptableObject
{

#region Variables & Properties

[SerializeField] public List<StructEffectiveArrow> structEffectiveArrowsList;

#endregion

}
