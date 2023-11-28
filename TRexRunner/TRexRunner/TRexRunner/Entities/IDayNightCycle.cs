namespace TRexRunner.Entities
{
	public interface IDayNightCycle
	{
		int NightCount { get; }
		bool IsNight { get; }
	}
}
