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

private bool arrowsGoOn = false;

#endregion

#region Methods

public void Startmove(ScriptableMove move)
{
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
    StartCoroutine(CheckInput());

    foreach (ArrowProperties arrow in move.GetMove().GetArrowPropertiesList())
    {
        yield return new WaitForSeconds(arrow.GetWaitingTime());
        GameObject arrowSpawned;
        
        switch (arrow.GetEnumArrow())
        {
            case EnumArrow.LeftArrow:
                
                arrowSpawned=leftArrowPooler.SpawnArrow();
                arrowSpawned.GetComponent<ArrowPoolable>().StartArrow(arrow.GetSpeed(), arrow.Getkey());
                arrowSpawnedList.Add(arrowSpawned);
                break;
            
            case EnumArrow.DownArrow:
                
                arrowSpawned=downArrowPooler.SpawnArrow();
                arrowSpawned.GetComponent<ArrowPoolable>().StartArrow(arrow.GetSpeed(), arrow.Getkey());
                arrowSpawnedList.Add(arrowSpawned);
                break;
            
            case EnumArrow.UpArrow:
                
                arrowSpawned=upArrowPooler.SpawnArrow();
                arrowSpawned.GetComponent<ArrowPoolable>().StartArrow(arrow.GetSpeed(), arrow.Getkey());
                arrowSpawnedList.Add(arrowSpawned);
                break;
            
            case EnumArrow.RightArrow:
                
                arrowSpawned=rightArrowPooler.SpawnArrow();
                arrowSpawned.GetComponent<ArrowPoolable>().StartArrow(arrow.GetSpeed(), arrow.Getkey());
                arrowSpawnedList.Add(arrowSpawned);
                break;
        }
    }

    yield return StartCoroutine(CheckAllListsEmpty());

}

private IEnumerator CheckInput()
{
    while (true)
    {
        GameObject arrowSelected;

        if (arrowSpawnedList.Count > 0)
        {
            foreach (GameObject arrow in arrowSpawnedList)
            {
                if (arrow.GetComponent<ArrowPoolable>().KeyIsPressed())
                {
                        //TODO
                        //ToDoBetter
                        CombatSystem.Instance.AddPointsToDamageCalculator(arrow.GetComponent<ArrowPoolable>().GetPoints());
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

#endregion

}
