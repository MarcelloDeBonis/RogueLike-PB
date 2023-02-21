using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoomButton : MonoBehaviour
{

#region Variables & Properties

public delegate void MouseEventHandler();

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

private void OnMouseDown()
{
    RoomManager.Instance.RoomComplete();
}

#endregion

}