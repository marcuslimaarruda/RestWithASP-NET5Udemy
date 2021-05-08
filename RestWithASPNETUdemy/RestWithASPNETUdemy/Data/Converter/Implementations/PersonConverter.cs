using RestWithASPNETUdemy.Data.Converter.Contract;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNETUdemy.Data.Converter.Implementations
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public Person Parse(PersonVO origin)
        {
            if (origin == null) return null;
            return new Person
            {
                id = origin.id,
                primeiroNome = origin.primeiroNome,
                UltimoNome = origin.UltimoNome,
                Endereco = origin.Endereco,
                Sexo = origin.Sexo
            };
        }

        public PersonVO Parse(Person origin)
        {
            if (origin == null) return null;
            return new PersonVO
            {
                id = origin.id,
                primeiroNome = origin.primeiroNome,
                UltimoNome = origin.UltimoNome,
                Endereco = origin.Endereco,
                Sexo = origin.Sexo
            };
        }

        public List<Person> Parse(List<PersonVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }


        public List<PersonVO> Parse(List<Person> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
