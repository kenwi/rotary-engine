namespace Engine.Managers
{
    internal class ManagerItem<T> where T : class
    {
        public ManagerItem(string name, T resource)
            : this(name, string.Empty, resource)
        {
        }

        public ManagerItem(string name, object parent, T resource)
        {
            Name = name;
            Parent = parent;
            Resource = resource;
        }

        /// <summary>
        ///     Item name
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     Parent can be used to group items into groups.
        ///     In this way you will be able to retrieve or delete elements only from one group.
        /// </summary>
        public object Parent { get; }

        public T Resource { get; }
    }
}