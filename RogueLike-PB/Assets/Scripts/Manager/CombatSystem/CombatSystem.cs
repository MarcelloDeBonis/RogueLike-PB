using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : Singleton<CombatSystem>
{

#region Variables & Properties

[SerializeField] private GameObject player;
[SerializeField] private List<GameObject> enemyList;
[SerializeField] private float speedMovementEntities;
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

private IEnumerator CharacterAttacks(GameObject character) {
    yield return StartCoroutine(MoveCharacter(character, character.GetComponent<Character>().combatInfo.GetAlignmentPosition(), character.GetComponent<Character>().combatInfo.GetAttackPosition()));
    Debug.Log("Arrived!");
    yield return StartCoroutine(PrepareUiForAttack(character.GetComponent<Character>()));
}

private IEnumerator MoveCharacter(GameObject character, Vector3 start, Vector3 end)
{
    float time = 0f;
    while (time < 1)
    {
        time += Time.deltaTime * speedMovementEntities;
        character.transform.position = Vector3.Lerp(start, end, time);
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
    yield return null;
}

#endregion

private bool SomeoneIsDied()
{
    return false;
}

private void EndCombat()
{
    
}


#region MyRegion



#endregion




#endregion

}
