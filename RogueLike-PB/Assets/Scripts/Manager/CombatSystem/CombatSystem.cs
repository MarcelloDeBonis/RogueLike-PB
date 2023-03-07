using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CombatSystem : Singleton<CombatSystem>
{

#region Variables & Properties

[SerializeField] private ScriptablePlayerInfo playerSaveData;

private int currentDamage = 0;

private GameObject player;
private List<GameObject> enemyList = new List<GameObject>();
[SerializeField] private float speedMovementEntities;

private EnumBattlePhase battlePhase = EnumBattlePhase.NoBattlePhase;
private GameObject characterIsDoingMove;
private GameObject opponentSelected;

[SerializeField] private ScriptableEffective scriptableEffective;
[SerializeField] private GameObject screenArrowPlayer;
[SerializeField] private GameObject screenArrowEnemies;
[SerializeField] private GameObject screenPlayerMove;
[SerializeField] private GameObject screenEnemyMove;
[SerializeField] private GameObject moveSpriteGameObject;

private ScriptableMove choosenMove;
private int strumentDamage;

[SerializeField] private GameObject moveCollector;
[FormerlySerializedAs("move2DObject")] [SerializeField] private GameObject move2DObjectPrefab;

[SerializeField] private Slider damageSliderReference;

bool choosen = false;

private List<Move2DComponent> move2dList = new List<Move2DComponent>();

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
    
    SetPlayerMovesButtons();
}

