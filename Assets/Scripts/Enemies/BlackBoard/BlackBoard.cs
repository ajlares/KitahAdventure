using System.Collections.Generic;

public class BlackBoard
{
    private Dictionary<string, object> data = new();
    public void Set<T>(string key, T value)
    {
        //data.TryAdd(key,value);
        data[key] = value;
    }
    public T Get<T>(string key)
    {
        if (data.TryGetValue(key, out var value))
        {
            return (T)value;
        }
        else
        {
            return default;
        }
    }
}