using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Floor
{

#region Variables & Properties

[SerializeField] public bool bossFloor;
[SerializeField] public bool encounterRoom;

[SerializeField] public List<ScriptableRoom> allPossibleEnemiesRooms;
[SerializeField] public List<ScriptableRoom> allPossibleLootOnlyRooms;

[SerializeField] public ScriptableRoom startingRoom;
[SerializeField] public ScriptableRoom endRoom;
[SerializeField] public ScriptableRoom bossRoom;
[SerializeField] public int maxNumberRoom;
[SerializeField] public int minNumberRoom;
[SerializeField] public int percentageRoomsWithEnemiesAtLeast;

private ScriptableRoom currentRoom;

//TODO for setting every room scene
[SerializeField] private Sprite BackGorundsprite;

private int roomNumbers;
private int enemyRoomNumber;
private int lootRoomNumber;

//Don't Touch in Editor
[SerializeField] private List<ScriptableRoom> roomList;

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

public void GenerateFloor()
{
    GenerateRoomNumbers();
    GenerateRoomSequence();
}

private void GenerateRoomNumbers()
{
    //DON'T TOUCH
    roomNumbers = Random.Range(minNumberRoom, maxNumberRoom + 1);
    int roomWithEnemiesMinimun = Mathf.CeilToInt(roomNumbers * percentageRoomsWithEnemiesAtLeast / 100);
    enemyRoomNumber = roomWithEnemiesMinimun + Random.Range(0, roomNumbers - roomWithEnemiesMinimun + 1);
    lootRoomNumber = roomNumbers - enemyRoomNumber;
    
    //TODO
    if (encounterRoom)
    {
        
    }
}

private void GenerateRoomSequence()
{
    roomList.Add(startingRoom);

    Debug.Log(lootRoomNumber + " loot room created");
    Debug.Log(enemyRoomNumber + " enemy room created");
    
    for (int i = 0; i < roomNumbers; i++)
    {
        if (enemyRoomNumber > 0 && lootRoomNumber > 0)
        {
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                enemyRoomNumber--;
                roomList.Add(allPossibleEnemiesRooms[Random.Range(0, allPossibleEnemiesRooms.Count)]);
            }
            else
            {
                lootRoomNumber--;
                roomList.Add(allPossibleLootOnlyRooms[Random.Range(0,allPossibleLootOnlyRooms.Count)]);
            }
        }
        else
        {
            if (enemyRoomNumber > 0)
            {
                enemyRoomNumber--;
                roomList.Add(allPossibleEnemiesRooms[Random.Range(0, allPossibleEnemiesRooms.Count)]);
            }
            else if (lootRoomNumber > 0)
            {
                lootRoomNumber--;
                roomList.Add(allPossibleLootOnlyRooms[Random.Range(0,allPossibleLootOnlyRooms.Count)]);
            }
        }
    }
    
    if (bossFloor)
    {
        roomList.Add(bossRoom);
    }
    roomList.Add(endRoom);
    
    currentRoom = roomList[0];
}

public ScriptableRoom GetCurrentRoom()
{
    return currentRoom;
}

public bool ExistNextRoom()
{
    int index = roomList.IndexOf(currentRoom);
    return (roomList[index + 1] != null);
}

public void SetNextRoom()
{
    int index = roomList.IndexOf(currentRoom);
    currentRoom = roomList[index + 1];
}

#endregion

}
