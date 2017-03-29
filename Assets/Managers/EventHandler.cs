using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EventHandler : Singleton<EventHandler>
{
    #region event handling for when a signle simulation ends
    public delegate void SimulationEndEventHandler();

    public static event SimulationEndEventHandler onSimulationEnd;

    public static void SelfDestruct(GameObject killing)
    {
        UnityEngine.Object.Destroy(killing);
        onSimulationEnd();

    }
    #endregion

    #region event handling for when an entire generations' simulation ends
    public delegate void GenerationEventHandler();

    public static event GenerationEventHandler onGenerationDone;

    public static void finishedGenerationSimulation()
    {
        onGenerationDone();
    }
    #endregion

    #region event handling for when a new generation has been created from an old one
    public delegate void NewGenEventHandler();

    public static event NewGenEventHandler onNewGenCompletion;

    public static void finishedGeneratingNewGen()
    {
        onNewGenCompletion();
    }
    #endregion
}
