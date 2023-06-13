using System;
using System.Collections.Generic;

namespace Managers
{
    public class ManagerLocator<T> : IManagerLocator<T>
    {
        protected Dictionary<Type, T> itemsMap { get; }

        public ManagerLocator()
        {
            itemsMap = new Dictionary<Type, T>();
        }

        public TP Register<TP>(TP newService) where TP : T
        {
            var type = newService.GetType();
            if (itemsMap.ContainsKey(type))
            {
                throw new Exception($"Cannot add item of type {type}. This type already exists in ServiceLocator");
            }

            itemsMap[type] = newService;
            return newService;
        }

        public void Unregister<TP>(TP service) where TP : T
        {
            var type = service.GetType();
            if (itemsMap.ContainsKey(type))
            {
                itemsMap.Remove(type);
            }
        }

        public TP Get<TP>() where TP : T
        {
            var type = typeof(TP);
            if (!itemsMap.ContainsKey(type))
            {
                throw new Exception($"There is no object of type {type} in ServiceLocaror");
            }

            return (TP) itemsMap[type];
        }
    }
}
