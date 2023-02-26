using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SecondaryStrument
{

#region Variables & Properties

[SerializeField] private int damage;

#endregion

public int GetDamage()
{
    return damage;
}

}
