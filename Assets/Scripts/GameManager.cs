using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Instance in Game Manager is NULL");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    private GameObject[] _lights;
    [SerializeField]
    private GameObject _outsideLight;
    [SerializeField]
    private GameObject _canvas;
    [SerializeField]
    private GameObject[] _pointLights;
    [SerializeField]
    private GameObject _player;

    public static bool PowerEnabled = false;


    /// <summary>
    /// Checks if the Power is ON
    /// If the Power is ON
    /// Enable all area lights game objects in the array
    /// Else
    /// Display UI Canvas
    /// </summary>
    public void LightsOn()
    {
        if (PowerEnabled)
        {
            foreach (var light in _lights)
            {
                light.SetActive(true);
            }

            UIManager.Instance.CanvasesToDisplayWithLights();
        }
        else
        {
            UIManager.Instance.DisplayDoorLeftCanvas();
        }
    }

    public void LightsOff()
    {
        if (PowerEnabled)
        {
            foreach (var light in _lights)
            {
                light.SetActive(false);
            }
        }
    }

    public void PowerOn()
    {
        PowerEnabled = true;
    }

    /// <summary>
    /// Checks if the Power is ON
    /// If the Power is ON
    /// Enable one light in the hallway outside the room
    /// Else
    /// Display UI Canvas
    /// </summary>
    public void OutsideLightOn()
    {
        if (PowerEnabled)
        {
            _outsideLight.SetActive(true);
        }
        else
        {
            UIManager.Instance.DisplayDoorRightCanvas();
        }
    }

    public void OutsideLightOff()
    {
        if (PowerEnabled)
            _outsideLight.SetActive(false);
    }
    /// <summary>
    /// Checks if the Power is ON
    /// If the Power is ON
    /// Enable all point lights game objects in the array
    /// Else
    /// Display UI Canvas
    /// </summary>
    public void PointLightsOn()
    {
        if (PowerEnabled)
        {
            foreach (var light in _pointLights)
            {
                light.SetActive(true);
            }
        }
        else
        {
            UIManager.Instance.DisplayDoorLeftCanvas();
        }
       
    }

    public void PoitLightsOff()
    {
        foreach(var light in _pointLights)
        {
            light.SetActive(false);
        }
    }

    //Restart the scene
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PowerEnabled = false;
    }

    //Quit the application
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LockPlayer()
    {
        ActionBasedContinuousMoveProvider _continuousMove = _player.GetComponent<ActionBasedContinuousMoveProvider>();
        ActionBasedSnapTurnProvider _snapTurn = _player.GetComponent<ActionBasedSnapTurnProvider>();
        TeleportationProvider _teleportProvider = _player.GetComponent<TeleportationProvider>();

        _continuousMove.enabled = false;
        _snapTurn.enabled = false;
        _teleportProvider.enabled = false;
    }
}
