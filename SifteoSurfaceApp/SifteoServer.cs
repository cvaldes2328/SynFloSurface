using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Artefact.Animation;
using System.Windows.Media;
using System.Windows.Controls;

namespace SifteoSurfaceApp
{
    public class SifteoServer
    {        
        TcpListener _myTcpListener;
        public int _port = 3070;
        public string _colorFromSifteoClient = "";
        private const string SURFACEIPADDRESS = "192.168.1.7";
        //Socket _socket;

        Socket s;
        public string color1 = "empty";
        public SurfaceWindow1 mySW;

        private SolidColorBrush _black = new SolidColorBrush(new Color { A = 200, B = (byte)0, G = (byte)0, R = (byte)0 });
        private SolidColorBrush _whiteColor = new SolidColorBrush(new Color { A = 255, B = 255, G = 255, R = 255 });

        /// <summary>
        /// Constructor. Starts listening for a message from the sifteo program.
        /// </summary>
        public SifteoServer()
        {
            //while (true)//loop until application closed
            //{
            //    try
            //    {
            //        Console.WriteLine("Tag value down: " + s);
            //        // use local m/c IP address, and 
            //        // use the same in the client
            //        IPAddress ipAd = IPAddress.Parse("149.130.194.94");

            //        //Initializes the Listener
            //         _myTcpListener = new TcpListener(ipAd, _port);

            //        //Start Listeneting at the specified port
            //        _myTcpListener.Start();

            //        Socket _socket = _myTcpListener.AcceptSocket();
            //        Console.WriteLine("Connection accepted from " + _socket.RemoteEndPoint);

            //        //Send tag to Sifteo client
            //        ASCIIEncoding asen = new ASCIIEncoding();
            //        _socket.Send(asen.GetBytes(s));
            //        Console.WriteLine("\nSent Tag Value to Sifteo");

            //        //Preallocates byte array that will hold what is sent
            //        //Make this bigger if you expect a bigger message
            //        byte[] b = new byte[7];
            //        int k = _socket.Receive(b);
            //        Console.WriteLine("Recieved...");

            //        _colorFromSifteoClient = ActualStringFromCube(b);
                    

            //        //Send response to Sifteo client
            //         asen = new ASCIIEncoding();
            //        _socket.Send(asen.GetBytes("The string was recieved by the server."));
            //        Console.WriteLine("\nSent Acknowledgement");
                    
            //        //Clean up
            //        _socket.Close();
            //        _myTcpListener.Stop();
            //        if (_colorFromSifteoClient == "Red" || _colorFromSifteoClient == "Blue" || _colorFromSifteoClient == "Yellow") 
            //            break;

            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine("Error..... " + e.StackTrace);
            //        ////Clean up
            //        //_socket.Close();
            //        //_myTcpListener.Stop();
            //    }
            //}
        }

        public SifteoServer receivedTagVal()
        {
            try
            {
                IPAddress ipAd = IPAddress.Parse(SURFACEIPADDRESS);
                // use local m/c IP address, and 
                // use the same in the client

                /* Initializes the Listener */
                _myTcpListener = new TcpListener(ipAd, _port);

                /* Start Listeneting at the specified port */
                _myTcpListener.Start();

                Console.WriteLine("The server is running at port ..." + _port);
                Console.WriteLine("The local End point is  :" +
                                  _myTcpListener.LocalEndpoint);
                Console.WriteLine("Waiting for a connection.....");

                Socket s = _myTcpListener.AcceptSocket();
                Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);
                ASCIIEncoding asen = new ASCIIEncoding();

                s.Send(asen.GetBytes(mySW.tag));
                Console.WriteLine("Sent tag");
                s.Close();
                _myTcpListener.Stop();

            }
            catch (Exception e)
            {
                //s.Close();
                //_myTcpListener.Stop();
                Console.WriteLine("ERROR: " + e.Message);
            }
            return this;
        }

        public SifteoServer executeLoop()
        {
            while (true)//loop until application closed
            {
                try
                {

                    IPAddress ipAd = IPAddress.Parse(SURFACEIPADDRESS);
                    // use local m/c IP address, and 
                    // use the same in the client

                    /* Initializes the Listener */
                    _myTcpListener = new TcpListener(ipAd, _port);

                    /* Start Listeneting at the specified port */
                    _myTcpListener.Start();

                    Console.WriteLine("The server is running at port ..." + _port);
                    Console.WriteLine("The local End point is  :" +
                                      _myTcpListener.LocalEndpoint);
                    Console.WriteLine("Waiting for a connection.....");

                    //ArtefactAnimator.AddEase(mySW.LayoutRoot,
                    //   Grid.BackgroundProperty, _black,
                    //   1, AnimationTransitions.CubicEaseOut, 0).Complete += (eo, p) => ArtefactAnimator.AddEase(mySW.LayoutRoot,
                    //   Grid.BackgroundProperty, _whiteColor, 1, AnimationTransitions.CubicEaseOut, 0);



                    Socket s = _myTcpListener.AcceptSocket();
                    Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);
                    ASCIIEncoding asen = new ASCIIEncoding();

                    s.Send(asen.GetBytes(mySW.tag));
                    Console.WriteLine("Sent tag");
                    
                    //preallocates byte array, may need enlarging based on image size
                    byte[] b = new byte[7];
                    int k = s.Receive(b);
                    Console.WriteLine("Recieved...");

                    string color = ActualStringFromCube(b);
                    color1 = color;

                    Console.WriteLine(@"trying to print color: " + color);
                    
                    /* clean up */
                    s.Close();
                    _myTcpListener.Stop();
                    Console.WriteLine("All closed up... going back to main thread...");
                    if (color1 == "Red" || color1 == "Blue" || color1 == "Yellow") break;


                }
                catch (Exception e)
                {
                    //s.Close();
                    //_myTcpListener.Stop();
                    Console.WriteLine("Error..... " + e.StackTrace);
                }

            }
            return this;

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

            if (color.Contains("yellow"))
            {
                return "Yellow";
            } 
            else 
            if (color.Contains("red"))
            {
                return "Red";
            } 
            else                        
            if (color.Contains("blue"))
            {
                return "Blue";
            }

            return "";
        }
       

    }
}
