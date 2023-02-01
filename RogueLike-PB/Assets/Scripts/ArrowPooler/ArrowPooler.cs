using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPooler : ObjectPooler<ArrowPooler>
{

    #region Methods

    public GameObject SpawnArrow()
    {
        return SpawnObjectPoolable<ArrowPoolable>();
    }

    #endregion
    
}
