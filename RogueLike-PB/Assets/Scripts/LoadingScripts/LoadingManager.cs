using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : Singleton<LoadingManager>
{

#region Variables & Properties

[SerializeField] private bool startWithBlackScreen;
[SerializeField] private bool startWithSlideBar;

[SerializeField] private Slider slider;
[SerializeField] private Image image;

[SerializeField] private float loadingTime = 5f;

[SerializeField] private float dissolveSpeed = 0.5f;
[SerializeField] private float appearTime = 1f;

private Material material;
private float dissolveAmount;
private float appearAmount;

[HideInInspector] public bool solved = false;
    
#endregion

#region Methods

protected override void Awake()
{
    base.Awake();
    
    image.gameObject.SetActive(startWithBlackScreen);
    slider.gameObject.SetActive(startWithSlideBar);
    
}

public void SetActivation(bool activation)
{
    image.gameObject.SetActive(activation);
    slider.gameObject.SetActive(activation);
}

public void NewRoom()
{
    material = image.material;
    StartCoroutine(LoadScreen());
}

public IEnumerator LoadScreen()
{
    SetMaxValue(loadingTime);
    yield return LoadSlider();
    slider.gameObject.SetActive(false);
    RoomManager.Instance.PrepareNewRoom(DungeonGenerator.Instance.GetCurrentDungeon().GetCurrentFloor().GetCurrentRoom());
    yield return Dissolve();
    image.gameObject.SetActive(false);
}
    
private void SetMaxValue(float maxValue)
{
    slider.maxValue = maxValue;
    slider.value = 0;
}

private IEnumerator LoadSlider()
{
    float elapsedTime = 0;

    while (elapsedTime < loadingTime)
    {
        elapsedTime += Time.deltaTime;
        SetValue(elapsedTime);
        yield return null;
    }
    
}

private void SetValue(float value)
{
    slider.value = value;
}

private IEnumerator Dissolve()
{
    float currentAlpha=1;
    float elapsedTime = 0;

    while (currentAlpha > 0f)
    {
        currentAlpha -= dissolveSpeed * Time.deltaTime;
        currentAlpha = Mathf.Clamp01(currentAlpha);

        Color newColor = image.color;
        newColor.a = currentAlpha;
        image.color = newColor;

        yield return null;
    }
}
    
private IEnumerator DeDissolve()
{
    float appearAmount;
    float elapsedTime = 0;

    while (elapsedTime < appearTime)
    {
        elapsedTime += Time.deltaTime;
        appearAmount = elapsedTime / appearTime;
        material.SetFloat("_DissolveAmount", 1 - appearAmount);
        yield return null;
    }

    solved = true;
}

#endregion

}
