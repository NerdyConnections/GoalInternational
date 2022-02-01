
namespace GoalInternational.Models
{
    public class GIntV3SubmitModel
    {
        public int UserID { get; set; }
        public int PatientID { get; set; }
        public bool? SignOff { get; set; }
        public bool CanUserSubmit { get; set; }
    }
}