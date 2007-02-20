using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace AlexandriaOrg.Alexandria.LastFM
{
	/// <summary>
	/// A blocking TCP client for sending and receiving Last.fm data
	/// </summary>
	public class BlockingClient : IDisposable
	{
		#region Constructors
		public BlockingClient()
		{			
		}
		#endregion
		
		#region IDisposable Members
		~BlockingClient()
		{
			Dispose(false);
		}
		
		protected void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					// Cleanup managed resources here
					if (socket != null)
					{
						socket.Close();
						socket = null;
					}
				}
			}
			disposed = true;
		}
		
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
		
		#region Constants
		private const int bufferSize = 1024;
		private const int portsToStep = 5;
		#endregion
		
		#region Private Fields
		private bool disposed;
		private System.Net.Sockets.Socket socket;
		private bool doPortStepping = true;		
		private int actualPort;
		#endregion
		
		#region Private Methods
		
		#region ConnectToSocket
		public Socket ConnectToSocket(IPAddress address, int port)
		{
			Socket socket;
			actualPort = port;

			try
			{
				socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

				if (doPortStepping)
				{
					bool connected = false;

					while (!connected && actualPort <= port + portsToStep)
					{
						try
						{
							socket.Connect(address, actualPort);

							// This port was available
							connected = true;
						}
						catch (System.Net.Sockets.SocketException ex)
						{
							// If there are ports left, try the next port
							// otherwise rethrow the exception
							if (actualPort < port + portsToStep)
								actualPort++;
							else throw ex;
						}
					}
				}
				else
				{
					socket.Connect(address, port);
				}
			}
			catch (Exception ex)
			{
				throw new ApplicationException("Could not connect to host: " + address.ToString(), ex);
			}

			return socket;

			#region C++ Example
			/*
			sockaddr_in sinRemote;
			sinRemote.sin_family = AF_INET;
			sinRemote.sin_addr.s_addr = nRemoteAddr;

			mnLastPort = nPort;
			sinRemote.sin_port = htons(mnLastPort);

			int nCode = connect(sd, (sockaddr*)&sinRemote, sizeof(sockaddr_in));
		    
			if (mbDoPortstepping)
			{
				// Do port stepping. Note that this happens regardless of the error
				// condition so if the error is something other than not finding the
				// port, it will iterate and then throw the exception.
				while (nCode == SOCKET_ERROR && mnLastPort <= (nPort + kPortsToStep))
				{
					sinRemote.sin_port = htons(++mnLastPort);
					nCode = connect(sd, (sockaddr*)&sinRemote, sizeof(sockaddr_in));
				}
			}
			*/
			#endregion
		}
		#endregion
		
		#endregion
		
		#region Public Methods
		
		#region Connect
		public int Connect(string host, int port)
		{
			IPAddress[] addresses = Dns.GetHostAddresses(host);
			if (addresses.Length > 0)
			{				
				socket = ConnectToSocket(addresses[0], port);
				
				return actualPort;
			}
			else throw new ApplicationException("Could not lookup host: " + host);
		}
		#endregion
		
		#region Send
		public void Send(byte[] data)
		{
			try
			{
				int bytesToSend = data.Length;
				int totalBytesSent = 0;
			
				while (totalBytesSent < bytesToSend)
				{
					int bytesSent = socket.Send(data, bytesToSend - totalBytesSent, SocketFlags.None);
					
					if (bytesSent == 0)
						throw new ApplicationException("Could not send data through socket");
						
					totalBytesSent += bytesSent;
				}
			}
			catch (SocketException ex)
			{
				
				throw new ApplicationException("Could not send data through socket", ex);
			}
			
			#region C++ Example
			/*
			int nBytesToSend = static_cast<int>(sData.size());
			int nTotalBytesSent = 0;

			while (nTotalBytesSent < nBytesToSend)
			{
				// Should never block
				int nBytesSent = send(mSocket,
									  sData.c_str(),
									  nBytesToSend - nTotalBytesSent,
									  0);

				if (nBytesSent == SOCKET_ERROR || nBytesSent == 0)
				{
					int nErrorCode = WSAGetLastError();
					ostringstream osErr;
					osErr << "Sending of data through socket failed. Socket error: " << nErrorCode;
					throw NetworkException(osErr.str());
				}

				// Send successful
				nTotalBytesSent += nBytesSent;
		        
			} // end while
			*/
			#endregion
		}
		#endregion
		
		#region Receive
		public string Receive()
		{
			string data = string.Empty;
		
			byte[] readBuffer = new byte[bufferSize];
			int bytesRead;
			int readSize = bufferSize - 1;
			bool keepGoing = true;
			
			try
			{
				do
				{
					bytesRead = socket.Receive(readBuffer, readSize, SocketFlags.None);
					
					if (bytesRead == 0)
						throw new ApplicationException("Could not receive data through socket");
					
					// Is the last character read a carriage return?
					if (readBuffer[bytesRead - 1] == '\n')
					{
						bytesRead--;
						
						// We're done reading
						keepGoing = false;
					}
					
					readBuffer[bytesRead] = Convert.ToByte('\0');
					
					// Append the read buffer data to
					data += readBuffer.ToString();
				}
				while (keepGoing);
			}
			catch (SocketException ex)
			{
				throw new ApplicationException("Could not receive data through socket", ex);
			}
		
			return data;
		
			#region C++ Example
			/*
			// Read data from server.
			char acReadBuffer[kBufferSize];
			int nReadSize = kBufferSize - 1; // so that we can append a \0
			int nBytesRead;

			// Go into loop that keeps going until a CR is received
			bool bKeepGoing = true;
			do
			{
				// Will block until there is data available or timeout is reached
				nBytesRead = recv(mSocket, acReadBuffer, nReadSize, 0);

				if (nBytesRead == SOCKET_ERROR || nBytesRead == 0)
				{
					// This happens when something goes wrong, client dies or something.
					// Caller should close talk socket and recover.
					int nErrorCode = WSAGetLastError();
					ostringstream osErr;
					osErr << "Receiving data through socket failed. Socket error: " << nErrorCode;
					sLine = "";
					throw NetworkException(osErr.str());
				}

				// Read successful, is last char a CR?
				if (acReadBuffer[nBytesRead - 1] == '\n')
				{
					// Step back to before \n
					nBytesRead--;
		            
					// If so, we're done
					bKeepGoing = false;
				}

				acReadBuffer[nBytesRead] = '\0';

				sLine += acReadBuffer;

			} while (bKeepGoing);
			*/
			#endregion
		}
		#endregion
		
		#region ShutDown
		public void ShutDown()
		{
			try
			{
				// Disallow any further data sends. This will tell the other side
				// that we want to go away.
				socket.Shutdown(SocketShutdown.Send);
								
				// Receive any extra data still sitting on the socket. After all
				// data is received, this call will block until the remote host
				// acknowledges the TCP control packet sent by the shutdown above.
				// Then we'll get a 0 back from recv, signalling that the remote
				// host has closed its side of the connection.
				byte[] readBuffer = new byte[bufferSize];
				while(true)
				{
					int bytesRead = socket.Receive(readBuffer, bufferSize, SocketFlags.None);
					if (bytesRead > 0)
						throw new ApplicationException("Received " + bytesRead.ToString() + " unexpected bytes during shutdown");
					else
						break;
				}
				
				try
				{
					// Close the socket
					socket.Close();
					socket = null;
				}
				catch (SocketException ex)
				{
					throw new ApplicationException("Could not close socket", ex);
				}
			}
			catch (SocketException ex)
			{
				throw new ApplicationException("Could not shutdown socket", ex);
			}
		
			#region C++ Example
			/*
			// Disallow any further data sends. This will tell the other side
			// that we want to go away.
			int nResult = shutdown(mSocket, SD_SEND);

			if (nResult == SOCKET_ERROR)
			{
				// Even though this is an unforeseen failures, we don't want to
				// throw as we're shutting down. Just carry on shutting down
				// best we can.
				int nErrorCode = WSAGetLastError();
				ostringstream osErr;
				osErr << "Socket shutdown failed. Socket error: " << nErrorCode;
				LOG(1, osErr.str() << "\n");
			}

			// Receive any extra data still sitting on the socket. After all
			// data is received, this call will block until the remote host
			// acknowledges the TCP control packet sent by the shutdown above.
			// Then we'll get a 0 back from recv, signalling that the remote
			// host has closed its side of the connection.
			char acReadBuffer[kBufferSize];
			while (true)
			{
				int nBytesRead = recv(mSocket, acReadBuffer, kBufferSize, 0);

				if (nBytesRead == SOCKET_ERROR)
				{
					// I don't think we care here if the call failed.
					// Just carry on shutting down best we can.
					int nErrorCode = WSAGetLastError();
					ostringstream osErr;
					osErr << "Waiting for shutdown confirmation failed. Socket error: " << nErrorCode << "\n";
					LOG(1, osErr.str() << "\n");
					break;
				}
				else if (nBytesRead != 0)
				{
					string sTemp;
					sTemp.append(acReadBuffer, nBytesRead);
					ostringstream osErr;
					osErr << "Received " << nBytesRead <<
							 " unexpected bytes during shutdown:\n" << sTemp.c_str();
					LOG(1, osErr.str() << "\n");
				}
				else
				{
					// Returned 0, what we expect
					break;
				}
			}

			// Close the socket
			nResult = closesocket(mSocket);

			if (nResult == SOCKET_ERROR)
			{
				// Again, we don't want to throw. Log the error and continue.
				int nErrorCode = WSAGetLastError();
				ostringstream osErr;
				osErr << "Couldn't close socket. Socket error: " << nErrorCode;
				LOG(1, osErr.str() << "\n");
			}
			*/
			#endregion
		}
		#endregion
		
		#endregion
	}
}