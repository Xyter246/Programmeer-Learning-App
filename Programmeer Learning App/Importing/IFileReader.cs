namespace Programmeer_Learning_App.Importing;

public interface IFileReader
{
    /// <summary>
    /// Creates a Program instance by reading the selected file (via OpenFileDialog).
    /// </summary>
    /// <returns>A Program instance, or Null if there was a wrong filepath.</returns>
    static abstract List<CommandLabel>? Readfile();
}
