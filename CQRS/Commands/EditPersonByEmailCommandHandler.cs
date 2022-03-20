using ManagePerson.CQRS.Entities;
using MediatR;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ManagePerson.CQRS.Commands
{
    public class EditPersonByEmailCommandHandler : IRequestHandler<EditPersonByEmailCommand, List<PersonEntity>>
    {
        public Task<List<PersonEntity>> Handle(EditPersonByEmailCommand request, CancellationToken cancellationToken)
        {
            List<PersonEntity> newPersonList = new List<PersonEntity>();
            using (IDbConnection conn = new SqlConnection(@"Server=.;Database=ScripsPerson;Trusted_Connection=True;"))
            {
                conn.Open();
                newPersonList = conn.Query<PersonEntity>(
"update dbo.HumanNames set Family = @Family, Given = @Given  where PersonId = (select PersonId from dbo.ContactPoints where System = 'Email' and Value = @Email) " + 
" update dbo.ContactPoints set Value = @Phone where Id = (Select Id from ContactPoints where System = 'Phone' and PersonId = (select PersonId from ContactPoints where System = 'Email' and Value = @Email))  " +
" SELECT X.Name,X.Family,X.Given,X.Email,Y.Phone FROM (" +
"select HN.Name,HN.Family,HN.Given,CP.Value as Email from dbo.Persons P " +
"inner join dbo.HumanNames HN on P.Id = HN.PersonId " +
"inner join dbo.ContactPoints CP on P.Id = CP.PersonId where CP.PersonId = (select distinct PersonId from ContactPoints where Value = @Email ) and CP.System = 'Email') AS X , " +
"(select HN.Name,HN.Family,HN.Given,CP.Value as Phone from dbo.Persons P " +
"inner join dbo.HumanNames HN on P.Id = HN.PersonId " +
"inner join dbo.ContactPoints CP on P.Id = CP.PersonId where CP.PersonId = (select distinct PersonId from ContactPoints where Value = @Email ) and CP.System = 'Phone' ) AS Y",

new { Email = request.Email, Family = request.Family, Given = request.Given , Phone = request.Phone }).ToList();
                conn.Close();

            }
            return Task.FromResult(newPersonList);
        }
    }
}
