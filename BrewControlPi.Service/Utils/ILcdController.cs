namespace BrewControlPi.Service.Utils
{
	public interface ILcdController
	{
		void Write(int x, int y, char[] data);

		void Clear();
	}
}
