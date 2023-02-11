using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Floor
{

#region Variables & Properties

[SerializeField] public bool bossFloor;
[SerializeField] public bool encounterRoom;

[SerializeField] public List<ScriptableRoom> allPossibleEnemiesRooms;
[SerializeField] public List<ScriptableRoom> allPossibleLootRoom;
[SerializeField] public ScriptableRoom startingRoom;
[SerializeField] public ScriptableRoom endRoom;
[SerializeField] public ScriptableRoom bossRoom;
[SerializeField] public int maxNumberRoom;
[SerializeField] public int minNumberRoom;
[SerializeField] public int percentageRoomsWithEnemiesAtLeast;
private List<ScriptableRoom> roomList;

[SerializeField] private Sprite sprite;

private int roomNumbers;
private int enemyRoomNumber;
private int lootRoomNumber;

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
    GenerateFloor();
}

private void GenerateRoomNumbers()
{
    //DON'T TOUCH
    int roomNumbers = Random.Range(minNumberRoom, maxNumberRoom + 1);
    int roomWithEnemiesMinimun = Mathf.CeilToInt(roomNumbers * percentageRoomsWithEnemiesAtLeast / 100);
    enemyRoomNumber = roomWithEnemiesMinimun + Random.Range(0, roomNumbers - roomWithEnemiesMinimun + 1);
    lootRoomNumber = roomNumbers - enemyRoomNumber;
}

#endregion

}
