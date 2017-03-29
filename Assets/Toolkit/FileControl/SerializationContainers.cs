
using Newtonsoft.Json;
using System.IO;

public abstract class SerializationContainers
{
    BlockManager blockMan { get; set; }
    IndividualManager indivMan { get; set; }
    MutationManager mutateMan { get; set; }
    object contains { get; set; }
    
    public SerializationContainers(BlockManager blk, 
                                   IndividualManager indiv, 
                                   MutationManager mut,
                                   object input)
    {
        blockMan = blk;
        indivMan = indiv;
        mutateMan = mut;
        contains = input;
    }
}

public static class Serializer<T>
{
    static void Serialize(string pathName, T obj)
    {
        StreamWriter streamwriter = new StreamWriter(pathName);
        JsonWriter writer = new JsonTextWriter(streamwriter);
        JsonSerializer serializer = new JsonSerializer();
        serializer.Serialize(writer, obj);
    }

    static T Deserialize(string pathName)
    {
        StreamReader streamreader = new StreamReader(pathName);
        JsonReader reader = new JsonTextReader(streamreader);
        JsonSerializer serializer = new JsonSerializer();
        T output = serializer.Deserialize<T>(reader);
        return output;
    }
}

