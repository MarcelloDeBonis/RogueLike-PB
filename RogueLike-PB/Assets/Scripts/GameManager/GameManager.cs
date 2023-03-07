using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

#region Variables & Properties

[SerializeField] private ScriptableSaves saveData;
[SerializeField] private List<Dungeon> dungeonList;

//Don't Touch in editor
public Dungeon currentDungeon;

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

private void StartNewGame()
{
    //TODO SAVING FILES
    //saveData.dungeonList= DungeonGenerator.Instance.GenerateDungeonList(dungeonList);
}

#endregion

}
