using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{

#region Variables & Properties



#endregion

#region MonoBehaviour

    // Awake is called when the script instance is being loaded
    void Awake()
    {
	    //TODO
    }

    // Start is called before the first frame update
    void Start()
    {
        DungeonGenerator.Instance.GenerateDungeonList();
        DungeonGenerator.Instance.GenerateNewScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

#endregion

#region Methods



#endregion

}
