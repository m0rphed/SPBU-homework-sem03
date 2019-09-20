using System;

namespace Task_01.Test
{
    using System.Threading;
    using NUnit.Framework;

    [TestFixture]
    class LazyRaceConditionTests
    {
        [Test]
        public static void CheckIfLazyConcurrentNotAllowsRaces()
        {
            var lockObject = new object();
            var counter = 0;

            var sut = LazyFactory<string>.CreateLazyConcurrent(() =>
            {

                lock (lockObject)
                {
                    counter++;
                }

                return "expected value";
            });

            var threads = new Thread[1000];

            for (var i = 0; i < threads.Length; ++i)
            {
                threads[i] = new Thread(() =>
                {
                    for (var j = 0; j < 100; ++j)
                    {
                        Assert.AreEqual("expected value", sut.Get(), "Unexpected value was returned");
                    }
                });
            }

            // поднимаем потоки
            foreach (var thread in threads)
            {
                thread.Start();
            }

            for (var j = 0; j < 10; ++j)
            {
                sut.Get();
            }

            // объединяем потоки
            foreach (var thread in threads)
            {
                thread.Join();
            }

            Assert.AreEqual(1, counter);
        }

        [Test]
        public static void CheckIfLazyConcurrentContinuesAfterExceptionInSupplier()
        {
            var sut = LazyFactory<object>.CreateLazyConcurrent(() => throw new Exception());
            var threads = new Thread[9000];

            for (var i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() => sut.Get());
            }

            // поднимаем потоки
            foreach (var thread in threads)
            {
                thread.Start();
            }

            // ожидаем каждый поток
            foreach (var thread in threads)
            {
                thread.Join();
            }

            Assert.Pass();
        }
    }
}
