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
               Example data from front end.
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

            //implement super impressive algorithm that google would use on interviews. /s
            //basically taking all decimal values from each emotion and putting them into their own list
            //from there sum the contents of each array so you have one numerical value for each emotion, biggest number wins.
            List<double> sad = new List<double>();
            List<double> happy = new List<double>();
            List<double> angry = new List<double>();
            List<double> neutral = new List<double>();

            foreach (Emotion emotions in emotData) {
                Console.WriteLine("Emotion values: "  + emotions);
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

            //Console.WriteLine(sortedList.First().Key + " " + sortedList.First().Value + " " + sortedList.Last().Key + " " + sortedList.Last().Value);
            
            //only going to send the top 2 emotions because my LCD can't fit much more than that.
            string msg = "";
            int itr = 0;
            foreach (var i in sortedList) {
                if (itr == 2) {
                    break;
                }
                msg += $"E{i.Key}V{i.Value}";
                itr++;
            }

            //send to Arduino via serial communication
            //Console.WriteLine(msg);
            SerialComms.PortWrite(msg);

            return Ok();
        }
    }
}
