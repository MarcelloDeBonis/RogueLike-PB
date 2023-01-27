using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CombatInfo
{

#region Variables & Properties

[SerializeField] private int life;
[SerializeField] private Vector3 alignmentPosition;
[SerializeField] private Vector3 attackPosition;

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

public void ChangeLife(int newLife)
{
    life = newLife;
}

public int GetLife()
{
    return life;
}

public bool IsDied()
{
    return life == 0;
}


#endregion

}
