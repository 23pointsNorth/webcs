using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace Marker
{
    class MarkerBase
    {
        public static List<ManualResetEvent> doneEvents;
        public static List<Bitmap> leftOvers;

        public MarkerBase()
        {
            this.markerNumber = nextMarkerNumber;
            nextMarkerNumber++;
            UpdateMarkers();
        }

        protected int markerNumber = 0;
        protected static int nextMarkerNumber = 0;

        // Wrapper method for use with thread pool.
        public void ThreadPoolCallback(object threadContext)
        {
            //add the result to a static list of bitmaps
            this.CalculateMarker((Bitmap)threadContext);
            doneEvents[markerNumber].Set();
        }

        ManualResetEvent manualResetEventFalse = new ManualResetEvent(false);
        public void ThreadCalculateMarker(Bitmap frame)
        {
            doneEvents[markerNumber] = manualResetEventFalse;
            ThreadPool.QueueUserWorkItem(this.ThreadPoolCallback,new Bitmap(frame));
        }

        public virtual void CalculateMarker(Bitmap frame) { }

        //After adding a new Marker, the UpdateMarker method should be called;
        public static void UpdateMarkers()
        {
            //update minimum number of idle threads;
            ThreadPool.SetMinThreads(nextMarkerNumber, nextMarkerNumber);
            doneEvents = new List<ManualResetEvent>(nextMarkerNumber);
            leftOvers = new List<Bitmap>(nextMarkerNumber);
            for (int i = 0; i < nextMarkerNumber; i++)
            {
                ColorMarker.doneEvents.Add(new ManualResetEvent(false));
                leftOvers.Add(new Bitmap(1, 1));
            }
        }
    }
}