private void SetPlayerMovesButtons()
{
    move2dList.Clear();
    
    foreach (ScriptableMove scriptableMove in player.GetComponent<Player>().GetCombatInfo().GetScriptableMove())
    {
        GameObject newCollectorMove = Instantiate(move2DObjectPrefab);
        newCollectorMove.transform.position = moveCollector.transform.position;
        newCollectorMove.transform.SetParent(moveCollector.transform);
        newCollectorMove.GetComponent<Move2DComponent>().InitObject(scriptableMove);
        move2dList.Add(newCollectorMove.GetComponent<Move2DComponent>());
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
        yield return StartCoroutine(MoveCharacter(player, player.GetComponent<Character>().GetCombatInfo().GetAlignmentPosition(), speedMovementEntities));
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
            yield return StartCoroutine(MoveCharacter(enemy, enemy.GetComponent<Character>().GetCombatInfo().GetAttackPosition(),speedMovementEntities));
        }
        
        yield return StartCoroutine(ChooseMove(enemy));
        yield return StartCoroutine(PrepareUiForMove(enemy));
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
            yield return StartCoroutine(MoveCharacter(enemy, enemy.GetComponent<Character>().GetCombatInfo().GetAlignmentPosition(), speedMovementEntities));
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

private void GenerateMoveSprite()
{
    Destroy(moveSpriteGameObject);
    moveSpriteGameObject = Instantiate(choosenMove.GetMove().GetPrefab());
    moveSpriteGameObject.GetComponent<Move2DSprite>().SetScale(0);
    moveSpriteGameObject.SetActive(false);
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

private IEnumerator MoveCharacter(GameObject character, Vector3 end, float speed)
{
    while (character.transform.position != end)
    {
        character.transform.position = Vector3.MoveTowards(character.transform.position, end, speed * Time.deltaTime);
        yield return null;
    } 
}

private IEnumerator PlayerDefend()
{
    yield return StartCoroutine(PrepareUiForMove(player));
    yield return StartCoroutine(StartMoveOnScreen(player));
    yield return null;
}

private IEnumerator ChooseMove(GameObject character)
{
    strumentDamage=character.GetComponent<Character>().GetCombatInfo().GetStrumentDamage();
    
    if (enemyList.Contains(character))
    {
        choosenMove = character.GetComponent<Character>().GetCombatInfo().GetRandomScriptableMove();
        GenerateMoveSprite();
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
    foreach (Move2DComponent move in move2dList)
    {
        if (move.scriptableMove!=_move)
        {
            move.DeactiveSelection();
        }
    }
    
    choosenMove = _move;
    choosen = true;

    GenerateMoveSprite();
}

private IEnumerator PrepareUiForMove(GameObject character)
{
    if (battlePhase != EnumBattlePhase.PlayerDefendingPhase)
    {
        damageSliderReference.value = 0;
        moveSpriteGameObject.GetComponent<Move2DSprite>().SetScale(0);
    }
    
    damageSliderReference.gameObject.SetActive(true);
    
    if (character.GetComponent<Player>() != null)
    {
        if (battlePhase != EnumBattlePhase.PlayerDefendingPhase)
        {
            moveSpriteGameObject.transform.position = screenPlayerMove.transform.position;
            moveSpriteGameObject.SetActive(true);
        }

        ArrowManager.Instance.GetUiArrow().transform.position = screenArrowPlayer.transform.position;
    }
    else
    {
        moveSpriteGameObject.transform.position = screenEnemyMove.transform.position;
        moveSpriteGameObject.SetActive(true);
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

    if (!enemyList.Contains(character))
    {
        yield return DoMoveAnimation();
    }

    damageSliderReference.gameObject.SetActive(false);
}

private IEnumerator DoMoveAnimation()
{
    //TODO BY CODE, BUT IN THE FUTURE, DONE BY DESIGN/2D ARTIST
    yield return MoveCharacter(moveSpriteGameObject, opponentSelected.transform.position, moveSpriteGameObject.GetComponent<Move2DSprite>().speedMoving);

    //TODO ANIMATION VFX WHEN ENEMY IS HITTED
    
    //Play Sound Hit
    SoundManager.Instance.PlayEffect(opponentSelected.GetComponent<Character>().GetCombatInfo().clipHitted);

    //TODO ANIMATION BY CAMERA
    
    //TODO ANIMATION OF CRIT
    
    //TODO ANIMATION HITTED and wait until is finished
    moveSpriteGameObject.GetComponent<Move2DSprite>().HitAnimation(FindSpriteDependingOfMultiplier(GetTotalMultiplier()));
    
}



private IEnumerator ApplyDamage(GameObject character)
{
    Debug.Log("damage before calculation: " + currentDamage);
    
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

private Sprite FindSpriteDependingOfMultiplier(EnumEffectType effectType)
{
    foreach (EffecTypeIconStruct iconStruct in scriptableEffective.effectTypeIconList)
    {
        if (iconStruct.effectType == effectType)
        {
            return iconStruct.sprite;
        }
    }

    return null;
}

private void CalculateDamage(GameObject character)
{
    float percentage =(float)currentDamage / (float)choosenMove.GetMove().GetMaxDamagePossible() * 100;
    percentage = percentage * strumentDamage * choosenMove.GetMove().GetDamage() / 100;
    
    percentage = percentage / (character.GetComponent<Character>().GetCombatInfo().GetDefence() * 2);
    
    
    
    percentage = percentage * GetNumberMultiplier();
    
    if (Crit())
    {
        percentage *= 2;
    }

    currentDamage = (int)Math.Ceiling(percentage);
}

#region MultiplierMethods


private float GetNumberMultiplier()
{
    foreach (EffectTypeMultiplierStruct multiplier in scriptableEffective.effectTypeMultiplierList)
    {
        if (GetTotalMultiplier() == multiplier.effectType)
        {
            return multiplier.mutiplier;
        }
    }

    return 1;
}

private EnumEffectType GetElementalMultiplier()
{
    //Elemental Multiplier
    if (choosenMove.GetMove().GetElementTyping().normalEffectiveList
        .Contains(opponentSelected.GetComponent<Character>().GetCombatInfo().defenceElementTyping))
    {
        return EnumEffectType.Normal;
    }
    else if (choosenMove.GetMove().GetElementTyping().superEffectiveList
             .Contains(opponentSelected.GetComponent<Character>().GetCombatInfo().defenceElementTyping))
    {
        return EnumEffectType.Effective;
    }
    else if (choosenMove.GetMove().GetElementTyping().notEffectiveList
             .Contains(opponentSelected.GetComponent<Character>().GetCombatInfo().defenceElementTyping))
    {
        return EnumEffectType.NotVeryEffective;
    }

    return EnumEffectType.Normal;
}

private EnumEffectType GetSoundMultiplier()
{
    //SoundMultiplier
    if (characterIsDoingMove.GetComponent<Character>().GetCombatInfo().GetStrumentSoundTyping().normalEffectiveList
        .Contains(opponentSelected.GetComponent<Character>().GetCombatInfo().defenceSoundTyping))
    {
        return EnumEffectType.Normal;
    }
    else if ((characterIsDoingMove.GetComponent<Character>().GetCombatInfo().GetStrumentSoundTyping().superEffectiveList
                 .Contains(opponentSelected.GetComponent<Character>().GetCombatInfo().defenceSoundTyping)))
    {
        return EnumEffectType.Effective;
    }
    else if (((characterIsDoingMove.GetComponent<Character>().GetCombatInfo().GetStrumentSoundTyping().notEffectiveList
                 .Contains(opponentSelected.GetComponent<Character>().GetCombatInfo().defenceSoundTyping))))
    {
        return EnumEffectType.NotVeryEffective;
    }

    return EnumEffectType.Normal;
}

private EnumEffectType GetTotalMultiplier()
{

    if (GetElementalMultiplier() == EnumEffectType.Effective)
    {
        if (GetSoundMultiplier() == EnumEffectType.Effective)
        {
            return EnumEffectType.SuperEffective;
        }
        else if (GetSoundMultiplier() == EnumEffectType.Normal)
        {
            return EnumEffectType.Effective;
        }
        else if (GetSoundMultiplier() == EnumEffectType.NotVeryEffective)
        {
            return EnumEffectType.Normal;
        }
    }
    else if (GetElementalMultiplier() == EnumEffectType.Normal)
    {
        return GetSoundMultiplier();
    }
    else if(GetElementalMultiplier() == EnumEffectType.NotVeryEffective)
    {
        if (GetSoundMultiplier() == EnumEffectType.Effective)
        {
            return EnumEffectType.Normal;
        }
        else if (GetSoundMultiplier() == EnumEffectType.Normal)
        {
            return EnumEffectType.NotVeryEffective;
        }
        else if (GetSoundMultiplier() == EnumEffectType.NotVeryEffective)
        {
            return EnumEffectType.NotEffective;
        }
    }

    return EnumEffectType.Normal;
}

#endregion


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
    currentDamage -= damagePoints;
    UpgradeDamageSlider();
    UpgradeMoveScale();
}

public void AddPointsToDamageCalculator(int damagePoints)
{
    currentDamage += damagePoints;
    UpgradeDamageSlider();
    UpgradeMoveScale();
}

private void ResetDamage()
{
    currentDamage = 0;
    damageSliderReference.value = 0;
}

private void UpgradeMoveScale()
{
    moveSpriteGameObject.GetComponent<Move2DSprite>().SetScale((float)currentDamage / (float)choosenMove.GetMove().GetMaxDamagePossible());
}

private void UpgradeDamageSlider()
{
    damageSliderReference.value = (float)currentDamage / (float)choosenMove.GetMove().GetMaxDamagePossible();
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
        playerSaveData.SetCombactInfo(player.GetComponent<Character>().GetCombatInfo());
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
