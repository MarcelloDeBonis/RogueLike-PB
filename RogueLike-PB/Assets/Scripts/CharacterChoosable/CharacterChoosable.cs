using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class CharacterChoosable : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private ScriptablePlayerInfo playerSettings;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        ChoosableManager.Instance.AssignPlayerSettings(playerSettings);
    }
}

