using System;
using System.Collections.Generic;
using ManagePerson.CQRS.Entities;
using MediatR;

namespace ManagePerson.CQRS.Commands
{
    public class EditPersonByEmailCommand: IRequest<List<PersonEntity>>
    {
        public String Email { get; set; }
        public String Family { get; set; }
        public String Given { get; set; }
        public String Phone { get; set; }
    }
}
