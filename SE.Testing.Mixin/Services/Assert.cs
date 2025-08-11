using System;
using System.Collections.Generic;
using System.Linq;

namespace IngameScript
{
    public static class Assert
    {
        public static void IsTrue(bool condition, string message = "")
        {
            if (!condition)
                Fail($"Assert.IsTrue failed. {message}");
        }

        public static void IsFalse(bool condition, string message = "")
        {
            if (condition)
                Fail($"Assert.IsFalse failed. {message}");
        }

        public static void AreEqual<T>(T expected, T actual, string message = "")
        {
            if (!EqualityComparer<T>.Default.Equals(expected, actual))
                Fail($"Assert.AreEqual failed. Expected: <{expected}>, Actual: <{actual}>. {message}");
        }

        public static void AreNotEqual<T>(T notExpected, T actual, string message = "")
        {
            if (EqualityComparer<T>.Default.Equals(notExpected, actual))
                Fail($"Assert.AreNotEqual failed. Both were <{actual}>. {message}");
        }

        public static void AreEqual<T>(T[] expected, T[] actual, string message = "")
        {
            if (!expected.SequenceEqual(actual))
                Fail($"Assert.AreEqual failed. Expected: <{expected}>, Actual: <{actual}>. {message}");
        }

        public static void AreNotEqual<T>(T[] notExpected, T[] actual, string message = "")
        {
            if (notExpected.SequenceEqual(actual))
                Fail($"Assert.AreNotEqual failed. Both were <{actual}>. {message}");
        }

        public static void IsNull(object obj, string message = "")
        {
            if (obj != null)
                Fail($"Assert.IsNull failed. Expected null but was <{obj}>. {message}");
        }

        public static void IsNotNull(object obj, string message = "")
        {
            if (obj == null)
                Fail($"Assert.IsNotNull failed. {message}");
        }

        public static T Throws<T>(Action action) where T : Exception
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            try
            {
                action();
            }
            catch (T ex)
            {
                return ex;
            }
            catch (Exception ex)
            {
                Fail($"Expected exception of type {typeof(T).Name} but an exception of type {ex.GetType().Name} was thrown.");
            }

            Fail($"Expected exception of type {typeof(T).Name} but no exception was thrown.");
            return null;
        }

        public static void DoesNotThrow(Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            try
            {
                action();
            }
            catch (Exception ex)
            {
                Fail($"Expected no exception to be thrown, but an exception of type {ex.GetType().Name} was thrown.");
            }
        }

        public static void Fail(string message = "")
        {
            throw new AssertionException(message);
        }
    }
}