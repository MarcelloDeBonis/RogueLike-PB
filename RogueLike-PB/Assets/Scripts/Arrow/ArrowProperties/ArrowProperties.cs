using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArrowProperties
{

#region Variables & Properties

[SerializeField] private float waitingTime;
[SerializeField] private float speed;
[SerializeField] private EnumArrow enumArrow;
[SerializeField] private KeyCode key;

#endregion


public KeyCode Getkey()
{
    
    return key;
}

public float GetWaitingTime()
{
    return waitingTime;
}


public float GetSpeed()
{
    return speed;
}

public EnumArrow GetEnumArrow()
{
    return enumArrow;
}

}
