using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dungeon
{

#region Variables & Properties

[SerializeField] private List<Floor> floorList;
private Floor currentFloor;

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

public void GenerateDungeon()
{
    foreach (Floor floor in floorList)
    {
        floor.GenerateFloor();
    }

    currentFloor = floorList[0];
}

public void SetNextFloor()
{
    int index = floorList.IndexOf(currentFloor);
    currentFloor = floorList[index + 1];
}

public bool ExistNextFloor()
{
    int index = floorList.IndexOf(currentFloor);
    return (floorList[index + 1] != null);
}

public Floor GetCurrentFloor()
{
    return currentFloor;
}

#endregion

}
