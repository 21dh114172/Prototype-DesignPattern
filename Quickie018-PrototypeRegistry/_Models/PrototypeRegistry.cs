using System.Collections.Generic;

namespace Quickie018
{
    public class PrototypeRegistry
    {
        private Dictionary<string, IPrototype> _prototypes = new Dictionary<string, IPrototype>();

        public void RegisterPrototype(string key, IPrototype prototype)
        {
            _prototypes[key] = prototype;
        }

        public IPrototype GetPrototype(string key)
        {
            if (_prototypes.TryGetValue(key, out IPrototype prototype))
            {
                return (IPrototype)prototype.DeepClone();
            }
            else
            {
                throw new KeyNotFoundException($"No prototype found for key: {key}");
            }
        }

        public List<string> GetAvailablePrototypes()
        {
            return new List<string>(_prototypes.Keys);
        }

        public IPrototype GetRandomPrototype()
        {
            var random = new Random();
            var keys = new List<string>(_prototypes.Keys);
            var randomKey = keys[random.Next(keys.Count)];
            return GetPrototype(randomKey);
        }
        
    }
}

