using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : Singleton<CombatSystem>
{

#region Variables & Properties

[SerializeField] private GameObject player;
[SerializeField] private List<GameObject> enemyList;
[SerializeField] private float speedMovementEntities;
private EnumBattlePhase battlePhase = EnumBattlePhase.NoBattlePhase;
private GameObject enemySelected;

#endregion

#region MonoBehaviour

protected override void Awake()
{
    base.Awake();
}

// Start is called before the first frame update
    void Start()
    {
        OnCombatStart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

#endregion

#region Methods

#region Pre Combat

public void OnCombatStart()
{
    //TODO
    PrepareCombat();
    StartCoroutine(CombatLoop());
}

private void PrepareCombat()
{
   
    //TODO
    //Teleport every character
   
}

#endregion

private IEnumerator CombatLoop()
{
    while (!SomeoneIsDied())
    {
        yield return StartCoroutine(CharacterAttacks(player));

        foreach (GameObject enemy in enemyList)
        {
            yield return StartCoroutine(CharacterAttacks(enemy));
            yield return StartCoroutine(PlayerDefend());
        }
        
    }

    EndCombat();

}

#region CombatFases

private IEnumerator CharacterAttacks(GameObject character) 
{
    //TODO
   // yield return StartCoroutine(ChooseMove());
   battlePhase = EnumBattlePhase.SelectingPhase;
   yield return StartCoroutine(ChooseTarget(character));
   yield return StartCoroutine(MoveCharacterAndEnemySelected(character));
   yield return StartCoroutine(PrepareUiForAttack(character.GetComponent<Character>()));
}


private IEnumerator ChooseTarget(GameObject character)
{

    bool choosen = false;

    while (!choosen)
    {
        if (character.GetComponent<Player>() != null)
        {
            foreach (GameObject enemy in enemyList)
            {
                if (enemy.GetComponent<Character>().GetIsSelected())
                {
                    enemy.GetComponent<Character>().DeactiveIsSelected();
                    enemySelected = enemy;
                    choosen = true;
                }
            }
        }
        
        if (character.GetComponent<Enemy>() != null)
        {
            enemySelected = player;
            choosen = true;
        }
        
        yield return null;
    }
    
}


private IEnumerator MoveCharacterAndEnemySelected(GameObject character)
{
    while (character.transform.position != character.GetComponent<Character>().combatInfo.GetAttackPosition())
    {
        character.transform.position = Vector3.MoveTowards(character.GetComponent<Character>().combatInfo.GetAlignmentPosition(), character.GetComponent<Character>().combatInfo.GetAttackPosition(), speedMovementEntities * Time.deltaTime);
        enemySelected.transform.position= Vector3.MoveTowards(enemySelected.GetComponent<Character>().combatInfo.GetAlignmentPosition(), enemySelected.GetComponent<Character>().combatInfo.GetAttackPosition(), speedMovementEntities * Time.deltaTime);
        yield return null;
    }
    
}

private IEnumerator PlayerDefend()
{
    //TODO
    yield return null;
}

private IEnumerator PrepareUiForAttack(Character character)
{
    //TODO
    while (true)
    {
        yield return null;
    }
}

#endregion

private bool SomeoneIsDied()
{
    return false;
}

private void EndCombat()
{
    
}


#region Getters

public EnumBattlePhase GetEnumBattlePhase()
{
    return battlePhase;
}

#endregion




#endregion

}
