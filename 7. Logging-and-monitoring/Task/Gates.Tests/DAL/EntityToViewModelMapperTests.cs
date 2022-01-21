using AutoMapper;
using Gates.DAL.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gates.Tests.DAL
{
    /// <summary>
    ///     Юнит тесты для проверки конфигурации Automapper
    /// </summary>
    [TestClass]
    public class EntityToViewModelMapperTests
    {
        /// <summary>
        ///     Проверка корректности конфигурации маппера
        /// </summary>
        [TestMethod]
        public void EntityToViewModelConfigTest()
        {
            //Arrange
            var config = new MapperConfiguration(conf => conf.AddProfile<EntityToViewModelProfile>());
            //Assert
            config.AssertConfigurationIsValid();
        }
    }
}