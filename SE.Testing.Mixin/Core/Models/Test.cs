using System;

namespace IngameScript
{
    public class Test
    {
        public string Name { get; }
        public Action Body { get; }

        private Test(string name, Action body)
        {
            Name = name;
            Body = body;
        }

        public static Test Make(Action testMethod)
        {
            if (testMethod == null)
                throw new ArgumentNullException(nameof(testMethod));
            return new Test(testMethod.Method.Name, testMethod);
        }

        public static Test Make<T1>(Action<T1> testMethod, T1 arg1)
        {
            if (testMethod == null)
                throw new ArgumentNullException(nameof(testMethod));
            var name = testMethod.Method.Name;
            Action body = () => testMethod(arg1);
            return new Test(name, body);
        }

        public static Test Make<T1, T2>(Action<T1, T2> testMethod, T1 arg1, T2 arg2)
        {
            if (testMethod == null)
                throw new ArgumentNullException(nameof(testMethod));
            var name = testMethod.Method.Name;
            Action body = () => testMethod(arg1, arg2);
            return new Test(name, body);
        }

        public static Test Make<T1, T2, T3>(Action<T1, T2, T3> testMethod, T1 arg1, T2 arg2, T3 arg3)
        {
            if (testMethod == null)
                throw new ArgumentNullException(nameof(testMethod));
            var name = testMethod.Method.Name;
            Action body = () => testMethod(arg1, arg2, arg3);
            return new Test(name, body);
        }

        public static Test Make<T1, T2, T3, T4>(Action<T1, T2, T3, T4> testMethod, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (testMethod == null)
                throw new ArgumentNullException(nameof(testMethod));
            var name = testMethod.Method.Name;
            Action body = () => testMethod(arg1, arg2, arg3, arg4);
            return new Test(name, body);
        }

        public static Test Make<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> testMethod, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (testMethod == null)
                throw new ArgumentNullException(nameof(testMethod));
            var name = testMethod.Method.Name;
            Action body = () => testMethod(arg1, arg2, arg3, arg4, arg5);
            return new Test(name, body);
        }
    }
}