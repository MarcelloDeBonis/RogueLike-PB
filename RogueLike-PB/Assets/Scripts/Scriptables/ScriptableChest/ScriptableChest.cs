using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Chest")]
public class ScriptableChest : ScriptableObject
{

#region Variables & Properties

[SerializeField] public ChestLoot chestLoot;

#endregion

}
