using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emu_pi2.Data.Services.Factory
{
    public class ServiceFactory
    {
        private static ServiceFactory _instance;
        private List<ServiceMapping> _mappings;

        private ServiceFactory()
        {
            // Define our mappings!
            _mappings = new List<ServiceMapping>()
            {
                new ServiceMapping() { InterfaceType = typeof(IConsoleRepository), ServiceType = typeof(MockConsoleRepository) },
                new ServiceMapping() { InterfaceType = typeof(IGameRepository), ServiceType = typeof(MockGameRepository) },
            };

        }

        public static ServiceFactory Current
        {
            get
            {
                if (_instance == null)
                    _instance = new ServiceFactory();
                return _instance;
            }
        }

        /// <summary>
        /// Creates a new instance of the requested service for some lazy calling classes.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Create<T>()
        {
            var type = typeof(T);
            var mappedtype = _mappings.First(x => x.InterfaceType.Equals(type));
            return (T)Activator.CreateInstance(mappedtype.ServiceType);
        }
    }
}
