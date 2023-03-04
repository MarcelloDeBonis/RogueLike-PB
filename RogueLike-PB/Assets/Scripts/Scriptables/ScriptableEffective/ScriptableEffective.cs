using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Effective Struct")]
public class ScriptableEffective: ScriptableObject
{

#region Variables & Properties

[SerializeField] public List<EffecTypeIconStruct> effectTypeIconList;
[SerializeField] public List<EffectTypeMultiplierStruct> effectTypeMultiplierList;

#endregion

}
