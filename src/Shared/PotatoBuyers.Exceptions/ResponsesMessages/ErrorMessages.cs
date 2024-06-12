using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotatoBuyers.Exceptions.ResponsesMessages
{
    public class ErrorMessages
    {
        public const string CPF_INVALID = "CPF inválido.";

        public const string EMAIL_INVALID = "Email inválido.";

        public const string TEL_INVALID = "Telefone inválido. Formato esperado: (XX) XXXXX-XXXX";

        public const string PASSWORD_INVALID_DIGITS = "A senha deve conter mais que 6 dígitos";

        public const string PASSWORD_REQUIRED_DIGITS = "A senha deve conter pelo menos 1 letra maiúscula, 1 número e 1 caractere especial (@, #, $, %, etc.).";

        public const string REQUIRED_FIELD = "Um campo obrigatório não foi preenchido!";
    }
}
