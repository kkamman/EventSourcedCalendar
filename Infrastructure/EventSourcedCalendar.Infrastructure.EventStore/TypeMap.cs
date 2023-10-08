namespace EventSourcedCalendar.Infrastructure.EventStore;

internal abstract class TypeMap
{
    private readonly Dictionary<Type, string> _typeToNameMap = new();
    private readonly Dictionary<string, Type> _nameToTypeMap = new();

    protected TypeMap(Dictionary<Type, string> typeMap)
    {
        foreach (var entry in typeMap)
        {
            _typeToNameMap[entry.Key] = entry.Value;
            _nameToTypeMap[entry.Value] = entry.Key;
        }
    }

    public string GetNameForType<T>() => _typeToNameMap[typeof(T)];
    public string GetNameForType(object obj) => _typeToNameMap[obj.GetType()];
    public Type GetTypeForName(string name) => _nameToTypeMap[name];
}
