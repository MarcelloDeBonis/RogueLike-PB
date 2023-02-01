using System.Collections;
using System.Collections.Generic;
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

#endregion

#region Methods

public void Startmove(ScriptableMove move)
{
    StartCoroutine(NewMove(move));
}

private  IEnumerator NewMove(ScriptableMove move)
{
    UiArrow.SetActive(true);
    SoundManager.Instance.PlaySound(move.GetClip());
    yield return StartCoroutine(SpawnArrowsInTime(move));
    UiArrow.SetActive(false);
}

public IEnumerator SpawnArrowsInTime(ScriptableMove move)
{
    StartCoroutine(CheckImput());

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

private IEnumerator CheckImput()
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
}

public void DeleteFromArrowInSceneList(GameObject arrow)
{
    arrow.GetComponent<ArrowPoolable>().DeactiveAfterTime(0);
    arrowSpawnedList.Remove(arrow);
}

#endregion

}
