namespace SGSpyWeb.Model
{
    public class RDependency : IAssemblyItem
    {
        public string Name { get; }
        public string Component { get; }
        public string Domain { get; }
        public string ID { get; }

        public RDependency(string componentID, string name)
        {
            Name = name;
            ID = $"{componentID}_{name}";
            if(name.StartsWith("SystemGroup"))
            {
                var parts = name.Split('.');                
                Domain = parts.Length > 1 ? parts[1] : "";
                Component = parts.Length > 2 ? parts[2] : "";
            }
        }
    }

    


}
