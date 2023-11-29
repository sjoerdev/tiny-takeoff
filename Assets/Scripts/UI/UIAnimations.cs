using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIAnimations : MonoBehaviour
{
    [Header("Start UI")]
    [SerializeField] private GameObject startButton;
    [SerializeField] private TextMeshProUGUI startButtonText;
    [SerializeField] private float startButtonFadeTime = 1f;


    [Header("Top UI")]
    [SerializeField] private float topUIWait;
    [SerializeField] private GameObject topUI;
    [SerializeField] private float topUIAnimationTime = 1f;
    [SerializeField] private float topUIResetWaitTime = 1f;
    [SerializeField] private float topUImovedY = 100f;
    private float topUINormalY;
    
    [Header("Bottom UI")]
    [SerializeField] private float bottomUIWait;
    [SerializeField] private GameObject bottomUI;
    [SerializeField] private float bottomUIAnimationTime = 1f;

    [SerializeField] private float bottomUIMovedY = 100f;
    private float bottomUINormalY;

    void Start()
    {
        topUINormalY = topUI.transform.localPosition.y;
        bottomUINormalY= bottomUI.transform.localPosition.y;
    }

    private GameStates previousState;
    void Update()
    {
        switch(GameManager.Instance.gameState)
        {
            case GameStates.beginning:
                if(previousState != GameStates.beginning)
                {
                    StartCoroutine(startGameCoroutine());
                }
                break;
            case GameStates.start:
                if(previousState != GameStates.start)
                {
                    StartCoroutine(StartMenu());
                }
                break;
        }
        
        
        
        previousState = GameManager.Instance.gameState;
    }

    private IEnumerator startGameCoroutine()
    {
        StartCoroutine(FadeStartOut());
        yield return new WaitForSeconds(topUIWait);
        StartCoroutine(ResetTopUIAnimation());
        yield return new WaitForSeconds(bottomUIWait);
        StartCoroutine(ShowBottomUI());
        yield return null;
    }

    private IEnumerator StartMenu()
    {
        StartCoroutine(FadeStartIn());
        StartCoroutine(HideBottomUI());
        yield return null;
    }

    private IEnumerator FadeStartOut()
    {
        float passedTime = 0f;
        while(passedTime < startButtonFadeTime)
        {
            passedTime += Time.deltaTime;
            startButtonText.alpha = Mathf.Lerp(1f,0f,passedTime/startButtonFadeTime);
            yield return new WaitForEndOfFrame();
        }
        startButton.SetActive(false);
        yield return null;
    }
    private IEnumerator FadeStartIn()
    {
        startButton.SetActive(true);
        float passedTime = 0f;
        while(passedTime < startButtonFadeTime)
        {
            passedTime += Time.deltaTime;
            startButtonText.alpha = Mathf.Lerp(0f,1f,passedTime/startButtonFadeTime);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    private IEnumerator ResetTopUIAnimation()
    {
        float passedTime = 0f;

        passedTime = 0;
        Vector3 position = new Vector3();
        while(passedTime < topUIAnimationTime)
        {
            passedTime += Time.deltaTime;
            position = topUI.transform.localPosition;
            position.y = Mathf.Lerp(topUINormalY,topUImovedY,passedTime/topUIAnimationTime);
            topUI.transform.localPosition = position;
            yield return new WaitForEndOfFrame();
        }
        position.y = topUImovedY;
        topUI.transform.localPosition = position;

        yield return new WaitForSeconds(topUIResetWaitTime);

        passedTime = 0f;
        while(passedTime < topUIAnimationTime)
        {
            passedTime += Time.deltaTime;
            position = topUI.transform.localPosition;
            position.y = Mathf.Lerp(topUImovedY,topUINormalY,passedTime/topUIAnimationTime);
            topUI.transform.localPosition = position;
            yield return new WaitForEndOfFrame();
        }
        position.y = topUINormalY;
        topUI.transform.localPosition = position;


        yield return null;
    }

    private IEnumerator ShowBottomUI()
    {
        float passedTime = 0f;
        Vector3 position = new Vector3();
        while(passedTime < bottomUIAnimationTime)
        {
            passedTime += Time.deltaTime;
            position = bottomUI.transform.localPosition;
            position.y = Mathf.Lerp(bottomUINormalY,bottomUIMovedY,passedTime/bottomUIAnimationTime);
            bottomUI.transform.localPosition = position;
            yield return new WaitForEndOfFrame();
        }
        position.y = bottomUIMovedY;
        bottomUI.transform.localPosition = position;

        yield return null;
    }
    private IEnumerator HideBottomUI()
    {
        float passedTime = 0f;
        Vector3 position = new Vector3();
        while(passedTime < bottomUIAnimationTime)
        {
            passedTime += Time.deltaTime;
            position = bottomUI.transform.localPosition;
            position.y = Mathf.Lerp(bottomUIMovedY,bottomUINormalY,passedTime/bottomUIAnimationTime);
            bottomUI.transform.localPosition = position;
            yield return new WaitForEndOfFrame();
        }
        position.y = bottomUINormalY;
        bottomUI.transform.localPosition = position;

        yield return null;
    }
}
