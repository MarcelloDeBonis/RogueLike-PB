using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonGenerator : Singleton<DungeonGenerator>
{

#region Variables & Properties

[SerializeField] private List<Dungeon> dungeonList;
private Dungeon currentDungeon;

#endregion

#region MonoBehaviour

// Awake is called when the script instance is being loaded
    protected override void Awake()
    {
        base.Awake();
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
}


public void GenerateDungeonList()
{
    foreach (Dungeon dungeon in dungeonList)
    {
        dungeon.GenerateDungeon();
    }

    SetCurrentDungeon(dungeonList[0]);
}

private void SetCurrentDungeon(Dungeon dungeon)
{
    currentDungeon = dungeon;
}

public bool IsFirstRoom(ScriptableRoom room)
{
    return currentDungeon.GetCurrentFloor().GetFirstRoom() == room;
}

public void NextRoom()
{
    if (currentDungeon.GetCurrentFloor().ExistNextRoom())
    {
        currentDungeon.GetCurrentFloor().SetNextRoom();
    }
    else
    {
        if (currentDungeon.ExistNextFloor())
        {
            currentDungeon.SetNextFloor();
        }
        else
        {
            SceneManager.LoadScene("WinGame");
        }
    }
    
    GenerateNewScene();
}

public Dungeon GetCurrentDungeon()
{
    return currentDungeon;
}

#endregion

}
