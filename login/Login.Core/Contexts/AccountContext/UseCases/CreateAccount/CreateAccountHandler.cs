using Login.Core.Contexts.AccountContext.Entities;
using Login.Core.Contexts.AccountContext.UseCases.CreateAccount.Contracts;
using Login.Core.Contexts.AccountContext.ValueObjects;

namespace Login.Core.Contexts.AccountContext.UseCases.CreateAccount
{
    public class CreateAccountHandler
    {
        private readonly ICreateAccountRepository _repository;
        private readonly ICreateAccountService _service;
        public CreateAccountHandler(ICreateAccountRepository repository, ICreateAccountService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<CreateAccountResponse> Handle(
            CreateAccountRequest request, CancellationToken cancellationToken)
        {
            User user;
            Email email;
            Password password;

            #region Valida a requisição
            try
            {
                var result = CreateAccountValidation.Assert(request);
                if(!result.IsValid)
                {
                    return new CreateAccountResponse("Requisição inválida", 400, result.Notifications);
                }
            }
            catch (Exception)
            {
                return new CreateAccountResponse("Não foi possível validar sua requisição", 500);
            }
            #endregion

            #region Gera os objetos
            try
            {
                email = new Email(request.Email);
                password = new Password(request.Password);

                user = new User(
                    email,
                    password,
                    request.Name);
            }
            catch (Exception ex)
            {
                return new CreateAccountResponse(ex.Message, 400);
            }
            #endregion

            #region Verifica se o usuário já existe
            try
            {
                var exist = await _repository.AnyAsync(request.Email, cancellationToken);
                if (exist)
                {
                    return new CreateAccountResponse("Este e-mail já está cadastrado", 400);
                }
            }
            catch (Exception)
            {
                return new CreateAccountResponse("Falha ao verificar e-mail cadastrado.", 500);
            }
            #endregion

            #region Persistindo o usuário
            try
            {
                await _repository.CreateAsync(user, cancellationToken);
            }
            catch (Exception)
            {
                return new CreateAccountResponse("Falha ao persistir usuário.", 500);
            }
            #endregion

            #region Enviando o e-mail de ativação
            try
            {
                await _service.SendVerificationEmailAsync(user, cancellationToken);
            } 
            catch
            {
            }
            #endregion

            return new CreateAccountResponse("Conta criada com sucesso!", new CreateAccountResponseData(user.Id, user.Name, user.Email));
        }
    }
}
