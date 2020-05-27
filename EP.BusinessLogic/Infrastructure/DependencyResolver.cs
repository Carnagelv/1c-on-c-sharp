using Ninject;
using System;

namespace OneC.BusinessLogic.Infrastructure
{
    public static class DependencyResolver
    {
        private static IKernel _kernel;

        public static void Initialize(IKernel kernel)
        {
            _kernel = kernel;
        }

        public static T Get<T>()
        {
            return _kernel.Get<T>();
        }

        public static object Get(Type type)
        {
            return _kernel.Get(type);
        }
    }
}
