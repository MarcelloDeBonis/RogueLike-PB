using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Typing/New Element Typing")]
public class ScriptableElementTyping : ScriptableObject
{

#region Variables & Properties

[SerializeField] private List<ScriptableElementTyping> superEffectiveList;
[SerializeField] private List<ScriptableElementTyping> notEffectiveList;
[SerializeField] private List<ScriptableElementTyping> normalEffectiveList;

#endregion

}
