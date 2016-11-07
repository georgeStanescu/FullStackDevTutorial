using System;
using NUnit.Framework;
using GigHub.Persistence;
using GigHub.Controllers;
using GigHub.IntegrationTests.Extensions;
using System.Linq;
using GigHub.Core.Models;
using System.Collections.Generic;
using FluentAssertions;

namespace GigHub.IntegrationTests.Controllers
{
    [TestFixture]
    public class GigsControllerTests
    {
        private GigsController _controller;
        private ApplicationDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Mine_WhenCalled_ShouldReturnUpcomingGigs()
        {
            //Arrange
            var user = _context.Users.First();
            var genre = _context.Genres.First();

            _controller.MockCurrentUser(user.Id, user.UserName);

            var gig = new Gig
            {
                Artist = user,
                DateTime = DateTime.Now.AddDays(1),
                Genre = genre,
                Venue = "-"
            };

            _context.Gigs.Add(gig);

            _context.SaveChanges();

            //Act
            var result = _controller.Mine();

            //Assert
            (result.ViewData.Model as IEnumerable<Gig>).Should().HaveCount(1);
        }
    }
}
