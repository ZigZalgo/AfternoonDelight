using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GenerationTest : MonoBehaviour
{

    public GameObject marker;
    public Material mat;
    public int generations;
    private int count;
    Generation gen;
    GameObject follow;
    bool finish = false;

    void Start()
    {
        Mathf.Clamp(generations, 1, 100);
        CustomEventHandler.onGenerationGenerated += createNextGen;
        count = 0;
        Randomizer.newSeed();
        gen = new Generation();
        GenerationFunctions.createNextGen(gen);
        Camera.main.transform.position = new Vector3(25, 25, 25);
        Camera.main.transform.LookAt(Vector3.zero);

    }

    public void createNextGen()
    {
        if (!GenerationFunctions.locked)
        {
            GenerationFunctions.createNextGen(gen);
        }
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
            if (finish)
            {
                Debug.Log("Resuming Simulation"); 
            }
            else
            {
                Debug.Log("Simulation ending. Please wait for the current generation to finish simulating in order to receive the results");

            }
            finish = !finish;
        }
    }



}

