﻿using System.Linq;
using Given.Common;
using Given.NUnit;
using NUnit.Framework;

namespace EntityFramework.Seeder.EF6.Tests
{
    [Story(AsA = "developer",
       IWant = "load a list of countries from an embedded resource",
       SoThat = "I do not need to enter it manually")]
    public class when_seeding_an_empty_db_set_from_a_resource : Scenario
    {
        public static CountryContext _context;

        given an_empty_context = () =>
                    {
                        _context = new CountryContext();
                    };

        when seeding_from_a_resource = () =>
        {
            _context.Countries.SeedFromResource("EntityFramework.Seeder.EF6.Tests.CountriesResource.csv", c => c.Code);
            _context.SaveChanges();
        };

        [then]
        public void the_db_set_should_contain_4_countries()
        {
            _context.Countries.Count().ShouldEqual(4);
        }

        [then]
        public void the_db_set_should_contain_the_expected_country_codes_and_names()
        {
            _context.Countries.ShouldContain(c => c.Code == "AT" && c.Name == "Austria");
            _context.Countries.ShouldContain(c => c.Code == "AU" && c.Name == "Australia");
            _context.Countries.ShouldContain(c => c.Code == "CA" && c.Name == "Canada");
            _context.Countries.ShouldContain(c => c.Code == "US" && c.Name == "United States");
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            _context.Dispose();
        }
    }
}
