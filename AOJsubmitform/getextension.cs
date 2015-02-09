namespace AOJsubmitform
{
	public class GetExtension
	{
		public static string getExtension(string language)
		{
			switch (language) {
				case "C++":
				case "C++11":
					return @".cpp";
				case "C":
					return @".c";
				case "JAVA":
					return @".java";
				case "C#":
					return @".cs";
				case "D":
					return @".d";
				case "Ruby":
					return @".rb";
				case "Python":
				case "Python3":
					return @".py";
				case "PHP":
					return @".php";
				case "JavaScript":
					return @".js";
				default:
					return @".txt";
			}
		}
	}
}