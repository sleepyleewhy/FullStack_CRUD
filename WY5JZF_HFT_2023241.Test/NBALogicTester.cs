using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using WY5JZF_HFT_2023241.Logic;
using WY5JZF_HFT_2023241.Models;
using WY5JZF_HFT_2023241.Repository;

namespace WY5JZF_HFT_2023241.Test
{
    [TestFixture]
    public class NBALogicTester
    {
        Mock<IRepository<Division>> mockDivisionRepo;
        Mock<IRepository<Team>> mockTeamRepo;
        Mock<IRepository<Player>> mockPlayerRepo;

        PlayerLogic playerLogic;
        DivisionLogic divisionLogic;
        TeamLogic teamLogic;


        [SetUp]
        public void Init()
        {
            mockDivisionRepo = new Mock<IRepository<Division>>();
            mockTeamRepo = new Mock<IRepository<Team>>();

            
            Player testplayer1 = new Player()
            {
                PlayerId = 1,
                PlayerName = "Test Player 1",
                Position = 2,
                AvgPoints = 10,
                Salary = 3000
            };
            Player testplayer2 = new Player()
            {
                PlayerName = "Test Player 2",
                Position = 2,
                AvgPoints = 20,
                Salary = 4000
            };
            Player testplayer3 = new Player()
            {
                PlayerName = "Test Player 3",
                Position = 3,
                AvgPoints = 30,
                Salary = 5000
            };
            Player testplayer5 = new Player()
            {
                PlayerName = "Test Player 5",
                Position = 4,
                AvgPoints = 25,
                Salary = 3000
            };
            Player testplayer4 = new Player()
            {
                PlayerName = "Test Player 4",
                Position = 2,
                AvgPoints = 40,
                Salary = 20000
            };
            Team testteam1 = new Team()
            {
                TeamName = "Test team 1",
                TeamId = 1,
                FanCount= 10000,
                Players = new List<Player>
                {
                    testplayer1, testplayer2, testplayer3,testplayer5
                }
            };
            Team testteam2 = new Team()
            {
                TeamName = "Test team 2",
                TeamId = 2,
                FanCount = 20000,
                Players = new List<Player>
                {
                    testplayer4
                }
            };

            Division testdivision1 = new Division()
            {
                Teams = new List<Team>() { testteam1, testteam2},
                DivisionId = 1,
                DivisionName = "Test Divison 1"
            };

            mockTeamRepo.Setup(t => t.ReadAll()).Returns(new List<Team>()
            {
                testteam1,testteam2
            }.AsQueryable());
            mockDivisionRepo.Setup(t => t.Read(1)).Returns(testdivision1);

            mockTeamRepo.Setup(t => t.Read(1)).Returns(testteam1);
            mockPlayerRepo = new Mock<IRepository<Player>>();
            playerLogic = new PlayerLogic(mockPlayerRepo.Object);
            divisionLogic = new DivisionLogic(mockDivisionRepo.Object);
            teamLogic = new TeamLogic(mockTeamRepo.Object);
            

        }


        [Test]
        public void PlayerCreateTest()
        {
            //Arrange
            Player player = new Player()
            {
                AvgPoints = 23.2,
                PlayerName = "Mock János",
                Position = 2,
                Salary = 5000
            };

            //Act
            playerLogic.Create(player);

            //Assert
            mockPlayerRepo.Verify(r => r.Create(player), Times.Once);
        }
        [Test]
        public void DivisionCreateTest()
        {
            //Arrange
            Division testDiv = new Division()
            {
                DivisionName = "Fejér",
                Population = 10000
            };

            //Act
            divisionLogic.Create(testDiv);

            //Assert
            mockDivisionRepo.Verify(r => r.Create(testDiv), Times.Once);
        }
        [Test]
        public void TeamCreateTest()
        {
            //Arrange
            Team mockTeam = new Team()
            {
                TeamName = "Alba",
                FanCount = 300

            };

            //Act
            teamLogic.Create(mockTeam);
            //Assert
            mockTeamRepo.Verify(r => r.Create(mockTeam), Times.Once);

        }

        [Test]

        public void AllPosPlayerInTeamTest()
        {
            //Arrange


            //Act
            IEnumerable<Player> result = teamLogic.AllPosPlayerInTeam(2, 1);


            //Assert


            Assert.That(result.FirstOrDefault(t => t.PlayerName == "Test Player 1") != null);
            Assert.That(result.FirstOrDefault(t => t.PlayerName == "Test Player 2") != null);
            Assert.That(result.Count() == 2);

        }

        [Test]
        public void AvgPointsPerTeamTest()
        {
            // Act
            double result = teamLogic.AvgPointsPerTeam(1);

            Assert.That(result == 85);
        }

        [Test]
        public void Top3PointsInDivTest()
        {

            //Act
            var result = divisionLogic.Top3PointsInDiv(1);

            Assert.That(result.Count() == 3);
            Assert.That(result.FirstOrDefault(t => t.AvgPoints == 40) != null);
            Assert.That(result.FirstOrDefault(t => t.AvgPoints == 30) != null);
            Assert.That(result.FirstOrDefault(t => t.AvgPoints == 25) != null);

        }
        [Test]
        public void TeamWithMostSalaryCostInDivTest()
        {
            // Act
            var result = divisionLogic.TeamWithMostSalaryCostInDiv(1);

            Assert.That(result.TeamId == 2);
        }
        [Test]
        public void AllFansPerDivisionTest()
        {
            var result = divisionLogic.AllFansPerDivision(1);

            Assert.That(result == 30000);
        }

        [Test]
        public void CreatePlayerIncorrectData()
        {
            //Arrange
            Player player = new Player()
            {
                AvgPoints = -1,
                PlayerName = "Valaki"
            };
            try
            {
                playerLogic.Create(player);
            }
            catch(ArgumentException)
            {

            }
            mockPlayerRepo.Verify(r => r.Create(player), Times.Never);

        }

        [Test]
        public void CreateTeamIncorrectData()
        {
            Team team = new Team()
            {
                TeamName = "Name",
                FanCount = -3
            };
            try
            {
                teamLogic.Create(team);
            }
            catch (ArgumentException)
            {

            }
            mockTeamRepo.Verify(r => r.Create(team), Times.Never);
        }

    }
}
