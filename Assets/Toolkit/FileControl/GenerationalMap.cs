using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class GenerationalMap : SerializationContainers
{
    public GenerationalMap(BlockManager man, IndividualManager indiv, MutationManager mut, Individual container) : base(man, indiv, mut, container) { }
}
