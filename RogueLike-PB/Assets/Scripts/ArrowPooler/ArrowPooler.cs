using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPooler : ObjectPooler
{

    #region Methods

    public GameObject SpawnArrow()
    {
        return SpawnObjectPoolable<ArrowPoolable>();
    }

    #endregion
    
}
