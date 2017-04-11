
using Newtonsoft.Json;
using System.IO;
using System.Text;

public abstract class SerializationContainers
{
    public BlockManager blockMan { get; set; }
    public IndividualManager indivMan { get; set; }
    public MutationManager mutateMan { get; set; }
    public object contains { get; set; }
    
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

public class Serializer<T>
{
    
    public void Serialize(string pathName, T obj)
    {
        string output = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        File.WriteAllBytes(pathName, Encoding.ASCII.GetBytes(output));
    }

    public T Deserialize(string pathName)
    {
        byte[] input = File.ReadAllBytes(pathName);
        string jsonString = Encoding.ASCII.GetString(input);
        T obj = (T)JsonConvert.DeserializeObject(jsonString);
        return obj;
    }
}

