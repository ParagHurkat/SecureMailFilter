using System;
using System.Text.RegularExpressions;

class Program
{
	static void Main()
	{
		string inputText = @"
0:000> k
# Child-SP          RetAddr               Call Site
00 0000006c`1227f9e8 00007ff9`efbc6d1f     ntdll!ZwWaitForSingleObject+0x14 [d:\rs1.obj.amd64fre\minkernel\ntdll\daytona\objfre\amd64\usrstubs.asm @ 211]
01 0000006c`1227f9f0 00007ff9`e83718ac     KERNELBASE!WaitForSingleObjectEx+0x8f [d:\rs1\minkernel\kernelbase\synch.c @ 1328]
02 (Inline Function) --------`--------     w3wphost!WP_IPM::WaitForShutdown+0xd [d:\rs1\inetsrv\iis\iisrearc\iisplus\w3wphost\wpipm.cxx @ 1588]
03 (Inline Function) --------`--------     w3wphost!W3WP_HOST::WaitForShutdown+0x16 [d:\rs1\inetsrv\iis\iisrearc\iisplus\w3wphost\w3wphost.cxx @ 4125]
04 0000006c`1227fa90 00007ff7`b8c9156e     w3wphost!AppHostInitialize+0x14c [d:\rs1\inetsrv\iis\iisrearc\iisplus\w3wphost\w3wphost.cxx @ 548]
05 0000006c`1227fad0 00007ff7`b8c92dcd     w3wp!wmain+0x412 [d:\rs1\inetsrv\iis\iisrearc\iisplus\w3wp\w3wp.cxx @ 461]
06 0000006c`1227fc70 00007ff9`f31284d4     w3wp!__wmainCRTStartup+0x14d [d:\rs1\minkernel\crts\crtw32\dllstuff\crtexe.c @ 692]
07 0000006c`1227fcb0 00007ff9`f35b1791     kernel32!BaseThreadInitThunk+0x14 [d:\rs1\base\win32\client\thread.c @ 64]
08 0000006c`1227fce0 00000000`00000000     ntdll!RtlUserThreadStart+0x21 [d:\rs1\minkernel\ntdll\rtlstrt.c @ 997]
Some normal text that should not be removed.
";

		string outputText = StripSourceLines(inputText);
		Console.WriteLine(outputText);
	}

	static string StripSourceLines(string inputText)
	{
		// Regular expression to match source lines
		string pattern = @"\s*\[.*?\]";

		// Replace source lines with an empty string
		string result = Regex.Replace(inputText, pattern, string.Empty);

		return result;
	}
}