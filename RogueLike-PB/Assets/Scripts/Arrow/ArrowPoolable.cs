using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPoolable : ObjectPoolable
{

#region Variables & Properties

private float speed;
private EnumArrow enumArrow;
private KeyCode key;
private int points = 0;

#endregion

#region Methods

private void SetSpeed(float newSpeed)
{
    speed = newSpeed;
}

public void StartArrow(GameObject colliderDeleteArrow ,float newSpeed, KeyCode newKey)
{
    SetKey(newKey);
    SetSpeed(newSpeed);
    StartCoroutine(MoveDown(colliderDeleteArrow));
}

private void SetKey(KeyCode newKey)
{
    key = newKey;
}

private IEnumerator MoveDown(GameObject colliderDeleteArrow)
{
    while (true)
    {
        transform.position = Vector3.MoveTowards(transform.position, colliderDeleteArrow.transform.position,  speed * Time.deltaTime);
        yield return null;
    }
}

public bool KeyIsPressed()
{
    return (Input.GetKeyDown(key));
}

public void SetPoints(int newPoints)
{
    points = newPoints;
}

public int GetPoints()
{
        return points;
}

#endregion

}
