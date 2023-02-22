using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosableManager : Singleton<ChoosableManager>
{
    [SerializeField] private ScriptablePlayerInfo player;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignPlayerSettings(ScriptablePlayerInfo _playerInfo)
    {
        player.SetCombactInfo(_playerInfo.GetCombactInfo().Clone());
        //TODO COSE
        SceneManager.LoadScene("MapScene");
    }
}
