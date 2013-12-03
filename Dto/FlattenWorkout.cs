using System;
using System.Runtime.Serialization;

namespace Dto
{
    [DataContract]
    public class FlattenWorkout
    {
        [DataMember]
        public int UniqueIdentifier { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string GoalDescription { get; set; }
        [DataMember]
        public DateTime StartTime { get; set; }
        [DataMember]
        public DateTime EndTime { get; set; }
    }
}