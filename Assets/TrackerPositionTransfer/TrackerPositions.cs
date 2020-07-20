using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using MessagePack;
using UnityEngine;

namespace TrackerPositionTransfer
{
    [MessagePackObject]
    public class TrackerPositions
    {
        [Key(0)] public Vector3[] trackerPositions;
        [Key(1)] public Quaternion[] trackerRotations;

        public TrackerPositions(IEnumerable<Transform> trackers)
        {
            this.trackerPositions = trackers.Select(x => x.position).ToArray();
            this.trackerRotations = trackers.Select(x => x.rotation).ToArray();
        }

        public TrackerPositions(Vector3[] trackerPositions, Quaternion[] trackerRotations)
        {
            this.trackerPositions = trackerPositions;
            this.trackerRotations = trackerRotations;
        }
    }
}