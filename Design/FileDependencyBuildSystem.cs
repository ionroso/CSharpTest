using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    namespace Design
    {
        public record Module(int Id, string Name);

        public class BuildSystem
        {
            private readonly Dictionary<int, Module> _modules = new();
            private readonly Dictionary<int, HashSet<int>> _dependencies = new();

            public void AddModule(Module module)
            {
                if (module == null)
                    throw new ArgumentNullException(nameof(module));

                if (_modules.ContainsKey(module.Id))
                    throw new InvalidOperationException("Module with same Id already exists.");

                _modules[module.Id] = module;
                _dependencies[module.Id] = new HashSet<int>();
            }

            public void AddDependency(int moduleId, int dependencyId)
            {
                if (!_modules.ContainsKey(moduleId) || !_modules.ContainsKey(dependencyId))
                    throw new InvalidOperationException("One or both modules do not exist.");

                if (moduleId == dependencyId)
                    throw new InvalidOperationException("A module cannot depend on itself.");

                _dependencies[moduleId].Add(dependencyId);
            }

            public List<Module> GetDirectDependencies(int moduleId)
            {
                if (!_modules.ContainsKey(moduleId))
                    throw new InvalidOperationException("Module does not exist.");

                return _dependencies[moduleId]
                    .Select(id => _modules[id])
                    .ToList();
            }

            public List<Module> GetAllDependencies(int moduleId)
            {
                if (!_modules.ContainsKey(moduleId))
                    throw new InvalidOperationException("Module does not exist.");

                var visited = new HashSet<int>();
                DfsCollectDependencies(moduleId, visited);

                visited.Remove(moduleId);

                return visited.Select(id => _modules[id]).ToList();
            }

            private void DfsCollectDependencies(int moduleId, HashSet<int> visited)
            {
                if (visited.Contains(moduleId))
                    return;

                visited.Add(moduleId);

                foreach (var dependencyId in _dependencies[moduleId])
                {
                    DfsCollectDependencies(dependencyId, visited);
                }
            }

            public bool HasCycle()
            {
                var visited = new HashSet<int>();
                var inCurrentPath = new HashSet<int>();

                foreach (var moduleId in _modules.Keys)
                {
                    if (!visited.Contains(moduleId))
                    {
                        if (DfsHasCycle(moduleId, visited, inCurrentPath))
                            return true;
                    }
                }

                return false;
            }

            private bool DfsHasCycle(int moduleId, HashSet<int> visited, HashSet<int> inCurrentPath)
            {
                visited.Add(moduleId);
                inCurrentPath.Add(moduleId);

                foreach (var dependencyId in _dependencies[moduleId])
                {
                    if (!visited.Contains(dependencyId))
                    {
                        if (DfsHasCycle(dependencyId, visited, inCurrentPath))
                            return true;
                    }
                    else if (inCurrentPath.Contains(dependencyId))
                    {
                        return true;
                    }
                }

                inCurrentPath.Remove(moduleId);
                return false;
            }

            public List<Module> GetBuildOrder()
            {
                if (HasCycle())
                    return new List<Module>();

                var visited = new HashSet<int>();
                var result = new List<Module>();

                foreach (var moduleId in _modules.Keys)
                {
                    if (!visited.Contains(moduleId))
                    {
                        DfsTopologicalSort(moduleId, visited, result);
                    }
                }

                return result;
            }

            private void DfsTopologicalSort(int moduleId, HashSet<int> visited, List<Module> result)
            {
                if (visited.Contains(moduleId))
                    return;

                visited.Add(moduleId);

                foreach (var dependencyId in _dependencies[moduleId])
                {
                    DfsTopologicalSort(dependencyId, visited, result);
                }

                result.Add(_modules[moduleId]);
            }
        }

        public class Program8
        {
            public static void Test()
            {
                var buildSystem = new BuildSystem();

                buildSystem.AddModule(new Module(1, "App"));
                buildSystem.AddModule(new Module(2, "Service"));
                buildSystem.AddModule(new Module(3, "Repository"));
                buildSystem.AddModule(new Module(4, "Database"));
                buildSystem.AddModule(new Module(5, "Logging"));

                buildSystem.AddDependency(1, 2); // App depends on Service
                buildSystem.AddDependency(2, 3); // Service depends on Repository
                buildSystem.AddDependency(3, 4); // Repository depends on Database
                buildSystem.AddDependency(2, 5); // Service depends on Logging

                Console.WriteLine("Direct dependencies of Service:");
                foreach (var module in buildSystem.GetDirectDependencies(2))
                {
                    Console.WriteLine(module.Name);
                }

                Console.WriteLine();
                Console.WriteLine("All dependencies of App:");
                foreach (var module in buildSystem.GetAllDependencies(1))
                {
                    Console.WriteLine(module.Name);
                }

                Console.WriteLine();
                Console.WriteLine($"Has cycle: {buildSystem.HasCycle()}");

                Console.WriteLine();
                Console.WriteLine("Build order:");
                foreach (var module in buildSystem.GetBuildOrder())
                {
                    Console.WriteLine(module.Name);
                }
            }
        }
    }
}
