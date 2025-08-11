using System;
using System.Collections.Generic;
using System.Linq;

namespace IngameScript
{
    public class MyFinalTestProvider : ITestProvider
    {
        public IEnumerable<Test> GetTests()
        {
            yield return Test.Make(Sum_Should_Be_Correct, 2, 2, 4);
            yield return Test.Make(Sum_Should_Be_Correct, -5, 10, 5);

            yield return Test.Make(Value_Should_Be_True, true);
            yield return Test.Make(Value_Should_Be_False, false);

            yield return Test.Make(Object_Should_Be_Null);
            yield return Test.Make(Object_Should_Not_Be_Null, "Some String");

            yield return Test.Make(Array_Summation_Should_Be_Correct, new int[] { 1, 2, 3, 4 }, 10);
            yield return Test.Make(Arrays_Should_Be_Equal);

            yield return Test.Make(Should_Throw_Expected_Exception);
            yield return Test.Make(Should_Not_Throw_Any_Exception);
        }

        private void Sum_Should_Be_Correct(int x, int y, int expected)
        {
            Assert.AreEqual(expected, x + y);
        }        

        private void Value_Should_Be_True(bool value)
        {
            Assert.IsTrue(value, "Value was expected to be true.");
        }

        private void Value_Should_Be_False(bool value)
        {
            Assert.IsFalse(value, "Value was expected to be false.");
        }

        private void Object_Should_Be_Null()
        {
            Assert.IsNull(null);
        }

        private void Object_Should_Not_Be_Null(object obj)
        {
            Assert.IsNotNull(obj);
        }

        private void Array_Summation_Should_Be_Correct(int[] numbers, int expectedSum)
        {
            Assert.AreEqual(expectedSum, numbers.Sum());
        }

        private void Arrays_Should_Be_Equal()
        {
            var arr1 = new[] { 1, 2, 3 };
            var arr2 = new[] { 1, 2, 3 };
            Assert.AreEqual(arr1, arr2, "This demonstrates reference inequality for arrays.");
        }

        private void Should_Throw_Expected_Exception()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                throw new ArgumentNullException("paramName");
            });

            Assert.AreEqual("paramName", ex.ParamName);
        }

        private void Should_Not_Throw_Any_Exception()
        {
            Assert.DoesNotThrow(() =>
            {
                int result = 5 * 10;
                Assert.AreEqual(50, result);
            });
        }
    }
}