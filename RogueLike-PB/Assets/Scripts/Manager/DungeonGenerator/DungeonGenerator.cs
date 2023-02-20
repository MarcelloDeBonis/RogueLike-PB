using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonGenerator : Singleton<DungeonGenerator>
{

#region Variables & Properties

[SerializeField] private List<Dungeon> dungeonList;
private Dungeon currentDungeon = new Dungeon();

#endregion

#region MonoBehaviour

// Awake is called when the script instance is being loaded
    protected override void Awake()
    {
        base.Awake();
        GenerateDungeonList();
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

public void GenerateNewScene()
{
    SceneManager.LoadScene("RoomScene");
    RoomManager.Instance.PrepareNewRoom(currentDungeon.GetCurrentFloor().GetCurrentRoom());
}

private void GenerateDungeonList()
{
    foreach (Dungeon dungeon in dungeonList)
    {
        dungeon.GenerateDungeon();
    }

    currentDungeon=dungeonList[0];
}

public void NextRoom()
{
    if (currentDungeon.GetCurrentFloor().ExistNextRoom())
    {
        currentDungeon.GetCurrentFloor().SetNextRoom();
    }
}



#endregion

}
