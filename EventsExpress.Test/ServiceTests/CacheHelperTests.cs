using System;
using System.Runtime.Caching;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Services;
using NUnit.Framework;

namespace EventsExpress.Test.ServiceTests
{
    [TestFixture]
    internal class CacheHelperTests
    {
        private readonly CacheDto cacheDto = new CacheDto { Key = "HelloDarkness", Value = "My old friend" };
        private readonly MemoryCache memoryCache = MemoryCache.Default;

        private CacheHelper service;

        [SetUp]
        protected void Initialize()
        {
            service = new CacheHelper();
        }

        [OneTimeTearDown]
        protected void BeforeEnd()
        {
            memoryCache.Dispose();
        }

        [Test]
        public void Add_inserts_the_object_into_the_cache()
        {
            // Act
            service.Add(cacheDto);
            var actual = (CacheDto)memoryCache.Get(cacheDto.Key);

            // Assert
            Assert.AreEqual(cacheDto, actual);
        }

        [Test]
        public void Add_objects_are_not_equal()
        {
            // Arrange
            memoryCache.Remove(cacheDto.Key);
            var differentCacheDto = new CacheDto
            {
                Key = cacheDto.Key,
                Value = "Another value",
            };

            // Act
            service.Add(differentCacheDto);
            var actual = (CacheDto)memoryCache.Get(cacheDto.Key);

            // Assert
            Assert.AreNotEqual(cacheDto, actual);
        }

        [Test]
        public void GetValue_returns_null()
        {
            // Arrange
            memoryCache.Remove(cacheDto.Key);

            // Act
            var actual = service.GetValue(cacheDto.Key);

            // Assert
            Assert.IsNull(actual);
        }

        [Test]
        public void GetValue_returns_object()
        {
            // Arrange
            memoryCache.Add(cacheDto.Key, cacheDto, DateTime.Now.AddDays(1));

            // Act
            var actual = service.GetValue(cacheDto.Key);

            // Assert
            Assert.NotNull(actual);
        }

        [Test]
        public void GetValue_returns_correct_value()
        {
            // Arrange
            memoryCache.Add(cacheDto.Key, cacheDto, DateTime.Now.AddDays(1));

            // Act
            var actual = service.GetValue(cacheDto.Key);

            // Assert
            Assert.AreEqual(cacheDto, actual);
        }

        [Test]
        public void Update_works_correctly()
        {
            // Arrange
            memoryCache.Add(cacheDto.Key, cacheDto.Value, DateTime.Now.AddDays(1));
            var testCacheDto = new CacheDto { Key = cacheDto.Key, Value = "Updated value" };

            // Act
            service.Update(testCacheDto);
            var actual = (CacheDto)memoryCache.Get(testCacheDto.Key);

            // Assert
            Assert.AreEqual(testCacheDto, actual);
        }

        [Test]
        public void Delete_works_correctly()
        {
            // Arrange
            memoryCache.Add(cacheDto.Key, cacheDto.Value, DateTime.Now.AddDays(1));

            // Act
            service.Delete(cacheDto.Key);
            var actual = (CacheDto)memoryCache.Get(cacheDto.Key);

            // Assert
            Assert.IsNull(actual);
        }
    }
}
