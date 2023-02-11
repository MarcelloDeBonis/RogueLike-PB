using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Chest")]
public class ScriptableChest : ScriptableObject
{

#region Variables & Properties

[SerializeField] private int rangeMaxCoins;
[SerializeField] private int rangeMinCoins;
[SerializeField] private int numberConsumable;
[SerializeField] private int numberChestPlates;
[SerializeField] private int numberHelms;
[SerializeField] private int numberInstruments;
[SerializeField] private int passiveObject;

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

public int GenerateCoins()
{
    return Random.Range(rangeMinCoins, rangeMaxCoins + 1);
}

#endregion

}
