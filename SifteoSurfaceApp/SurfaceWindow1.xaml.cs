using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Surface;
using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Input;
using System.Windows.Media.Effects;
using Artefact.Animation;
using System.Windows.Media.Animation;

namespace SifteoSurfaceApp
{
    /// <summary>
    /// Interaction logic for SurfaceWindow1.xaml
    /// </summary>
    public partial class SurfaceWindow1 : SurfaceWindow
    {
        private ProgressBarWrapper _progressBarWrapper;
        public string tag = "";
        public bool hasTag = false;
        public int NumberOfCubesInitialized = 0;
        public int NumberOfCubesWeWorkingWith = 6;

        private const bool DEBUGGING = false;
        private Random randomGen;

        private SolidColorBrush _yellowPlasmidColor = new SolidColorBrush(new Color { A = 255, B = (byte)9, G = (byte)247, R = (byte)255 });
        private SolidColorBrush _bluePlasmidColor = new SolidColorBrush(new Color { A = 255, B = (byte)247, G = (byte)202, R = (byte)9 });
        private SolidColorBrush _redPlasmidColor = new SolidColorBrush(new Color { A = 200, B = (byte)0, G = (byte)0, R = (byte)255 });
        private SolidColorBrush _whiteColor = new SolidColorBrush(new Color { A = 255, B = 255, G = 255, R = 255 });

        public List<WhiteRedEcoli> redEcoliList = new List<WhiteRedEcoli>();
        public List<WhiteYellowEcoli> yellowEcoliList = new List<WhiteYellowEcoli>();
        public List<WhiteBlueEcoli> blueEcoliList = new List<WhiteBlueEcoli>();
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public SurfaceWindow1()
        {
            InitializeComponent();

            // Add handlers for window availability events
            AddWindowAvailabilityHandlers();
            
                randomGen = new Random();            
        }

        private void TagVisualizationDefinition_VisualizationCreated(object sender, TagVisualizationEventArgs e)
        {
            TagVisualization1 t = e.Visualization as TagVisualization1;

            //if (DEBUGGING)
                //CreateRandomEcoliOnTagForTestingWithOutClient(t);
            //else
            //{//You're running the sifteo server
                SetTagStringToSendFromByteTag(e);

                if (tag != "") 
                    hasTag = true;
                

                if (NumberOfCubesInitialized < NumberOfCubesWeWorkingWith )
                {
                    SendTagValueToEstablishTagsWithSifteo();
                    NumberOfCubesInitialized++;
                }
                else if (tag != "none")
                {
                    SendTagAndAddEcoliBasedonColorRecdFromClient(t);
                }
            //}
        }

        /// <summary>
        /// For testing surface stuff without sifteo client running
        /// </summary>
        /// <param name="t"></param>
        private void CreateRandomEcoliOnTagForTestingWithOutClient(TagVisualization1 t)
        {
            int selector = randomGen.Next(0, 3);//for testing colors

            switch (selector)
            {
                case 0:
                    AddWhiteBlueEcoli(t);
                    break;
                case 1:
                    AddWhiteRedEcoli(t);
                    break;
                case 2:
                    AddWhiteYellowEcoli(t);
                    break;
                default:
                    break;
            }
        }

        private void SetTagStringToSendFromByteTag(TagVisualizationEventArgs e)
        {   
            switch (e.Visualization.VisualizedTag.Value)
            {
                case 0xA3:
                    tag = "0xA3";
                    break;
                case 0x2A:
                    tag = "0x2A";
                    break;
                case 0xA4:
                    tag = "0xA4";
                    break;
                case 0x1A:
                    tag = "0x1A";
                    break;
                case 0xC5:
                    tag = "0xC5";
                    break;
                case 0x3A:
                    tag = "0x3A";
                    break;
                default:
                    tag = "none";
                    break;
            }
        }

        private void SendTagValueToEstablishTagsWithSifteo()
        {
            SifteoServer p = new SifteoServer();
            p.mySW = this;
            _progressBarWrapper = new ProgressBarWrapper(new Action(ObligatoryProgressBarShowHideMethod), new Action(ObligatoryProgressBarShowHideMethod));
            Func<String, SifteoServer> longOperation =
                    delegate(String s) { return p.receivedTagVal(); };

            Action<SifteoServer> callback = delegate(SifteoServer data)
            { //No UI update necessary

            };
            _progressBarWrapper.execute<String, SifteoServer>(longOperation, "", callback);
        }
        
        /// <summary>
        /// Placeholder for progress indicator method. Nothing really needs to go here for this app.
        /// </summary>
        private void ObligatoryProgressBarShowHideMethod()
        {

        }

