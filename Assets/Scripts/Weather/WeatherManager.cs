using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    enum WeatherType { Sunny, Rain, Snow, Fog }
    private WeatherType currentWeather = WeatherType.Sunny;

    // References to your weather effect game objects or components
    public GameObject fogEffect;
    public GameObject rainEffect;
    public GameObject snowEffect;
    // Sunny might not need an effect, but you can add one if you like

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeWeather(-1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeWeather(1);
        }
    }

    void ChangeWeather(int direction)
    {
        currentWeather += direction;

        if (currentWeather < 0)
            currentWeather = WeatherType.Fog;
        else if (currentWeather > WeatherType.Fog)

            currentWeather = WeatherType.Sunny;

        UpdateWeatherEffects();
    }

    void UpdateWeatherEffects()
    {
        // Disable all effects first
        fogEffect.SetActive(false);
        rainEffect.SetActive(false);
        snowEffect.SetActive(false);

        // Enable the effect based on the current weather
        switch (currentWeather)
        {
            case WeatherType.Fog:
                fogEffect.SetActive(true);
                break;
            case WeatherType.Rain:
                rainEffect.SetActive(true);
                break;
            case WeatherType.Snow:
                snowEffect.SetActive(true);
                break;
                // Sunny case does not need to enable any effect
        }
    }
}
