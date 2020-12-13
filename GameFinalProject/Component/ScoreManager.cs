using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameFinalProject
{
    public class ScoreManager
    {
        private static string fileName = "scores.txt";
        public List<Score> Highscores { get; private set; }
        public List<Score> Scores { get; private set; }

        public ScoreManager()
            : this(new List<Score>())
        {

        }

        public ScoreManager(List<Score> scores)
        {
            Scores = scores;
            UpdateHighScores();
        }

        public void Add(Score score)
        {
            Scores.Add(score);

            //Scores = Scores.OrderByDescending(c => c.Value).ToList();

            //UpdateHighScores();
        }

        public static string Load()
        {
            if (!File.Exists(fileName))
            {
                using (StreamWriter writer = new StreamWriter(new FileStream(fileName, FileMode.Append)))
                {

                }
                //return new ScoreManager();
            }
            string loadedScore = "";

            using (StreamReader reader = new StreamReader(new FileStream(fileName, FileMode.Open)))
            {
                //var serilizer = new XmlSerializer(typeof(List<Score>));

                //var scores = (List<Score>)serilizer.Deserialize(reader);
                while (!reader.EndOfStream)
                {
                    loadedScore += reader.ReadLine() + "\n";
                }

                return loadedScore;
            }
        }

        public void UpdateHighScores()
        {
            Highscores = Scores.Take(5).ToList();
        }

        public static void Save(string scoreName)
        {
            using(StreamWriter writer = File.AppendText(fileName))
            {
                writer.WriteLine(scoreName);
                //var serilizer = new XmlSerializer(typeof(List<Score>));

                //serilizer.Serialize(writer, scoreManager.Scores);
            }
        }
    }
}
