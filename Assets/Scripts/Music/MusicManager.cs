using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] List<AudioSource> dayMusic;
    [SerializeField] List<AudioSource> nightMusic;

    [SerializeField] private float minimumWait;
    [SerializeField] private float maximumWait;
    private LightingManager timeManager;

    [SerializeField] private float morningTime = 12;
    [SerializeField] private float nightTime = 36;
    bool day;
    private Coroutine musicTimer;
    void Update()
    {
        if(GameManager.Instance.gameState == GameStates.beginning && musicTimer == null)
        {
            musicTimer = StartCoroutine(MusicTimer());
            timeManager = GetComponentInParent<LightingManager>();
        }
    }

    private IEnumerator MusicTimer()
    {
        float songLength = 0;
        while(GameManager.Instance.gameState != GameStates.start)
        {

        if(timeManager.GetTimeOfDay() > morningTime && timeManager.GetTimeOfDay() < nightTime)
        {
            day = true;
        }
        else
        {
            day = false;
        }

        int index;
        switch(day)
        {
            case true:
                index = Random.Range(0,dayMusic.Count);
                dayMusic[index].Play();
                songLength = dayMusic[index].clip.length;
                break;
            case false:
                index = Random.Range(0,nightMusic.Count);
                nightMusic[index].Play();
                songLength = nightMusic[index].clip.length;
                break;
        }
        yield return new WaitForSeconds(Random.Range(minimumWait, maximumWait) + songLength);
        }
        musicTimer = null;
        yield return null;
    }
}
