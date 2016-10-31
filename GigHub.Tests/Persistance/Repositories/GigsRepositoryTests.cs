using Microsoft.VisualStudio.TestTools.UnitTesting;
using GigHub.Persistence.Repositories;
using GigHub.Persistence;
using Moq;
using GigHub.Core.Models;
using System.Data.Entity;
using System;
using GigHub.Tests.Extensions;
using System.Collections.Generic;
using FluentAssertions;

namespace GigHub.Tests.Persistance.Repositories
{
    [TestClass]
    public class GigsRepositoryTests
    {
        private const string _artistId = "1";
        private GigRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;
        private Mock<DbSet<Attendance>> _mockAttendances;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGigs = new Mock<DbSet<Gig>>();
            _mockAttendances = new Mock<DbSet<Attendance>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(c => c.Gigs).Returns(_mockGigs.Object);
            mockContext.SetupGet(c => c.Attendances).Returns(_mockAttendances.Object);

            _repository = new GigRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(-1), ArtistId = _artistId };

            _mockGigs.SetSource(new List<Gig> { gig });

            var gigs = _repository.GetUpcomingGigsByArtist(_artistId);

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsCanceled_ShouldNotBeReturned()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = _artistId };

            gig.Cancel();

            _mockGigs.SetSource(new List<Gig> { gig });

            var gigs = _repository.GetUpcomingGigsByArtist(_artistId);

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForADifferentArtist_ShouldNotBeReturned()
        {
            var gig = new Gig { ArtistId = _artistId };

            _mockGigs.SetSource(new List<Gig> { gig });

            var gigs = _repository.GetUpcomingGigsByArtist(_artistId + "_");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsFortheGivenArtistAndIsForTheFuture_ShouldBeReturned()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = _artistId };

            _mockGigs.SetSource(new List<Gig> { gig });

            var gigs = _repository.GetUpcomingGigsByArtist(_artistId);

            gigs.Should().Contain(gig);
        }

        [TestMethod]
        public void GetGigsUserAttending_GigIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1) };
            var attendance = new Attendance { Gig = gig, AttendeeId = "1" };

            _mockAttendances.SetSource(new[] { attendance });

            var gigs = _repository.GetGigsUserAttending(attendance.AttendeeId);

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_AttendanceForADifferentUser_ShouldNotBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1) };
            var attendance = new Attendance { Gig = gig, AttendeeId = "1" };

            _mockAttendances.SetSource(new[] { attendance });

            var gigs = _repository.GetGigsUserAttending(attendance.AttendeeId + "-");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_UpcomingGigUserAttending_ShouldBeReturned()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1) };
            var attendance = new Attendance { Gig = gig, AttendeeId = "1" };

            _mockAttendances.SetSource(new[] { attendance });

            var gigs = _repository.GetGigsUserAttending(attendance.AttendeeId);

            gigs.Should().Contain(gig);
        }
    }
}
