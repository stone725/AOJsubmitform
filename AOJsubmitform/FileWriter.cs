using System.IO;
using System.Text;

namespace AOJsubmitform
{
	public class FileWriter
	{
		public void Write(string directoryName, string fileName, string text)
		{
			if (File.Exists(directoryName) == false)
			{
				Directory.CreateDirectory(directoryName);
			}
			StreamWriter sourceCodeFileWriter = new StreamWriter(directoryName + fileName, false, Encoding.Default);
			sourceCodeFileWriter.Write(text);
			sourceCodeFileWriter.Close();
		} 
	}
}