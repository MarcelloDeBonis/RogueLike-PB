using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChestManager : Singleton<UIChestManager>
{

#region Variables & Properties

[SerializeField] private Transform transformChest;
[SerializeField] private Canvas objectCanvas;

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

public void SpawnChest(GameObject chest, ChestLoot loot)
{
    
}

#endregion

}
