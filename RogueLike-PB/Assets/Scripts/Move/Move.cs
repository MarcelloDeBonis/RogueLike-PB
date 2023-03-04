using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Move
{

#region Variables & Properties

[SerializeField] private string name;
[SerializeField] private AudioClip clip;
[SerializeField] private GameObject moveImage;
[SerializeField] private ScriptableElementTyping elementTyping;
[SerializeField] private int damage;
[SerializeField] private List<ArrowProperties> arrowPropertiesList;

#endregion

#region Methods

public List<ArrowProperties> GetArrowPropertiesList()
{
    return arrowPropertiesList;
}

public AudioClip GetAudioClip()
{
    return clip;
}

public string GetName()
{
    return name;
}

public int GetMaxDamagePossible()
{
    //TODO FOR THE MOMENT BEST SCORE YOU CAN DO WITH A MOVE IS THREE
    return arrowPropertiesList.Count * 3;
}

public int GetDamage()
{
    return damage;
}

public ScriptableElementTyping GetElementTyping()
{
    return elementTyping;
}

public GameObject GetPrefab()
{
    return moveImage;
}

#endregion

}
