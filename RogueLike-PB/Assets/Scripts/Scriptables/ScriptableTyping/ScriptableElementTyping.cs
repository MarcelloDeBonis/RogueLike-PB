using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Typing/New Element Typing")]
public class ScriptableElementTyping : ScriptableObject
{

#region Variables & Properties

[SerializeField] public List<ScriptableElementTyping> superEffectiveList;
[SerializeField] public List<ScriptableElementTyping> notEffectiveList;
[SerializeField] public List<ScriptableElementTyping> normalEffectiveList;

#endregion

}
