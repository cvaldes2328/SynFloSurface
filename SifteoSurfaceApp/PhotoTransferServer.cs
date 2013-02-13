/*
 * Server side loop for use with Phototransferclient>client
 * run this on server side to receive photos from tablet (or other device) while running client on tablet (or other device)
 * modified code from http://www.codeproject.com/Articles/1415/Introduction-to-TCP-client-server-in-C
 * @c.grote 06/19/12
 */



using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.IO;

public class PhotoTransferServer
{
    public PhotoTransferServer()
    {
        while (true)//loop until application closed
        {
            try
            {
                IPAddress ipAd = IPAddress.Parse("149.130.195.79");
                // use local m/c IP address, and 
                // use the same in the client

                /* Initializes the Listener */
                TcpListener myList = new TcpListener(ipAd, 3070);

                /* Start Listeneting at the specified port */
                myList.Start();

                Console.WriteLine("The server is running at port 3070...");
                Console.WriteLine("The local End point is  :" +
                                  myList.LocalEndpoint);
                Console.WriteLine("Waiting for a connection.....");

                Socket s = myList.AcceptSocket();
                Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);

                //preallocates byte array, may need enlarging based on image size
                //byte[] b = new byte[3 * 70000];
                //int k = s.Receive(b);
                //Console.WriteLine("Recieved...");

                //convert byte array to jpg
                byte[] b = new byte[7];
                int k = s.Receive(b);
                Console.WriteLine("Recieved...");

                String _colorFromSifteoClient = ActualStringFromCube(b);
                Console.WriteLine("Recieved..." + _colorFromSifteoClient);
                //filename from time stamp
                //String filename = DateTime.Now.Date.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString();
               // Console.WriteLine("saved as C:\\Users\\oshaer\\Desktop\\tabletUpload\\"+filename+".jpg");
                //img.Save(@"C:\Users\oshaer\Desktop\tabletUpload\"+filename+".jpg");

                ASCIIEncoding asen = new ASCIIEncoding();
                s.Send(asen.GetBytes("The string was recieved by the server."));
                Console.WriteLine("\nSent Acknowledgement");
                /* clean up */
                s.Close();
                myList.Stop();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }
    }

    /// <summary>
    /// Parse byte array received from the Sifteo cubes into the string
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    private string ActualStringFromCube(byte[] buffer)
    {
        string color = "";

        //convert bytes to sstring characters
        foreach (byte element in buffer)
        {
            color = color + (char)element;
        }

        if (color.Contains("Yellow"))
        {
            return "Yellow";
        }
        else
            if (color.Contains("Red"))
            {
                return "Red";
            }
            else
                if (color.Contains("Blue"))
                {
                    return "Blue";
                }

        return "";
    }

}
