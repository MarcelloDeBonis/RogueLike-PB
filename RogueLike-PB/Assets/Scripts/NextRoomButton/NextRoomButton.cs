using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoomButton : MonoBehaviour
{

#region Variables & Properties

[SerializeField] public bool canGo;

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

public void LockNextRoom()
{
    canGo = false;
}

public void UnlockNextRoom()
{
    canGo = true;
}

private void OnMouseDown()
{
    if (canGo)
    {
        RoomManager.Instance.RoomComplete();
    }
}

#endregion

}
