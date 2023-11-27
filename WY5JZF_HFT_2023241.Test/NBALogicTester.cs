using Moq;
using NUnit.Framework;
using System;
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
            mockPlayerRepo = new Mock<IRepository<Player>>();
            playerLogic = new PlayerLogic(mockPlayerRepo.Object);
            divisionLogic = new DivisionLogic(mockDivisionRepo.Object);
            teamLogic = new TeamLogic(mockTeamRepo.Object);
            // mockTeamRepo.Setup           
        }


        [Test]
        public void PlayerCreateTest()
        {
            //Arrange
            Player player = new Player()
            {
                AvgPoints= 23.2,
                PlayerName = "Mock János",
                Position = 2,
                Salary = 5000
            };

            //Act
            playerLogic.Create(player);

            //Assert
            mockPlayerRepo.Verify(r => r.Create(player), Times.Once);
        }

        public void DivisionCreateTest()
        {
            //Arrange
            Division testDiv = new Division()
            {
                DivisionName = "Fejér",
                Population = 10000
            };

        }

    }
}
