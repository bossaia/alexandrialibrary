#region License (MIT)
/***************************************************************************
 *  Copyright (C) 2007 Dan Poage
 ****************************************************************************/

/*  THIS FILE IS LICENSED UNDER THE MIT LICENSE AS OUTLINED IMMEDIATELY BELOW: 
 *
 *  Permission is hereby granted, free of charge, to any person obtaining a
 *  copy of this software and associated documentation files (the "Software"),  
 *  to deal in the Software without restriction, including without limitation  
 *  the rights to use, copy, modify, merge, publish, distribute, sublicense,  
 *  and/or sell copies of the Software, and to permit persons to whom the  
 *  Software is furnished to do so, subject to the following conditions:
 *
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 *  FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
 *  DEALINGS IN THE SOFTWARE.
 */
#endregion

using System;
using System.Collections.Generic;

using Alexandria;
using Alexandria.Media;
using Alexandria.Media.IO;

namespace Alexandria.Console
{
	class Program
	{	
		static void Main(string[] args)
		{
			CommandRunner runner = new CommandRunner();
			
			WriteHeader();
			
			while(runner.CurrentContext.IsOpen)
			{
				runner.Reset();
				runner.Prompt();
				runner.ParseInput();
				runner.Run();
				
				WriteBody();
			}
			
			WriteFooter();
		}
		
		private static void WriteHeader()
		{
			System.Console.WriteLine("Alexandria Media Library - Command Line Client v. 1.0.0.0");
			System.Console.WriteLine("© 2007 Dan Poage");
			System.Console.WriteLine();
		}
		
		private static void WriteBody()
		{
			System.Console.WriteLine();
		}
		
		private static void WriteFooter()
		{
			System.Console.WriteLine("Closing...");
		}
	}
}
