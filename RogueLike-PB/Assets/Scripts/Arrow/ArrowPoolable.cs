using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPoolable : ObjectPoolable
{

#region Variables & Properties

private float speed;

private KeyCode key;
private EnumEffectiveArrow effectiveArrow;
private AudioClip clip;

#endregion

#region Methods

private void SetSpeed(float newSpeed)
{
    speed = newSpeed;
}

public void StartArrow(GameObject colliderDeleteArrow ,float newSpeed, KeyCode newKey, AudioClip newClip)
{
    ResetPoints();
    SetKey(newKey);
    SetSpeed(newSpeed);
    SetClip(newClip);
    StartCoroutine(MoveDown(colliderDeleteArrow));
}

private void SetClip(AudioClip newClip)
{
    clip = newClip;
}

private void ResetPoints()
{
    effectiveArrow = EnumEffectiveArrow.Bad;
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

public void SetEffective(EnumEffectiveArrow newEffectivearrow)
{
    effectiveArrow = newEffectivearrow;
}

public EnumEffectiveArrow GetEffectiveArrow()
{
    return effectiveArrow;
}

public void SoundArrow()
{
    SoundManager.Instance.PlaySound(clip);
}

public KeyCode GetEnumArrow()
{
    return key;
}

#endregion

}
