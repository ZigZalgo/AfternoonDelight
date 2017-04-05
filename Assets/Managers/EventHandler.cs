using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CustomEventHandler : Singleton<CustomEventHandler>
{
    #region event handling for when a signle simulation ends
    public delegate void SimulationEndEventHandler();

    public static event SimulationEndEventHandler onIndividualSimulationDone;

    public static void SelfDestruct(GameObject killing)
    {
        UnityEngine.Object.Destroy(killing);
        onIndividualSimulationDone();

    }
    #endregion

    #region event handling for when an entire generations' simulation ends
    public delegate void GenerationEventHandler();

    public static event GenerationEventHandler onGenerationSimulationDone;

    public static void finishedGenerationSimulation()
    {
        onGenerationSimulationDone();
    }
    #endregion

    #region event handling for when a new generation has been created from an old one
    public delegate void NewGenEventHandler();

    public static event NewGenEventHandler onGenerationGenerated;

    public static void finishedGeneratingGeneration()
    {
        onGenerationGenerated();
    }
    #endregion
}
