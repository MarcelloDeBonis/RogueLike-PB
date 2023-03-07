using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Move2DSprite : MonoBehaviour
{

#region Variables & Properties

[SerializeField] private Vector3 startScale;
[SerializeField] private Vector3 endScale;
[SerializeField] public Image moveSprite;
[SerializeField] public Image iconSprite;

[SerializeField] public float secondsIconActived;

    //TODO ATTENCTION!!! THIS COULD BE WITH AN ARTIST, NOT BY TWEEN
[SerializeField] public float speedMoving;

#endregion

#region Methods

public void SetScale(float t)
{
    moveSprite.transform.localScale = Vector3.Lerp(startScale, endScale, t);
}

public void HitAnimation(Sprite icon, EnumEffectType effectType)
{
    if (effectType != EnumEffectType.Normal)
    {
        iconSprite.sprite = icon;
        iconSprite.gameObject.SetActive(true);
    }
    
    //TODO WITH ARTIST
    StartCoroutine(DoAnimation());
}

private IEnumerator DoAnimation()
{
    moveSprite.gameObject.SetActive(false);
    iconSprite.gameObject.SetActive(true);
    yield return new WaitForSeconds(secondsIconActived);
    this.gameObject.SetActive(false);
}

#endregion

}
