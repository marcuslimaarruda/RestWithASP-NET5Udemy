using RestWithASPNETUdemy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Services.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {
        private volatile int count;

        public Person Create(Person person)
        {
            return person;
        }

        public void Delete(long id)
        {

        }
        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();
            for (int i = 0; i < 8; i++)
            {
                Person person = clonePerson(i);
                persons.Add(person);
            }
            return persons;
        }

        public Person FindById(long id)
        {
            return new Person
            {
                id = 1,
                primeiroNome = "Marcus",
                UltimoNome = "Arruda",
                Endereco = "Rua Visconde do Uriguai, 315/1103, Centro/Niteroi/Rj - Brasil",
                Sexo = "Masculino"
            };
        }

        public Person Update(Person person)
        {
            return person;
        }
        private Person clonePerson(int i)
        {
            return new Person
            {
                id = IncrementAndGet(),
                primeiroNome = "Pessoa Nome" + i,
                UltimoNome = "Pessoa Sobrenome" + i,
                Endereco = "Endereço da pessoa" + i,
                Sexo = "Feminino"
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
