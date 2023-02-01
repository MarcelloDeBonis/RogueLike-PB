using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : Singleton<CombatSystem>
{

#region Variables & Properties

private int currentDamage = 0;
private ScriptableMove choosenMove;
[SerializeField] private GameObject player;
[SerializeField] private List<GameObject> enemyList;
[SerializeField] private float speedMovementEntities;
private EnumBattlePhase battlePhase = EnumBattlePhase.NoBattlePhase;
private GameObject opponentSelected;

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
        yield return StartCoroutine(ChoosingAttack(player));
        yield return StartCoroutine(MoveCharacterAndEnemySelected(player, player.GetComponent<Character>().combatInfo.GetAttackPosition(), opponentSelected, opponentSelected.GetComponent<Character>().combatInfo.GetAttackPosition()));
        yield return StartCoroutine(ChooseMove(player.GetComponent<Character>()));
        //Try to attack
        //Calculate damage
        //EnemySelectedLoseThatDamage
        //EnemyAndPlayerComeBackToTheirDefaultPosition
        yield return StartCoroutine(MoveCharacterAndEnemySelected(player, player.GetComponent<Character>().combatInfo.GetAlignmentPosition(), opponentSelected, opponentSelected.GetComponent<Character>().combatInfo.GetAlignmentPosition()));
        
        GameObject firsteEnemy = enemyList[0];
        GameObject lastEnemy = enemyList[enemyList.Count - 1];
        
        foreach (GameObject enemy in enemyList)
        {
            yield return StartCoroutine(ChoosingAttack(enemy));
            
            if (enemy== firsteEnemy)
            {
                yield return StartCoroutine(MoveCharacterAndEnemySelected(enemy, enemy.GetComponent<Character>().combatInfo.GetAttackPosition(), opponentSelected, opponentSelected.GetComponent<Character>().combatInfo.GetAttackPosition()));
            }
            else
            {
                yield return StartCoroutine(MoveCharacter(enemy, enemy.GetComponent<Character>().combatInfo.GetAttackPosition()));
            }
            
            //TODO
            //Rhythm attack + calculate damage
            yield return StartCoroutine(PlayerDefend());

            if (enemy == lastEnemy)
            {
                yield return StartCoroutine(MoveCharacterAndEnemySelected(enemy, enemy.GetComponent<Character>().combatInfo.GetAlignmentPosition(), opponentSelected, opponentSelected.GetComponent<Character>().combatInfo.GetAlignmentPosition()));
            }

        }
        
        //TODO
        //player comes back
    }

    EndCombat();

}

#region CombatFases

private IEnumerator ChoosingAttack(GameObject character) 
{ 
    //TODO
   //yield return StartCoroutine(ChooseMove());
   battlePhase = EnumBattlePhase.SelectingPhase;
   yield return StartCoroutine(ChooseTarget(character));

}


private IEnumerator ChooseTarget(GameObject character)
{

    if (enemyList.Contains(character))
    {
        opponentSelected = player;
    }
    else
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
                        opponentSelected = enemy;
                        choosen = true;
                    }
                }
            }
        
            if (character.GetComponent<Enemy>() != null)
            {
                opponentSelected = player;
                choosen = true;
            }
        
            yield return null;
        }
    }
    
}


private IEnumerator MoveCharacterAndEnemySelected(GameObject character1, Vector3 end1, GameObject character2, Vector3 end2)
{
    while (character1.transform.position != end1)
    {
        character1.transform.position = Vector3.MoveTowards(character1.transform.position, end1, speedMovementEntities * Time.deltaTime);
        opponentSelected.transform.position= Vector3.MoveTowards(opponentSelected.transform.position, end2, speedMovementEntities * Time.deltaTime);
        yield return null;
    }
    
}

private IEnumerator MoveCharacter(GameObject character, Vector3 end)
{
    while (character.transform.position != character.GetComponent<Character>().combatInfo.GetAttackPosition())
    {
        character.transform.position = Vector3.MoveTowards(character.transform.position, character.GetComponent<Character>().combatInfo.GetAttackPosition(), speedMovementEntities * Time.deltaTime);
        opponentSelected.transform.position= Vector3.MoveTowards(opponentSelected.transform.position, opponentSelected.GetComponent<Character>().combatInfo.GetAttackPosition(), speedMovementEntities * Time.deltaTime);
        yield return null;
    } 
}

private IEnumerator PlayerDefend()
{
    //TODO
    yield return null;
}

private IEnumerator ChooseMove(Character character)
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
