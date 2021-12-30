using System;
using System.Collections.Generic;
using System.Linq;

namespace EmotionController.Models
{
    public class Emotion
    {
        public double angry {get; set;}
        public double disgusted {get; set;}
        public double fearful {get; set;}
        public double happy {get; set;}
        public double neutral {get; set;}
        public double sad {get; set;}
        public double surprised {get; set;}
    }

    public class EmotionData
    {
        public List<Emotion> Emotions;
    }
}
