using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ArrowManager : Singleton<ArrowManager>
{

#region Variables & Properties

[SerializeField] private GameObject UiArrow;
private List<GameObject> arrowSpawnedList = new List<GameObject>();

[SerializeField] private ArrowPooler leftArrowPooler;
[SerializeField] private ArrowPooler downArrowPooler;
[SerializeField] private ArrowPooler upArrowPooler;
[SerializeField] private ArrowPooler rightArrowPooler;

[SerializeField] private GameObject colliderDeleteUp;
[SerializeField] private GameObject colliderDeleteDown;
[SerializeField] private GameObject colliderDeleteLeft;
[SerializeField] private GameObject colliderDeleteRight;

private GameObject attackingEntity;

private bool arrowsGoOn = false;

#endregion

#region Methods

public void Startmove(ScriptableMove move, GameObject newCharacter)
{
    attackingEntity = newCharacter;
    arrowsGoOn = true;
    StartCoroutine(NewMove(move));
}

private  IEnumerator NewMove(ScriptableMove move)
{
    UiArrow.SetActive(true);
    SoundManager.Instance.PlaySound(move.GetClip());
    yield return StartCoroutine(SpawnArrowsInTime(move));
    UiArrow.SetActive(false);
    arrowsGoOn = false;
}

public IEnumerator SpawnArrowsInTime(ScriptableMove move)
{

    if (attackingEntity.GetComponent<Player>() != null)
    {
        StartCoroutine(CheckInput());
    }

    foreach (ArrowProperties arrow in move.GetMove().GetArrowPropertiesList())
    {
        yield return new WaitForSeconds(arrow.GetWaitingTime());
        GameObject arrowSpawned;
        
        switch (arrow.GetEnumArrow())
        {
            case EnumArrow.LeftArrow:
                
                arrowSpawned=leftArrowPooler.SpawnArrow();
                arrowSpawned.GetComponent<ArrowPoolable>().StartArrow(colliderDeleteLeft, arrow.GetSpeed(), arrow.Getkey(), arrow.GetClip());
                arrowSpawnedList.Add(arrowSpawned);
                break;
            
            case EnumArrow.DownArrow:
                
                arrowSpawned=downArrowPooler.SpawnArrow();
                arrowSpawned.GetComponent<ArrowPoolable>().StartArrow(colliderDeleteDown, arrow.GetSpeed(), arrow.Getkey(), arrow.GetClip());
                arrowSpawnedList.Add(arrowSpawned);
                break;
            
            case EnumArrow.UpArrow:
                
                arrowSpawned=upArrowPooler.SpawnArrow();
                arrowSpawned.GetComponent<ArrowPoolable>().StartArrow(colliderDeleteUp, arrow.GetSpeed(), arrow.Getkey(), arrow.GetClip());
                arrowSpawnedList.Add(arrowSpawned);
                break;
            
            case EnumArrow.RightArrow:
                
                arrowSpawned=rightArrowPooler.SpawnArrow();
                arrowSpawned.GetComponent<ArrowPoolable>().StartArrow(colliderDeleteRight, arrow.GetSpeed(), arrow.Getkey(), arrow.GetClip());
                arrowSpawnedList.Add(arrowSpawned);
                break;
        }
    }

    yield return StartCoroutine(CheckAllListsEmpty());

}

private IEnumerator CheckInput()
{
    while (arrowsGoOn)
    {
        if (arrowSpawnedList.Count > 0)
        {
            foreach (GameObject arrow in arrowSpawnedList)
            {
                if (arrow.GetComponent<ArrowPoolable>().KeyIsPressed())
                {
                    arrow.GetComponent<ArrowPoolable>().SoundArrow();
                    
                    if (CombatSystem.Instance.GetEnumBattlePhase() == EnumBattlePhase.CharacterAttackingPhase)
                    {
                        CombatSystem.Instance.AddPointsToDamageCalculator(arrow.GetComponent<ArrowPoolable>().GetPoints());
                    }
                    else if(CombatSystem.Instance.GetEnumBattlePhase() == EnumBattlePhase.PlayerDefendingPhase)
                    {
                        CombatSystem.Instance.RemovePointsToDamageCalculator(arrow.GetComponent<ArrowPoolable>().GetPoints());
                    }
                    DeleteFromArrowInSceneList(arrow);
                    break;
                }
            }
        }
        
        yield return null;
    }
}

private IEnumerator CheckAllListsEmpty()
{
    while (arrowSpawnedList.Count>0)
    {
        yield return null;
    }
    //TODO
    //Not interrupt song
}

public void DeleteFromArrowInSceneList(GameObject arrow)
{
    arrow.GetComponent<ArrowPoolable>().DeactiveAfterTime(0);
    arrowSpawnedList.Remove(arrow);
}

public bool GetArrowGoOn()
{
    return arrowsGoOn;
}

public GameObject GetUiArrow()
{
    return UiArrow;
}

public GameObject GetAttackingEntity()
{
    return attackingEntity;
}

#endregion

}
