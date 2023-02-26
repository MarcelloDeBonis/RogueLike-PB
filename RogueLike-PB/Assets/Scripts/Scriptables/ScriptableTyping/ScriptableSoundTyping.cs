using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Typing/New Sound Typing")]
public class ScriptableSoundTyping : ScriptableObject
{

#region Variables & Properties

[SerializeField] public List< ScriptableSoundTyping> superEffectiveList;
[SerializeField] public List< ScriptableSoundTyping> notEffectiveList;
[SerializeField] public List< ScriptableSoundTyping> normalEffectiveList;

#endregion

}
