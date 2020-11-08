using MfpeDrugsApi.Models;
using MfpeDrugsApi.Provider;
using MfpeDrugsApi.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DrugApiTesting
{
    [TestFixture]
    public class Test
    {
        
        private DrugRepository obj;
      /*  public static List<LocationWiseDrug> locationwisedruglisttest = new List<LocationWiseDrug>
        {
            new LocationWiseDrug { Id = 1, Name = "Disprin", ManufacturedDate = Convert.ToDateTime("2020-5-6 12:12:00.0000000"), ExpiryDate = Convert.ToDateTime("2021-5-6 12:12:00.0000000"), Manufacturer = "Abbot",Location="Delhi",Quantity=20}
        };*/

        [SetUp]
        public void Setup()
        {
            obj = new DrugRepository();
        }

        [Test]
        public void GetDetailsWhenGivenValidDrugId()
        {
            var result = obj.searchDrugsByID(2);
            // Assert.Pass();
          // Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result, Is.Not.Empty);
        }
        [Test]
        public void GetDetailsWhenGivenInvalidDrugId()
        {
            var result = obj.searchDrugsByID(10);
            Assert.That(result.Count, Is.EqualTo(0));
            Assert.That(result, Is.Empty);
        }
        [Test]
        public void GetDetailsWhenGivenValidDrugName()
        {
            var result = obj.searchDrugsByName("Disprin");
            Assert.That(result, Is.Not.Empty);
        }
        [Test]
        public void GetDetailsWhenGivenInvalidDrugName()
        {
            var result = obj.searchDrugsByName("Dcoldtotal");
            Assert.That(result.Count, Is.EqualTo(0));
           // Assert.That(result, Is.Empty);
        }
        [Test]
        public void GetResponseWhenGivenValidDrugIdAndLocation()
        {
            var result = obj.getDispatchableDrugStock(1, "Delhi");
            Assert.That(result, Is.True);
        }
        [Test]
        public void GetResponseWhenGivenInValidDrugIdAndLocation()
        {
            var result = obj.getDispatchableDrugStock(10, "Mumbai");
            Assert.That(result, Is.False);
        }
    }
}