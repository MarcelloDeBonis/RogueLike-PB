using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CombatSystem : Singleton<CombatSystem>
{

#region Variables & Properties

private int currentDamage = 0;

private GameObject player;
private List<GameObject> enemyList = new List<GameObject>();
[SerializeField] private float speedMovementEntities;

private EnumBattlePhase battlePhase = EnumBattlePhase.NoBattlePhase;
private GameObject characterIsDoingMove;
private GameObject opponentSelected;

[SerializeField] private GameObject screenArrowPlayer;
[SerializeField] private GameObject screenArrowEnemies;
private ScriptableMove choosenMove;
private int strumentDamage;

[SerializeField] private GameObject moveCollector;
[SerializeField] private GameObject move2DObjectPrefab;

//[SerializeField] private Text 

bool choosen = false;

#endregion

#region MonoBehaviour

protected override void Awake()
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

#region Pre Combat

public void OnCombatStart(GameObject _player, List<GameObject> _enemyList)
{
    PrepareCombat(_player, _enemyList);
    StartCoroutine(CombatLoop());
}

private void PrepareCombat(GameObject _player, List<GameObject> _enemylist)
{
    player = _player;
    
    player.GetComponent<Character>().UpgradeLife();
    player.GetComponent<Character>().UpgradeName();
    
    foreach (GameObject enemy in _enemylist)
    {
        enemyList.Add(enemy);
        enemy.GetComponent<Character>().UpgradeLife();
        enemy.GetComponent<Character>().UpgradeName();
    }
    
    moveCollector.SetActive(true);
    
    foreach (ScriptableMove scriptableMove in player.GetComponent<Player>().GetCombatInfo().GetScriptableMove())
    {
      //  //TODO BUG MOVES
        GameObject newCollectorMove = Instantiate(move2DObjectPrefab);
        newCollectorMove.transform.position = moveCollector.transform.position;
        newCollectorMove.transform.SetParent(moveCollector.transform);
        newCollectorMove.GetComponent<Move2DComponent>().InitObject(scriptableMove);
    }
}

#endregion

private IEnumerator CombatLoop()
{
    while (!TeamWon())
    {
        yield return StartCoroutine(PlayerPhase());
        
        if (enemyList.Count > 0)
        {
            yield return StartCoroutine(EnemiesPhase());
        }
    }

    EndCombat();

}

private IEnumerator PlayerPhase()
{
    characterIsDoingMove = player;
    yield return StartCoroutine(PrepareUiForMovesChooses());
    yield return StartCoroutine(ChooseMove(player));
    battlePhase = EnumBattlePhase.SelectingPhase;
    yield return StartCoroutine(ChooseTarget(player));
    yield return StartCoroutine(MoveCharacterAndEnemySelected(player, player.GetComponent<Character>().GetCombatInfo().GetAttackPosition(), opponentSelected, opponentSelected.GetComponent<Character>().GetCombatInfo().GetAttackPosition()));
    yield return StartCoroutine(PrepareUiForMove(player));
    battlePhase = EnumBattlePhase.CharacterAttackingPhase;
    yield return StartCoroutine(StartMoveOnScreen(player));
    yield return StartCoroutine(ApplyDamage(opponentSelected));
    if (opponentSelected != null)
    {
        yield return StartCoroutine(MoveCharacterAndEnemySelected(player, player.GetComponent<Character>().GetCombatInfo().GetAlignmentPosition(), opponentSelected, opponentSelected.GetComponent<Character>().GetCombatInfo().GetAlignmentPosition()));
    }
    else
    {
        yield return StartCoroutine(MoveCharacter(player, player.GetComponent<Character>().GetCombatInfo().GetAlignmentPosition()));
    }
}

private IEnumerator EnemiesPhase()
{
    GameObject firsteEnemy = enemyList[0];
    GameObject lastEnemy = enemyList[enemyList.Count - 1];
        
    foreach (GameObject enemy in enemyList)
    {
        characterIsDoingMove = enemy;
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
        yield return StartCoroutine(ChooseMove(enemy));
        battlePhase = EnumBattlePhase.CharacterAttackingPhase;
        yield return StartCoroutine(StartMoveOnScreen(enemy));
        battlePhase = EnumBattlePhase.PlayerDefendingPhase;
        yield return StartCoroutine(PlayerDefend());
        yield return StartCoroutine(ApplyDamage(opponentSelected));

        if (opponentSelected==null)
        {
            break;
        }  
        
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

#region CombatFases


private IEnumerator PrepareUiForMovesChooses()
{
    moveCollector.SetActive(true);
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
        
        moveCollector.SetActive(false);
    }
    
}


private IEnumerator MoveCharacterAndEnemySelected(GameObject character1, Vector3 end1, GameObject character2, Vector3 end2)
{
    while (character1.transform.position != end1)
    {
        character1.transform.position = Vector3.MoveTowards(character1.transform.position, end1, speedMovementEntities * Time.deltaTime);
        character2.transform.position= Vector3.MoveTowards( character2.transform.position, end2, speedMovementEntities * Time.deltaTime);
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
    //TODO dannometro a schermo
    yield return null;
}

private IEnumerator ChooseMove(GameObject character)
{
    strumentDamage=character.GetComponent<Character>().GetCombatInfo().GetStrumentDamage();
    
    if (enemyList.Contains(character))
    {
        choosenMove = character.GetComponent<Character>().GetCombatInfo().GetRandomScriptableMove();
    }
    else
    {
        choosen = false;

        while (!choosen)
        {
            yield return null;
        }
    }
    
    
}

public void ChooseMove(ScriptableMove _move)
{
    choosenMove = _move;
    choosen = true;
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

    CalculateDamage(character);
    
    character.GetComponent<Character>().TakeDamage(currentDamage);

    if (character.GetComponent<Character>().GetCombatInfo().IsDied())
    {
        if (enemyList.Contains(character))
        {
            enemyList.Remove(character);
        }
        else
        {
            //TODO IN CASE OF ALLIES
        }
        character.GetComponent<Character>().Die();
    }
    yield return null;
}

private void CalculateDamage(GameObject character)
{
    float percentage = (float)choosenMove.GetMove().GetMaxDamagePossible() / (float)currentDamage;
    percentage = percentage * 100 * strumentDamage * choosenMove.GetMove().GetDamage();
    if (character.GetComponent<Character>().GetCombatInfo().GetDefence() != 0)
    {
        percentage = percentage / (character.GetComponent<Character>().GetCombatInfo().GetDefence() * 2);
    }
    percentage = percentage * CalculateMultiplier();

   //TODO
    if (Crit())
    {
        percentage *= 2;
    }

    currentDamage = (int)Math.Ceiling(percentage);
}

private float CalculateMultiplier()
{
    float multiplier = 1f;

    
    
    
    
    //Elemental Multiplier
    if (choosenMove.GetMove().GetElementTyping().normalEffectiveList
        .Contains(opponentSelected.GetComponent<Character>().GetCombatInfo().defenceElementTyping))
    {
        
    }
    else if (choosenMove.GetMove().GetElementTyping().superEffectiveList
             .Contains(opponentSelected.GetComponent<Character>().GetCombatInfo().defenceElementTyping))
    {
        //TODO DELETE THOSE MAGIC NUMBERS!!!
        multiplier *= 2;
    }
    else if (choosenMove.GetMove().GetElementTyping().notEffectiveList
             .Contains(opponentSelected.GetComponent<Character>().GetCombatInfo().defenceElementTyping))
    {
        multiplier *= 0.5f;
    }

    
    
    
    
    
    
    
    //SoundMultiplier
    if (characterIsDoingMove.GetComponent<Character>().GetCombatInfo().GetStrumentSoundTyping().normalEffectiveList
        .Contains(opponentSelected.GetComponent<Character>().GetCombatInfo().defenceSoundTyping))
    {
        
    }
    else if ((characterIsDoingMove.GetComponent<Character>().GetCombatInfo().GetStrumentSoundTyping().superEffectiveList
                 .Contains(opponentSelected.GetComponent<Character>().GetCombatInfo().defenceSoundTyping)))
    {
        multiplier *= 2;
    }
    else if (((characterIsDoingMove.GetComponent<Character>().GetCombatInfo().GetStrumentSoundTyping().notEffectiveList
                 .Contains(opponentSelected.GetComponent<Character>().GetCombatInfo().defenceSoundTyping))))
    {
        multiplier *= 0.5f;
    }
    
    return multiplier;
}

private bool Crit()
{
    int critic = Random.Range(0, 101);

    //TODO MAGIC NUMBER FOR CRITIC NO!!!! THEY COULD BE SETTED BY SOME STRUMENTS OR OTHER THINGS
    if (critic < 5)
    {
        return true;
    }
    else
    {
        return false;
    }
}

#endregion

public void RemovePointsToDamageCalculator(int damagePoints)
{
    //TODO DANNOMETRO
    currentDamage -= damagePoints;
}

public void AddPointsToDamageCalculator(int damagePoints)
{
    //TODO DANNOMETRO
    currentDamage += damagePoints;
    Debug.Log("Current damage: " + currentDamage);
}

private void ResetDamage()
{
    currentDamage = 0;
}

private bool TeamWon()
{
    if (player==null || enemyList.Count == 0)
    {
        return true;
    }
    return false;
}

private void EndCombat()
{
    if (player==null)
    {
        Debug.Log("You lost!!");
        SceneManager.LoadScene("GameOver");
    }
    else if (enemyList.Count == 0)
    {
        RoomManager.Instance.RoomEmpty();
    }
    
    
}


#region Getters

public EnumBattlePhase GetEnumBattlePhase()
{
    return battlePhase;
}

#endregion




#endregion

}
