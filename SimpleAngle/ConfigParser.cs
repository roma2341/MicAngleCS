using SimpleAngle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SimpleAngle
{
    class ConfigParser
    {
        public static List<Microphone> parseMicrophones(String testString)
        {
            //Звук(X:-1;y:1.0;D:1.0)

            //String micPattern = new String("^Звук[(]x:\\d+;y:\\d+;A:\\d+[)]$");
            //String testString = new String("Звук(x:0;y:10;А:10)");
            // String micPattern = new String("М[(][x,X]:(-?([0-9]+.)?[0-9]+);[y,Y]:(-?([0-9]+.)?[0-9]+);[d,D]:(-?([0-9]+.)?[0-9]+)[)]");
            String micPattern = "М[(][x,X]:(-?([0-9]+.)?[0-9]+);[y,Y]:(-?([0-9]+.)?[0-9]+)[)]";
            Regex newReg = new Regex(micPattern);
            MatchCollection matches = newReg.Matches(testString);
            bool isCorrect = false;
            double x = 0, y = 0, d = 0;
            List<Microphone> microphones = new List<Microphone>();
            foreach (Match m in matches)
            {
                isCorrect = true;
                x = Double.Parse(m.Groups[1].Value);
                y = Double.Parse(m.Groups[3].Value);
                microphones.Add(new Microphone(x, y));
            }
            microphones = microphones.OrderBy(m => m.X).ToList<Microphone>();
            if (!isCorrect) Console.Out.WriteLine("Невірні данні");
            return microphones;
            //Звук(x:0;y:10;А:10)

        }

        public static List<SoundEmiter> parseSoundEmiters(String testString)
        {
            //String micPattern = new String("^Звук[(]x:\\d+;y:\\d+;A:\\d+[)]$");
            //String testString = new String("Звук(x:0;y:10;А:10)");
            // String micPattern = new String("З[(][x,X]:(-?([0-9]+.)?[0-9]+);[y,Y]:(-?([0-9]+.)?[0-9]+);[a,A]:(-?([0-9]+.)?[0-9]+)[)]");
            String micPattern = "З[(][x,X]:(-?([0-9]+.)?[0-9]+);[y,Y]:(-?([0-9]+.)?[0-9]+);[a,A]:(-?([0-9]+.)?[0-9]+)[)]";
            Regex newReg = new Regex(micPattern);
            MatchCollection matches = newReg.Matches(testString);
            bool isCorrect = false;
            double x = 0, y, a;
            List<SoundEmiter> soundsList = new List<SoundEmiter>();
            foreach (Match m in matches)
            {
                isCorrect = true;
                x = Double.Parse(m.Groups[1].Value);
                y = Double.Parse(m.Groups[3].Value);
                a = double.Parse(m.Groups[5].Value);
                soundsList.Add(new SoundEmiter(x, y, a));
            }
            if (!isCorrect) Console.Out.WriteLine("Невірні данні");
            return soundsList;
            //Звук(x:0;y:10;А:10)

        }

        public static int? parseSamplingRate(String testString)
        {
            String micPattern = "[sS][aA][mM][pP][lL][iI][nN][gG][rR][aA][tT][eE][(]([0-9]+)[)]";
            Regex newReg = new Regex(micPattern);
            MatchCollection matches = newReg.Matches(testString);
            bool isCorrect = false;
            int? samplingRate = null;
            foreach (Match m in matches)
            {
                isCorrect = true;
                samplingRate = int.Parse(m.Groups[1].Value);
            }
            if (isCorrect)
            {
                return samplingRate;
            }
            else
            {
                Console.Out.WriteLine("Невірні данні");
                return null;
            }

        }



    }
}
