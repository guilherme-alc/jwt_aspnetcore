using Login.Core.Contexts.AccountContext.Entities;
using Login.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using MediatR;

namespace Login.Core.Contexts.AccountContext.UseCases.Authenticate;

public class AuthenticateHandler : IRequestHandler<AuthenticateRequest, AuthenticateResponse>
{
    private readonly IAuthenticateRepository _repository;

    public AuthenticateHandler(IAuthenticateRepository repository)
    {
        _repository = repository;
    }
    public async Task<AuthenticateResponse> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
    {
        User? user;
        #region Valida requisição
        try
        {
            var result = AuthenticateValidation.Assert(request);
            if (!result.IsValid)
                return new AuthenticateResponse("Requisição inválida", 400, result.Notifications);
        }
        catch
        {
            return new AuthenticateResponse("Não foi possível validar sua requisição.", 500);
        }
        #endregion

        #region Recupera perfil
        try
        {
            user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (user == null)
                return new AuthenticateResponse("Usuário não encontrado", 404);
        }
        catch
        {
            return new AuthenticateResponse("Não foi possível recuperar o perfil.", 500);
        }
        #endregion
        
        #region Verifica senha
        
        if(!user.Password.Challenge(request.Password))
            return new AuthenticateResponse("Usuário ou senha incorretos", 401);

        #endregion
        
        #region Verifica se conta está ativa

        try
        {
            if (!user.Email.Verification.IsActive)
                return new AuthenticateResponse("Conta inativa", 400);
        }
        catch
        {
            return new AuthenticateResponse("Não foi possível verificar seu perfil", 500);
        }
        #endregion

        #region Retorna os dados
        try
        {
            var data = new AuthenticatetResponseData
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                Roles = Array.Empty<string>()
            };
            
            return new AuthenticateResponse(string.Empty, data);
        }
        catch (Exception ex)
        {
            return new AuthenticateResponse("Não foi possível obter os dados do perfil", 500);
        }
        #endregion
    }
}