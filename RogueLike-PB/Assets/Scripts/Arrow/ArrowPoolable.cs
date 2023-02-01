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

public void StartArrow(float newSpeed, KeyCode newKey)
{
    SetKey(newKey);
    SetSpeed(newSpeed);
    StartCoroutine(MoveDown());
}

private void SetKey(KeyCode newKey)
{
    key = newKey;
}

private IEnumerator MoveDown()
{
    while (true)
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
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
    return points
}

#endregion

}
