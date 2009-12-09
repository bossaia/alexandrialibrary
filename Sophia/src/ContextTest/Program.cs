using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;

namespace Sophia.ContextTest
{
	public class Program
	{
		private const string Id = "context";
		private const string Localhost = ".";
		private static readonly IList<string> Children = new List<string>();

		static int Main(string[] args)
		{
			if (args == null || args.Length < 1)
			{
				Console.WriteLine("Usage:\n\nContextTest <id1> <id2> <id3>...");
				return 1;
			}

			foreach (var name in args)
			{
				Children.Add(name);
				Console.WriteLine("Child: {0}", name);
			}

			Console.WriteLine("\n\nContext is ready...");

			while (true)
			{
				Console.WriteLine("...");

				Receive();

				Thread.Sleep(500);
			}
		}

		private static void Receive()
		{			
			try
			{
				using (var pipeClient = new NamedPipeClientStream(Localhost, Id, PipeDirection.In))
				{

					// Connect to the pipe or wait until the pipe is available.
					Console.Write("Attempting to connect to pipe...");
					pipeClient.Connect(5000);

					Console.WriteLine("Connected to context.");
					Console.WriteLine("There are currently {0} node connections.", pipeClient.NumberOfServerInstances);

					using (var reader = new StreamReader(pipeClient))
					{
						// Display the read text to the console
						string message;
						while ((message = reader.ReadLine()) != null)
						{
							Console.WriteLine("Received from node: {0}", message);
							
							if (!string.IsNullOrEmpty(message))
							{
								Console.WriteLine("Sending to children");


								var sender = message.Substring(0, 1);
								foreach (var child in Children)
								{
									if (child != sender)
									{
										Send(child, message);

										Console.WriteLine("Send to {0}: {1}", child, message);
									}
								}	
							}
						}
					}

					Console.WriteLine("Nothing more received from node");
				}
			}
			catch (TimeoutException)
			{
				Console.WriteLine("Timeout waiting to receive from node");
			}
			catch (IOException ex)
			{
				Console.WriteLine("Error Receiving: {0}", ex.Message);
			}
			//Console.Write("Press Enter to continue...");
			//Console.ReadLine();
		}

		private static void Send(string id, string message)
		{
			try
			{
				using (var pipeServer = new NamedPipeServerStream(id, PipeDirection.Out))
				{
					Console.WriteLine("NamedPipeServerStream object created.");

					// Wait for a client to connect
					Console.Write("Waiting for context connection...");
					pipeServer.WaitForConnection();
					//BeginWaitForConnection(new AsyncCallback(CheckForClient), null);
					//WaitForConnection();

					Console.WriteLine("Writing to child node: {0}", message);

					// Read user input and send that to the client process.
					using (var writer = new StreamWriter(pipeServer))
					{
						writer.AutoFlush = true;
						//Console.Write("Enter message: ");
						writer.WriteLine(message); //Console.ReadLine()
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error Sending: {0}", ex.Message);
			}
		}
	}
}
