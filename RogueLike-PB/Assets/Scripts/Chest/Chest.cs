using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chest : MonoBehaviour
{

#region Variables & Properties

[SerializeField] public ScriptableChest chestReference;
[SerializeField] public AudioClip clip;
[SerializeField] public GameObject canvas;

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

public void OnMouseDown()
{
    OpenChest();
}

private void OpenChest()
{
    SoundManager.Instance.PlayEffect(clip);
    UIChestManager.Instance.SpawnChest(this.gameObject, GenerateChestLoot());
}

private ChestLoot GenerateChestLoot()
{
    return chestReference.chestLoot.Clone();
}

#endregion

}
