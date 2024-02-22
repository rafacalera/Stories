using Microsoft.AspNetCore.Mvc;
using Moq;
using Stories.API.Application.Models.Requests;
using Stories.API.Controllers;
using Stories.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.API.UnitTest
{
    public class VoteControllerTest 
    {
        [Fact]
        public async Task Add_ResultTrue_Created()
        {
            var service = new Mock<IVoteService>();
            var voteRequest = new VoteRequest(true, 1, 1);
            service.Setup(s => s.Add(It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);
            var controller = new VotesController(service.Object);

            var result = await controller.Add(voteRequest) as OkResult;

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Add_ResultFalse_BadRequest()
        {
            var service = new Mock<IVoteService>();
            var voteRequest = new VoteRequest(true, 1, 1);
            service.Setup(s => s.Add(It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(false);
            var controller = new VotesController(service.Object);

            var result = await controller.Add(voteRequest) as BadRequestResult;

            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_ResultTrue_Ok()
        {
            var service = new Mock<IVoteService>();
            service.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(true);
            var controller = new VotesController(service.Object);

            var result = await controller.Delete(1) as OkResult;

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task Delete_ResultFalse_NotFound()
        {
            var service = new Mock<IVoteService>();
            service.Setup(s => s.Delete(It.IsAny<int>())).ReturnsAsync(false);
            var controller = new VotesController(service.Object);

            var result = await controller.Delete(1) as NotFoundResult;

            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
