using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Move2DComponent : MonoBehaviour
{

#region Variables & Properties

[HideInInspector]
public ScriptableMove scriptableMove;

private Move move = new Move();
[SerializeField] private Text text;
[SerializeField] private AudioClip clipClick;
[SerializeField] private GameObject lightPressedImage;

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

public void SetActivationLightAroundMove(bool active)
{
    lightPressedImage.SetActive(active);
}

public void InitObject(ScriptableMove newMove)
{
    scriptableMove = newMove;
    move = newMove.GetMove();
    SetMoveName();
}

private void SetMoveName()
{
    text.text = move.GetName();
}

public void DeactiveSelection()
{
    SetActivationLightAroundMove(false);
}



public void Click()
{
    SetActivationLightAroundMove(true);
    CombatSystem.Instance.ChooseMove(scriptableMove);
    SoundManager.Instance.PlayEffect(clipClick);
}

#endregion

}
