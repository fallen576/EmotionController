using System;
using System.IO.Ports;

namespace EmotionController
{
    public class SerialComms
    { 

        private static SerialPort _port = new SerialPort();
        public static void Init()
        {

            _port.BaudRate = 9600;
            //_port.PortName = "COM3"; windows
            _port.PortName = @"/dev/cu.usbmodem14201"; //mac
            Console.WriteLine(_port.PortName + " " + _port.IsOpen);
            _port.Open();
            Console.WriteLine(_port.PortName + " " + _port.IsOpen);
        }

        public static void PortWrite(string data)
        {
            _port.Write(data);
        }

        public static void PrintPorts()
        {
            foreach (string s in SerialPort.GetPortNames())
            {
                Console.WriteLine("Port: " + s);
            }
        }
    }
}