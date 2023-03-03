using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLoot : MonoBehaviour, ICloneable<ChestLoot>
{

#region Variables & Properties

[SerializeField] public int rangeMaxCoins;
[SerializeField] public int rangeMinCoins;
[HideInInspector] public int coins;

[SerializeField] public int numberConsumable;

[SerializeField] public int numberChestPlates;
[SerializeField] public int numberHelms;

[SerializeField] public int numberInstruments;

[SerializeField] public int passiveObject;

#endregion

#region Methods

public ChestLoot Clone()
{
    ChestLoot clone = new ChestLoot();
    clone.coins = GenerateCoins();
    //TODO Generate following a rule, all other things

    return clone;
}

public int GenerateCoins()
{
    return Random.Range(rangeMinCoins, rangeMaxCoins + 1);
}

#endregion

}
