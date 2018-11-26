namespace BrewControlPi.Service.Core.Services
{
	public interface IPinService
	{
		bool GetStatus(int id);
		
		void UpdateStatus(int id, int status);
	}
}
