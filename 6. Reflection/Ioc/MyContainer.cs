using Ioc.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Ioc
{
    /// <summary>
    /// The class represents IoC(?)container
    /// </summary>
    public class MyContainer
    {
        /// <summary>
        /// Types storage
        /// </summary>
        private readonly Dictionary<Type, Type> _dictionaryTypes;

        public MyContainer()
        {
            _dictionaryTypes = new Dictionary<Type, Type>();
        }

        /// <summary>
        /// Add a Type to Container.
        /// </summary>
        /// <param name="type">Type to add.</param>
        public void AddType(Type type)
        {
            AddType(type, type);
        }

        /// <summary>
        /// Add Type with Contract into Container.
        /// </summary>
        /// <param name="type">Type to add.</param>
        /// <param name="contract">Contract that the type implements.</param>
        public void AddType(Type type, Type contract)
        {
            _dictionaryTypes.TryAdd(contract, type);
        }

        /// <summary>
        /// Add all dependences, marked attributes, of the executing assembly into Container.
        /// </summary>
        /// <param name="executingAssembly">Executing assembly.</param>
        public void AddAssembly(Assembly executingAssembly)
        {
            AddToDictionary(executingAssembly);
        }

        /// <summary>
        /// Returns Instance of a Type, that Container contains. If Container doesn't contains the Type, returns null.
        /// </summary>
        /// <param name="instanceType">Type to create a instance.</param>
        /// <returns>Instance of the type.</returns>
        public object CreateInstance(Type instanceType)
        {
            var isTypeExists = _dictionaryTypes.TryGetValue(instanceType, out Type typeToCreate);

            if (!isTypeExists)
            {
                return null;
            }

            var constructor = typeToCreate.GetConstructors().FirstOrDefault();

            // Create dependences for constructor
            var args = constructor.GetParameters()
                .Select(p => {
                var type = _dictionaryTypes[p.ParameterType];
                return Activator.CreateInstance(type);
                }).ToArray();

            var instance = Activator.CreateInstance(typeToCreate, args);

            // Create and set properties of the instance
            foreach (var property in instanceType.GetProperties())
            {
                var instanceProperty = CreateInstance(property.PropertyType);
                property.SetValue(instance, instanceProperty);
            }

            return instance;
        }

        /// <summary>
        /// Returns Instance of a Type, that Container contains. If Container doesn't contains the Type, returns null.
        /// </summary>
        /// <typeparam name="T">Type to create a instance.</typeparam>
        /// <returns>Instance of the type.</returns>
        public T CreateInstance<T>() where T : class
        {
            return CreateInstance(typeof(T)) as T;
        }

        /// <summary>
        /// Add types marked attributes into dictionary.
        /// </summary>
        /// <param name="assembly">Assembly to find attributes.</param>
        private void AddToDictionary(Assembly assembly)
        {
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                // If attribute has been found
                var importConstructorAttribute = type.GetCustomAttribute<ImportConstructorAttribute>();
                if (importConstructorAttribute != null)
                {
                    _dictionaryTypes.TryAdd(type, type);
                }

                var exportAttribute = type.GetCustomAttribute<ExportAttribute>();
                if (type.GetCustomAttribute<ExportAttribute>() != null)
                {
                    if (_dictionaryTypes.ContainsKey(exportAttribute.Type))
                    {
                        _dictionaryTypes[exportAttribute.Type] = type;
                    }
                    _dictionaryTypes.TryAdd(exportAttribute.Type?? type, type);
                }

                // Finding Import attribute in properties
                foreach (var property in type.GetProperties())
                {
                    if (property.GetCustomAttribute<ImportAttribute>() != null)
                    {
                        _dictionaryTypes.TryAdd(type, type);
                        _dictionaryTypes.TryAdd(property.PropertyType, property.PropertyType);
                    }
                }
            }        
        }
    }
}
