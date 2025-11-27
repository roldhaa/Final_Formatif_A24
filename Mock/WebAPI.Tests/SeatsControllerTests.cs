using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Tests;

[TestClass]
public class SeatsControllerTests
{
    [TestMethod]
    public void ReserveSeat_ok()
    {
        Mock<SeatsService> serviceMock = new Mock<SeatsService>();
        Mock<SeatsController> controller = new Mock<SeatsController>(serviceMock.Object) { CallBase = true };

        Seat seat = new Seat();
        seat.Id = 1;
        seat.Number = 1;

        serviceMock.Setup(s => s.ReserveSeat(It.IsAny<string>(), It.IsAny<int>())).Returns(seat);
        controller.Setup(c => c.UserId).Returns("11111");

        var actionResult = controller.Object.ReserveSeat(seat.Number);

        var result = actionResult.Result as OkObjectResult;

        Assert.IsNotNull(result);

     

    }

    [TestMethod]
    public void ReserveSeat_Unauthorized()
    {
        Mock<SeatsService> serviceMock = new Mock<SeatsService>();
        Mock<SeatsController> controller = new Mock<SeatsController>(serviceMock.Object) { CallBase = true };


        serviceMock.Setup(s => s.ReserveSeat(It.IsAny<string>(), It.IsAny<int>())).Throws(new SeatAlreadyTakenException());
        controller.Setup(c => c.UserId).Returns("11111");

        var actionResult = controller.Object.ReserveSeat(1);

        var result = actionResult.Result as UnauthorizedResult;

        Assert.IsNotNull(result);
       

    }

    [TestMethod]
    public void ReserveSeat_NotFound()
    {
        Mock<SeatsService> serviceMock = new Mock<SeatsService>();
        Mock<SeatsController> controller = new Mock<SeatsController>(serviceMock.Object) { CallBase = true };


        serviceMock.Setup(s => s.ReserveSeat(It.IsAny<string>(), It.IsAny<int>())).Throws(new SeatOutOfBoundsException());
        controller.Setup(c => c.UserId).Returns("11111");

        var seatNumber = 1;

        var actionResult = controller.Object.ReserveSeat(seatNumber);

        var result = actionResult.Result as NotFoundObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual("Could not find " + seatNumber, result.Value);

    }
    [TestMethod]
    public void ReserveSeat_BadRequest()
    {
        Mock<SeatsService> serviceMock = new Mock<SeatsService>();
        Mock<SeatsController> controller = new Mock<SeatsController>(serviceMock.Object) { CallBase = true };


        serviceMock.Setup(s => s.ReserveSeat(It.IsAny<string>(), It.IsAny<int>())).Throws(new UserAlreadySeatedException());
        controller.Setup(c => c.UserId).Returns("11111");

        var actionResult = controller.Object.ReserveSeat(1);

        var result = actionResult.Result as BadRequestResult;

        Assert.IsNotNull(result);
       


    }
}
