using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GenerationTest : MonoBehaviour
{

    Generation gen;
    GameObject follow;


    bool finished = false;

    void Start()
    {
        Randomizer.newSeed();
        gen = new Generation();
        GenerationFunctions.init(gen);
        GenerationFunctions.createNextGen();
        Camera.main.transform.position = new Vector3(25, 25, 25);
        Camera.main.transform.LookAt(Vector3.zero);

    }

    public void finish(string path)
    {
        Time.timeScale = 1;
        Serializer<GenerationalMap> serializer = new Serializer<GenerationalMap>();
        GenerationalMap map = serializer.Deserialize(path);
        BlockManager.set(map.blockMan);
        MutationManager.set(map.mutateMan);
        IndividualManager.set(map.indivMan);

        List<Individual> gens = map.contains as List<Individual>;
        Debug.Log("Cost : "+gens[gens.Count - 1].cost);
        Debug.Log("Fuel (m^3) : " + gens[gens.Count - 1].fuelVolume);
        Debug.Log("Wegiht : "+gens[gens.Count - 1].weight);
        Debug.Log("Score : " + gens[gens.Count - 1].score);

        follow = IndividualFunctions.Instantiate(gens[gens.Count - 1]);

    }

    private void Update()
    {
        if (follow != null)
        {
            Camera.main.transform.position = follow.transform.position + new Vector3(0, 0, -20);
            Camera.main.transform.LookAt(follow.transform);
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (GenerationFunctions.continueSimulation)
            {
                Debug.Log("Simulation ending. Please wait for the current generation to finish simulating in order to receive the results");
                
            }
            else
            {
                Debug.Log("Resuming Simulation"); 
            }
            GenerationFunctions.continueSimulation = !GenerationFunctions.continueSimulation;
        }
    }



}

