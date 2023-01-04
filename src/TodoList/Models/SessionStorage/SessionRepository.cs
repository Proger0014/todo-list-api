using TodoList.Extensions;

namespace TodoList.Models.SessionStorage;

public class SessionRepository
{
    private ApplicationContext _context;

	public SessionRepository(ApplicationContext applicationContext)
	{
		_context = applicationContext;
	}

	public Session GetSessionById(string sessionId)
	{
		var targetSession = _context.SessionStorage.SingleOrDefault(s => s.Id == sessionId);

		if (targetSession == null)
		{
			throw new Exception("Not found session");
		}

		return targetSession;
	}

	public void DeleteSessionById(Session session) 
	{
		_context.SessionStorage.Remove(session);
		_context.SaveChanges();
	}

	public void AddSession(Session newSession)
	{
		_context.Add(newSession);
		_context.SaveChanges();
	}

	public void ChangeSession(Session changedSession)
	{
		_context.SessionStorage
			.FirstOrDefault(s => s.Id == changedSession.Id)!
			.ChangeSession(changedSession);

		_context.SaveChanges();
	}
}