namespace Task_01.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class LazyTests
    {
        [Test]
        public static void CheckIfLazyWorks()
        {
            var sut = LazyFactory<int>.CreateLazy(() => 48);
            Assert.AreEqual(48, sut.Get());
        }

        [Test]
        public static void CheckIfLazyConcurrentWorks()
        {
            var sut = LazyFactory<int>.CreateLazyConcurrent(() => 13 * 37);
            Assert.AreEqual(481, sut.Get());
        }

        [Test]
        public static void CheckIfLazyWorksOnMultipleCalls()
        {
            var sut = LazyFactory<string>.CreateLazy(() => "Like the legend of the phoenix...");

            for (var i = 0; i < 100; ++i)
            {
                Assert.AreEqual("Like the legend of the phoenix...", sut.Get());
            }
        }

        [Test]
        public static void CheckIfLazyConcurrentWorksOnMultipleCalls()
        {
            var sut = LazyFactory<char>.CreateLazyConcurrent(() => 'O');

            for (var i = 0; i < 100; ++i)
            {
                Assert.AreEqual('O', sut.Get());
            }
        }

        [Test]
        public static void CheckLazySameObject()
        {
            var sut = LazyFactory<double[]>.CreateLazy(() => new[] { 0.9, 0.7, 0.3, 0.1, -0.1 });
            var firstResult = sut.Get();

            for (var i = 0; i < 100; ++i)
            {
                Assert.AreEqual(firstResult, sut.Get());
            }
        }

        [Test]
        public static void CheckLazyConcurrentSameObject()
        {
            var sut = LazyFactory<int[]>.CreateLazyConcurrent(() => new[] { -17, 15, -13, 11, -9 });
            var firstResult = sut.Get();

            for (var i = 0; i < 100; ++i)
            {
                Assert.AreEqual(firstResult, sut.Get());
            }
        }

        [Test]
        public static void CheckSupplierCalledOnceByLazy()
        {
            var counter = 0;

            var sut = LazyFactory<object>.CreateLazy(() =>
            {
                counter++;
                return null;
            });

            for (var i = 0; i < 100; ++i)
            {
                sut.Get();
            }

            Assert.AreEqual(1, counter);
        }

        [Test]
        public static void CheckSupplierCalledOnceByLazyConcurrent()
        {
            var counter = 0;

            var sut = LazyFactory<object>.CreateLazyConcurrent(() =>
            {
                counter++;
                return null;
            });

            for (var i = 0; i < 100; ++i)
            {
                sut.Get();
            }

            Assert.AreEqual(1, counter);
        }
    }
}
