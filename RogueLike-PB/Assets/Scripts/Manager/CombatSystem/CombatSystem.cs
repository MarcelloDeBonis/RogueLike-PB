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

[SerializeField] private GameObject screenArrowPlayer;
[SerializeField] private GameObject screenArrowEnemies;
private ScriptableMove choosenMove;

[SerializeField] private GameObject playerCanvas;
[SerializeField] private GameObject moveCollector;
[SerializeField] private GameObject move2DObjectPrefab;

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
        yield return StartCoroutine(PrepareUiForMovesChooses());
        yield return StartCoroutine(ChooseMove(player));
        battlePhase = EnumBattlePhase.SelectingPhase;
        yield return StartCoroutine(ChooseTarget(player));
        yield return StartCoroutine(MoveCharacterAndEnemySelected(player, player.GetComponent<Character>().GetCombatInfo().GetAttackPosition(), opponentSelected, opponentSelected.GetComponent<Character>().GetCombatInfo().GetAttackPosition()));
        yield return StartCoroutine(PrepareUiForMove(player));
        battlePhase = EnumBattlePhase.CharacterAttackingPhase;
        yield return StartCoroutine(StartMoveOnScreen(player));
        yield return StartCoroutine(ApplyDamage(opponentSelected));
        yield return StartCoroutine(MoveCharacterAndEnemySelected(player, player.GetComponent<Character>().GetCombatInfo().GetAlignmentPosition(), opponentSelected, opponentSelected.GetComponent<Character>().GetCombatInfo().GetAlignmentPosition()));
        
        GameObject firsteEnemy = enemyList[0];
        GameObject lastEnemy = enemyList[enemyList.Count - 1];
        
        foreach (GameObject enemy in enemyList)
        {
            yield return StartCoroutine(ChooseTarget(enemy));
            
            if (enemy== firsteEnemy)
            {
                yield return StartCoroutine(MoveCharacterAndEnemySelected(enemy, enemy.GetComponent<Character>().GetCombatInfo().GetAttackPosition(), opponentSelected, opponentSelected.GetComponent<Character>().GetCombatInfo().GetAttackPosition()));
            }
            else
            {
                yield return StartCoroutine(MoveCharacter(enemy, enemy.GetComponent<Character>().GetCombatInfo().GetAttackPosition()));
            }
            
            yield return StartCoroutine(PrepareUiForMove(enemy));
            battlePhase = EnumBattlePhase.CharacterAttackingPhase;
            yield return StartCoroutine(StartMoveOnScreen(enemy));
            battlePhase = EnumBattlePhase.PlayerDefendingPhase;
            yield return StartCoroutine(PlayerDefend());
            yield return StartCoroutine(ApplyDamage(opponentSelected));
            
          
            if (enemy == lastEnemy)
            {
                yield return StartCoroutine(MoveCharacterAndEnemySelected(enemy, enemy.GetComponent<Character>().GetCombatInfo().GetAlignmentPosition(), opponentSelected, opponentSelected.GetComponent<Character>().GetCombatInfo().GetAlignmentPosition()));
            }
            else
            {
                yield return StartCoroutine(MoveCharacter(enemy, enemy.GetComponent<Character>().GetCombatInfo().GetAlignmentPosition()));
            }

        }
        
    }

    EndCombat();

}

#region CombatFases


private IEnumerator PrepareUiForMovesChooses()
{
    playerCanvas.SetActive(true);
    foreach (ScriptableMove scriptableMove in player.GetComponent<Player>().GetCombatInfo().GetScriptableMove())
    {
        GameObject newCollectorMove = Instantiate(move2DObjectPrefab, moveCollector.transform.position, moveCollector.transform.rotation);
        newCollectorMove.transform.parent = moveCollector.transform;
        newCollectorMove.GetComponent<Move2DComponent>().InitObject(scriptableMove);
    }

    yield return null;
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

            yield return null;
        }
        
        playerCanvas.SetActive(false);
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
    if (enemyList.Contains(character))
    {
        choosenMove = character.GetComponent<Character>().GetCombatInfo().GetRandomScriptableMove();
    }
    else
    {
        bool choosen = false;

        while (!choosen)
        {
            if (character.GetComponent<Player>() != null)
            {
                foreach (Transform child in moveCollector.transform)
                {
            if (child.gameObject.GetComponent<GameObject>() != null)
                {
                            GameObject move = child.gameObject.GetComponent<GameObject>();
                    if (move.GetComponent<Move2DComponent>().GetIsSelected())
                    {
                        move.GetComponent<Move2DComponent>().DeactiveIsSelected();
                        choosenMove = move.GetComponent<Move2DComponent>().GetScriptableMove();
                        choosen = true;
                    }
                }
                }
            }

            yield return null;
        }
    }
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
