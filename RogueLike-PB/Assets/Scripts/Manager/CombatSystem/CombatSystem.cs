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
        yield return StartCoroutine(MoveCharacterAndEnemySelected(player, player.GetComponent<Character>().combatInfo.GetAlignmentPosition(), enemySelected, enemySelected.GetComponent<Character>().combatInfo.GetAlignmentPosition()));
        
        foreach (GameObject enemy in enemyList)
        {
            //TODO
            //Firstenemy and player moves to the centre
            //else only enemies moves to centre
            yield return StartCoroutine(CharacterAttacks(enemy));
            yield return StartCoroutine(PlayerDefend());
            
            //EnemyComesBacks
        }
        
        //TODO
        //player comes back
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
   yield return StartCoroutine(MoveCharacterAndEnemySelected(character, character.GetComponent<Character>().combatInfo.GetAttackPosition(), enemySelected, enemySelected.GetComponent<Character>().combatInfo.GetAttackPosition()));
   yield return StartCoroutine(PrepareUiForAttack(character.GetComponent<Character>()));
   
   //TODO
   //Rhythm attack + calculate damage
   
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


private IEnumerator MoveCharacterAndEnemySelected(GameObject character1, Vector3 end1, GameObject character2, Vector3 end2)
{
    while (character1.transform.position != end1)
    {
        character1.transform.position = Vector3.MoveTowards(character1.transform.position, end1, speedMovementEntities * Time.deltaTime);
        enemySelected.transform.position= Vector3.MoveTowards(enemySelected.transform.position, end2, speedMovementEntities * Time.deltaTime);
        yield return null;
    }
    
}

private IEnumerator MoveCharacter(GameObject character, Vector3 end)
{
    while (character.transform.position != character.GetComponent<Character>().combatInfo.GetAttackPosition())
    {
        character.transform.position = Vector3.MoveTowards(character.transform.position, character.GetComponent<Character>().combatInfo.GetAttackPosition(), speedMovementEntities * Time.deltaTime);
        enemySelected.transform.position= Vector3.MoveTowards(enemySelected.transform.position, enemySelected.GetComponent<Character>().combatInfo.GetAttackPosition(), speedMovementEntities * Time.deltaTime);
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
