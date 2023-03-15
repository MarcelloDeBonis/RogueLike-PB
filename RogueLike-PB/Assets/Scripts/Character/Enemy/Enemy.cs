using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

#region Variables & Properties

#endregion

#region MonoBehaviour

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        combatInfo = combactInfoReference.GetCombactInfo().Clone();
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

#endregion

}
