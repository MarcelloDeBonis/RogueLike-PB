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

[HideInInspector]
public bool doingAnimation = false;

#endregion

#region Methods

public void SetScale(float t)
{
    moveSprite.transform.localScale = Vector3.Lerp(startScale, endScale, t);
}

public void HitAnimation(Sprite icon)
{

    iconSprite.sprite = icon;
    iconSprite.gameObject.SetActive(true);
    doingAnimation = true;
    
    //TODO WITH ARTIST
    StartCoroutine(DoAnimation());
}

private IEnumerator DoAnimation()
{
    doingAnimation = true;
    yield return new WaitForSeconds(3);
    doingAnimation = false;
    iconSprite.gameObject.SetActive(false);
}

#endregion

}
