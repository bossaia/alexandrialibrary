using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using Sophia.Core;

namespace Sophia.NodeTest
{
	public class Program
	{
		private const string Context = "context";
		private const string Localhost = ".";
		private static string _id;

		static int Main(string[] args)
		{
			if (args == null || args.Length < 1)
			{
				Console.WriteLine("Usage:\n\nNodeTest <id>");
				return 1;
			}

			_id = args[0];

			Console.WriteLine("Node {0} ready...", _id);

			while (true)
			{
				Console.WriteLine("...");

				Receive();

				Send();

				Thread.Sleep(1000);
			}
		}

		private static void Send()
		{
			try
			{
				using (var pipeServer = new NamedPipeServerStream(Context, PipeDirection.Out))
				{
					//Console.WriteLine("NamedPipeServerStream object created.");

					// Wait for a client to connect
					//Console.Write("Waiting for context connection...");
					pipeServer.WaitForConnection();
					//Console.WriteLine("Context listening");

					// Read user input and send that to the client process.
					using (var writer = new StreamWriter(pipeServer))
					{
						writer.AutoFlush = true;
						Console.Write("Send: ");
						writer.WriteLine(Console.ReadLine());
					}
				}
			}
			catch (IOException) // ex)
			{
				//Console.WriteLine("Error: {0}", ex.Message);
			}			
		}

		private static void Receive()
		{
			using (var pipeClient = new NamedPipeClientStream(Localhost, _id, PipeDirection.In))
			{
				try
				{
					// Connect to the pipe or wait until the pipe is available.
					//Console.Write("Attempting to receive from context...");
					pipeClient.Connect(5000);

					//Console.WriteLine("Connected to context.");
					//Console.WriteLine("There are currently {0} context connections.", pipeClient.NumberOfServerInstances);

					using (var reader = new StreamReader(pipeClient))
					{
						// Display the read text to the console
						string message;
						while ((message = reader.ReadLine()) != null)
						{
							Console.WriteLine("Receive: {0}", message);
						}
					}

					//Console.WriteLine("Nothing more received from context");
				}
				catch (TimeoutException)
				{
					//Console.WriteLine("Timeout waiting to receive from context");
				}
			}
		}
	}
}
