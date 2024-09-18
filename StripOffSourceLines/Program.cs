using System;
using System.Text.RegularExpressions;
using Azure.AI.TextAnalytics;
using Azure.AI;
using Azure;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using System.Runtime.InteropServices;

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

Visit Microsoft at https://www.microsoft.com

but check out this non-Microsoft link instead: https://www.example.com

";

        //string endpoint = "<YOUR_TEXT_ANALYTICS_ENDPOINT>";
        //string apiKey = "<YOUR_TEXT_ANALYTICS_API_KEY>";

		
        //AzureKeyCredential credential = new AzureKeyCredential(apiKey);

        //var credentials = new ApiKeyServiceClientCredentials(apiKey);
        //var client = new TextAnalyticsClient(credentials)
        //{
        //    Endpoint = endpoint
        //};


        string outputText = StripSourceLines(inputText);
		Console.WriteLine(outputText);
	}

	static string StripSourceLines(string inputText)
	{

        string endpoint = "https://paragaiservice.cognitiveservices.azure.com/";
        string apiKey = "b70652227398482a81cde45c35c0b190";

        var credential = new AzureKeyCredential(apiKey);
        var client = new TextAnalyticsClient(new Uri(endpoint), credential);

        // Regular expression to match source lines
        string pattern = @"\s*\[.*?\]";

		// Replace source lines with an empty string
		string result = Regex.Replace(inputText, pattern, string.Empty);

        var documents = new List<TextDocumentInput>()
        {
            new TextDocumentInput("1", result)
            {
                Language = "en"
            }
        };

        var finalResult = client.RecognizeEntitiesBatch(documents);

        var nonMicrosoftLinks = finalResult.Value.FirstOrDefault()?.Entities
            .Where(e => e.Category == EntityCategory.Url)
            .Select(e => e.Text)
            .Where(url => !url.Contains("microsoft.com"))
            .ToList();



        foreach (var link in nonMicrosoftLinks)
        {
            Console.WriteLine(link);
        }

        return result;
	}
}