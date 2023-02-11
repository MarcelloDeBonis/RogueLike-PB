using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DungeonGenerator : Singleton<DungeonGenerator>
{

#region Variables & Properties

//TODO
//OGNI PIANO HA LA SEGUENTE STRUTTURA, QUINDI UN DUNGEON è UNA LISTA DI PIANI. CI SONO N DUNGEON, QUINDI UNA LISTA DI DUNGEON.
    [SerializeField] private List<Dungeon> dungeonList;


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

private void GenerateFloor()
{
    
}

#endregion

}
