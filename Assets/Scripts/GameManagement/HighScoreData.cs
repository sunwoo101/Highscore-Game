/*
Saves the top 3 scores
*/
public class HighScoreData
{
    public string[] names;
    public int[] highScores;

    public HighScoreData()
    {
        names = new[] { "None", "None", "None" };
        highScores = new[] { 0, 0, 0 };
    }

    public HighScoreData(string[] names, int[] highScores)
    {
        this.names = names;
        this.highScores = highScores;
    }
}
