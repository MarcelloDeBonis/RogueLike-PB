using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2DSprite : MonoBehaviour
{

#region Variables & Properties

[SerializeField] private Vector3 startScale;
[SerializeField] private Vector3 endScale;

#endregion

#region Methods

public void SetScale(float t)
{
    transform.localScale = Vector3.Lerp(startScale, endScale, t);
}

public Vector3 GetStartScale()
{
    return startScale;
}

public Vector3 GetEndScale()
{
    return endScale;
}

#endregion

}
