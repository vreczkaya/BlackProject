using System.Collections.Generic;
using System.IO;

public class TextHandler
{
    private string[] paths = new string[2];

    private string[] readLines;

    public static Queue<string[]> NPCTextQueue;
    public static Queue<string[]> QuestTextQueue;

    private string separator = "_";

    public TextHandler()
    {
        paths[0] = "NPCPhrases.txt";
        paths[1] = "Quests.txt";

        NPCTextQueue = new Queue<string[]>();
        QuestTextQueue = new Queue<string[]>();

        ReadTextFromFile(paths[0]);
        PreparePhrasesQueue(ref NPCTextQueue);

        ReadTextFromFile(paths[1]);
        PreparePhrasesQueue(ref QuestTextQueue);
    }

    private void ReadTextFromFile(string path)
    {
        readLines = File.ReadAllLines(path);
    }

    private void PreparePhrasesQueue(ref Queue<string[]> textQueue)
    {
        int size = 0;
        for (int i = 0; i < readLines.Length; i++)
        {
            if (readLines[i] != separator)
            {
                size++;
            }
            else
            {
                string[] temp = new string[size];
                int index = i - size;
                for (int j = 0; j < size; j++, index++)
                {
                    temp[j] = readLines[index];
                }
                textQueue.Enqueue(temp);
                size = 0;
            }
        }
    }
}
