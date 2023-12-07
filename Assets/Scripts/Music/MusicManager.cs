using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] List<AudioSource> dayMusic;
    [SerializeField] List<AudioSource> nightMusic;

    [SerializeField] private float minimumWait;
    [SerializeField] private float maximumWait;
    public bool day;
    void Start()
    {
        StartCoroutine(MusicTimer());
    }

    private IEnumerator MusicTimer()
    {
        float songLength = 0;
        while(GameManager.Instance.gameState == GameStates.beginning || GameManager.Instance.gameState == GameStates.playing)
        {
        yield return new WaitForSeconds(Random.Range(minimumWait, maximumWait) + songLength);

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
        }

        yield return null;
    }
}