        private void SendTagAndAddEcoliBasedonColorRecdFromClient(TagVisualization1 t)
        {
            SifteoServer p = new SifteoServer();
            p.mySW = this;
            
            _progressBarWrapper = new ProgressBarWrapper(new Action(ObligatoryProgressBarShowHideMethod), 
                new Action(ObligatoryProgressBarShowHideMethod));
            
            Func<String, SifteoServer> longOperation = delegate(String s) { return p.executeLoop(); };

            Action<SifteoServer> callback = delegate(SifteoServer data)
            { //Show ecoli based on color from sifteo client
                switch (data.color1)
                {
                    case "Blue":
                        Console.WriteLine("Adding Blue Ecoli from Sifteo data");
                        AddWhiteBlueEcoli(t);
                        break;

                    case "Yellow":
                        Console.WriteLine("Adding Yellow Ecoli from Sifteo data");
                        AddWhiteYellowEcoli(t);
                        break;

                    case "Red":
                        Console.WriteLine("Adding Red Ecoli from Sifteo data");
                        AddWhiteRedEcoli(t);
                        break;
                    default:
                        break;
                }

            };
            _progressBarWrapper.execute<String, SifteoServer>(longOperation, "", callback);
        }

        #region Add White Ecoli Methods

        private void AddWhiteBlueEcoli(TagVisualization1 t)
        {
            WhiteBlueEcoli wb = new WhiteBlueEcoli(t);
            blueEcoliList.Add(wb);
            wb.sw1 = this;

            t.CanvasToAddEcoliTo.Children.Add(wb);
            //t.Animate(wb);
        }

        private void AddWhiteRedEcoli(TagVisualization1 t)
        {
            WhiteRedEcoli wr = new WhiteRedEcoli(t);
            redEcoliList.Add(wr);
            wr.sw1 = this;

            t.CanvasToAddEcoliTo.Children.Add(wr);
            //t.Animate(wr);
        }

        private void AddWhiteYellowEcoli(TagVisualization1 t)
        {
            WhiteYellowEcoli wy = new WhiteYellowEcoli(t);
            //wy.SlideTo(randomGen.Next(0, t._myWidth), randomGen.Next(0, t._myWidth), 5, AnimationTransitions.ElasticEaseOut, 0);//.Complete += (eo, p) => t.CanvasToAddEcoliTo.Children.Remove(wy);
            //ArtefactAnimator.AddEase(wy, RenderTransformProperty, new TranslateTransform(1000, 500), 5);

            yellowEcoliList.Add(wy);
            wy.sw1 = this;

            t.CanvasToAddEcoliTo.Children.Add(wy);
            //t.Animate(wy);
        }

      
        #endregion

        #region Toxin Viz Handlers

        private void RedToxin_VisualizationCreated(object sender, TagVisualizationEventArgs e)
        {
           ArtefactAnimator.AddEase(LayoutRoot,
           Grid.BackgroundProperty, _redPlasmidColor,
           1, AnimationTransitions.CubicEaseOut, 0).Complete += (eo, p) => ArtefactAnimator.AddEase(LayoutRoot,
           Grid.BackgroundProperty, _whiteColor, 1, AnimationTransitions.CubicEaseOut, 0);

            foreach (WhiteRedEcoli wb in redEcoliList)
            {                
                RedEcoli b = new RedEcoli(wb.myParent);
                b.CurrentX = wb.CurrentX;
                b.CurrentY = wb.CurrentY;
                b.SpeedX = wb.SpeedX;
                b.SpeedY = wb.SpeedY;
                wb.myParent.CanvasToAddEcoliTo.Children.Add(b);
                //wb.myParent.Animate(b);
                wb.Opacity = -1;//kills old white ecoli                   
            }
        }

        private void BlueToxin_VisualizationCreated(object sender, TagVisualizationEventArgs e)
        {
           ArtefactAnimator.AddEase(LayoutRoot,
           Grid.BackgroundProperty, _bluePlasmidColor,
           1, AnimationTransitions.CubicEaseOut, 0).Complete += (eo, p) => ArtefactAnimator.AddEase(LayoutRoot,
           Grid.BackgroundProperty, _whiteColor, 1, AnimationTransitions.CubicEaseOut, 0);

            foreach (WhiteBlueEcoli wb in blueEcoliList)
            {               
                BlueEcoli b = new BlueEcoli(wb.myParent);
                b.CurrentX = wb.CurrentX;
                b.CurrentY = wb.CurrentY;
                b.SpeedX = wb.SpeedX;
                b.SpeedY = wb.SpeedY;
                wb.myParent.CanvasToAddEcoliTo.Children.Add(b);
                //wb.myParent.Animate(b);
                wb.Opacity = -1;//kills old white ecoli
            }
        }

