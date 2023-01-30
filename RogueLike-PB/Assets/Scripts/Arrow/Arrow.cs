using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : ObjectPoolable
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

public void SetSpeed(float NewSpeed)
{
    speed = NewSpeed;
}

public void StartMove()
{
    StartCoroutine(MoveDown());
}

private IEnumerator MoveDown()
{
    while ()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        yield return null;
    }
}

#endregion

}
