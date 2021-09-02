using NS.Core.Utils;

namespace NS.Core.DomainObjects
{
    public class Cpf
    {
        public const int MaxLength = 11;

        public string Number { get; private set; }

        /*Remeber: This constructor is exclusive for EF*/
        protected Cpf()
        {
        }

        public Cpf(string number)
        {
            if (!Validate(number))
            {
                throw new DomainException("CPF number is invalid");
            }

            Number = number;
        }

        public static bool Validate(string cpf)
        {
            cpf = cpf.GetNumbers();

            if (cpf.Length > MaxLength)
            {
                return false;
            }

            while (cpf.Length != MaxLength)
            {
                cpf = '0' + cpf;
            }

            var same = true;

            for (var i = 1; i < MaxLength && same; i++)
            {
                if (cpf[i] != cpf[0])
                {
                    same = false;
                }
            }

            if (same || cpf == "12345678909")
            {
                return false;
            }

            var numeros = new int[MaxLength];

            for (var i = 0; i < MaxLength; i++)
            {
                numeros[i] = int.Parse(cpf[i].ToString());
            }

            var soma = 0;

            for (var i = 0; i < 9; i++)
            {
                soma += (10 - i) * numeros[i];
            }

            var resultado = soma % MaxLength;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                {
                    return false;
                }
            }
            else if (numeros[9] != MaxLength - resultado)
            {
                return false;
            }

            soma = 0;

            for (var i = 0; i < 10; i++)
            {
                soma += (MaxLength - i) * numeros[i];
            }

            resultado = soma % MaxLength;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                {
                    return false;
                }
            }
            else if (numeros[10] != MaxLength - resultado)
            {
                return false;
            }

            return true;
        }
    }
}