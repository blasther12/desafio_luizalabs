using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TesteLuizaLabs.Application.Exceptions
{
    public class TesteLuizaLabsExceptions : Exception
    {
        public enum Type
        {
            UnknownError = 1,
            Error = 2,
            ExistingCustomer = 3,
            ExistingFavoriteProduct = 4,
            IncompleteData = 5,
            CustomerNotFound = 6,
            ProductNotFound = 7,
            CustomerProductNotFound = 8,
            ExceededNumberOfData = 9,
            UnknownProduct = 10,
            IncompleteCredentials = 11,
            InvalidEmail = 12
        }

        private Type _type;
        private int _statusCode;

        /// <summary>
        /// Realiza o tratamento de exceptions personalizadas
        /// </summary>
        /// <param name="type"></param>
        /// <param name="statusCode"></param>
        /// <param name="msg"></param>
        public TesteLuizaLabsExceptions(Type type, HttpStatusCode statusCode = HttpStatusCode.BadRequest,  string msg = "")
        {
            _type = type;
            _statusCode = (int)statusCode;
            Data.Add("info", msg);
        }

        public Type type => _type;
        public int statusCode => _statusCode;

        public override string Message
        {
            get
            {
                return _type switch
                {
                    Type.UnknownError => "Erro desconhecido!",
                    Type.Error => "Ocorreu um erro durante a execução!",
                    Type.ExistingCustomer => "O e-mail informado já está em uso!",
                    Type.ExistingFavoriteProduct => "O produto informado já existe na lista de favoritos do Cliente!",
                    Type.IncompleteData => "Dados do cliente incompleto, devem ser informados Nome e E-mail!",
                    Type.CustomerNotFound => "Cliente não encontrado!",
                    Type.ProductNotFound => "Produto não encontrado!",
                    Type.CustomerProductNotFound => "Produto favorito não encontrado!",
                    Type.ExceededNumberOfData => "Numero maximo de registros excedido!",
                    Type.UnknownProduct => "O ID do produto deve ser informado!",
                    Type.IncompleteCredentials => "Usuario e/ou senha devem ser informados!",
                    Type.InvalidEmail => "Formato de E-mail invalido!",
                    _ => "",
                };
            }
        }
    }
}
