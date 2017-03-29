﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class GenerationFunctions
{

    static Stack<Individual> toBeDone;
    static Generation currentlyWorking;
    /// <summary>
    /// We want to simulate each individual in this generation
    /// </summary>
    /// <param name="g"></param>
    public static void createNextGen(Generation g)
    {
        currentlyWorking = g;
        EventHandler.onSimulationEnd += doNextSim;
        EventHandler.onGenerationDone += finishGeneration;
        ///Creating a stack of individuals we still need to score;
        toBeDone = new Stack<Individual>();
        foreach (Individual indi in currentlyWorking.currentGen)
        {
            toBeDone.Push(indi);
        }

        IndividualFunctions.Simulate(toBeDone.Pop());
    }

    /// <summary>
    /// As long as we have individuals we need to simulate, we need to simulate them
    /// otherwise we signal the event handler that all the simulations have finished
    /// </summary>
    private static void doNextSim()
    {
        if (toBeDone.Count < 1)
        {
            EventHandler.finishedGenerationSimulation();
        }
        else
        {
            IndividualFunctions.Simulate(toBeDone.Pop());
        }
    }
    
    /// <summary>
    /// We are done simulating so we need to score, and move on to culling/replacing
    /// </summary>
    private static void finishGeneration()
    {
        foreach (Individual i in currentlyWorking.currentGen)
        {
            if (currentlyWorking.currentBest == null || currentlyWorking.currentBest.score < i.score)
            {
                currentlyWorking.currentBest = i;
                currentlyWorking.bestMap.Add(i);
            }
        }
        Debug.Log("All simulated");
        cullGeneration();
        Debug.Log("Signalling Event");
        ///We are now a new generation
        currentlyWorking.genNum++;
        EventHandler.finishedGeneratingNewGen();
    }


    /// <summary>
    /// Replaces all the individuals in the generation with the current best
    /// </summary>
    /// <param name="g"></param>
    private static void cullGeneration()
    {
        Debug.Log("culling...");
        currentlyWorking.currentGen = new List<Individual>();
        for (int i = 0; i < MutationManager.Instance.generationSize; i++)
        {
            currentlyWorking.currentGen.Add(new Individual(currentlyWorking.currentBest));
        }

        mutateGeneration();
    }


    /// <summary>
    /// Now we need to mutate all the individuals a bit
    /// </summary>
    private static void mutateGeneration()
    {
        Debug.Log("Mutating...");
        foreach (Individual indi in currentlyWorking.currentGen)
        {
            mutateIndividual(indi);
        }
    }


    private static void mutateIndividual(Individual indi)
    {
        alterPieces(indi);
        addPieces(indi);
    }

    private static void alterPieces(Individual indiv)
    {
        Debug.Log("Altering Blocks...");
        ///For each pass defined by degree of mutation
        for (int i = 0; i < MutationManager.Instance.degreeToMutate; i++)
        {
            List<double> toDelete = new List<double>();
            Dictionary<double, Block> replaceAtWith = new Dictionary<double, Block>();

            ///for each block
            foreach (double b in indiv.hull.container.contents.Keys)
            {
                double rand = Randomizer.random.NextDouble();
                ///WE DO NOT ALTER THE PAYLOAD NOR IGNORED BLOCKS
                if (indiv.payload.container.contents.Keys.Contains(b))
                {
                    continue;
                }
                if(rand < MutationManager.Instance.doNothingProbability)
                {
                    continue;
                }
                rand = Randomizer.random.NextDouble();
                ///If we should alter it
                if (Randomizer.random.NextDouble() < MutationManager.Instance.alterOrDeleteProbability)
                {
                    ///RANDOMIZE BLOCK
                    Block newBlock = BlockFunctions.generateRandomBlock();
                    replaceAtWith.Add(b, newBlock);
                }
                ///or delete it
                else
                {
                    toDelete.Add(b);
                }
            }
            foreach (double b in toDelete)
            {
                indiv.hull.container.openSpaces.Add(b);
                indiv.hull.container.contents.Remove(b);
            }
            foreach(double b in replaceAtWith.Keys)
            {
                indiv.payload.container.contents[b] = replaceAtWith[b];
            }
        }

    }

    private static void addPieces(Individual indiv)
    {
        Debug.Log("Adding Blocks...");
        List<double> insert = new List<double>();
        foreach(double b in indiv.hull.container.openSpaces)
        {
            ///We have already added enough blocks
            if (indiv.hull.container.contents.Count >= IndividualManager.Instance.maxIndividualSize)
            {
                continue;
            }
            if(Randomizer.random.NextDouble() < MutationManager.Instance.additionProbability)
            {
                insert.Add(b);
            }
        }
        ///Doing it this way because we cannot alter a structure while iterating through it
        foreach(double inserting in insert)
        {
            BlockFunctions.insertBlockAtRandom(indiv.hull.container);
        }
        Debug.Log("Individual in generation "+ currentlyWorking.genNum+" created");
    }




}
