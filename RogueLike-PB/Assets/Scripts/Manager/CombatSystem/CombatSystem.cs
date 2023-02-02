using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : Singleton<CombatSystem>
{

#region Variables & Properties

private int currentDamage = 0;

[SerializeField] private GameObject player;
[SerializeField] private List<GameObject> enemyList;
[SerializeField] private float speedMovementEntities;

private EnumBattlePhase battlePhase = EnumBattlePhase.NoBattlePhase;
private GameObject opponentSelected;

[SerializeField] private GameObject selectMovesParent;

[SerializeField] private GameObject screenArrowPlayer;
[SerializeField] private GameObject screenArrowEnemies;
private ScriptableMove choosenMove;

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
        yield return StartCoroutine(ChooseMove(player));
        battlePhase = EnumBattlePhase.SelectingPhase;
        yield return StartCoroutine(ChooseTarget(player));
        yield return StartCoroutine(MoveCharacterAndEnemySelected(player, player.GetComponent<Character>().combatInfo.GetAttackPosition(), opponentSelected, opponentSelected.GetComponent<Character>().combatInfo.GetAttackPosition()));
        yield return StartCoroutine(PrepareUiForMove(player));
        battlePhase = EnumBattlePhase.CharacterAttackingPhase;
        yield return StartCoroutine(StartMoveOnScreen(player));
        yield return StartCoroutine(ApplyDamage(opponentSelected));
        yield return StartCoroutine(MoveCharacterAndEnemySelected(player, player.GetComponent<Character>().combatInfo.GetAlignmentPosition(), opponentSelected, opponentSelected.GetComponent<Character>().combatInfo.GetAlignmentPosition()));
        
        GameObject firsteEnemy = enemyList[0];
        GameObject lastEnemy = enemyList[enemyList.Count - 1];
        
        foreach (GameObject enemy in enemyList)
        {
            yield return StartCoroutine(ChooseTarget(enemy));
            
            if (enemy== firsteEnemy)
            {
                yield return StartCoroutine(MoveCharacterAndEnemySelected(enemy, enemy.GetComponent<Character>().combatInfo.GetAttackPosition(), opponentSelected, opponentSelected.GetComponent<Character>().combatInfo.GetAttackPosition()));
            }
            else
            {
                yield return StartCoroutine(MoveCharacter(enemy, enemy.GetComponent<Character>().combatInfo.GetAttackPosition()));
            }
            
            yield return StartCoroutine(PrepareUiForMove(enemy));
            battlePhase = EnumBattlePhase.CharacterAttackingPhase;
            yield return StartCoroutine(StartMoveOnScreen(enemy));
            battlePhase = EnumBattlePhase.PlayerDefendingPhase;
            yield return StartCoroutine(PlayerDefend());
            yield return StartCoroutine(ApplyDamage(opponentSelected));
            
          
            if (enemy == lastEnemy)
            {
                yield return StartCoroutine(MoveCharacterAndEnemySelected(enemy, enemy.GetComponent<Character>().combatInfo.GetAlignmentPosition(), opponentSelected, opponentSelected.GetComponent<Character>().combatInfo.GetAlignmentPosition()));
            }
            else
            {
                yield return StartCoroutine(MoveCharacter(enemy, enemy.GetComponent<Character>().combatInfo.GetAlignmentPosition()));
            }

        }
        
    }

    EndCombat();

}

#region CombatFases


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
    while (character.transform.position != end)
    {
        character.transform.position = Vector3.MoveTowards(character.transform.position, end, speedMovementEntities * Time.deltaTime);
        yield return null;
    } 
}

private IEnumerator PlayerDefend()
{

    yield return StartCoroutine(PrepareUiForMove(player));
    yield return StartCoroutine(StartMoveOnScreen(player));
    //TODO
    yield return null;
}

private IEnumerator ChooseMove(GameObject character)
{
    //TODO
    
    //if (character.GetComponent<Player>()!=null)
    {
      //  yield return StartCoroutine(SelectMoveScreen());
    }
 //   else
    {
        
        
    }
    
    choosenMove = character.GetComponent<Character>().GetCombatInfo().GetRandomScriptableMove();
    yield return null;
}

private IEnumerator SelectMoveScreen()
{
    //TODO BETTER
    //selectMovesParent.SetActive(true);
    {
        
        yield return null;
    }
    //selectMovesParent.SetActive(false);
}

private IEnumerator PrepareUiForMove(GameObject character)
{
    //TODO

    if (character.GetComponent<Player>() != null)
    {
        ArrowManager.Instance.GetUiArrow().transform.position = screenArrowPlayer.transform.position;
    }
    else
    {
        ArrowManager.Instance.GetUiArrow().transform.position = screenArrowEnemies.transform.position;
    }

    yield return null;
}

private IEnumerator StartMoveOnScreen(GameObject character)
{

    if (battlePhase == EnumBattlePhase.CharacterAttackingPhase)
    {
        ResetDamage();
    }

    ArrowManager.Instance.Startmove(choosenMove, character);
    yield return null;
    while (ArrowManager.Instance.GetArrowGoOn())
    {
        yield return null;
    }
}

private IEnumerator ApplyDamage(GameObject character)
{
    //TODO
    yield return null;
}

#endregion

public void RemovePointsToDamageCalculator(int damagePoints)
{
    currentDamage -= damagePoints;
    Debug.Log("Current damage: " + currentDamage);
}

public void AddPointsToDamageCalculator(int damagePoints)
{
    currentDamage += damagePoints;
    Debug.Log("Current damage: " + currentDamage);
}

private void ResetDamage()
{
    currentDamage = 0;
}

private bool SomeoneIsDied()
{
    return false;
}

private void EndCombat()
{
    //TODO
}


#region Getters

public EnumBattlePhase GetEnumBattlePhase()
{
    return battlePhase;
}

#endregion




#endregion

}