        private void YellowToxin_VisualizationCreated(object sender, TagVisualizationEventArgs e)
        {
            ArtefactAnimator.AddEase(LayoutRoot,
            Grid.BackgroundProperty, _yellowPlasmidColor,
            1, AnimationTransitions.CubicEaseOut, 0).Complete += (eo, p) => ArtefactAnimator.AddEase(LayoutRoot,
            Grid.BackgroundProperty, _whiteColor, 1, AnimationTransitions.CubicEaseOut, 0);
            
            foreach (WhiteYellowEcoli wb in yellowEcoliList)
            {                
                YellowEcoli b = new YellowEcoli(wb.myParent);
                b.CurrentX = wb.CurrentX;
                b.CurrentY = wb.CurrentY;
                b.SpeedX = wb.SpeedX;
                b.SpeedY = wb.SpeedY;
                wb.myParent.CanvasToAddEcoliTo.Children.Add(b);
                //wb.myParent.Animate(b);
                wb.Opacity = -1;//kills old white ecoli
                   
            }
        }

        #endregion

        private void SurfaceWindow_TouchDown(object sender, TouchEventArgs e)
        {
            Rect touchBounds = e.TouchDevice.GetBounds(null);

            foreach (WhiteBlueEcoli wb in blueEcoliList)
            {
                if (wb.CurrentX > touchBounds.BottomLeft.X - 300 && wb.CurrentX < touchBounds.BottomLeft.X + 300)
                {
                    if (wb.CurrentY > touchBounds.BottomLeft.Y - 300 && wb.CurrentY < touchBounds.BottomLeft.Y + 300)
                    {                        
                        wb.SpeedX = -wb.SpeedX;
                        wb.SpeedY = -wb.SpeedY;
                        
                    }
                }
            }

            foreach (WhiteRedEcoli wb in redEcoliList)
            {
                if (wb.CurrentX > touchBounds.BottomLeft.X - 300 && wb.CurrentX < touchBounds.BottomLeft.X + 300)
                {
                    if (wb.CurrentY > touchBounds.BottomLeft.Y - 300 && wb.CurrentY < touchBounds.BottomLeft.Y + 300)
                    {
                        wb.SpeedX = -wb.SpeedX;
                        wb.SpeedY = -wb.SpeedY;

                    }
                }
            }

            foreach (WhiteYellowEcoli wb in yellowEcoliList)
            {
                if (wb.CurrentX > touchBounds.BottomLeft.X - 300 && wb.CurrentX < touchBounds.BottomLeft.X + 300)
                {
                    if (wb.CurrentY > touchBounds.BottomLeft.Y - 300 && wb.CurrentY < touchBounds.BottomLeft.Y + 300)
                    {
                        wb.SpeedX = -wb.SpeedX;
                        wb.SpeedY = -wb.SpeedY;

                    }
                }
            }

        }

        #region default methods
        /// <summary>
        /// Occurs when the window is about to close. 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Remove handlers for window availability events
            RemoveWindowAvailabilityHandlers();
        }

        /// <summary>
        /// Adds handlers for window availability events.
        /// </summary>
        private void AddWindowAvailabilityHandlers()
        {
            // Subscribe to surface window availability events
            ApplicationServices.WindowInteractive += OnWindowInteractive;
            ApplicationServices.WindowNoninteractive += OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable += OnWindowUnavailable;
        }

        /// <summary>
        /// Removes handlers for window availability events.
        /// </summary>
        private void RemoveWindowAvailabilityHandlers()
        {
            // Unsubscribe from surface window availability events
            ApplicationServices.WindowInteractive -= OnWindowInteractive;
            ApplicationServices.WindowNoninteractive -= OnWindowNoninteractive;
            ApplicationServices.WindowUnavailable -= OnWindowUnavailable;
        }

        /// <summary>
        /// This is called when the user can interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowInteractive(object sender, EventArgs e)
        {
            //TODO: enable audio, animations here
        }

        /// <summary>
        /// This is called when the user can see but not interact with the application's window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowNoninteractive(object sender, EventArgs e)
        {
            //TODO: Disable audio here if it is enabled

            //TODO: optionally enable animations here
        }

        /// <summary>
        /// This is called when the application's window is not visible or interactive.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWindowUnavailable(object sender, EventArgs e)
        {
            //TODO: disable audio, animations here
        }
        #endregion

        private void SurfaceWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TagVisualization1 t = new TagVisualization1();// = e.Visualization as TagVisualization1;

            //if (DEBUGGING)
            //    CreateRandomEcoliOnTagForTestingWithOutClient(t);
            //else
            //{//You're running the sifteo server
                //SetTagStringToSendFromByteTag(e);

                //if (tag != "")
                    //hasTag = true;


                //if (NumberOfCubesInitialized < NumberOfCubesWeWorkingWith)
                //{
                //    SendTagValueToEstablishTagsWithSifteo();
                //    NumberOfCubesInitialized++;
                //}
                //else if (tag != "none")
                //{
                    SendTagAndAddEcoliBasedonColorRecdFromClient(t);
                //}
            //}
        }

        private void SurfaceWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}