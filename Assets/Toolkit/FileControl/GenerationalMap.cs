using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GenerationalMap : SerializationContainers
{
    public GenerationalMap(BlockManager man, IndividualManager indiv, MutationManager mut, List<Individual> container) : base(man, indiv, mut, container) { }
}
