using TodoList.Models.SessionStorage;

namespace TodoList.Services;

public class SessionService
{
    private SessionRepository _sessionStorageRepository;

	public SessionService(SessionRepository sessionStorageRepository)
	{
		_sessionStorageRepository = sessionStorageRepository;
	}

	public Session GetSessionById(string sessionId)
	{
		return _sessionStorageRepository.GetSessionById(sessionId);
	}

	public void SessionDelete(string sessionId)
	{
		try
		{
			var session = _sessionStorageRepository.GetSessionById(sessionId);
			_sessionStorageRepository.DeleteSessionById(session);
		} catch(Exception) { }
	}

	public void NewSession(Session newSession)
	{
		_sessionStorageRepository.AddSession(newSession);
	}
}