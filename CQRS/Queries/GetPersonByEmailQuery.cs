using ManagePerson.CQRS.Entities;
using MediatR;
using System;
using System.Collections.Generic;

namespace ManagePerson.CQRS.Queries
{
    public class GetPersonByEmailQuery : IRequest<List<PersonEntity>>
    {
        public String Email { get; set; }
    }
}
