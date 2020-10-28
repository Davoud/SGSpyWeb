namespace SGSpyWeb.Model
{
    public class Dependency : IAssemblyItem
    {
        public string Name { get; }
        public string ComponentName { get; }
        public string Domain { get; }
        public string ID { get; }

        public Dependency(string componentID, string name)
        {
            Name = name;
            ID = $"{componentID}_{name}";
            if(name.StartsWith("SystemGroup"))
            {
                var parts = name.Split('.');                
                Domain = parts.Length > 1 ? parts[1] : "";
                ComponentName = parts.Length > 2 ? parts[2] : "";
            }
        }
    }

    


}
