using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : Singleton<CombatSystem>
{

#region Variables & Properties

[SerializeField] private GameObject player;
[SerializeField] private List<GameObject> enemyList;

#endregion

#region MonoBehaviour

    // Awake is called when the script instance is being loaded
    void Awake()
    {
	    base.Awake();
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

public void OnCombatStart()
{
    //TODO
    PrepareCombat();
    CombatLoop();
}

private void PrepareCombat()
{
   
    //TODO
    //Teleport every character
   
}

private void CombatLoop()
{
    
    while (!SomeoneIsDied())
    {
        CharacterAttacks(player);

        foreach (GameObject enemy in enemyList)
        {
            CharacterAttacks(enemy);
            PlayerDefend();
        }
        
    }

    EndCombat();

}

#region CombatFases

private void CharacterAttacks(GameObject character)
{
    MoveCharacter(character);
    PrepareUiForAttack(character.GetComponent<Character>());
}

private void MoveCharacter(GameObject character)
{
    //StartCoroutine(Move)
}

private void PlayerDefend()
{
    
}

private void PrepareUiForAttack(Character character)
{
    
}

#endregion

private bool SomeoneIsDied()
{
    return false;
}

private void EndCombat()
{
    
}







#endregion

}
