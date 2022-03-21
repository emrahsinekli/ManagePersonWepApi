using ManagePerson.Controllers;
using ManagePerson.CQRS.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using ManagePerson.CQRS.Queries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace TestProject
{
    public class ManagePersonTest
    {
        [Fact]
        public void GetByEmailReturnOk()
        {
            var mediator = new Mock<IMediator>();
            string email = "reddy@yopmail.com";
            var result = new ManagePersonController(mediator.Object);

            Assert.IsType<OkObjectResult>(result.Get(email));
        }

        [Fact]
        public async Task GetByEmailQueryHandlerReturnCountTest()
        {
            var query = new GetPersonByEmailQuery { Email = "reddy@yopmail.com" };
            CancellationToken cancellationToken = new CancellationToken();

            var sut = new GetPersonByEmailQueryHandler();
            var result = await sut.Handle(query, cancellationToken);
            Assert.Equal(1, result.Count);
        }
    }
}
