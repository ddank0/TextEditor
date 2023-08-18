// using System.IO; // to use StreamWriter
using System.Diagnostics; //to use linux bash(shellExec)

namespace TextEditor;

class Program
{
	public static string savePath = "/mnt/c/dev/c#/TextEditor/saves/"; //global variable to static method

	static void Main(string[] args)
	{
		Menu();
	}

	static void Menu() {

		Console.Clear();
		Console.WriteLine("\n-----------------------------------------");
		Console.WriteLine("|	O que você deseja fazer?  	|");
		Console.WriteLine("|	1 - Abrir arquivo 		|");
		Console.WriteLine("|	2 - Criar novo arquivo		|");
		Console.WriteLine("|	0 - sair			|");
		Console.WriteLine("-----------------------------------------");
		Console.Write("===> ");

		short option =  short.Parse(Console.ReadLine());

		switch (option) {

			case 0: 
				System.Environment.Exit(0);
				break;
			case 1:
				OpenFile();
				break;
			case 2:
				MakeFile();
				break;
			default:
				Menu();
				break;
		}
	}

	static void OpenFile() {

		Console.Clear();
		Console.WriteLine("Digite o nome do arquivo que deseja editar:");
		string fileName = Console.ReadLine();
		
		using(var file = new StreamReader($"{savePath}{fileName}.txt")) {
			string text = file.ReadToEnd();
			Console.WriteLine(text);
		}
		Console.WriteLine("");
		Console.ReadLine();
		
		Menu();
	}

	static void MakeFile() {

		Console.Clear();
		Console.WriteLine("Digite seu texto abaixo: (ESC para sair)");
		Console.WriteLine("------------------------");
		string text = "";
		// Console.Read();

		do {
			text += Console.ReadLine();
			text += Environment.NewLine;
		}
		while(Console.ReadKey().Key != ConsoleKey.Escape);

		// Console.Write(text);
		SaveFile(text);

	}

	static void SaveFile(string text) {

		Console.Clear();

		Console.WriteLine("Digite o nome do arquivo de texto:");
		Console.Write("===>");
		string fileName = Console.ReadLine();

		if (!Directory.Exists(savePath)) {

			Shellexec($"rm -r {savePath.TrimEnd('/')} 2> /dev/null");
			DirectoryInfo dir = Directory.CreateDirectory(savePath);	
			Console.WriteLine("Pasta criada com sucesso em {0}.", Directory.GetCreationTime(savePath));
		}
		
		//Add validation if file exists
		//here
		
		//Close files automatically
		using(var file = new StreamWriter($"{savePath}{fileName}.txt")) {

			file.WriteLine(text);
		}
		Console.WriteLine("Arquivo salvo com sucesso!");
		Thread.Sleep(2500);
		Menu();
	}

	static void Shellexec(string comando) {

		// Criação de um processo
		Process processo 				= new Process();

		processo.StartInfo.FileName 			= "/bin/bash"; 		// Shell no Linux
		processo.StartInfo.Arguments 			= $"-c \"{comando}\""; 	// Passando o comando como argumento
		processo.StartInfo.RedirectStandardOutput 	= true; 		// Redirecionando a saída padrão
		processo.StartInfo.UseShellExecute 		= false; 		// Não usar o shell padrão
		processo.StartInfo.CreateNoWindow 		= true; 		// Não criar janela de console

		// Iniciar o processo e aguardar a conclusão
		processo.Start();
		// string resultado = processo.StandardOutput.ReadToEnd();
		processo.WaitForExit();

		// Exibir a saída do comando
		// Console.WriteLine(resultado);
	}
}
