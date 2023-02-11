using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Room")]
public class Room : ScriptableObject
{

#region Variables & Properties

[SerializeField] private int difficulty;
[SerializeField] private List<ScriptableCombactInfo> enemyList;
[SerializeField] private List<ScriptableChest> chestList;

#endregion

#region MonoBehaviour

    // Awake is called when the script instance is being loaded
    void Awake()
    {
	
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

#endregion

#region Methods



#endregion

}
