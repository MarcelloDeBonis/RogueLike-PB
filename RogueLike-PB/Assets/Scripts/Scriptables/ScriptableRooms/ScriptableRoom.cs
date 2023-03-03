using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Room")]
public class ScriptableRoom : ScriptableObject
{

#region Variables & Properties

[SerializeField] public int difficulty;
[SerializeField] public GameObject frontWall;
[SerializeField] public GameObject rightWall;
[SerializeField] public Vector3 playerAlignmentPosition;
[SerializeField] public Vector3 playerAttackPosition;
[SerializeField] public List<StructEnemyCombact> enemyList;
[SerializeField] public List<StructChest> chestList;
[SerializeField] public List<GameObjectRoomStruct> otherObjectToBeSpawned;

#endregion

#region Methods



#endregion

}
