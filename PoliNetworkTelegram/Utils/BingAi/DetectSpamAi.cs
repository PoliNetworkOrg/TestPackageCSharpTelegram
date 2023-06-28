using BingChat;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace SampleNuGet.Utils.BingAi;

[PublicAPI]
[Serializable]
[JsonObject(MemberSerialization.Fields)]
public class DetectSpamAi
{
    public static (bool?, string) DetectSpam(string messageToCheck)
    {
        // Construct the chat client
        var client = new BingChatClient(new BingChatClientOptions
        {
            // Tone used for conversation
            Tone = BingChatTone.Balanced
        });

        var message =
            "Do you think this message is scam/spam? Be strict and give me a yes/no answer, only yes or no.\n\n\n----\n\n" +
            messageToCheck;
        var answer = AskClientBing(client, message);
        var trueAnswer = DetectIfTrue(answer);

        return (trueAnswer, answer);
    }

    public static int CountSubstringOccurrences(string mainString, string subString)
    {
        var count = 0;
        var index = 0;

        while ((index = mainString.IndexOf(subString, index, StringComparison.OrdinalIgnoreCase)) != -1)
        {
            count++;
            index += subString.Length;
        }

        return count;
    }


    private static bool? DetectIfTrue(string answer)
    {
        if (string.IsNullOrEmpty(answer))
            return false;

        answer = answer.ToLower();
        var cannot = CountSubstringOccurrences(answer, "cannot confirm");
        if (cannot > 0)
            return null;

        var cannot2 = CountSubstringOccurrences(answer, "couldn't find");
        if (cannot2 > 0)
            return null;


        var xYes = CountSubstringOccurrences(answer, "yes");
        var xYeah = CountSubstringOccurrences(answer, "yeah");
        var xNo = CountSubstringOccurrences(answer, "no");

        return xYeah + xYes > xNo;
    }

    private static string AskClientBing(BingChatClient client, string message)
    {
        var answerTask = client.AskAsync(message);
        answerTask.Wait();
        var answer = answerTask.Result;
        return answer;
    }
}