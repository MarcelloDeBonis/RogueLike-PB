using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Room")]
public class ScriptableRoom : ScriptableObject
{

#region Variables & Properties

[SerializeField] public int difficulty;
[SerializeField] public GameObject frontWall;
[SerializeField] public GameObject leftWall;
[SerializeField] public GameObject rightWall;
[SerializeField] public GameObject backWall;
[SerializeField] public List<StructEnemyCombact> enemyList;
[SerializeField] public List<StructChest> chestList;

#endregion

#region Methods



#endregion

}
