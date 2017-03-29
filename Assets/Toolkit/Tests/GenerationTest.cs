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

    void Start()
    {
        EventHandler.onNewGenCompletion += createNextGen;
        count = 0;
        Randomizer.newSeed();
        gen = new Generation();
        GenerationFunctions.createNextGen(gen);

    }

    public void createNextGen()
    {
        
        Debug.Log("In the delegate");
        
        if (count <= generations)
        {
            GenerationFunctions.createNextGen(gen);
            count++;
        }
        else
        {
        
            lookAtBest();
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
    }



}

