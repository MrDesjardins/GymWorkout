using System.Runtime.Serialization;

namespace Dto
{
    [DataContract]
    public class FlattenExercise
    {
        [DataMember]
        public int UniqueIdentifier { get; set; }
        [DataMember]
        public string FrenchName { get; set; }
        [DataMember]
        public string EnglishName { get; set; }

        [DataMember]
        public int MuscleUniqueIdentifier { get; set; }
        [DataMember]
        public string MuscleFrenchName { get; set; }
        [DataMember]
        public string MuscleEnglishName { get; set; }
    }
}