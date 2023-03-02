using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using Unity.Mathematics;
using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{

#region Variables & Properties

private GameObject rightWall;
[SerializeField] private Camera camera;
[SerializeField] private Transform combatCamera;
[SerializeField] private Transform freeRoomCamera;
[SerializeField] private GameObject player;
private GameObject frontWall;

//Don't Touch In Editor

    [SerializeField] private ScriptableRoom roomInfo;
    [SerializeField] private List<GameObject> enemiesInGame;
    [SerializeField] private List<GameObject> chestList;

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
        PrepareNewRoom(DungeonGenerator.Instance.GetCurrentDungeon().GetCurrentFloor().GetCurrentRoom());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

#endregion

#region Methods

public void PrepareNewRoom(ScriptableRoom _room)
{
    roomInfo = _room;
    PrepareWalls();
    PreparePlayer();
    if (IsThereChests())
    {
       PrepareChests();
       DeactiveAllChests();
    }
    
    if (IsThereEnemies())
    {
        PrepareEnemyList();
        StartCombact();
    }
    else
    {
        RoomEmpty();
    }
    
}

private void ActiveRightWalls()
{
    rightWall.SetActive(true);
}

private void DeactiveRightWall()
{
    rightWall.SetActive(false);
}

private void DeactiveAllChests()
{
    foreach (GameObject chest in chestList)
    {
        chest.SetActive(false);
    }
}

private void ActiveAllChests()
{
    foreach (GameObject chest in chestList)
    {
        chest.SetActive(true);
    }
}

public void RoomEmpty()
{
    frontWall.GetComponentInChildren<NextRoomButton>().UnlockNextRoom();
    ActiveAllChests();
    SetFreeRoomCamera();
    ActiveRightWalls();
}

private void SetFreeRoomCamera()
{
    camera.transform.position = freeRoomCamera.position;
    camera.transform.rotation = freeRoomCamera.rotation;
}

private void SetCombatCamera()
{
    camera.transform.position = combatCamera.position;
    camera.transform.rotation = combatCamera.rotation;
}

private bool IsThereChests()
{
    return roomInfo.chestList.Count > 0;
}

private bool IsThereEnemies()
{
    return roomInfo.enemyList.Count > 0;
}

private void PreparePlayer()
{
    player.transform.position = roomInfo.playerAlignmentPosition;
    player.GetComponent<Player>().InitCombactInfo(roomInfo.playerAlignmentPosition, roomInfo.playerAttackPosition);
}

private void PrepareEnemyList()
{
    foreach (StructEnemyCombact enemydata in roomInfo.enemyList)
    {
        GameObject newEnemy = Instantiate(enemydata.enemy, enemydata.alignmentEnemyPosition, quaternion.identity);
        enemiesInGame.Add(newEnemy);
        newEnemy.GetComponent<Enemy>().InitCombactInfo(enemydata.alignmentEnemyPosition , enemydata.attackEnemyPosition);
    }
}

private void StartCombact()
{
    frontWall.GetComponentInChildren<NextRoomButton>().LockNextRoom();
    SetCombatCamera();
    DeactiveRightWall();
    CombatSystem.Instance.OnCombatStart(player, enemiesInGame);
}

private void PrepareChests()
{
    foreach (StructChest chestInfo in roomInfo.chestList)
    {
        GameObject newChest = Instantiate(chestInfo.chest, chestInfo.chestPosition, quaternion.identity);
        chestList.Add(newChest);
    }
}

private void PrepareWalls()
{
    frontWall= Instantiate(roomInfo.frontWall);
    Instantiate(roomInfo.backWall);
    rightWall = Instantiate(roomInfo.rightWall);
    Instantiate(roomInfo.leftWall);
}


public void RoomComplete()
{
    DungeonGenerator.Instance.NextRoom();
}

#endregion

}
