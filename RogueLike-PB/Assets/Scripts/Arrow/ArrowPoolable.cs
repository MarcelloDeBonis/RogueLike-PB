using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPoolable : ObjectPoolable
{

#region Variables & Properties

private float speed;

#endregion

#region MonoBehaviour

    // Awake is called when the script instance is being loaded
    void Awake()
    {
	
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

private void SetSpeed(float newSpeed)
{
    speed = newSpeed;
}

public void StartMove(float newSpeed)
{
    SetSpeed(newSpeed);
    StartCoroutine(MoveDown());
}

private IEnumerator MoveDown()
{
    while (true)
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        yield return null;
    }
}

#endregion

}
