using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New CombactInfo")]
public class ScriptableCombactInfo : ScriptableObject
{

#region Variables & Properties

[SerializeField] private CombatInfo combactinfo;

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

public void SetCombactInfo(CombatInfo _combactInfo)
{
    combactinfo = _combactInfo;
    combactinfo.InitPrimaryStrument();
}

public CombatInfo GetCombactInfo()
{
    return combactinfo;
}

#endregion

}
