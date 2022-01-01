using EmotionController.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using System;

namespace EmotionController.Controllers
{
    [ApiController]
    [Route("/api")]
    public class ApiController : Controller
    {
        [Route("calc/emotion")]
        [HttpPost]
        public IActionResult CalculateEmotion(List<Emotion> emotData)
        {
            /*
             
               [
                {
                    "angry" : ".9104",
                    "disgusted" : ".99",
                    "fearful" : "11.520",
                    "happy" : "10.852",
                    "neutral": "10.23",
                    "sad": "5.541",
                    "surprised" : "0.0154",
                },
                {
                    "angry" : ".9104",
                    "disgusted" : ".99",
                    "fearful" : "11.520",
                    "happy" : "10.852",
                    "neutral": "10.23",
                    "sad": "5.541",
                    "surprised" : "0.0154",
                },
                ]
             
             
             */

            List<double> sad = new List<double>();
            List<double> happy = new List<double>();
            List<double> angry = new List<double>();
            List<double> neutral = new List<double>();
            Console.WriteLine("we here???");
            foreach (Emotion emotions in emotData) {
                Console.WriteLine("!!!! "  + emotions);
                sad.Add(emotions.sad);
                happy.Add(emotions.happy);
                angry.Add(emotions.angry);
                neutral.Add(emotions.neutral);
            }

            double sadCount = sad.Sum();
            double happyCount = happy.Sum();
            double angryCount = angry.Sum();
            double neutralCount = neutral.Sum();

            Dictionary<string, double> emotionTotals = new Dictionary<string, double>();
            emotionTotals.Add("sad", sadCount);
            emotionTotals.Add("happy", happyCount);
            emotionTotals.Add("angry", angryCount);
            emotionTotals.Add("neutral", neutralCount);

            var sortedDict = from entry in emotionTotals orderby entry.Value descending select entry;
            var sortedList = sortedDict.ToList();
            Console.WriteLine(sortedList.First().Key + " " + sortedList.First().Value + " " + sortedList.Last().Key + " " + sortedList.Last().Value);
            //"E:sad-V:.9090E:angry-V:.3875"
            //msg.split("E:")[1].split("-V:")[0] -> sad
            //msg.split("-V:")[1].split("E:")[0] -> .9090
            //msg.split("E:")[2].split("-V:")[0] -> angry
            //msg.split("-V:")[1].split("E:")[1] -> .3875
            string msg = "";
            int itr = 0;
            foreach (var i in sortedList) {
                if (itr == 2) {
                    break;
                }
                msg += $"E{i.Key}V{i.Value}";
                itr++;
            }
            //string msg = $"E:{sortedList.First().key}-V:{sortedList.First().Value}E:{sortedList.ElementAt(1).key}-V:{sortedList.ElementAt(1).Value}";
            Console.WriteLine(msg);
            //SerialComms.PortWrite(sortedList.First().Key);
            SerialComms.PortWrite(msg);

            return Ok();
        }
    }
}
