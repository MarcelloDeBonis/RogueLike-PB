using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Typing/New Sound Typing")]
public class ScriptableSoundTyping : ScriptableObject
{

#region Variables & Properties

[SerializeField] private  ScriptableSoundTyping self;
[SerializeField] private List< ScriptableSoundTyping> superEffectiveList;
[SerializeField] private List< ScriptableSoundTyping> notEffectiveList;
[SerializeField] private List< ScriptableSoundTyping> normalEffectiveList;

#endregion

}
