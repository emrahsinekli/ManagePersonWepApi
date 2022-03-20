using ManagePerson.CQRS.Entities;
using MediatR;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ManagePerson.CQRS.Queries
{
    public class GetPersonByEmailQueryHandler : IRequestHandler<GetPersonByEmailQuery, List<PersonEntity>>
    {
     
        public Task<List<PersonEntity>> Handle(GetPersonByEmailQuery request, CancellationToken cancellationToken)
        {
            List<PersonEntity> personList = new List<PersonEntity>();
            using (IDbConnection conn = new SqlConnection(@"Server=.;Database=ScripsPerson;Trusted_Connection=True;"))
            {
                conn.Open();
                personList = conn.Query<PersonEntity>(
"SELECT X.Name,X.Family,X.Given,X.Email,Y.Phone FROM (" +
"select HN.Name,HN.Family,HN.Given,CP.Value as Email from dbo.Persons P " +
"inner join dbo.HumanNames HN on P.Id = HN.PersonId " +
"inner join dbo.ContactPoints CP on P.Id = CP.PersonId where CP.PersonId = (select distinct PersonId from ContactPoints where Value = @Email ) and CP.System = 'Email') AS X , " +
"(select HN.Name,HN.Family,HN.Given,CP.Value as Phone from dbo.Persons P " +
"inner join dbo.HumanNames HN on P.Id = HN.PersonId " +
"inner join dbo.ContactPoints CP on P.Id = CP.PersonId where CP.PersonId = (select distinct PersonId from ContactPoints where Value = @Email ) and CP.System = 'Phone' ) AS Y", new { Email = request.Email}).ToList(); 
                conn.Close();
                
            }
            return Task.FromResult(personList);
        }
    }
}
