using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail; // pour la partie mail
using System.IO; // pour la communication en serie sur l'arduino
using System.IO.Ports; // pour la meme chose

namespace sketch_jun25a // nom du fichier arduino sur mon ordinateur et du dossier dans lequel il est
{
    class Program
    {
        static void Main(string[] args)
        {
            /*partie permettant la com en serie avc Arduino*/
            SerialPort myport = new SerialPort();
            myport.Baudrate = 9600;
            myport.PortName = "COM4"; // nom du port usb utilisé pour mon arduino
            myport.Open();

            /*partie pour l'envoi du mail*/
            // (From, To, Header mail , Body mail)
            MailMessage mail = new MailMessage (    "oussama.laifi@gmail.com", 
                                                    "oussama.laifi@gmail.com"
                                                    "intrusion a votre domicile"
                                                    "intrusion potentielle detectée par le systeme");
            Smtpclient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587; // c'est le port SMTP couramment utilisé pour les connex sécurisées
            // Login et mdp du compte gmail fourni
            client.Credentials = new System.Net.NetworkCredential(  "oussama.laifi@gmail.com",
                                                                    "motdepasse du mail")
            client.EnableSsl = true;

            while (true)
            {
                String data_rx = myport.ReadLine ; // lis le port serie

                if(Convert.ToChar(data_rx[0]) == "Intrusion détectée!") // arduino a envoyé le signal donc acceleration >< 150
                {
                    client.Send(mail); // envoi du mail
                    myport.WriteLine("Intrusion détectée!"); // renvoie comme quoi ca a été exécuté
                }
            }
        }
    }
}
