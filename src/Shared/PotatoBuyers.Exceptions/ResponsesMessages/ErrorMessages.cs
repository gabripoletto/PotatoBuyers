namespace PotatoBuyers.Exceptions.ResponsesMessages
{
    public class ErrorMessages
    {
        public const string INTERNAL_ERROR = "Erro interno no servidor";

        public const string CPF_INVALID = "CPF inválido.";

        public const string EMAIL_INVALID = "Email inválido.";

        public const string TEL_INVALID_FORMAT = "Telefone inválido. Formato esperado: (XX) XXXXX-XXXX";

        public const string CPF_INVALID_FORMAT = "CPF inválido. Formato esperado: XXX.XXX.XXX-XX";

        public const string PASSWORD_INVALID_DIGITS = "A senha deve conter mais que 6 dígitos";

        public const string PASSWORD_REQUIRED_DIGITS = "A senha deve conter pelo menos 1 letra maiúscula, 1 número e 1 caractere especial (@, #, $, %, etc.).";

        public const string REQUIRED_FIELD = "Um campo obrigatório não foi preenchido!";

        public const string EMAIL_ALREADY_REGISTERED = "E-mail já registrado";

        public const string EMAIL_OR_PASSWORD_INVALID = "Email e/ou senha inválidos.";
    }
}
