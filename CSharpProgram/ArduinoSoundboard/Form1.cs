using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using WMPLib;

namespace ArduinoSoundboard
{
    public partial class Houckboard : Form
    {
        WindowsMediaPlayer mediaPlayer = new WindowsMediaPlayer();
        JObject soundsFile;
        public Houckboard()
        {
            InitializeComponent();
            mediaPlayer.settings.volume = 10;
            string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (!Directory.Exists(Path.Combine(appdataPath, "houckboard")))
            {
                string newDirectory = Path.Combine(appdataPath, "houckboard");
                Directory.CreateDirectory(newDirectory);
                File.Create(Path.Combine(appdataPath, "houckboard", "sounds.json"));
            }
            Console.WriteLine(Path.Combine(appdataPath, "houckboard", "sounds.json"));
            
            soundsFile= JObject.Parse(File.ReadAllText(Path.Combine(appdataPath, "houckboard", "sounds.json")));
            if(soundsFile["sound1"] != null)
            {
                this.button1.Text = Path.GetFileName((string)soundsFile["sound1"]);
            }
            if (soundsFile["sound2"] != null)
            {
                this.button2.Text = Path.GetFileName((string)soundsFile["sound2"]);
            }
            if (soundsFile["sound3"] != null)
            {
                this.button3.Text = Path.GetFileName((string)soundsFile["sound3"]);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            doFileDialog(this.button1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            doFileDialog(this.button2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            doFileDialog(this.button3);
        }

        private void doFileDialog(Button btn)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Title = "Select an audio file";
            openFile.Filter = "Audio Files|*.mp3;*.ogg;*.wav;*.aac";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                string text = Path.GetFileName(openFile.FileName);
                JObject newJson;
                if(btn == button1)
                {
                    newJson = new JObject(new JProperty("sound1", openFile.FileName));
                }
                else if(btn == button2)
                {
                    newJson = new JObject(new JProperty("sound2", openFile.FileName));
                }
                else
                {
                    newJson = new JObject(new JProperty("sound3", openFile.FileName));
                }

                soundsFile.Merge(newJson);
                string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "houckboard", "sounds.json");
                File.WriteAllText(savePath, soundsFile.ToString());

                using (StreamWriter file = File.CreateText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "houckboard", "sounds.json")))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    soundsFile.WriteTo(writer);
                }

                btn.Text = text;
            }
        }


        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort = (SerialPort)sender;

            string input = serialPort.ReadLine();
            string url;

            if (input.StartsWith("sound1"))
            {
                try
                {
                    mediaPlayer.URL = (string)soundsFile["sound1"];
                    mediaPlayer.controls.stop();
                    mediaPlayer.controls.play();
                } catch (Exception ex)
                {
                    //lol
                }
            } else if (input.StartsWith("sound2"))
            {
                try
                {
                    mediaPlayer.URL = (string)soundsFile["sound2"];
                    mediaPlayer.controls.stop();
                    mediaPlayer.controls.play();
                } catch (Exception ex)
                {
                    //lol
                }
            } else if (input.StartsWith("sound3"))
            {
                try
                {
                    mediaPlayer.URL = (string)soundsFile["sound3"];
                    mediaPlayer.controls.stop();
                    mediaPlayer.controls.play();
                } catch (Exception ex)
                {
                    //lol
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serialPort1.Open();
        }


    }
}
