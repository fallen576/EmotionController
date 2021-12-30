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

            foreach (Emotion emotions in emotData) {
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

            SerialComms.PortWrite(sortedList.First().Key);

            return Ok();
        }
    }
}
