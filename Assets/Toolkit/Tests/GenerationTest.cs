using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GenerationTest : MonoBehaviour
{

    public GameObject marker;
    public Material mat;
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

    private void lookAtBest()
    {
        for (int i = -100; i < 100; i += 2)
        {
            GameObject mark = GameObject.Instantiate(marker);
            mark.transform.position = new Vector3(0, i, 20);
            if (i % 10 == 0)
            {
                mark.GetComponent<MeshRenderer>().material = mat;
            }

        }

        Time.timeScale = 1;
        Debug.Log(gen.currentBest.score);
        Individual copy = new Individual(gen.currentBest);
        follow = IndividualFunctions.Simulate(copy);

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

